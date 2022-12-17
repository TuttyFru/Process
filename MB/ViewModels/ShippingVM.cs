using MB.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MB.ViewModels
{
    public class ShippingVM
    {
        public Shipping Shipping { get; set; }
        public IEnumerable<SelectListItem> TDDRoute { get; set; }
        public IEnumerable<SelectListItem> TDDDriver { get; set; }
        public IEnumerable<SelectListItem> TDDCargo { get; set; }
    }
}
