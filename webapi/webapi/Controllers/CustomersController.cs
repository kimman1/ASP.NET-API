using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using webapi;
using webapi.Models;

namespace webapi.Controllers
{
    public class CustomersController : ApiController
    {
        private ShopMVCEntities db = new ShopMVCEntities();

        // GET: api/Customers
        public List<CustomerViewModel> GetCustomers()
        {
            List<CustomerViewModel> listCus = new List<CustomerViewModel>();
            foreach (Customer c in db.Customers)
            {
                CustomerViewModel cvm = new CustomerViewModel();
                cvm.CustomerID = c.CustomerID;
                cvm.CustomerName = c.Name;
                cvm.CustomerAddress = c.Address;
                cvm.CustomerPhone = c.Phone;
                listCus.Add(cvm);
            }
            return listCus;
        }

        // GET: api/Customers/5
        [ResponseType(typeof(CustomerViewModel))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            CustomerViewModel cvm = new CustomerViewModel();
            cvm.CustomerID = customer.CustomerID;
            cvm.CustomerAddress = customer.Address;
            cvm.CustomerName = customer.Name;
            cvm.CustomerPhone = customer.Phone;
            return Ok(cvm);
        }
      
        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Customer cus = new Customer();
            cus.CustomerID = customer.CustomerID;
            cus.Name = customer.CustomerName;
            cus.Phone = customer.CustomerPhone;
            cus.Address = customer.CustomerAddress;
            db.Entry(cus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
              throw;  
            }

            return Ok();
        }

        // POST: api/Customers
        [ResponseType(typeof(CustomerViewModel))]
        public IHttpActionResult PostCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Customer cc = new Customer();
            cc.CustomerID = customer.CustomerID;
            cc.Address = customer.CustomerAddress;
            cc.Name = customer.CustomerName;
            cc.Phone = customer.CustomerPhone;
            db.Customers.Add(cc);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerID }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            ObjectParameter result = new ObjectParameter("outputresult",typeof(int));
            db.deleteCustomer(id, result);
            int status = Convert.ToInt32(result.Value);
            if (status != 0)
            {
                return Ok();
            }
            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,"Delete Failed"));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}