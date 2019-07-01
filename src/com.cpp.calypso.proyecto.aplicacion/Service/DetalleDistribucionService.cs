using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class DetalleDistribucionAsyncBaseCrudAppService
        : AsyncBaseCrudAppService<DetalleDistribucion, DetalleDistribucionDto, PagedAndFilteredResultRequestDto>,
        IDetalleDistribucionAsyncBaseCrudAppService
    {
        public DetalleDistribucionAsyncBaseCrudAppService(IBaseRepository<DetalleDistribucion> repository) : base(repository)
        {
            Repository = repository;
        }

        public IBaseRepository<DetalleDistribucion> Repository { get; }



        public JArray Sync(int version, JArray registrosJson, List<int> usuarios)
        {
            var diccionario = Sincronizar(version, registrosJson, usuarios);
            var registros = GetRegistros(version, usuarios, diccionario);

            var json = GenerarRegistrosMovil(diccionario, registros);
            return json;

        }

        public Dictionary<int, int> Sincronizar(int version, JArray registrosJson, List<int> usuariosId)
        {
            //Aplicar los cambios que vienen del movil y generar el binding de Ids SqlServer vs SqlLite
            var lKeyBinding = SincronizacionLocal(registrosJson);

            return lKeyBinding;
        }

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<DetalleDistribucion> registros)
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
                DetalleDistribucion instancia = JsonToObject(obj);

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

        public List<DetalleDistribucion> GetRegistros(int version, List<int> usuarios, Dictionary<int, int> diccionario)
        {
            var solicitudesViandasEnMovil = diccionario.Keys.ToList();
            var fechaActual = DateTime.Today;

            var registros = Repository.GetAllIncluding(o => o.DistribucionVianda)
                    .Where(o => o.Version > version)
                    .Where(o => o.IsDeleted || o.IsDeleted == false)
                    .Where(o => solicitudesViandasEnMovil.Contains(o.Id) || o.DistribucionVianda.Fecha == fechaActual)
                    .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(DetalleDistribucion entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));

            objJson.Add("distribucion_vianda_id", entidad.DistribucionViandaId);
            objJson.Add("solicitud_vianda_id", entidad.SolicitudViandaId);
            objJson.Add("total_asignado", entidad.TotalAsignado);
            objJson.Add("total_entregado", entidad.TotalEntregado);
            return objJson;
        }

        public DetalleDistribucion JsonToObject(JObject json)
        {

            var entity = new DetalleDistribucion();
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


            entity.DistribucionViandaId = (int)json.GetValue("distribucion_vianda_id");
            entity.SolicitudViandaId = (int)json.GetValue("solicitud_vianda_id");
            entity.TotalAsignado = (int)json.GetValue("total_asignado");
            entity.TotalEntregado = (int)json.GetValue("total_entregado");

            return entity;
        }
    }
}
