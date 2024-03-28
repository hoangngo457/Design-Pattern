using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vieon.Models;

namespace Vieon.Controllers
{
    public class MuaGoiVipController : Controller
    {
        // GET: MuaGoiVip

        private VieONEntities db = new VieONEntities();
        public ActionResult ThongTinGoi()
        {
            return View();
        }
        public ActionResult MuaGoi()
        {
            return View();
        }
        private double TinhTongTien_USD()
        {
            double TongTien = 69000/24650;
            return TongTien;
        }
        public ActionResult DangKiGoiVip(FormCollection form)
        {
            if (Session["TaiKhoan"] == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            try
            {
                ThanhToan tt = new ThanhToan();
                tt.ID_User = Convert.ToInt32(Session["ID"]);
                tt.NgayBatDau = DateTime.Now;
                tt.NgayKetThuc = DateTime.Now.AddMonths(1);
                
                db.ThanhToans.Add(tt);
                db.SaveChanges();
                int idUser = Convert.ToInt32(Session["ID"]);
                User us = db.Users.Find(idUser);
                if(us != null)
                {
                    us.RoleUser = "Vip";
                    db.Entry(us).State = EntityState.Modified;
                    db.SaveChanges();
                }
                RefreshUserData();
                Session["Permission"] = "A";
                return RedirectToAction("Success", "MuaGoiVip");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return Content("Lỗi thanh toán");
            }
        }
        private void RefreshUserData()
        {
            var idUser = Convert.ToInt32(Session["ID"]);
            var user = db.Users.FirstOrDefault(u => u.ID_User == idUser);

            if (user != null)
            {
                Session["Role"] = user.RoleUser;
            }
        }

        public ActionResult Success()
        {
            if (Session["Permission"] != "A")
            {
                return Content("Không có quyền truy cập");
            }
            Session["Permission"] = "";
            return View();
        }

        public ActionResult FailureView()
        {
            return View();
        }

        public ActionResult SuccessView()
        {
            return View();
        }
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            if (Session["TaiKhoan"] == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/MuaGoiVip/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            try
            {
                ThanhToan tt = new ThanhToan();
                tt.ID_User = Convert.ToInt32(Session["ID"]);
                tt.NgayBatDau = DateTime.Now;
                tt.NgayKetThuc = DateTime.Now.AddMonths(1);

                db.ThanhToans.Add(tt);
                db.SaveChanges();
                int idUser = Convert.ToInt32(Session["ID"]);
                User us = db.Users.Find(idUser);
                if (us != null)
                {
                    us.RoleUser = "Vip";
                    db.Entry(us).State = EntityState.Modified;
                    db.SaveChanges();
                }
                RefreshUserData();
                Session["Permission"] = "A";
                return RedirectToAction("Success", "MuaGoiVip");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return Content("Lỗi đặt hàng");
            }
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            var total = 69000/24650;
            
            /*foreach (var item in gioHang)
            {
                string name = (item.TenSach).ToString();
                string currency = "USD";
                int priceAsInt = (int)item.DonGia;
                string price = priceAsInt.ToString();
                string quantity = (item.SoLuong).ToString();
                string sku = (item.MaSach).ToString();
            }
            itemList.items.Add(new Item()
            {
                name = "Item Name comes here",
                currency = "USD",
                price = "1",
                quantity = "1",
                sku = "sku"
            });*/

            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            //Final amount with details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = TinhTongTien_USD().ToString()
            };
            /*var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = "2"
            };*/

            var amount = new Amount()
            {
                currency = "USD",
                total = TinhTongTien_USD().ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };

            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(), //Generate an Invoice No    
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
    }
}