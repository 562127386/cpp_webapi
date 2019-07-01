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
    public class EspacioHabitacionAsyncBaseCrudAppService : AsyncBaseCrudAppService<EspacioHabitacion, EspacioHabitacionDto, PagedAndFilteredResultRequestDto>, IEspacioHabitacionAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<Habitacion> _habitacionRepository;
        private readonly IBaseRepository<Proveedor> _proveedorRepository;

        public EspacioHabitacionAsyncBaseCrudAppService(
            IBaseRepository<EspacioHabitacion> repository,
            IBaseRepository<Habitacion> habitacionRepository,
            IBaseRepository<Proveedor> proveedorRepository
            ) : base(repository)
        {
            Repository = repository;
            _habitacionRepository = habitacionRepository;
            _proveedorRepository = proveedorRepository;
        }

        public IBaseRepository<EspacioHabitacion> Repository { get; }

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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<EspacioHabitacion> registros)
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
                EspacioHabitacion instancia = JsonToObject(obj);

                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                Int32 unixTimestamp2 = (Int32)(DateTime.UtcNow.Subtract(new DateTime())).TotalSeconds;
                instancia.Version = unixTimestamp;

                var habitacion = _habitacionRepository.GetAll()
                    .Where(o => o.Ref == instancia.HabitacionRef)
                    .FirstOrDefault();

                if (habitacion != null)
                    instancia.HabitacionId = habitacion.Id;

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

        public List<EspacioHabitacion> GetRegistros(int version, List<int> usuarios)
        {
            var proveedoresLogeados = _proveedorRepository.GetAll()
                .Where(o => o.IsDeleted || o.IsDeleted == false)
                .Where(o => usuarios.Contains((int) o.Usuario))
                .Select(proveedor => proveedor.Id)
                .ToList();

            var registros = Repository.GetAllIncluding(o => o.Habitacion)
                .Where(o => o.Version > version)
                    .Where(o => o.IsDeleted || o.IsDeleted == false)
                    .Where(o => proveedoresLogeados.Contains(o.Habitacion.ProveedorId))
                .ToList()
                ;
            return registros;
            
        }

        public JObject ObjectToJson(EspacioHabitacion entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));

            //objJson.Add("proveedor_id", entidad.ProveedorId);
            objJson.Add("habitacion_id", entidad.HabitacionId);
            objJson.Add("habitacion_ref", entidad.HabitacionRef);
            objJson.Add("codigo_espacio", entidad.CodigoEspacio);
            objJson.Add("estado", entidad.Estado ? 1 : 0);
            objJson.Add("activo", entidad.Activo ? 1 : 0);
            objJson.Add("observaciones", entidad.Observaciones);
            objJson.Add("creation_time", GetStringFromDateTime(entidad.CreationTime));
            objJson.Add("creator_user_id", entidad.CreatorUserId);

            return objJson;
        }

        public EspacioHabitacion JsonToObject(JObject json)
        {
            var entity = new EspacioHabitacion();

            if (json.GetValue("m_id").Type == JTokenType.Null)
                json["m_id"] = 0;

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

            //entity.ProveedorId = (int)json["proveedor_id"];
            if (json.GetValue("habitacion_id").Type != JTokenType.Null)
                entity.HabitacionId = (int)json["habitacion_id"];

            entity.HabitacionRef = (string)json["habitacion_ref"];
            entity.CodigoEspacio = (string)json["codigo_espacio"];
            entity.Estado = (int)json["estado"] == 1 ? true : false;
            entity.Activo = (int)json["activo"] == 1 ? true : false;
            entity.Observaciones = (string)json["observaciones"];

            if (json.GetValue("creator_user_id").Type != JTokenType.Null)
                entity.CreatorUserId = (long)json["creator_user_id"];

            entity.CreationTime = GetDateTimeFromString((string)json["creation_time"]);

            return entity;
        }
    }
}
