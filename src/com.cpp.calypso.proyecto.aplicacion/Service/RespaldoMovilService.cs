using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.proyecto.dominio.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class RespaldoMovilAsyncBaseCrudAppService : AsyncBaseCrudAppService<RespaldoMovil, RespaldoMovilDto, PagedAndFilteredResultRequestDto>, IRespaldoMovilAsyncBaseCrudAppService
    {
        private readonly IBaseRepository<UsuarioMovil> _usuarioMovilRepository;

        public RespaldoMovilAsyncBaseCrudAppService(
            IBaseRepository<RespaldoMovil> repository,
            IBaseRepository<UsuarioMovil> usuarioMovilRepository
            ) : base(repository)
        {
            _usuarioMovilRepository = usuarioMovilRepository;
        }


        public void Create(string json, int userId)
        {
            var entity = new RespaldoMovil()
            {
                Estado = "SOL",
                FechaRegistro = DateTime.Now,
                Json = json,
                UserId = userId
            };

            var code = Guid.NewGuid().ToString("n").Substring(0, 5);
            var usuario = _usuarioMovilRepository
                .GetAll()
                .FirstOrDefault(o => o.UsuarioId == userId);

            if (usuario != null)
            {
                usuario.CodigoSincronizacion = code;
                _usuarioMovilRepository.Update(usuario);
            }
            

            Repository.Insert(entity);
        }
    }
}
