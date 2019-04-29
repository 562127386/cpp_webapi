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
    public class ProveedorAsyncBaseCrudAppService : AsyncBaseCrudAppService<Proveedor, ProveedorDto, 
        PagedAndFilteredResultRequestDto>,
        IProveedorAsyncBaseCrudAppService
    {
        public ProveedorAsyncBaseCrudAppService(IBaseRepository<Proveedor> repository) : base(repository)
        {
            Repository = repository;
        }

        public IBaseRepository<Proveedor> Repository { get; }


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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<Proveedor> registros)
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
                Proveedor instancia = JsonToObject(obj);

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

        public List<Proveedor> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(Proveedor entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));


            objJson.Add("tipo_identificacion", entidad.TipoIdentificacion == ProveedorTipoIdentificacion.Cedula ? 1
                : entidad.TipoIdentificacion == ProveedorTipoIdentificacion.Ruc ? 2
                : 3);
            objJson.Add("identificacion", entidad.Identificacion);
            objJson.Add("razon_social", entidad.RazonSocial);
            objJson.Add("estado", entidad.Estado == ProveedorEstado.Activo ? 1
                : 0);
            objJson.Add("es_externo", entidad.EsExterno);
            objJson.Add("coordenadas", entidad.Coordenadas);
            objJson.Add("codigo", entidad.CodigoSap);
            objJson.Add("usuario", entidad.Usuario);

            return objJson;
        }

        public Proveedor JsonToObject(JObject json)
        {

            var entity = new Proveedor();
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
            entity.TipoIdentificacion = TipoIdentificacion == 1 ? ProveedorTipoIdentificacion.Cedula
                : TipoIdentificacion == 2 ? ProveedorTipoIdentificacion.Ruc
                : ProveedorTipoIdentificacion.Passaporte;
            entity.Identificacion = (string)json["identificacion"];
            entity.RazonSocial = (string)json["razon_social"];
            //obj. = (int)json["colaborador_id"]; No esta contacto ID
            int Estado = (int)json["estado"];
            entity.Estado = Estado == 1 ? ProveedorEstado.Activo
                : ProveedorEstado.Inactivo;
            entity.EsExterno = (int)json["es_externo"];
            entity.Coordenadas = (string)json["coordenadas"];
            entity.CodigoSap = (string)json["codigo"];
            //obj.Orde = (string)json["codigo"]; No hay orden de compra
            entity.Usuario = (string)json["usuario"];


            return entity;
        }
    }
}
