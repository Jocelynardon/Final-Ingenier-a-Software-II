using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APIFinalSoftwareII.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace APIFinalSoftwareII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly FinalSoftwareContext _context;

        public UsuariosController(FinalSoftwareContext context)
        {
            _context = context;
        }

        //======================================================= CHECK//

        [Route("Check")]
        [HttpPost]
        public async Task<bool> Check([FromBody] APIFinalSoftwareII.Models.Usuario usuario1)
        {
            FinalSoftwareIIModel.GeneralResult generalResult = new FinalSoftwareIIModel.GeneralResult
            {
                Result = false
            };
            try
            {
                APIFinalSoftwareII.Models.Usuario usuario = await _context.Usuarios.Select(s =>
                new APIFinalSoftwareII.Models.Usuario
                {
                    Usuario1 = s.Usuario1,
                    Password = s.Password,
                    TipoUsuario=s.TipoUsuario,
                    RealizoVoto = s.RealizoVoto,
                    Nombres = s.Nombres,
                    Apellidos = s.Apellidos,
                    Genero = s.Genero,
                    Partido = s.Partido
                }
                ).FirstOrDefaultAsync(s => s.Usuario1 == usuario1.Usuario1 && s.Password == usuario1.Password);
                if (usuario != null)
                {
                    generalResult.Result = true;
                }
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult.Result;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //======================================================= GET//
        [Route("GetList")]
        [HttpPost]
        public async Task<IEnumerable<APIFinalSoftwareII.Models.Usuario>> GetList()
        {
            //LuxHom1Context _moviesContext = new MoviesContext();
            IEnumerable<APIFinalSoftwareII.Models.Usuario> usuarios = await _context.Usuarios.Select(s =>
            new APIFinalSoftwareII.Models.Usuario
            {
                Usuario1 = s.Usuario1,
                Password = s.Password,
                TipoUsuario=s.TipoUsuario,
                RealizoVoto = s.RealizoVoto,
                Nombres = s.Nombres,
                Apellidos = s.Apellidos,
                Genero = s.Genero,
                Partido = s.Partido
            }
            ).ToListAsync();
            return usuarios;
        }

        //======================================================= SET//
        [Route("Set")]
        [HttpPost]
        public async Task<FinalSoftwareIIModel.GeneralResult> Set(APIFinalSoftwareII.Models.Usuario usuario)
        {
            FinalSoftwareIIModel.GeneralResult generalResult = new FinalSoftwareIIModel.GeneralResult
            {
                Result = false
            };
            try
            {
                Models.Usuario usuario1 = new Models.Usuario
                {
                    Usuario1 = usuario.Usuario1,
                    Password = usuario.Password,
                    TipoUsuario = usuario.TipoUsuario,
                    RealizoVoto = usuario.RealizoVoto,
                    Nombres = usuario.Nombres,
                    Apellidos = usuario.Apellidos,
                    Genero = usuario.Genero,
                    Partido = usuario.Partido
                };
                _context.Usuarios.Add(usuario1);
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }

        //======================================================= UPDATE//
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Update")]
        [HttpPost]
        public async Task<FinalSoftwareIIModel.GeneralResult> Update(APIFinalSoftwareII.Models.Usuario usuario)
        {
            FinalSoftwareIIModel.GeneralResult generalResult = new FinalSoftwareIIModel.GeneralResult
            {
                Result = false
            };
            try
            {
                Models.Usuario usuario1 = new Models.Usuario
                {
                    Usuario1 = usuario.Usuario1,
                    Password = usuario.Password,
                    TipoUsuario = usuario.TipoUsuario,
                    RealizoVoto = usuario.RealizoVoto,
                    Nombres = usuario.Nombres,
                    Apellidos = usuario.Apellidos,
                    Genero = usuario.Genero,
                    Partido = usuario.Partido
                };
                _context.Usuarios.Update(usuario1);
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }

        //======================================================= DELETE//
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Delete")]
        [HttpPost]
        public async Task<FinalSoftwareIIModel.GeneralResult> Delete([FromBody] string usuario1)
        {
            FinalSoftwareIIModel.GeneralResult generalResult = new FinalSoftwareIIModel.GeneralResult
            {
                Result = false
            };
            try
            {
                APIFinalSoftwareII.Models.Usuario usuario = await _context.Usuarios.Select(s =>
                new APIFinalSoftwareII.Models.Usuario
                {
                    Usuario1 = s.Usuario1,
                    Password = s.Password,
                    TipoUsuario = s.TipoUsuario,
                    RealizoVoto = s.RealizoVoto,
                    Nombres = s.Nombres,
                    Apellidos = s.Apellidos,
                    Genero = s.Genero,
                    Partido = s.Partido
                }
                ).FirstOrDefaultAsync(s => s.Usuario1 == usuario1);
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }
    }
}
