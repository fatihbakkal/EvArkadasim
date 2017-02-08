using System.Linq;
using System.Web.Mvc;
using MaterialTemplate.Models;
using MaterialTemplate.Context;
using MaterialTemplate.CommonClasses;
using System;
using System.Data.Entity;
using PagedList;

namespace MaterialTemplate.Controllers
{
    public class HomeController : Controller
    {
        FmrContext db = new FmrContext();

        // GET: Home
        public ActionResult Index(int? pageNum)
        {
            var pageIndex = pageNum ?? 1;
            var model = db.Advert.ToList().ToPagedList(pageIndex, 6);

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult AllAdverts(SearchModel model)
        {
            ViewBag.Province = (from a in db.Advert
                                join p in db.Province
                                on a.ProvinceID equals p.ProvinceID
                                select p).Distinct();

            ViewBag.Categories = db.Property.Where(x => x.Property_Type == 1);


            if (!string.IsNullOrEmpty(model.SearchButton) || model.Page.HasValue)
            {
                var results = db.Advert
                    .Where(p => (p.ProvinceID == model.ProvinceID || model.ProvinceID == null)
                             && (p.DistrictID == model.DistrictID || model.DistrictID == null)
                             && (p.Advert_Category == model.CategoryID || model.CategoryID == null)
                             && (p.Advert_Price >= model.MinPrice)
                             && (p.Advert_Price <= model.MaxPrice))
                    .OrderBy(p => p.Advert_Price);

                var pageIndex = model.Page ?? 1;
                model.SearchResults = results.ToList().ToPagedList(pageIndex, 6);
            }
            else
            {
                model.MaxPrice = 3000;
                model.MinPrice = 0;
                var pageIndex = model.Page ?? 1;
                model.SearchResults = db.Advert.ToList().ToPagedList(pageIndex, 6);
            }
            return View(model);
        }

        [UserLoginControl]
        public ActionResult Profil()
        {
            var userId = CommonFunctions.GetUserId();

            var model = db.User.Where(x => x.UserID == userId).FirstOrDefault();
            return View(model);
        }

        public ActionResult MyAdverts()
        {
            var userId = CommonFunctions.GetUserId();

            var model = db.User.Where(x => x.UserID == userId).FirstOrDefault();

            return View(model);
        }

        public ActionResult Messages(int advertId)
        {
            var userId = CommonFunctions.GetUserId();
            var model = db.Advert.Where(x => x.AdvertID == advertId).FirstOrDefault();

            Message msgModel = new Message();
            msgModel.MessageReceiver = model.UserID;
            msgModel.MessageSender = userId;
            msgModel.UserReceiver = db.User.Where(x => x.UserID == model.UserID).FirstOrDefault();
            msgModel.MessageSubject = model.Advert_Title + "başlıklı ilan hakkında";


            return View(msgModel);
        }

        public ActionResult SendMessage()
        {
            return View();
        }

        public ActionResult Favorites()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RegisterForm(User user)
        {
            if (ModelState.IsValid)
            {
                var isExist = db.User.Any(x => x.User_Mail == user.User_Mail);

                if (!isExist)
                {
                    db.User.Add(user);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else if (isExist)
                {
                    TempData["error"] = "Bu email adresi ile kayıt mevcuttur.";
                }
            }

            return View("Register", user);
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var userData = db.User.Where(x => x.User_Username == user.User_Username).Where(x => x.User_Password == user.User_Password).FirstOrDefault();

            if (userData != null)
            {
                Session["User"] = userData.UserID + "|" + userData.User_Name + " " + userData.User_Surname;

                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }

        public PartialViewResult PartialUserPanel()
        {
            var userId = CommonFunctions.GetUserId();
            var model = db.User.Where(x => x.UserID == userId).FirstOrDefault();
            return PartialView("PartialUserPanel", model);
        }

        //[UserLoginControl]
        public ActionResult PostAd()
        {
            ViewBag.Provinces = db.Province;
            ViewBag.Categories = db.Property.Where(x => x.Property_Type == 1);
            ViewBag.HeatingTypes = db.Property.Where(x => x.Property_Type == 2);
            ViewBag.Features = db.Feature.ToList();

            return View();
        }

        public ActionResult EditAd(int advertId)
        {
            ViewBag.Provinces = db.Province;
            ViewBag.Categories = db.Property.Where(x => x.Property_Type == 1);
            ViewBag.HeatingTypes = db.Property.Where(x => x.Property_Type == 2);
            ViewBag.Features = db.Feature.ToList();

            var model = db.Advert.Where(x => x.AdvertID == advertId).FirstOrDefault();

            return View(model);
        }

        public ActionResult DeleteAd(int advertId)
        {
            Advert advert = db.Advert.Where(x => x.AdvertID == advertId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Advert.Remove(advert);
                db.SaveChanges();

                return Json(new { success = true, JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { success = false, JsonRequestBehavior.AllowGet });
            }
        }

        public ActionResult AdvertDetail(int advertId)
        {
            var model = db.Advert.Where(x => x.AdvertID == advertId).FirstOrDefault();
            ViewBag.Properties = db.Property.ToList();
            return View(model);
        }

        public JsonResult GetDistrictsById(int province, int? search)
        {
            if (search != null)
            {
                var districts = (from a in db.Advert
                                 join d in db.District
                                 on a.DistrictID equals d.DistrictID
                                 where a.ProvinceID == province
                                 select d).Distinct();
                return Json(districts, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var districts = db.District.Where(m => m.ProvinceID == province).OrderBy(m => m.District_Name);
                return Json(districts, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateAdvert(Advert advert, string checkBox)
        {
            if (ModelState.IsValid)
            {
                if (checkBox != "")
                {
                    var features = Array.ConvertAll(checkBox.Split(','), int.Parse);

                    foreach (var featureId in features)
                    {
                        advert.AdvertFeature.Add(new AdvertFeature { AdvertID = advert.AdvertID, FeatureID = featureId });
                    }
                }

                advert.UserID = CommonFunctions.GetUserId();

                db.Advert.Add(advert);
                db.SaveChanges();

                return Json(new { success = true, JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { success = false, JsonRequestBehavior.AllowGet });
            }
        }

        [HttpPost]
        public ActionResult UpdateAdvert(Advert advert, string checkBox)
        {
            if (ModelState.IsValid)
            {

                advert.DT_MODIFIED = DateTime.Now;
                advert.UserID = CommonFunctions.GetUserId();
                db.Entry(advert).State = EntityState.Modified;

                var properties = db.AdvertFeature.Where(x => x.AdvertID == advert.AdvertID).ToList();


                if (checkBox != "")
                {
                    var features = Array.ConvertAll(checkBox.Split(','), int.Parse);

                    db.AdvertFeature.Where(x => x.AdvertID == advert.AdvertID).ToList().ForEach(x => db.Entry(x).State = EntityState.Deleted);

                    foreach (var featureId in features)
                    {
                        db.AdvertFeature.Add(new AdvertFeature { AdvertID = advert.AdvertID, FeatureID = featureId });
                    }
                }


                db.SaveChanges();

                return Json(new { success = true, JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { success = false, JsonRequestBehavior.AllowGet });
            }
        }

        public ActionResult UpdateProfil(User user)
        {
            var dbUser = db.User.Where(x => x.UserID == user.UserID).FirstOrDefault();

            dbUser.User_Name = user.User_Name;
            dbUser.User_Surname = user.User_Surname;
            dbUser.User_Phone = user.User_Phone;
            dbUser.User_About = user.User_About;
            dbUser.User_Mail = user.User_Mail;
            dbUser.User_Title = user.User_Title;


            db.Entry(dbUser).State = EntityState.Modified;
            db.SaveChanges();

            TempData["success"] = "Profiliniz güncellenmiştir.";

            return View("Profil", user);
        }

        public ActionResult ChangePassword(FormCollection frm)
        {
            var UserID = Convert.ToInt32(frm["UserID"]);
            var oldPass = frm["oldPass"];
            var newPass = frm["newPass"];
            var newPassR = frm["newPassR"];

            var dbUser = db.User.Where(x => x.UserID == UserID).FirstOrDefault();

            if (oldPass == dbUser.User_Password && newPass == newPassR)
            {
                dbUser.User_Password = newPass;
                db.Entry(dbUser).State = EntityState.Modified;
                db.SaveChanges();

                TempData["pwSuccess"] = "Şifreniz değiştirilmiştir.";
            }
            else
            {
                TempData["pwError"] = "Şifre değiştirme işlemi başarısız!";
            }

            return View("Profil", dbUser);
        }
    }
}