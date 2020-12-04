using System.Web.Mvc;
///===============================================================
/// <summary>
/// FileName : HomeController.cs
/// Description : Home 컨트롤러
/// Copyright : 2020 by PayLetter Inc. All rights reserved.
/// Author : mh_kim@payletter.com, 2020-11-02
/// Modify History : Just Created.
/// </summary>
///================================================================
namespace BillAdmin.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}