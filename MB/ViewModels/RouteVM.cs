using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using MB.Data;

namespace MB.ViewModels
{
    public class RouteVM
    {
        public Route Route { get; set; }
        public IEnumerable<SelectListItem> TDDAddress_departure { get; set; }
        public IEnumerable<SelectListItem> TDDAddress_arrival { get; set; }
    }
}
