using CustomerViews.IServices;
using CustomerViews.Models;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CustomerViews.Helpers
{
    public class BagCartViewComponent : ViewComponent
    {
        private readonly IAllServices _services;
        public BagCartViewComponent(IAllServices services)
        {
            _services = services;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lstGHCT = await  _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
            BagCartViewModel bagCartView;
            if (lstGHCT==null||lstGHCT.Count==0)
            {                
                lstGHCT = null;
                return View(lstGHCT);
            }
            else
            {
                bagCartView = new BagCartViewModel()
                {
                    numberOfItems = lstGHCT.Sum(x => x.SoLuong)
                };
                return View(bagCartView);
            }
            
        }
    }
}
