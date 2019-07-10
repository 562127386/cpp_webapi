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
    public class ConsumoSinReservaHospedajeAsyncBaseCrudAppService : AsyncBaseCrudAppService<ConsumoSinReservaHospedaje, ConsumoSinReservaHospedajeDto, PagedAndFilteredResultRequestDto>, IConsumoSinReservaHospedajeAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<Proveedor> _proveedorBaseRepository;

        public ConsumoSinReservaHospedajeAsyncBaseCrudAppService(
            IBaseRepository<ConsumoSinReservaHospedaje> repository,
            IBaseRepository<Proveedor> proveedorBaseRepository
            ) : base(repository)
        {
            _proveedorBaseRepository = proveedorBaseRepository;
            Repository = repository;
        }

        public IBaseRepository<ConsumoSinReservaHospedaje> Repository { get; }

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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<ConsumoSinReservaHospedaje> registros)
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
                ConsumoSinReservaHospedaje instancia = JsonToObject(obj);

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

        public List<ConsumoSinReservaHospedaje> GetRegistros(int version, List<int> usuarios,
            Dictionary<int, int> diccionario)
        {
            var consumosSinReservaEnMovil = diccionario.Keys.ToList();
            var fechaActual = DateTime.Today;

            var proveedoresLogeados = _proveedorBaseRepository.GetAll()
                .Where(o => o.IsDeleted || o.IsDeleted == false)
                .Where(o => usuarios.Contains((int) o.Usuario))
                .Select(proveedor => proveedor.Id)
                .ToList();

            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .Where(o => o.IsDeleted || o.IsDeleted == false)
                .Where(o => proveedoresLogeados.Contains(o.ProveedorId))
                .Where(o => consumosSinReservaEnMovil.Contains(o.Id) || o.FechaRegistro == fechaActual)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(ConsumoSinReservaHospedaje entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));

            //
            objJson.Add("proveedor_id", entidad.ProveedorId);
            objJson.Add("colaborador_id", entidad.ColaboradorId);
            objJson.Add("identificacion_colaborador", entidad.IdentificacionColaborador);
            objJson.Add("nombres", entidad.Nombres);
            objJson.Add("primer_apellido", entidad.PrimerApellido);
            objJson.Add("segundo_apellido", entidad.SegundoApellido);
            objJson.Add("origen_consumo_id", entidad.OrigenConsumoId);
            objJson.Add("tipo_habitacion_id", entidad.TipoHabitacionId);
            objJson.Add("fecha_registro", GetStringFromDate(entidad.FechaRegistro));
            objJson.Add("numero_habitacion", entidad.NumeroHabitacion);
            objJson.Add("autorizacion", entidad.Autorizacion);



            return objJson;
        }

        public ConsumoSinReservaHospedaje JsonToObject(JObject json)
        {
            var entity = new ConsumoSinReservaHospedaje();

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

            entity.ProveedorId = (int)json["proveedor_id"];

            if (json.GetValue("colaborador_id").Type != JTokenType.Null)
                entity.ColaboradorId = (int)json["colaborador_id"];

            entity.IdentificacionColaborador = (string)json["identificacion_colaborador"];
            entity.Nombres = (string)json["nombres"];
            entity.PrimerApellido = (string)json["primer_apellido"];
            entity.SegundoApellido = (string)json["segundo_apellido"];
            entity.OrigenConsumoId = (int)json["origen_consumo_id"];
            entity.TipoHabitacionId = (int)json["tipo_habitacion_id"];
            entity.FechaRegistro = GetDateFromString((string)json["fecha_registro"]);
            entity.NumeroHabitacion = (int)json["numero_habitacion"];

            if (json.GetValue("autorizacion").Type != JTokenType.Null)
                entity.Autorizacion = (int)json["autorizacion"];

            return entity;
        }
    }
}
