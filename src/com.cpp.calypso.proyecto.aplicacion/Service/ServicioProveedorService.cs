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
    public class ServicioProveedorAsyncBaseCrudAppService : AsyncBaseCrudAppService<ServicioProveedor, ServicioProveedorDto, PagedAndFilteredResultRequestDto>, IServicioProveedorAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<Proveedor> _proveedoRepository;

        public ServicioProveedorAsyncBaseCrudAppService(
            IBaseRepository<ServicioProveedor> repository,
            IBaseRepository<Proveedor> proveedoRepository
            ) : base(repository)
        {
            _proveedoRepository = proveedoRepository;
            Repository = repository;
        }

        public IBaseRepository<ServicioProveedor> Repository { get; }


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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<ServicioProveedor> registros)
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
                ServicioProveedor instancia = JsonToObject(obj);

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

        public List<ServicioProveedor> GetRegistros(int version, List<int> usuarios)
        {
            var proveedoresLogeados = _proveedoRepository.GetAll()
                .Where(o => o.IsDeleted || o.IsDeleted == false)
                .Where(o => usuarios.Contains((int) o.Usuario))
                .Select(proveedor => proveedor.Id)
                .ToList();


            var registros = Repository.GetAll()
                //.Where(o => o.Version >= version)
                .Where(o => o.IsDeleted || o.IsDeleted == false)
                .Where(o => proveedoresLogeados.Contains(o.ProveedorId))
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(ServicioProveedor entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));


            objJson.Add("servicio_id", entidad.ServicioId);
            objJson.Add("proveedor_id", entidad.ProveedorId);
            objJson.Add("estado", entidad.Estado == ServicioEstado.Inactivo ? 0 : 1);
            return objJson;
        }

        public ServicioProveedor JsonToObject(JObject json)
        {

            var entity = new ServicioProveedor();
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

            entity.ServicioId = (int)json["servicio_id"];
            entity.ProveedorId = (int)json["proveedor_id"];
            int estado = (int)json["estado"];
            entity.Estado = estado == 0 ? ServicioEstado.Inactivo : ServicioEstado.Activo;

            return entity;
        }
    }
}
