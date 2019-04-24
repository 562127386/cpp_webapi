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
    public class ContratoProveedorAsyncBaseCrudAppService : AsyncBaseCrudAppService<ContratoProveedor, 
        ContratoProveedorDto, PagedAndFilteredResultRequestDto>, IContratoProveedorAsyncBaseCrudAppService
    {
        public ContratoProveedorAsyncBaseCrudAppService(IBaseRepository<ContratoProveedor> repository) : base(repository)
        {
            Repository = repository;
        }

        public IBaseRepository<ContratoProveedor> Repository { get; }


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

        public JArray GenerarRegistrosMovil(Dictionary<int, int> diccionario, List<ContratoProveedor> registros)
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
                ContratoProveedor instancia = JsonToObject(obj);

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

        public List<ContratoProveedor> GetRegistros(int version, List<int> usuarios)
        {
            var registros = Repository.GetAll()
                .Where(o => o.Version > version)
                .ToList()
                ;
            return registros;
        }

        public JObject ObjectToJson(ContratoProveedor entidad)
        {
            JObject objJson = new JObject();

            objJson.Add("m_id", entidad.Id);
            if (entidad.Ref != null)
            {
                objJson.Add("m_ref", entidad.Ref);
            }
            objJson.Add("m_version", entidad.Version);
            objJson.Add("vigente", GetStringFromBool(entidad.IsDeleted == false));

            objJson.Add("proveedor_id", entidad.ProveedorId);
            objJson.Add("empresa_id", entidad.EmpresaId);
            objJson.Add("codigo", entidad.Codigo);
            objJson.Add("objeto", entidad.Objeto);
            objJson.Add("fecha_inicio", GetStringFromDate(entidad.FechaInicio));
            objJson.Add("fecha_fin", GetStringFromDate(entidad.FechaFin));
            objJson.Add("plazo_pago", entidad.PlazoPago);
            objJson.Add("monto", entidad.Monto);
            objJson.Add("orden_compra", entidad.OrdenCompra);
            objJson.Add("estado", entidad.Estado == ContratoEstado.Inactivo ? 0 : 1);
            objJson.Add("documentacion_id", entidad.DocumentacionId);
            return objJson;
        }

        public ContratoProveedor JsonToObject(JObject json)
        {

            var entity = new ContratoProveedor();
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
            entity.EmpresaId = (int)json["empresa_id"];
            entity.Codigo = (string)json["codigo"];
            entity.Objeto = (string)json["objeto"];
            entity.FechaInicio = GetDateFromString((string)json["fecha_inicio"]);
            entity.FechaFin = GetDateFromString((string)json["fecha_fin"]);
            entity.PlazoPago = (int)json["plazo_pago"];
            entity.Monto = (decimal)json["monto"];
            entity.OrdenCompra = (string)json["orden_compra"];
            int estado = (int)json["estado"];
            entity.Estado = estado == 0 ? ContratoEstado.Inactivo : ContratoEstado.Activo;
            entity.DocumentacionId = (int)json["documentacion_id"];

            return entity;
        }
    }
}
