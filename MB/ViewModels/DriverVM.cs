using MB.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MB.ViewModels
{
    public class DriverVM
    {
        public Driver Driver { get; set; }
        public IEnumerable<SelectListItem> TDDCar { get; set; }
    }
}
