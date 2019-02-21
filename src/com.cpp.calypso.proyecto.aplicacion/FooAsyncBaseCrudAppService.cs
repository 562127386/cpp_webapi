//using Abp.Application.Services.Dto;
//using Abp.AutoMapper;
//using com.cpp.calypso.comun.aplicacion;
//using com.cpp.calypso.comun.dominio;
//using com.cpp.calypso.framework;
//using com.cpp.calypso.proyecto.dominio;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace com.cpp.calypso.proyecto.aplicacion
//{
//    public interface IFooAsyncBaseCrudAppService: IAsyncBaseCrudAppService<Foo, FooDto, 
//        ConsultaFooPagedAndFilteredResultRequestDto>
//    {
//        List<FooDto> Consultar(ConsultaFooPagedAndFilteredResultRequestDto consulta);
       
//    }

//    public class FooAsyncBaseCrudAppService :
//        AsyncBaseCrudAppService<Foo, FooDto, ConsultaFooPagedAndFilteredResultRequestDto>, IFooAsyncBaseCrudAppService
//    {
//        public FooAsyncBaseCrudAppService(
//             IHandlerExcepciones manejadorExcepciones,
//            IBaseRepository<Foo> repository) : base(manejadorExcepciones,repository)
//        {

//        }

//        public override Task<PagedResultDto<FooDto>> GetAll(ConsultaFooPagedAndFilteredResultRequestDto 
//            input)
//        {
//            ///Linq.. (ret)
//            return base.GetAll(input);
//        }

//        protected override IQueryable<Foo> ApplyFilter(IQueryable<Foo> query, 
//            ConsultaFooPagedAndFilteredResultRequestDto input)
//        {

          
//            if (string.IsNullOrWhiteSpace(input.Codigo))
//            {
//                query.Where(p => p.Codigo == input.Codigo);
//            }

//            if (input.Estado.HasValue)
//            {
//                query.Where(p => p.Estado == input.Estado);
//            }

//            return query;

//            //return base.ApplyFilter(query, input);
//        }

//        public List<FooDto> Consultar(ConsultaFooPagedAndFilteredResultRequestDto consulta) {

//            var query =  Repository.GetAll();
 
//            if (string.IsNullOrWhiteSpace(consulta.Codigo)) {
//                query.Where(p => p.Codigo == consulta.Codigo);
//            }

//            if (consulta.Estado.HasValue)
//            {
//                query.Where(p => p.Estado == consulta.Estado);
//            }

//            var list = query.ToList().Select(MapToEntityDto).ToList();

//            return list;
//        }
 
//    }


//    public class ConsultaFooPagedAndFilteredResultRequestDto : PagedAndFilteredResultRequestDto {

//        public string Codigo { get; set; }

//        public Estado? Estado { get; set; }
//    }

//    [AutoMap(typeof(Foo))]
//    [Serializable]
//    public class FooDto : EntityDto
//    {
//        public string Codigo { get; set; }

//        [Obligado]
//        public string Nombre { get; set; }

//        public DateTime FechaInicio { get; set; }

//        public DateTime FechaFinal { get; set; }

//        public Estado Estado { get; set; }

//        public ICollection<ListaDto> Items { get; set; }
//    }

//    [AutoMap(typeof(Lista))]
//    [Serializable]
//    public class ListaDto : EntityDto
//    {
//        public string Nombre { get; set; }

//        public int ProyectoId { get; set; }

//    }
//}
