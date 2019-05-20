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
    public class ChoferAsyncBaseCrudAppService : AsyncBaseCrudAppService<Chofer, ChoferDto, PagedAndFilteredResultRequestDto>, IChoferAsyncBaseCrudAppService
    {
        public ChoferAsyncBaseCrudAppService(
            IBaseRepository<Chofer> repository
            ) : base(repository)
        {
            Repository = repository;
        }

        public IBaseRepository<Chofer> Repository { get; }

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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<Chofer> registros)
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
                Chofer instancia = JsonToObject(obj);

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

        public List<Chofer> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(Chofer entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("eliminado", GetStringFromBool(entidad.IsDeleted == false));

            //objJson.Add("fecha_registro", GetStringFromDateTime(entidad.FechaRegistro));
            objJson.Add("proveedor_id", entidad.ProveedorId);
            objJson.Add("catalogo_tipo_identificacion_id", entidad.CatalogoTipoIdentificacionId);
            objJson.Add("numero_identificacion", entidad.NumeroIdentificacion);
            objJson.Add("nombres", entidad.Nombres);
            objJson.Add("apellidos", entidad.Apellidos);
            objJson.Add("catalogo_genero_id", entidad.CatalogoGeneroId);
            objJson.Add("catalogo_estado_civil_id", entidad.CatalogoEstadoCivilId);
            objJson.Add("fecha_nacimiento", GetStringFromDate(entidad.FechaNacimiento));
            objJson.Add("celular", entidad.Celular);
            objJson.Add("mail", entidad.Mail);
            objJson.Add("estado", entidad.Estado);
            objJson.Add("fecha_estado", GetStringFromDate(entidad.FechaEstado));
            objJson.Add("apellidos_nombres", entidad.ApellidosNombres);

            return objJson;
        }

        public Chofer JsonToObject(JObject json)
        {

            var entity = new Chofer();
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
                entity.IsDeleted = (bool)json["eliminado"] == false;
            }


            //entity.FechaRegistro = GetDateTimeFromString((string)json["fecha_registro"]);
            entity.ProveedorId = (int)json["proveedor_id"];
            entity.CatalogoTipoIdentificacionId = (int)json["catalogo_tipo_identificacion_id"];
            entity.NumeroIdentificacion = (string)json["numero_identificacion"];
            entity.Nombres = (string)json["nombres"];
            entity.Apellidos = (string)json["apellidos"];
            entity.CatalogoGeneroId = (int)json["catalogo_genero_id"];
            entity.CatalogoEstadoCivilId = (int)json["catalogo_estado_civil_id"];
            entity.FechaNacimiento = GetDateFromString((string)json["fecha_nacimiento"]);
            entity.Celular = (string)json["celular"];
            entity.Mail = (string)json["mail"];
            entity.Estado = (int)json["estado"];
            entity.FechaEstado = GetDateFromString((string)json["fecha_estado"]);

            return entity;
        }
    }
}
