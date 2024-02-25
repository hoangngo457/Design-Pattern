using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Vieon.Models;

namespace Vieon.Controllers
{
    public class NguoiDungController : Controller
    {
        VieONVipProEntities db = new VieONVipProEntities();
        [HttpGet]
        public ActionResult DangNhap()
        {
            if (Session["TaiKhoan"] == "")
            {
                return View();
            }
            return RedirectToAction("index", "PhimKhachs");
        }
        [HttpPost]
        public ActionResult DangNhap(User us)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(us.SDT))
                {
                    ModelState.AddModelError(string.Empty, "Số điện thoại không được để trống");
                }
                if (string.IsNullOrEmpty(us.MatKhau))
                {
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                }
                if (ModelState.IsValid)
                {
                    var khach = db.Users.FirstOrDefault(k => k.SDT == us.SDT && k.MatKhau == us.MatKhau);


                    if (khach != null)
                    {
                        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                        Session["ID"] = khach.ID_User;
                        Session["TaiKhoan"] = khach;
                        Session["SDT"] = khach.SDT;
                        Session["HoTen"] = khach.HoTen;
                        Session["Email"] = khach.Email;
                        Session["Role"] = khach.RoleUser;
                        if(khach.RoleUser == "Admin")
                        {
                            return RedirectToAction("Index", "Phims");
                        }
                        else
                        {
                            return RedirectToAction("Index", "PhimKhachs");
                        }    
                        
                    }
                    else
                    {
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            if (Session["TaiKhoan"] == "")
            {
                return View();
            }
            return RedirectToAction("index", "Phims");
        }
        [HttpPost]
        public ActionResult DangKy(User us)
        {
            if (ModelState.IsValid)
            {
                us.RoleUser = "User";
                if (string.IsNullOrEmpty(us.HoTen))
                {
                    ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
                }
                if (string.IsNullOrEmpty(us.SDT))
                {
                    ModelState.AddModelError(string.Empty, "Số điện thoại không được để trống");
                }
                if (string.IsNullOrEmpty(us.MatKhau))
                {
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                }
                if (string.IsNullOrEmpty(us.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                }
                var khachhang = db.Users.FirstOrDefault(k => k.SDT == us.SDT);
                if (khachhang != null)
                {
                    ModelState.AddModelError(string.Empty, "Số điện thoại này đã được đăng kí");
                }
                if (ModelState.IsValid)
                {
                    us.RoleUser = "User";
                    db.Users.Add(us);
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("DangNhap");
        }
        public ActionResult DangXuat()
        {
            Session["ID"] = "";
            Session["TaiKhoan"] = "";
            Session["HoTen"] = "";
            Session["SDT"] = "";
            Session["Email"] = "";
            Session["Role"] = "";
            return RedirectToAction("index", "PhimKhachs");
        }
    }
}