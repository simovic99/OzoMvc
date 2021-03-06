using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OzoMvc.Models;
using Microsoft.Extensions.Options;
using OzoMvc.Controllers.AutoComplete;


namespace OzoMvc.Controllers.AutoComplete
{
    [Route("autocomplete/[controller]")]
    public class PonudjivacController : Controller
    {
        private readonly PI05Context ctx;
        private readonly AppSettings appSettings;

        public PonudjivacController(PI05Context ctx, IOptionsSnapshot<AppSettings> options)
        {
            this.ctx = ctx;
            appSettings = options.Value;
        }
        
        [HttpGet]      
        public IEnumerable<IdLabel> Get(string term)
        {
            var query = ctx.Ponudjivaci
                            .Select(p => new IdLabel
                            {
                                Id = p.Id,
                                Label = p.Naziv
                            })
                            .Where(l => l.Label.Contains(term));
          
            var list = query.OrderBy(l => l.Label)
                            .ThenBy(l => l.Id)
                            .Take(appSettings.AutoCompleteCount)
                            .ToList();           
            return list;
        }       
    }
}