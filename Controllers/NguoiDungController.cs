using System;
using BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class NguoiDungController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        // Get DangKi 
        public ActionResult DangKi()
        {
            return View();
        }

        //Get thong tin nguoi dung 
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public ActionResult DangKi(FormCollection collection,KhachHang kh)
        {//neu hop le thy cho them vao
            /* if (ModelState.IsValid)
             {
                 db.KhachHangs.Add(kh);
                 db.SaveChanges();
             }

             return View();
            */
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var gioitinh = collection["Gioitinh"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Tên đăng nhập không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Mật khẩu không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Nhập lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được để trống";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi7"] = "Điện thoại không được để trống";
            }
            else 
            {
                //Gán giá trị cho đối tượng được tạo mới (kh)
                kh.HoTen = hoten;
                kh.TaiKhoan = tendn;
                kh.MatKhau = matkhau;
                kh.Email = email;
                kh.DiaChi = diachi;
                kh.DienThoai = dienthoai;
                kh.GioiTinh = gioitinh;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.DangKi();
        }

        // Get Dang Nhap
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

         [HttpPost]
         public ActionResult DangNhap(FormCollection collection)
         {
            /* string tk = f["txtTaiKhoan"].ToString();
             string mk = f["txtMatKhau"].ToString();
             if (tk == "" || mk == "")
             {
                 ViewBag.ThongBao = "Bạn Chưa Nhập Tài Khoản Hoặc Mật Khẩu.";
                 return View();
             }

             KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == tk && n.MatKhau == mk);
             if (kh != null)
             {
                 ViewBag.ThongBao = "Chúc Mừng Bạn Đã Đăng Nhập Thành Công.";
                 Session["TaiKhoan"] = kh;
                 return RedirectToAction("Index", "Home");
             }

             ViewBag.ThongBao = "Tên Tài Khoản Hoặc Mật Khẩu Không Đúng.";
             return View
            */
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Yêu cầu nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Yêu cầu nhập mật khẩu";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (kh)
                KhachHang kh = db.KhachHangs.SingleOrDefault(n=>n.TaiKhoan == tendn && n.MatKhau == matkhau);
                if (kh != null)
                {
                    //ViewBag.ThongBao = "Đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index","Home");
                }
                else
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu chưa đúng";             
                }
            return View();

         }




    }
}