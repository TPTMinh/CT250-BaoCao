﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BookStore.Models;
using PagedList;
using PagedList.Mvc;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace BookStore.Controllers
{
    public class QLDonHangController : Controller
    {
        // GET: QLDonHang
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        public ActionResult IndexDH(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.DonHangs.ToList().OrderBy(n=>n.MaDonHang).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Delete(int? MaDH)
        {
            //Lấy ra đối tượng sách theo mã 
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDH);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(dh);
        }
        [HttpPost]
        public ActionResult Delete(int MaDH)
        {
            //Xoa chi tiet don hang
            ChiTietDonHang ct = db.ChiTietDonHangs.SingleOrDefault(n => n.MaDonHang == MaDH);
            if (ct == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.ChiTietDonHangs.Remove(ct);
            db.SaveChanges();

            //Xoa don hang
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDH);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DonHangs.Remove(dh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? madh)
        {
            if (madh == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang book = db.DonHangs.SingleOrDefault(s => s.MaDonHang == madh);
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(int madh, FormCollection f)
        {
            var bookUpdate = db.DonHangs.Find(madh);
            if (TryUpdateModel(bookUpdate, "", new string[] { /*"NgayGiao", "NgayDat",*/ "DaThanhToan", "TinhTrangGiaoHang"/*, "MaKH"*/ }))
            {
                try
                {
                    db.Entry(bookUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult XemCTDH(int madh)
        {
            TempData["madh"] = madh;
            return Json(new { Url = Url.Action("XemCTDHPartial") });
        }
        public PartialViewResult XemCTDHPartial()
        {
            int madh = (int)TempData["madh"];
            var lstCTDH = db.ChiTietDonHangs.Where(n => n.MaDonHang == madh).ToList();
            return PartialView(lstCTDH);
        }
        public ActionResult Details(int madh)
        {
            DonHang kh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == madh);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
    }
}