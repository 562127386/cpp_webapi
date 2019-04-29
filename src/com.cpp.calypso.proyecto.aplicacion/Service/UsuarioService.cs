using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class UsuarioAsyncBaseCrudAppService : AsyncBaseCrudAppService<Usuario, UsuarioDto, PagedAndFilteredResultRequestDto>
    {
        public UsuarioAsyncBaseCrudAppService(
            IBaseRepository<Usuario> repository) : base(repository)
        {
        }
    }
}
