using OzoMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OzoMvc.ViewModels
{
    public class PVIewModel
    {
        public IEnumerable<PView> Poslovi { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
