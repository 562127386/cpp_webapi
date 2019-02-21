using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using com.cpp.calypso.framework;
using com.cpp.calypso.seguridad.aplicacion;

namespace com.cpp.calypso.webapi.Api.Controllers
{
    public class RolController : BaseApiController
    {
        public IRolService EntityService { get; }


        public RolController(
            IRolService entityService,
            IHandlerExcepciones manejadorExcepciones) : base(manejadorExcepciones)
        {
            EntityService = entityService;
        }

        
        public async Task<IEnumerable<RolDto>> GetAll()
        {
            return await EntityService.GetAll();
        }

        public async Task<RolDto> Post(RolDto input) {

            return await EntityService.Create(input);
            
        }

        public async Task<RolDto> Put(RolDto input)
        {

            return await EntityService.Update(input);

        }

        public async Task Delete(int input)
        {
             await EntityService.Delete(new EntityDto<int>(input));
        }
         


    }
}
