using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Vieon.Models;

namespace Vieon.Controllers
{
    public class PhimYeuThichesController : Controller
    {
        private VieONEntities db = new VieONEntities();

        // GET: PhimYeuThiches
        public ActionResult Index()
        {
            int? id = Session["ID"] as int?;


            // Kiểm tra xem có dữ liệu tài khoản không
            if (id.HasValue)
            {
                // Truy vấn cơ sở dữ liệu để lấy danh sách các phim yêu thích của người dùng dựa trên idUser
                var favoriteMovies = db.PhimYeuThiches
                    .Where(m => m.ID_User == id)
                    .Select(m => new Favorite
                    {
                        ID_Phim = m.Phim.ID_Phim,
                        TenPhim = m.Phim.TenPhim,
                        ID_PhimYeuThich = m.ID_PhimYeuThich
                    })
                    .ToList();

                // Trả về view và truyền danh sách phim yêu thích vào
                if (favoriteMovies.Count > 0)
                {
                    return View(favoriteMovies);
                }
                else
                {
                    return RedirectToAction("Empty", "PhimYeuThiches");
                }
                
            }
            else
            {
                return View();
            }
        }

        public ActionResult Empty()
        {
            return View();
        }

        public ActionResult Create(int id_phim)
        {
            if (Session["ID"] == "")
            {
                // Nếu không có session ID, chuyển hướng đến trang đăng nhập
                return RedirectToAction("DangNhap", "NguoiDung"); 
            }

            int id_user = Convert.ToInt32(Session["ID"]);

            // Tạo một đối tượng mới PhimYeuThich với id_phim và id_user
            PhimYeuThich phimYeuThich = new PhimYeuThich
            {
                ID_Phim = id_phim,
                ID_User = id_user
            };

            // Thêm đối tượng mới vào DbContext và lưu thay đổi
            db.PhimYeuThiches.Add(phimYeuThich);
            db.SaveChanges();

            return RedirectToAction("Details", "PhimKhachs", new { id = id_phim });
        }


        public ActionResult Delete(int id)
        {
            PhimYeuThich phimYeuThich = db.PhimYeuThiches.Find(id);
            if (phimYeuThich == null)
            {
                return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy đối tượng
            }
            db.PhimYeuThiches.Remove(phimYeuThich);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteInDetails(int id_phim, int id_user)
        {
            int id_Phim = Convert.ToInt32(RouteData.Values["id"]);
            // Tìm dòng dữ liệu PhimYeuThich dựa trên id_phim và id_user
            PhimYeuThich phimYeuThich = db.PhimYeuThiches.FirstOrDefault(p => p.ID_Phim == id_phim && p.ID_User == id_user);

            if (phimYeuThich == null)
            {
                return HttpNotFound(); // Trả về lỗi 404 nếu không tìm thấy dòng dữ liệu
            }

            // Xóa dòng dữ liệu PhimYeuThich
            db.PhimYeuThiches.Remove(phimYeuThich);
            db.SaveChanges();

            return RedirectToAction("Details", "PhimKhachs", new { id = id_Phim });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
