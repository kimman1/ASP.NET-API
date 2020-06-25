using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace ShopAPI.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            List<Customer> cusList;
            HttpResponseMessage respone = GlobalVariable.WebApiClient.GetAsync("Customers").Result;
            cusList = respone.Content.ReadAsAsync<List<Customer>>().Result;
            return View(cusList);
        }
        public ActionResult Details(int id)
        {
            HttpResponseMessage respone = GlobalVariable.WebApiClient.GetAsync("Customers/" + id).Result;
            Customer c = respone.Content.ReadAsAsync<Customer>().Result;
            return View(c);
        }
        public ActionResult Delete(int? id)
        {
            HttpResponseMessage respone = GlobalVariable.WebApiClient.GetAsync("Customers/" + id).Result;
            if (respone.IsSuccessStatusCode)
            {
                Customer c = respone.Content.ReadAsAsync<Customer>().Result;
                return View(c);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            HttpResponseMessage respone = GlobalVariable.WebApiClient.DeleteAsync("Customers/" + id.ToString()).Result;
            if (respone.IsSuccessStatusCode)
            {
                TempData["status"] = "Delete Succeed";
                return RedirectToAction("Index");
            }
            else
            {
               TempData["status"] = "Delete Failed";
                return RedirectToAction("Index");
                
            }
            
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            HttpResponseMessage respone = GlobalVariable.WebApiClient.PostAsJsonAsync("Customers", customer).Result;
            TempData["status"] = "Save Successfully";
            return RedirectToAction("Index");
        }
    }
}