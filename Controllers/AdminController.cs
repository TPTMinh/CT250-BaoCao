using BookStore.Models;
using Microsoft.Win32.SafeHandles;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: Admin
        // Index Sach :
        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 10;

            return View(db.Saches.ToList().OrderBy(n => n.MaSach).ToPagedList(pageNum, pageSize));
        }
        [HttpGet]

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            //gán giá trị người dùng nhập liệu cho biến
            var tendn = collection["Username"];
            var matkhau = collection["Password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Yêu cầu nhập tên đăng nhập";
            }
            else if(String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Yêu cầu nhập mật khẩu";
            }
            else
            {
                //gán giá trị cho đối tượng được tạo mới (ad)
                Admin ad = db.Admins.SingleOrDefault(n => n.Username == tendn && n.Password == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index","Admin");
                }
                else
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        //Index DatHang :
        public ActionResult IndexDH(int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 10;

            return View(db.DonHangs.ToList().OrderBy(n => n.MaKH).ToPagedList(pageNum, pageSize));
        }
        //Index ChiTietDonHang :
        public ActionResult IndexCTDH(int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 10;

            return View(db.ChiTietDonHangs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(pageNum, pageSize));
        }

        // Thêm sách mới
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes.OrderBy(n => n.TenChuDe).ToList(), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.OrderBy(n => n.TenNXB).ToList(), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(Sach sach, HttpPostedFileBase fileUpload)
        {
            //đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes.OrderBy(n => n.TenChuDe).ToList(), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.OrderBy(n => n.TenNXB).ToList(), "MaNXB", "TenNXB");
            //kiểm tra đường dẫn file
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh";
                return View();
            }
            //Thêm vào CSDL
            
            if (ModelState.IsValid)
            {
                    //lưu tên file, lưu ý bổ sung thư viện using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //lưu đường dẫn của file
                    var path = Path.Combine(Server.MapPath("~/HinhAnhSP"), fileName);
                    //kiểm tra hình ảnh đã tồn tại chưa
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        //lưu hình ảnh vào đường dẫn
                        fileUpload.SaveAs(path);
                    }
                    sach.AnhBia = fileName;
                //Lưu vào CSDL
                db.Saches.Add(sach);
                db.SaveChanges();
            }
                return View();
        }
        //Xóa sách
        public ActionResult Xoa(int iMaSach)
        {
            var sach = db.Saches.Find(iMaSach);
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Chỉnh sửa sách
        [HttpGet]
        public ActionResult ChinhSua(int iMaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
                return HttpNotFound();
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList(), "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList(), "MaNXB", "TenNXB", sach.MaNXB);

            return View(sach);
        }
        [HttpPost]
        public ActionResult ChinhSua(Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList(), "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList(), "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }
        //hiển thị thông tin sách
        public ActionResult HienThi(int iMaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                return HttpNotFound();
            }

            return View(sach);
        }

    }
}