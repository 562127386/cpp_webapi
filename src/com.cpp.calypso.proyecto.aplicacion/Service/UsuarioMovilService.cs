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
    public class UsuarioMovilAsyncBaseCrudAppService : AsyncBaseCrudAppService<UsuarioMovil, UsuarioMovilDto, PagedAndFilteredResultRequestDto>, IUsuarioMovilAsyncBaseCrudAppService
    {
        public UsuarioMovilAsyncBaseCrudAppService(
            IBaseRepository<UsuarioMovil> repository
            ) : base(repository)
        {
            Repository = repository;
        }

        public IBaseRepository<UsuarioMovil> Repository { get; }


        public JArray Sync(int version, JArray registrosJson, List<int> usuarios)
        {
            var lKeyDictionary = SincronizacionLocal(registrosJson); 
            var registros = GetRegistros(registrosJson, usuarios);
            var json = GenerarRegistrosMovil(lKeyDictionary, registros);
            return json;

        }

        public Dictionary<int, int> Sincronizar(int version, JArray registrosJson, List<int> usuariosId)
        {
            //Aplicar los cambios que vienen del movil y generar el binding de Ids SqlServer vs SqlLite
            var lKeyBinding = SincronizacionLocal(registrosJson);
            return lKeyBinding;
        }

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<UsuarioMovil> registros)
        {
            JArray registroJson = new JArray();
            foreach (var entidad in registros)
            {

                int usuarioId = entidad.UsuarioId;
                var objJson = ObjectToJson(entidad);


                //En el caso de registros nuevos y actualizados desde movil hay que reflejar el bindgin
                //con el ID local
                if (diccionario.ContainsKey(usuarioId))
                {
                    objJson.Add("lkey", diccionario[usuarioId]);
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

                lKeyBinding.Add(obj.GetValue("m_id").Value<int>(), obj.GetValue("lkey").Value<int>());
            }
            return lKeyBinding;
        }

        public List<UsuarioMovil> GetRegistros(JArray registrosJson, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                 .Where(o => usuarios.Contains(o.UsuarioId) || o.Rol == "ANO")
                 .ToList()
                 ;
            return registros;
        }

        public JObject ObjectToJson(UsuarioMovil entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.UsuarioId); //UsuarioId en el servidor tiene el id de la tabla de usuario
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));



            objJson.Add("username", entidad.Username);
            objJson.Add("apellidos_nombres", entidad.ApellidosNombres);
            objJson.Add("pin", entidad.Pin);
            objJson.Add("fecha_creacion", entidad.FechaCreacion);
            objJson.Add("ultimo_acceso", entidad.UltimoAcceso);
            objJson.Add("rol", entidad.Rol);
            objJson.Add("activo_movil", GetStringFromBool(entidad.ActivoMovil));
            objJson.Add("codigo_sincronizacion", entidad.CodigoSincronizacion);
            return objJson;
        }

        public UsuarioMovil JsonToObject(JObject json)
        {

            var entity = new UsuarioMovil();
            if (json.Property("m_id") != null && (int)json.Property("m_id") != 0)
            {
                int id = (int)json["m_id"];
                entity = Repository.GetAll()
                    .Where(o => o.UsuarioId == id)
                    .FirstOrDefault();
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

            entity.Username = (string)json["username"];
            entity.ApellidosNombres = (string)json["apellidos_nombres"];
            entity.Pin = (string)json["pin"];
            entity.Rol = (string)json["rol"];
            entity.CodigoSincronizacion = (string)json["codigo_sincronizacion"];
            entity.FechaCreacion = GetDateFromString((string)json["fecha_creacion"]);
            entity.UltimoAcceso = GetDateFromString((string)json["ultimo_acceso"]);
            var activo = (int)json["activo_movil"];
            entity.ActivoMovil = activo == 1 ? true : false;
            return entity;
        }

        public UsuarioMovilDto FindByUsernamePin(string username, string pin)
        {
            var entity = Repository.GetAll()
                .Where(o => o.Username == username)
                .Where(o => o.Pin == pin)
                .Where(o => o.ActivoMovil)
                .FirstOrDefault();
            return MapToEntityDto(entity);
        }
    }
}
