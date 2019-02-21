using Abp.Application.Services.Dto;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public class AuditoriaService : AsyncBaseCrudAppService<AuditoriaEntidad, AuditoriaDto, PagedAndFilteredResultRequestDto>,
        IAuditoriaService
    {
        public AuditoriaService(IHandlerExcepciones manejadorExcepciones,
            IBaseRepository<AuditoriaEntidad> repository) :
            base( repository)
        {

        }


        protected override IQueryable<AuditoriaEntidad> ApplyFilter(IQueryable<AuditoriaEntidad> query, PagedAndFilteredResultRequestDto input)
        {
            //Try to sort query if available
            var filterInput = input as PagedAndFilteredResultRequestDto;
            if (filterInput != null && filterInput.Filter != null)
            {
                //TODO: Mejorar el proceso, mas reutilizable.
                //Campos fechas, aplicar Truncate.   Value>=CampoFecha && Value<=CampoFecha+1 

                //Exclude
                object valueFilterCreatedDate =null;
                var newFilters = new List<FilterEntity>();
                foreach (var filter in filterInput.Filter)
                {
                    if (filter.Field.ToUpper() == "CREATEDDATE") { 
                        valueFilterCreatedDate = filter.Value;
                        continue;
                    }

                    newFilters.Add(filter);
                }
                query =  query.Where(newFilters);

                //Add Date
                if (valueFilterCreatedDate != null) {
                    DateTime? CreatedDate = valueFilterCreatedDate as DateTime?; 
                    query = query.Where(p => p.CreatedDate >= DbFunctions.TruncateTime(CreatedDate) && p.CreatedDate <= DbFunctions.AddDays(CreatedDate, 1));
 
                }
            }

            //No filter
            return query;

            
        }

        public override async Task<AuditoriaDto> Get(EntityDto<int> input)
        {
            return await Task.Run(() =>
            {
                var item = Repository.GetAllIncluding(include => include.Properties)
                    .Where(a => a.AuditEntryID == input.Id).SingleOrDefault();

                if (item == null)
                    return null;

                var output = MapToEntityDto(item);

                return output;
            });
        }
    }
}
