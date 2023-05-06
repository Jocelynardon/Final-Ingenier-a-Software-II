using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APIFinalSoftwareII.Models;

namespace APIFinalSoftwareII.Controllers
{
    public class VotoesController : Controller
    {
        private readonly FinalSoftwareContext _context;

        public VotoesController(FinalSoftwareContext context)
        {
            _context = context;
        }

        // GET: Votoes
        public async Task<IActionResult> Index()
        {
            var finalSoftwareContext = _context.Votos.Include(v => v.UsuarioNavigation);
            return View(await finalSoftwareContext.ToListAsync());
        }

        // GET: Votoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Votos == null)
            {
                return NotFound();
            }

            var voto = await _context.Votos
                .Include(v => v.UsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voto == null)
            {
                return NotFound();
            }

            return View(voto);
        }

        // GET: Votoes/Create
        public IActionResult Create()
        {
            ViewData["Usuario"] = new SelectList(_context.Usuarios, "Usuario1", "Usuario1");
            return View();
        }

        // POST: Votoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Usuario,Partido,Hora,FechaVoto,Ip,VotosValidosTotales,VotosFraude")] Voto voto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Usuario"] = new SelectList(_context.Usuarios, "Usuario1", "Usuario1", voto.Usuario);
            return View(voto);
        }

        // GET: Votoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Votos == null)
            {
                return NotFound();
            }

            var voto = await _context.Votos.FindAsync(id);
            if (voto == null)
            {
                return NotFound();
            }
            ViewData["Usuario"] = new SelectList(_context.Usuarios, "Usuario1", "Usuario1", voto.Usuario);
            return View(voto);
        }

        // POST: Votoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Usuario,Partido,Hora,FechaVoto,Ip,VotosValidosTotales,VotosFraude")] Voto voto)
        {
            if (id != voto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VotoExists(voto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Usuario"] = new SelectList(_context.Usuarios, "Usuario1", "Usuario1", voto.Usuario);
            return View(voto);
        }

        // GET: Votoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Votos == null)
            {
                return NotFound();
            }

            var voto = await _context.Votos
                .Include(v => v.UsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voto == null)
            {
                return NotFound();
            }

            return View(voto);
        }

        // POST: Votoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Votos == null)
            {
                return Problem("Entity set 'FinalSoftwareContext.Votos'  is null.");
            }
            var voto = await _context.Votos.FindAsync(id);
            if (voto != null)
            {
                _context.Votos.Remove(voto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VotoExists(int id)
        {
          return (_context.Votos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
