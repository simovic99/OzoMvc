using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OzoMvc.Models;

namespace OzoMvc.Controllers
{
    public class PosaoOpremasController : Controller
    {
        private readonly PI05Context _context;

        public PosaoOpremasController(PI05Context context)
        {
            _context = context;
        }

        // GET: PosaoOpremas
        public async Task<IActionResult> Index()
        {
            var pI05Context = _context.PosaoOprema.Include(p => p.Oprema).Include(p => p.Posao);
            return View(await pI05Context.ToListAsync());
        }

        // GET: PosaoOpremas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posaoOprema = await _context.PosaoOprema
                .Include(p => p.Oprema)
                .Include(p => p.Posao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posaoOprema == null)
            {
                return NotFound();
            }

            return View(posaoOprema);
        }

        // GET: PosaoOpremas/Create
        public IActionResult Create()
        {
            ViewData["OpremaId"] = new SelectList(_context.Oprema, "InventarniBroj", "InventarniBroj");
            ViewData["PosaoId"] = new SelectList(_context.Posao, "Id", "Id");
            return View();
        }

        // POST: PosaoOpremas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PosaoId,OpremaId,Satnica")] PosaoOprema posaoOprema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posaoOprema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OpremaId"] = new SelectList(_context.Oprema, "InventarniBroj", "InventarniBroj", posaoOprema.OpremaId);
            ViewData["PosaoId"] = new SelectList(_context.Posao, "Id", "Id", posaoOprema.PosaoId);
            return View(posaoOprema);
        }

        // GET: PosaoOpremas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posaoOprema = await _context.PosaoOprema.FindAsync(id);
            if (posaoOprema == null)
            {
                return NotFound();
            }
            ViewData["OpremaId"] = new SelectList(_context.Oprema, "InventarniBroj", "InventarniBroj", posaoOprema.OpremaId);
            ViewData["PosaoId"] = new SelectList(_context.Posao, "Id", "Id", posaoOprema.PosaoId);
            return View(posaoOprema);
        }

        // POST: PosaoOpremas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PosaoId,OpremaId,Satnica")] PosaoOprema posaoOprema)
        {
            if (id != posaoOprema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posaoOprema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosaoOpremaExists(posaoOprema.Id))
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
            ViewData["OpremaId"] = new SelectList(_context.Oprema, "InventarniBroj", "InventarniBroj", posaoOprema.OpremaId);
            ViewData["PosaoId"] = new SelectList(_context.Posao, "Id", "Id", posaoOprema.PosaoId);
            return View(posaoOprema);
        }

        // GET: PosaoOpremas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posaoOprema = await _context.PosaoOprema
                .Include(p => p.Oprema)
                .Include(p => p.Posao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posaoOprema == null)
            {
                return NotFound();
            }

            return View(posaoOprema);
        }

        // POST: PosaoOpremas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posaoOprema = await _context.PosaoOprema.FindAsync(id);
            _context.PosaoOprema.Remove(posaoOprema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosaoOpremaExists(int id)
        {
            return _context.PosaoOprema.Any(e => e.Id == id);
        }
    }
}
