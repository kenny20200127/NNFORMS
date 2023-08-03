using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NNPEFWEB.ViewModel
{
    public class PayslipRequestModel
    {
        public string startMonth { get; set; }
        public string endMonth { get; set; }
        public string startYear { get; set; }
        public string endYear { get; set; }
        public List<SelectListItem> monthlist { get; set; }
        public List<SelectListItem> yearlist { get; set; }
        public List<PayslipViewModel> slipList { get; set; }

    }
}
