using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OzoMvc.Models;
using OzoMvc.ViewModels;

namespace OzoMvc.Controllers
{
    public class ZaposlenikPosaosController : Controller
    {
        private readonly PI05Context _context;

        public ZaposlenikPosaosController(PI05Context context)
        {
            _context = context;
        }

        // GET: ZaposlenikPosaos
        public IActionResult Index()
        {

            var po = _context.Posao.AsNoTracking();

            var z = _context.ZaposlenikPosao.AsNoTracking().Where(x => x.Posao == po).ToList();
            var o = _context.PosaoOprema.AsNoTracking().Where(y => y.Posao == po).ToList();
            var zaposlenik = z.Select(x => new ZaposlenikPosao
            {
                ZaposlenikId=x.ZaposlenikId,
                Posao=x.Posao,
                PosaoId=x.PosaoId,
                Satnica=x.Satnica,
                ZaposlenikNavigation=x.ZaposlenikNavigation,

                
            })
                        .ToList();
            var oprema = o.Select(y => new PosaoOprema {
            
              Posao=y.Posao,
              PosaoId=y.PosaoId,
              Oprema=y.Oprema,
              OpremaId=y.OpremaId,
              Satnica=y.Satnica,

            }).ToList();
                 
            var poslovi = po.Select(p => new PosaoViewModel
            {
                Id = p.Id,
                Vrijeme = p.Vrijeme,    
                UslugaNaziv = p.UslugaNavigation.Naziv,
                MjestoNaziv = p.MjestoNavigation.Naziv,
                Cijena = p.Cijena,
                Troskovi = p.Troskovi,
                ZaposlenikPosao=zaposlenik,
                PosaoOprema=oprema,
         

            }).ToList();
            var posao = new PosloviViewModel()
            {
                Poslovi = poslovi,
            };
            return View(posao);
        }

        // GET: ZaposlenikPosaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenikPosao = await _context.ZaposlenikPosao
                .Include(z => z.Posao)
                .Include(z => z.ZaposlenikNavigation)
                .Where(z=> z.PosaoId==id).FirstOrDefaultAsync();
            if (zaposlenikPosao == null)
            {
                return NotFound();
            }
        
            return View(zaposlenikPosao);
        }

        // GET: ZaposlenikPosaos/Create
        public IActionResult Create()
        {
            ViewData["PosaoId"] = new SelectList(_context.Posao, "Id", "Id");
            ViewData["ZaposlenikId"] = new MultiSelectList(_context.Zaposlenik, "Id", "Ime");
            return View();
        }

        // POST: ZaposlenikPosaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ZaposlenikPosao zaposlenikPosao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaposlenikPosao);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PosaoId"] = new SelectList(_context.Posao, "Id", "Id", zaposlenikPosao.PosaoId);
            ViewData["ZaposlenikId"] = new SelectList(_context.Zaposlenik, "Id", "Ime", zaposlenikPosao.ZaposlenikId);
            return View(zaposlenikPosao);
        }

        // GET: ZaposlenikPosaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenikPosao = await _context.ZaposlenikPosao.FindAsync(id);
            if (zaposlenikPosao == null)
            {
                return NotFound();
            }
            ViewData["PosaoId"] = new SelectList(_context.Posao, "Id", "Id", zaposlenikPosao.PosaoId);
            ViewData["ZaposlenikId"] = new SelectList(_context.Zaposlenik, "Id", "Ime", zaposlenikPosao.ZaposlenikId);
            return View(zaposlenikPosao);
        }

        // POST: ZaposlenikPosaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ZaposlenikId,PosaoId,Satnica")] ZaposlenikPosao zaposlenikPosao)
        {
            if (id != zaposlenikPosao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaposlenikPosao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaposlenikPosaoExists(zaposlenikPosao.Id))
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
            ViewData["PosaoId"] = new SelectList(_context.Posao, "Id", "Id", zaposlenikPosao.PosaoId);
            ViewData["ZaposlenikId"] = new SelectList(_context.Zaposlenik, "Id", "Ime", zaposlenikPosao.ZaposlenikId);
            return View(zaposlenikPosao);
        }

        // GET: ZaposlenikPosaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenikPosao = await _context.ZaposlenikPosao
                .Include(z => z.Posao)
                .Include(z => z.ZaposlenikNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaposlenikPosao == null)
            {
                return NotFound();
            }

            return View(zaposlenikPosao);
        }

        // POST: ZaposlenikPosaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposlenikPosao = await _context.ZaposlenikPosao.FindAsync(id);
            _context.ZaposlenikPosao.Remove(zaposlenikPosao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaposlenikPosaoExists(int id)
        {
            return _context.ZaposlenikPosao.Any(e => e.Id == id);
        }
    }
}
