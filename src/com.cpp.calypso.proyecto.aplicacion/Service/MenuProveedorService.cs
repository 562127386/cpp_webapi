using com.cpp.calypso.comun.aplicacion;
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
    public class MenuProveedorAsyncBaseCrudAppService
        : AsyncBaseCrudAppService<MenuProveedor, MenuProveedorDto, PagedAndFilteredResultRequestDto>,
        IMenuProveedorAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<Archivo> _archivoRepository;
        private readonly IBaseRepository<Proveedor> _proveedorRepository;

        public MenuProveedorAsyncBaseCrudAppService(
            IBaseRepository<MenuProveedor> repository,
            IBaseRepository<Archivo> archivoRepository,
            IBaseRepository<Proveedor> proveedorRepository
            ) : base(repository)
        {
            Repository = repository;
            _archivoRepository = archivoRepository;
            _proveedorRepository = proveedorRepository;
        }

        public IBaseRepository<MenuProveedor> Repository { get; }


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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<MenuProveedor> registros)
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
                MenuProveedor instancia = JsonToObject(obj);

                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                Int32 unixTimestamp2 = (Int32)(DateTime.UtcNow.Subtract(new DateTime())).TotalSeconds;
                instancia.Version = unixTimestamp;


                // Asignar el archivo al menu proveedor
                var archivoRef = instancia.DocumentacionRef;
                var archivo = _archivoRepository.GetAll()
                    .Where(o => o.Ref == archivoRef)
                    .FirstOrDefault();

                if(archivo != null)
                    instancia.DocumentacionId = archivo.Id;


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

        public List<MenuProveedor> GetRegistros(int version, List<int> usuarios)
        {
            var proveedoresLogeados = _proveedorRepository.GetAll()
                .Where(o => o.IsDeleted || o.IsDeleted == false)
                .Where(o => usuarios.Contains((int) o.Usuario))
                .Select(proveedor => proveedor.Id)
                .ToList();

            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .Where(o => o.IsDeleted || o.IsDeleted == false)
                .Where(o => proveedoresLogeados.Contains(o.ProveedorId))
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(MenuProveedor entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));

            objJson.Add("aprobado", entidad.Aprobado);
            objJson.Add("descripcion", entidad.Descripcion);
            objJson.Add("archivo_id", entidad.DocumentacionId);
            objJson.Add("fecha_aprobacion", GetStringFromDateTime(entidad.FechaAprobacion));
            objJson.Add("fecha_inicial", GetStringFromDateTime(entidad.FechaInicial));
            objJson.Add("fecha_final", GetStringFromDateTime(entidad.FechaFinal));
            objJson.Add("proveedor_id", entidad.ProveedorId);


            return objJson;
        }

        public MenuProveedor JsonToObject(JObject json)
        {

            var entity = new MenuProveedor();
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

            entity.Aprobado = (bool)json.GetValue("aprobado");
            entity.Descripcion = (string)json.GetValue("descripcion");
            var archivoEsNull = json.GetValue("archivo_id").Type == JTokenType.Null;
            if(!archivoEsNull)
                entity.DocumentacionId = (int)json.GetValue("archivo_id");

            entity.DocumentacionRef = (string)json.GetValue("archivo_ref");

            var fechaAprobacionEsNull = json.GetValue("fecha_aprobacion").Type == JTokenType.Null;
            if(!fechaAprobacionEsNull)
                entity.FechaAprobacion = GetDateTimeFromString((string)json.GetValue("fecha_aprobacion"));

            entity.FechaInicial = GetDateFromString((string)json.GetValue("fecha_inicial"));
            entity.FechaFinal = GetDateFromString((string)json.GetValue("fecha_final"));
            entity.ProveedorId = (int)json.GetValue("proveedor_id");


            return entity;
        }
    }
}
