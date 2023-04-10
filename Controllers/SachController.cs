using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class SachController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: Sach
        public ActionResult Index()
        {
            return View();
        }
        //List danh sach :
       /** public ActionResult DanhsachSach()
        {
            return View(db.Saches.ToList());
        }
       */
        public PartialViewResult SachMoiPartial()
        {
            var ListSachMoi = db.Saches.Take(3).ToList();
            return PartialView(ListSachMoi);
        }
        public ViewResult XemChiTiet(int Ma = 0)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == Ma);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
    }
}