using NNPEFWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class ShipReport
    {
        public List<ef_ship> commandlist { get; set; }
        public List<ef_ship> shiplist { get; set; }
    }
   
}
