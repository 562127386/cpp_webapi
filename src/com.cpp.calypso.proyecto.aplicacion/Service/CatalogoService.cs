using System;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class CatalogoAsyncBaseCrudAppService : AsyncBaseCrudAppService<Catalogo, CatalogoDto, 
        PagedAndFilteredResultRequestDto>, ICatalogoAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<Catalogo> _repository;

        public CatalogoAsyncBaseCrudAppService(
            IBaseRepository<Catalogo> repository
            ) : base(repository)
        {
            _repository = repository;
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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<Catalogo> registros)
        {
            //IList<TEntityDto> registros = GetRegistros(version, usuariosId);

            JArray registroJson = new JArray();
            foreach (Catalogo entidad in registros)
            {

                int Id = entidad.Id;
                JObject objJson = ObjectToJson(entidad);


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
                Catalogo instancia = JsonToObject(obj);

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
                    System.Diagnostics.Debug.WriteLine(" ... 7.1.10 ...");

                    //Vinculamos el ID SqlServer al Id Sqlite
                    lKeyBinding.Add(instancia.Id, obj.GetValue("lkey").Value<int>());
                }
            }
            return lKeyBinding;
        }

        public List<Catalogo> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(Catalogo entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));


            objJson.Add("codigo", entidad.Codigo);
            objJson.Add("descripcion", entidad.Descripcion);
            objJson.Add("nombre", entidad.Nombre);
            objJson.Add("ordinal", entidad.Ordinal);
            objJson.Add("predeterminado", entidad.Predeterminado);
            objJson.Add("tipo_catalogo_id", entidad.TipoCatalogoId);

            return objJson;
        }

        public Catalogo JsonToObject(JObject json)
        {

            Catalogo entity = new Catalogo();
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

            entity.Codigo = (string)json.GetValue("codigo");
            entity.Nombre = (string)json.GetValue("nombre");
            entity.Descripcion = (string)json.GetValue("descripcion");
            entity.TipoCatalogoId = (int)json.GetValue("tipo_catalogo_id");
            entity.Predeterminado = (bool)json.GetValue("predeterminado");
            entity.Ordinal = (int)json.GetValue("ordinal");

            return entity;
        }

        protected string GetStringFromBool(bool b)
        {
            return b ? "1" : "0";
        }
    }
}
