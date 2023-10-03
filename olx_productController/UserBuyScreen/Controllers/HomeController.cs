using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserBuyScreen.Models;

namespace UserBuyScreen.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index() 
        { 
        return View();
        }
        public ActionResult Index1(int productCategoryId)
        {
            DataAccess access = new DataAccess();

            List<ModelMyAdvertise> advertises = access.GetCategoryById(productCategoryId);
            if (advertises != null && advertises.Count > 0)
            {
                return View(advertises);
            }



            return View();
        }
        public ActionResult ShowBySubCategory(int productSubCategoryId)
        {
            DataAccess dataAccess = new DataAccess();
            List<ModelMyAdvertise> list = dataAccess.GetProductBySubcategory(productSubCategoryId);
            if (list != null)
            {
                return View(list);
            }

            return View();

        }

        public ActionResult ShowByState(int stateId)
        {
            DataAccess dataAccess = new DataAccess();
            List<ModelMyAdvertise> list = dataAccess.GetProductByState(stateId);
            if (list != null)
            {
                return View(list);
            }

            return View();

        }

        public ActionResult ShowByCity(int cityId)
        {
            DataAccess dataAccess = new DataAccess();
            List<ModelMyAdvertise> citylist = dataAccess.GetProductByCity(cityId);
            if (citylist != null)
            {
                return View(citylist);
            }

            return View();
        }
        public ActionResult ShowByPrice(decimal min, decimal max)
        {
            DataAccess dataAccess = new DataAccess();
            List<ModelMyAdvertise> price = dataAccess.GetByPrice(min, max);
            if (price != null)
            {
                return View(price);
            }
            return View();
        }

        public ActionResult ShowByProducts()
        {
            DataAccess dataAccess = new DataAccess();
            IEnumerable<ModelAdvertiseImages> product = dataAccess.GetProducts();
           
            return View(product);
        }
        public ActionResult showAllCategory()
        {
            DataAccess dataAccess = new DataAccess();
            IEnumerable<ModelProductCategory> category = dataAccess.GetCategory();

            return View(category);
        }
        public ActionResult showAllSubCategory()
        {
            DataAccess dataAccess = new DataAccess();
            IEnumerable<ModelProductSubCategory> Subcategory = dataAccess.GetSubCategory();

            return View(Subcategory);
        }
    }
}