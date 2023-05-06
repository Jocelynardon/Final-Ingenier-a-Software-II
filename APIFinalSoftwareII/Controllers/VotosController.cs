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
    public class VotoesController : Controller
    {
        private readonly FinalSoftwareContext _context;

        public VotoesController(FinalSoftwareContext context)
        {
            _context = context;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //======================================================= GET//
        [Route("GetList")]
        [HttpPost]
        public async Task<IEnumerable<APIFinalSoftwareII.Models.Voto>> GetList()
        {
            //LuxHom1Context _moviesContext = new MoviesContext();
            IEnumerable<APIFinalSoftwareII.Models.Voto> usuarios = await _context.Votos.Select(s =>
            new APIFinalSoftwareII.Models.Voto
            {
                Id = s.Id,
                Usuario = s.Usuario,
                Partido = s.Partido,
                Hora = s.Hora,
                FechaVoto = s.FechaVoto,
                Ip = s.Ip,
                VotosValidosTotales = s.VotosValidosTotales,
                VotosFraude = s.VotosFraude
            }
            ).ToListAsync();
            return usuarios;
        }

        //======================================================= SET//
        [Route("Set")]
        [HttpPost]
        public async Task<FinalSoftwareIIModel.GeneralResult> Set(APIFinalSoftwareII.Models.Voto voto)
        {
            FinalSoftwareIIModel.GeneralResult generalResult = new FinalSoftwareIIModel.GeneralResult
            {
                Result = false
            };
            try
            {
                Models.Voto voto1 = new Models.Voto
                {
                    Id = voto.Id,
                    Usuario = voto.Usuario,
                    Partido = voto.Partido,
                    Hora = voto.Hora,
                    FechaVoto = voto.FechaVoto,
                    Ip = voto.Ip,
                    VotosValidosTotales = voto.VotosValidosTotales,
                    VotosFraude = voto.VotosFraude
                };
                _context.Votos.Add(voto1);
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
        public async Task<FinalSoftwareIIModel.GeneralResult> Update(APIFinalSoftwareII.Models.Voto voto)
        {
            FinalSoftwareIIModel.GeneralResult generalResult = new FinalSoftwareIIModel.GeneralResult
            {
                Result = false
            };
            try
            {
                Models.Voto voto1 = new Models.Voto
                {
                    Id = voto.Id,
                    Usuario = voto.Usuario,
                    Partido = voto.Partido,
                    Hora = voto.Hora,
                    FechaVoto = voto.FechaVoto,
                    Ip = voto.Ip,
                    VotosValidosTotales = voto.VotosValidosTotales,
                    VotosFraude = voto.VotosFraude
                };
                _context.Votos.Update(voto);
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
        public async Task<FinalSoftwareIIModel.GeneralResult> Delete([FromBody] int id)
        {
            FinalSoftwareIIModel.GeneralResult generalResult = new FinalSoftwareIIModel.GeneralResult
            {
                Result = false
            };
            try
            {
                APIFinalSoftwareII.Models.Voto voto = await _context.Votos.Select(s =>
                new APIFinalSoftwareII.Models.Voto
                {
                    Id = s.Id,
                    Usuario = s.Usuario,
                    Partido = s.Partido,
                    Hora = s.Hora,
                    FechaVoto = s.FechaVoto,
                    Ip = s.Ip,
                    VotosValidosTotales = s.VotosValidosTotales,
                    VotosFraude = s.VotosFraude
                }
                ).FirstOrDefaultAsync(s => s.Id == id);
                if (voto != null)
                {
                    _context.Votos.Remove(voto);
                    await _context.SaveChangesAsync();
                    generalResult.Result = true;
                }
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
