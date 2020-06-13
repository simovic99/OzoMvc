using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzoMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk.Query;
using OzoMvc.ViewModels;
using System.Text.Json;
using OzoMvc.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lucene.Net.Support;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PdfRpt.Core.Helper;
using Microsoft.Crm.Sdk.Messages;




// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OzoMvc.Controllers
{
    public class PosaoController : Controller
    {
        private readonly PI05Context ctx;
        private readonly AppSettings appData;
        private readonly ILogger<PosaoController> logger;

        public PosaoController(PI05Context ctx, IOptionsSnapshot<AppSettings> options, ILogger<PosaoController> logger)
        {
            this.ctx = ctx;
            appData = options.Value;

            this.logger = logger;
        }

  


        public IActionResult Index(string filter,int page = 1, int sort = 1, bool ascending = true)
        {
            int pagesize = appData.PageSize;


            PrepareDropDownLists();
            //var x = ctx.Posao.Include(p => p.ZaposlenikPosao).ThenInclude(z => z.Zaposlenik).AsNoTracking();

            var query = ctx.Posao.Include(z=>z.ZaposlenikPosao).ThenInclude(z=>z.ZaposlenikNavigation)
                        .AsNoTracking();

            var x = ctx.ZaposlenikPosao.Where(z => z.Posao == query).AsNoTracking();
           
            int count = query.Count();
            if (count == 0)
            {
                logger.LogInformation("Ne postoji nijedan posao");
                TempData[Constants.Message] = "Ne postoji niti jedan posao.";
                TempData[Constants.ErrorOccurred] = false;
                return RedirectToAction(nameof(Create));
            }
           PosaoFilter nf = new PosaoFilter();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                nf = PosaoFilter.FromString(filter);
                if (!nf.IsEmpty())
                {

                    query = nf.Apply(query);
                }
            }

            var pagingInfo = new ViewModels.PagingInfo
            {
                CurrentPage = page,
                Sort = sort,
                Ascending = ascending,
                ItemsPerPage = pagesize,
                TotalItems = count
            };
            if (page < 1)
            {
                page = 1;
            }
            else if (page > pagingInfo.TotalPages)
            {
                return RedirectToAction(nameof(Index), new { page = pagingInfo.TotalPages, sort, ascending });
            }

            System.Linq.Expressions.Expression<Func<Posao, object>> orderSelector = null;
            switch (sort)
            {
                case 1:
                    orderSelector = d => d.Id;
                    break;
                case 2:
                    orderSelector = d => d.UslugaId;
                    break;
                case 3:
                    orderSelector = d => d.Cijena;
                    break;
                case 4:
                    orderSelector = d => d.Troskovi;
                    break;
            }
            if (orderSelector != null)
            {
                query = ascending ?
                       query.OrderBy(orderSelector) :
                       query.OrderByDescending(orderSelector);
            }




            var poslovi = query.Select(p => new PosaoViewModel
            {
                Id = p.Id,
                Vrijeme = p.Vrijeme,
                UslugaNaziv = p.UslugaNavigation.Naziv,
                MjestoNaziv = p.MjestoNavigation.Naziv,
                Cijena = p.Cijena,
                Troskovi = p.Troskovi,




            })
                        .Skip((page - 1) * pagesize)
                        .Take(pagesize)
                        .ToList();
            var model = new PosloviViewModel
            {
              
                Poslovi = poslovi,
                PagingInfo = pagingInfo
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Filter(PosaoFilter filter)
        {
            return RedirectToAction(nameof(Index), new { filter = filter.ToString() });
        }
        [HttpGet]
        public IActionResult Create()
        {
            PrepareDropDownLists();

            return View();
        }
        private void PrepareDropDownLists()
        {
            var usluge = ctx.Usluga.Select(d => new { d.Naziv, d.Id }).ToList();
            var zaposlenici = ctx.Zaposlenik.Select(d => new { d.Ime, d.Id,d.Prezime}).ToList();
            var mjesta = ctx.Mjesto.Select(d => new { d.Naziv, d.Id }).ToList();
            var oprema= ctx.Oprema.Select(d => new { d.Naziv, d.InventarniBroj }).ToList();
            ViewBag.Mjesta = new SelectList(mjesta, nameof(Mjesto.Id), nameof(Mjesto.Naziv));
            ViewBag.Usluge = new SelectList(usluge, nameof(Usluga.Id), nameof(Usluga.Naziv));
            ViewBag.Zaposlenici = new MultiSelectList(zaposlenici, nameof(Zaposlenik.Id), nameof(Zaposlenik.Ime),nameof(Zaposlenik.Prezime));
            ViewBag.Oprema = new MultiSelectList(oprema, nameof(Oprema.InventarniBroj), nameof(Oprema.Naziv));


        }
     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PosaoViewModel posao)

        {
            PrepareDropDownLists();
            logger.LogTrace(JsonSerializer.Serialize(posao));
            if (ModelState.IsValid)
            {
                try
                {
                    Posao p = new Posao()
                    {
                       
                        UslugaId = posao.UslugaId,
                        Vrijeme = posao.Vrijeme,
                        Cijena = posao.Cijena,
                        Troskovi = posao.Troskovi,
                        MjestoId = posao.MjestoId,
                       
                       
                    };
                    ctx.Add(p);

                    for (int i = 0; i < posao.ZaposlenikId.Length; i++)
                    {
                       
                       ZaposlenikPosao zp= new ZaposlenikPosao()
                        {
                           Posao=p,
                           ZaposlenikId = posao.ZaposlenikId[i],
                          Satnica=posao.SatnicaZaposlenika,
                          

                        };
                        ctx.Add(zp);
                       
                    }
                    for (int i = 0; i < posao.OpremaId.Length; i++) {
                        PosaoOprema op = new PosaoOprema()
                        {
                            Posao=p,
                            OpremaId = posao.OpremaId[i],
                           Satnica=posao.SatnicaOpreme,
                           
                           

                        };
                        ctx.Add(op);
                        
                    }
               

                    ctx.SaveChanges();

                    logger.LogInformation(new EventId(1000), $"Posao {p.Id} dodan.");

                    TempData[Constants.Message] = $"Posao {p.Id} dodan.";
                    TempData[Constants.ErrorOccurred] = false;
                    return RedirectToAction(nameof(Index));
                
                }
        
                catch (Exception exc)
                {
                    logger.LogError("Pogreška prilikom dodavanja novog posla: {0}", exc.CompleteExceptionMessage());
                    ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
                    return View(posao);
                }
            }
            else
            {
                return View(posao);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Id, int page = 1, int sort = 1, bool ascending = true)
        {
            var posao = ctx.Posao
                             .AsNoTracking()
                             .Where(d => d.Id == Id)
                             .SingleOrDefault();
            if (posao != null)
            {
                try
                {
                    int id = posao.Id;
                    ctx.Remove(posao);
                    ctx.SaveChanges();
                    logger.LogInformation($"Posao {id} uspješno obrisan");
                    TempData[Constants.Message] = $"Posao {id} uspješno obrisan";
                    TempData[Constants.ErrorOccurred] = false;
                }
                catch (Exception exc)
                {
                    TempData[Constants.Message] = "Pogreška prilikom brisanja posla: " + exc.CompleteExceptionMessage();
                    TempData[Constants.ErrorOccurred] = true;

                    logger.LogError("Pogreška prilikom brisanja posla: " + exc.CompleteExceptionMessage());
                }
            }
            else
            {
                logger.LogWarning("Ne postoji posao s oznakom: {0} ", Id);
                TempData[Constants.Message] = "Ne postoji posao s oznakom: " + Id;
                TempData[Constants.ErrorOccurred] = true;
            }
            return RedirectToAction(nameof(Index), new { page = page, sort = sort, ascending = ascending });
        }

        [HttpGet]
        public IActionResult Edit(int id, int page = 1, int sort = 1, bool ascending = true)
        {
            PrepareDropDownLists();
            var p= ctx.Posao.AsNoTracking().Where(d => d.Id == id).SingleOrDefault();
            var z = ctx.ZaposlenikPosao.AsNoTracking().Where(z => z.PosaoId == id).Select(z=>z.ZaposlenikId).ToArray();
            var o = ctx.PosaoOprema.AsNoTracking().Where(z => z.PosaoId == id).Select(z=>z.OpremaId).ToArray();
            var so = ctx.PosaoOprema.AsNoTracking().Where(z => z.PosaoId == id).Select(z => z.Satnica).FirstOrDefault();
            var sz = ctx.ZaposlenikPosao.AsNoTracking().Where(z => z.PosaoId == id).Select(z => z.Satnica).FirstOrDefault();
            PosaoViewModel posao = new PosaoViewModel()
            {
                Id = p.Id,
                Vrijeme = p.Vrijeme,
                MjestoId = p.MjestoId,
                UslugaId = p.UslugaId,
                Troskovi = p.Troskovi,
                Cijena = p.Cijena,
                ZaposlenikId = z,
                OpremaId=o,
                SatnicaZaposlenika = sz,
                SatnicaOpreme = so,

            };
            if (posao == null)
            {
                logger.LogWarning("Ne postoji posao s oznakom: {0} ", id);
                return NotFound("Ne postoji posao s oznakom: " + id);
            }
            else
            {
                ViewBag.Page = page;
                ViewBag.Sort = sort;
                ViewBag.Ascending = ascending;
                return View(posao);
            }
        }
       
        public IActionResult Row(int id)
        {
            var z = ctx.ZaposlenikPosao.AsNoTracking().Where(z => z.PosaoId == id).Select(z => z.ZaposlenikId).ToArray();
            var o = ctx.PosaoOprema.AsNoTracking().Where(z => z.PosaoId == id).Select(z => z.OpremaId).ToArray();
            var posao = ctx.Posao
                             .Where(p => p.Id == id)
                             .Select(p => new PosaoViewModel
                             {
                                 Id = p.Id,
                                 Vrijeme = p.Vrijeme,
                                 UslugaNaziv = p.UslugaNavigation.Naziv,
                                 MjestoNaziv = p.MjestoNavigation.Naziv,
                                 Cijena = p.Cijena,
                                 Troskovi = p.Troskovi,
                                 OpremaId=o,
                                 ZaposlenikId=z,
                             })
                             .SingleOrDefault();
            if (posao != null)
            {
                return PartialView(posao);
            }
            else
            {
                //vratiti prazan sadržaj?
                return NoContent();
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p = await ctx.ZaposlenikPosao
                .Include(z => z.Posao)
                .Include(z => z.ZaposlenikNavigation)
                .Where(z => z.Posao.Id == id).FirstOrDefaultAsync();
            var z = ctx.ZaposlenikPosao.AsNoTracking().Where(z => z.PosaoId == id).Select(z => z.ZaposlenikId).ToArray();
            var o = ctx.PosaoOprema.AsNoTracking().Where(z => z.PosaoId == id).Select(z => z.OpremaId).ToArray();
            var so = ctx.PosaoOprema.AsNoTracking().Where(z => z.PosaoId == id).Select(z => z.Satnica).FirstOrDefault();
            var sz = ctx.ZaposlenikPosao.AsNoTracking().Where(z => z.PosaoId == id).Select(z => z.Satnica).FirstOrDefault();
            var posao = new PosaoViewModel()
            {
                
                Id = p.PosaoId,
                Vrijeme = p.Posao.Vrijeme,
                MjestoId=p.Posao.MjestoId,
                UslugaId=p.Posao.UslugaId,
                UslugaNaziv = p.Posao.UslugaNavigation.Naziv,
                MjestoNaziv = p.Posao.MjestoNavigation.Naziv,
                Cijena = p.Posao.Cijena,
                Troskovi = p.Posao.Troskovi,
                SatnicaZaposlenika=sz,
                SatnicaOpreme=so,
                OpremaId = o,
                ZaposlenikId = z,
            };

            return View(posao);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PosaoViewModel posao, int page = 1, int sort = 1, bool ascending = true)
        {
            if (posao == null)
            {
                return NotFound("Nema poslanih podataka");
            }
            bool checkId = ctx.Posao.Any(m => m.Id == posao.Id);
            if (!checkId)
            {
                return NotFound($"Neispravan id mjesta: {posao?.Id}");
            }

            PrepareDropDownLists();
            if (ModelState.IsValid)
            {
                try
                {
                    Posao po = new Posao()
                    {
                        Id=posao.Id,
                        UslugaId = posao.UslugaId,
                        Vrijeme = posao.Vrijeme,
                        Cijena = posao.Cijena,
                        Troskovi = posao.Troskovi,
                        MjestoId = posao.MjestoId,
           
                    };
                    ctx.Update(po);
  
                    for (int i = 0; i < posao.ZaposlenikId.Length; i++)
                    {

                        ZaposlenikPosao zp = new ZaposlenikPosao()
                        {
                            
                            Posao = po,
                            ZaposlenikId = posao.ZaposlenikId[i],
                            Satnica = posao.SatnicaZaposlenika,


                        };
                        ctx.ZaposlenikPosao.Add(zp);

                    }
                    for (int i = 0; i < posao.OpremaId.Length; i++)
                    {
                        PosaoOprema op = new PosaoOprema()
                        {
                            Posao = po,
                            OpremaId = posao.OpremaId[i],
                            Satnica = posao.SatnicaOpreme,

                        };
                        ctx.Add(op);

                    }




                    ctx.SaveChanges();

                    TempData[Constants.Message] = "Posao ažuriran.";
                    TempData[Constants.ErrorOccurred] = false;
                    return RedirectToAction(nameof(Index), new { page, sort, ascending });
                }
                catch (Exception exc)
                {
                    ModelState.AddModelError(string.Empty, exc.CompleteExceptionMessage());
                    return View(posao);
                }
            }
            else
            {
                return View(posao);
            }
        }

    }
}


    
