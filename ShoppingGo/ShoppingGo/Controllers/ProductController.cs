using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingGo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            //宣告回傳商品列表result
            List<Models.Product> result = new List<Models.Product>();

            //使用Entities
            using (Models.ShoppingGoDataEntities db = new Models.ShoppingGoDataEntities())
            {
                //使用LinQ抓取目前Products資料庫的所有資料
                result = (from s in db.Product select s).ToList();

                //將result傳送給檢視
                return View(result);

            }
        }
        public ActionResult Creat()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Creat(Models.Product PostBackData)
        {
            //資料驗證
            if (this.ModelState.IsValid)
            {
                using (Models.ShoppingGoDataEntities db = new Models.ShoppingGoDataEntities())
                {
                    //將新增好的資料放置Product表單裡
                    db.Product.Add(PostBackData);

                    //儲存異動資料
                    db.SaveChanges();

                    //回傳成功訊息
                    TempData["ResultMessage"] = string.Format("商品[{0}]成功編輯", PostBackData.Name);

                    //回Index
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            using (Models.ShoppingGoDataEntities db = new Models.ShoppingGoDataEntities())
            {
                //抓取Id相符的資料
                var result = (from p in db.Product where p.Id == id select p).FirstOrDefault();
                if (result != default(Models.Product))
                {
                    return View(result);
                }
                else
                {
                    //如果沒資料則會上Index
                    TempData["ResultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Edit(Models.Product PostBackData)
        {
            using (Models.ShoppingGoDataEntities db = new Models.ShoppingGoDataEntities())
            {
                //抓取Id相符的資料
                var result = (from p in db.Product where p.Id == PostBackData.Id select p).FirstOrDefault();

                //儲存使用者更便資料
                result.Name = PostBackData.Name;
                result.Description = PostBackData.Description;
                result.CategoryId = PostBackData.CategoryId;
                result.Price = PostBackData.Price;
                result.PublishDate = PostBackData.PublishDate;
                result.Status = PostBackData.Status;
                result.DefaultImageId = PostBackData.DefaultImageId;
                result.Quantity = PostBackData.Quantity;

                db.SaveChanges();

                //回傳成功訊息
                TempData["ResultMessage"] = string.Format("商品[{0}]成功編輯", PostBackData.Id);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(Models.Product PostBackData)
        {
            using (Models.ShoppingGoDataEntities db = new Models.ShoppingGoDataEntities())
            {
                //抓取Id相符的資料
                var result = (from p in db.Product where p.Id == PostBackData.Id select p).FirstOrDefault();
                if (result != default(Models.Product))
                {
                    db.Product.Remove(result);

                    //儲存
                    db.SaveChanges();

                //回傳成功訊息
                TempData["ResultMessage"] = string.Format("商品[{0}]成功刪除", PostBackData.Id);
                return RedirectToAction("Index");
                }
                else
                {
                    //如果沒資料則會上Index
                    TempData["ResultMessage"] = "指定資料不存在，無法刪除，請重新操作";
                    return RedirectToAction("Index");
                }


            }
        }
    }
}