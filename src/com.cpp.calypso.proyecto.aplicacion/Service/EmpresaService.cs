﻿using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class EmpresaAsyncBaseCrudAppService : AsyncBaseCrudAppService<Empresa, EmpresaDto, 
        PagedAndFilteredResultRequestDto>, IEmpresaAsyncBaseCrudAppService
    {
        public EmpresaAsyncBaseCrudAppService(IBaseRepository<Empresa> repository) : base(repository)
        {
            Repository = repository;
        }

        public IBaseRepository<Empresa> Repository { get; }

        public JArray Sync(int version, JArray registrosJson, List<int> usuarios)
        {
            var diccionario = Sincronizar(version, registrosJson, usuarios);
            var registros = GetRegistros(version, usuarios);

            var json = GenerarRegistrosMovil(diccionario, registros);
            return json;

        }

        public Dictionary<int, int> Sincronizar(int version, JArray registrosJson, List<int> usuariosId)
        {
            //Aplicar los cambios que vienen del movil y generar el binding de Ids SqlServer vs SqlLite
            var lKeyBinding = SincronizacionLocal(registrosJson);

            return lKeyBinding;
        }

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<Empresa> registros)
        {
            //IList<TEntityDto> registros = GetRegistros(version, usuariosId);

            JArray registroJson = new JArray();
            foreach (var entidad in registros)
            {

                int Id = entidad.Id;
                var objJson = ObjectToJson(entidad);


                //En el caso de registros nuevos y actualizados desde movil hay que reflejar el bindgin
                //con el ID local
                if (diccionario.ContainsKey(Id))
                {
                    objJson.Add("lkey", diccionario[Id]);
                }
                registroJson.Add(objJson);
            }

            return registroJson;
        }

        public Dictionary<int, int> SincronizacionLocal(JArray registrosJson)
        {

            var lKeyBinding = new Dictionary<int, int>();

            foreach (JObject obj in registrosJson)
            {

                //Recuperamos la instacia de la entidad concreta
                Empresa instancia = JsonToObject(obj);

                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                Int32 unixTimestamp2 = (Int32)(DateTime.UtcNow.Subtract(new DateTime())).TotalSeconds;
                instancia.Version = unixTimestamp;

                if (instancia.Id == 0)
                {
                    try
                    {
                        var created = Repository.InsertAndGetId(instancia);

                        //Vinculamos el ID SqlServer al Id Sqlite
                        lKeyBinding.Add(created, obj.GetValue("lkey").Value<int>());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                    }
                }
                else
                {

                    //Si la instancia ha sido persistida realizamos un update
                    Repository.Update(instancia);

                    //Vinculamos el ID SqlServer al Id Sqlite
                    lKeyBinding.Add(instancia.Id, obj.GetValue("lkey").Value<int>());
                }
            }
            return lKeyBinding;
        }

        public List<Empresa> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(Empresa entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));

            objJson.Add("tipo_identificacion", entidad.TipoIdentificacion == TipoDeIdentificacion.Cedula ? 1
                : entidad.TipoIdentificacion == TipoDeIdentificacion.Passaporte ? 2
                : 0);
            objJson.Add("identificacion", entidad.Identificacion);
            objJson.Add("razon_social", entidad.RazonSocial);
            objJson.Add("direccion", entidad.Direccion);
            objJson.Add("correo", entidad.Correo);
            objJson.Add("estado", entidad.Estado);
            objJson.Add("telefono", entidad.Telefono);
            objJson.Add("tipo_sociedad", entidad.TipoSociedad == TipoDeSociedad.Especial ? 1 : 0);
            objJson.Add("observaciones", entidad.Observaciones);
            objJson.Add("es_principal", entidad.EsPrincipal);
            objJson.Add("tipo_contribuyente", entidad.TipoContribuyente == TipoDeContribuyente.Normal ? 0 : 1);
            objJson.Add("codigo_sap", entidad.CodigoSap);
            return objJson;
        }

        public Empresa JsonToObject(JObject json)
        {

            var entity = new Empresa();
            if (json.Property("m_id") != null && (int)json.Property("m_id") != 0)
            {
                int id = (int)json["m_id"];
                entity = Repository.Get(id);
            }

            //Si el obj viene con referencia UUID se carga.
            if (json.Property("m_ref") != null)
            {
                entity.Ref = (string)json["m_ref"];
            }

            //Se encera la version
            entity.Version = 0;
            if (entity.Id == 0)
            {
                entity.IsDeleted = false;
            }
            else
            {
                entity.IsDeleted = (bool)json["vigente"] == false;
            }


            int TipoIdentificacion = (int)json["tipo_identificacion"];
            entity.TipoIdentificacion = TipoIdentificacion == 1 ? TipoDeIdentificacion.Cedula
                : TipoIdentificacion == 2 ? TipoDeIdentificacion.Passaporte
                : TipoDeIdentificacion.RUC;
            entity.Identificacion = (string)json["identificacion"];
            entity.RazonSocial = (string)json["razon_social"];
            entity.Direccion = (string)json["direccion"];
            entity.Correo = (string)json["correo"];
            entity.Estado = (bool)json["estado"];
            entity.Telefono = (string)json["telefono"];
            int TipoSociedad = (int)json["tipo_sociedad"];
            entity.TipoSociedad = TipoSociedad == 1 ? TipoDeSociedad.Especial : TipoDeSociedad.Normal;
            entity.Observaciones = (string)json["observaciones"];
            entity.EsPrincipal = (bool)json["es_principal"];
            int TipoContribuyente = (int)json["tipo_contribuyente"];
            entity.TipoContribuyente = TipoContribuyente == 0 ? TipoDeContribuyente.Normal : TipoDeContribuyente.Especial;
            entity.CodigoSap = (string)json["codigo_sap"];

            return entity;
        }
    }
}
