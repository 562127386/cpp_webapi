using System;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class VehiculoAsyncBaseCrudAppService : AsyncBaseCrudAppService<Vehiculo, VehiculoDto, PagedAndFilteredResultRequestDto>, IVehiculoAsyncBaseCrudAppService
    {
        public IBaseRepository<Vehiculo> Repository { get; }

        public VehiculoAsyncBaseCrudAppService(
            IBaseRepository<Vehiculo> repository
            ) : base(repository)
        {
            Repository = repository;
        }

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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<Vehiculo> registros)
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
                Vehiculo instancia = JsonToObject(obj);

                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
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

        public List<Vehiculo> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(Vehiculo entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));

            //objJson.Add("fecha_registro", GetStringFromDateTime(entidad.FechaRegistro));
            objJson.Add("proveedor_id", entidad.ProveedorId);
            objJson.Add("codigo", entidad.Codigo);
            objJson.Add("catalogo_tipo_vehiculo_id", entidad.CatalogoTipoVehiculoId);
            objJson.Add("numero_placa", entidad.NumeroPlaca);
            objJson.Add("marca", entidad.Marca);
            objJson.Add("capacidad", entidad.Capacidad);
            objJson.Add("anio_fabricacion", entidad.AnioFabricacion);
            objJson.Add("color", entidad.Color);
            objJson.Add("fecha_vencimiento_matricula", entidad.FechaVencimientoMatricula);
            objJson.Add("estado", entidad.Estado);
            objJson.Add("fecha_estado", GetStringFromDate(entidad.FechaEstado));

            return objJson;
        }

        public Vehiculo JsonToObject(JObject json)
        {

            var entity = new Vehiculo();
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


            //entity.FechaRegistro = GetDateTimeFromString((string)json["fecha_registro"]);
            entity.ProveedorId = (int)json["proveedor_id"];
            entity.Codigo = (string)json["codigo"];
            entity.CatalogoTipoVehiculoId = (int)json["catalogo_tipo_vehiculo_id"];
            entity.NumeroPlaca = (string)json["numero_placa"];
            entity.Marca = (string)json["marca"];
            entity.Capacidad = (int)json["capacidad"];
            entity.AnioFabricacion = (int)json["anio_fabricacion"];
            entity.Color = (string)json["color"];
            entity.FechaVencimientoMatricula = GetDateFromString((string)json["fecha_vencimiento_matricula"]);
            entity.Estado = (string)json["estado"];
            entity.FechaEstado = GetDateFromString((string)json["fecha_estado"]);


            return entity;
        }
    }
}
