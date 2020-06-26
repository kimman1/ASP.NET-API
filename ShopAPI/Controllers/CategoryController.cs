using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace ShopAPI.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Categories").Result;
            List<Category> listCat = response.Content.ReadAsAsync<List<Category>>().Result;
            return View(listCat);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Categories/" + id).Result;
           Category Cat = response.Content.ReadAsAsync<Category>().Result;
            return View(Cat);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category cat)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync<Category>("Categories", cat).Result;
            if (response.StatusCode != HttpStatusCode.BadRequest)
            {
                TempData["status"] = "Create Successfully";
                return RedirectToAction("Index");
            }
            TempData["status"] = "Create Fail";
            return RedirectToAction("Index");
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Categories/" + id.ToString()).Result;
            Category cat = response.Content.ReadAsAsync<Category>().Result;
            return View(cat);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit( Category cat)
        {

            HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("Categories/" +  cat.CatID.ToString(), cat).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["status"] = "Save Successfully";
                return RedirectToAction("Index");
            }
            TempData["status"] = "Save Error";
            return RedirectToAction("Index");
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Categories/" + id).Result;
            Category Cat = response.Content.ReadAsAsync<Category>().Result;
            return View(Cat);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.DeleteAsync("Categories/" + id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["status"] = "Delete Succeed";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["status"] = "Delete Fail";
                return RedirectToAction("Index");
            }
        }
    }
}
