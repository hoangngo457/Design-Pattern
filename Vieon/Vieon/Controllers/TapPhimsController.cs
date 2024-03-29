using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Vieon.Controllers.Observer;
using Vieon.Models;


namespace Vieon.Controllers
{
    public class TapPhimsController : Controller
    {
        //private VieONEntities db = new VieONEntities();
        VieONEntities db = DbContextSingleton.getInstance;

        private Subject subject;

        public TapPhimsController()
        {
            subject = new Subject();
        }

        // GET: TapPhims
        public ActionResult Index()
        {
            var tapPhims = db.TapPhims.Include(t => t.Phim);
            return View(tapPhims.ToList());
        }

        // GET: TapPhims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TapPhim tapPhim = db.TapPhims.Find(id);
            if (tapPhim == null)
            {
                return HttpNotFound();
            }
            return View(tapPhim);
        }

        // GET: TapPhims/Create
        public ActionResult Create()
        {
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim");
            return View();
        }

        // POST: TapPhims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TapPhim,ID_Phim,SoTap,TenTap,MoTa,ThoiLuong,NgayRaMat,UrlPhim")] TapPhim tapPhim)
        {
            if (ModelState.IsValid)
            {
                tapPhim.NgayRaMat = DateTime.Now;
                db.TapPhims.Add(tapPhim);
                db.SaveChanges();

                List<PhimYeuThich> phimYeuThichs = db.PhimYeuThiches.Where(p => p.ID_Phim == tapPhim.ID_Phim).ToList(); 
                foreach (var phimYeuThich in phimYeuThichs)
                {
                    subject.AddObserver(new EmailObserver("smtp.ethereal.email", 587, "lavada30@ethereal.email", "thEvScX5FccsNk6dgB"));
                    subject.NotifyObservers(phimYeuThich, tapPhim);
                }

                //List<PhimYeuThich> phimYeuThichs = db.PhimYeuThiches.Where(p => p.ID_Phim == tapPhim.ID_Phim).ToList();// Lấy danh sách các bộ phim yêu thích từ cơ sở dữ liệu
                //foreach (var item in phimYeuThichs)
                //{

                //    string recipientEmail = item.User.Email;
                //    string subject = "Thông báo tập phim mới";
                //    string body = $"Xin chào,\n\nBộ phim {item.Phim.TenPhim} mà bạn yêu thích đã có tập phim mới. Hãy truy cập vào trang web của chúng tôi để xem.\n\nTrân trọng,";

                //    SendEmail(recipientEmail, subject, body);
                //}
                return RedirectToAction("Details", "Phims", new { id = tapPhim.ID_Phim });
            }

            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", tapPhim.ID_Phim);
            return View(tapPhim);
        }

        public void SendEmail(string receiverEmail, string subject, string body)
        {
            string senderEmail = "lavada30@ethereal.email";

            // Tạo đối tượng MimeMessage
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Lavada Schuster", senderEmail));
            message.To.Add(new MailboxAddress("", receiverEmail)); // Địa chỉ email người nhận
            message.Subject = subject; // Tiêu đề email

            // Tạo phần thân của email (nội dung)
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = body; // Nội dung email
            message.Body = bodyBuilder.ToMessageBody(); // Thiết lập nội dung email

            // Thiết lập thông tin SMTP
            using (var smpt = new MailKit.Net.Smtp.SmtpClient())
            {
                smpt.Connect("smtp.ethereal.email", 587, false);
                smpt.Authenticate("lavada30@ethereal.email", "thEvScX5FccsNk6dgB");
                smpt.Send(message);

                smpt.Disconnect(true);
            }


        }

        // GET: TapPhims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TapPhim tapPhim = db.TapPhims.Find(id);
            if (tapPhim == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", tapPhim.ID_Phim);
            return View(tapPhim);
        }

        // POST: TapPhims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TapPhim,ID_Phim,SoTap,TenTap,MoTa,ThoiLuong,NgayRaMat,UrlPhim")] TapPhim tapPhim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tapPhim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Phims", new { id = tapPhim.ID_Phim });
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", tapPhim.ID_Phim);
            return View(tapPhim);
        }

        // GET: TapPhims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TapPhim tapPhim = db.TapPhims.Find(id);
            if (tapPhim == null)
            {
                return HttpNotFound();
            }
            return View(tapPhim);
        }

        // POST: TapPhims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TapPhim tapPhim = db.TapPhims.Find(id);
            int? idPhim = tapPhim.ID_Phim;
            db.TapPhims.Remove(tapPhim);
            db.SaveChanges();
            return RedirectToAction("Edit", "Phims", new { id = idPhim });
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
