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
    }
}