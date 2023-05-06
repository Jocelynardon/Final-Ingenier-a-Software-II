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
    public class ControlVotosController : Controller
    {
        private readonly FinalSoftwareContext _context;

        public ControlVotosController(FinalSoftwareContext context)
        {
            _context = context;
        }
        // GET: ControlVotosController
        //======================================================= CerrarCandidato//

        [Route("CerrarProceso")]
        [HttpPost]
        public async Task<int> CerrarProceso([FromBody] int vigente)
        {
            FinalSoftwareIIModel.GeneralResult generalResult = new FinalSoftwareIIModel.GeneralResult
            {
                Result = false
            };
            try
            {
                if (vigente == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                generalResult.ErrorMessage = ex.Message;
                return 0;
            }
            return 0;
        }
    }
}
