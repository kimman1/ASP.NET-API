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
using webapi;
using webapi.Models;

namespace webapi.Controllers
{
    public class CategoriesController : ApiController
    {
        private ShopMVCEntities db = new ShopMVCEntities();

        // GET: api/Categories
        public List<CategoryViewModel> GetCategories()
        {
            List<CategoryViewModel> listCat = new List<CategoryViewModel>();
            foreach (Category c in db.Categories)
            {
                CategoryViewModel cvm = new CategoryViewModel();
                cvm.CatID = c.CategoryID;
                cvm.CatName = c.CategoryName;
                cvm.Description = c.Description;
                listCat.Add(cvm);
            }
            return listCat;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(CategoryViewModel))]
        public IHttpActionResult GetCategory(int id)
        {
            Category c = db.Categories.Find(id);
            if (c == null)
            {
                return NotFound();
            }
            CategoryViewModel cvm = new CategoryViewModel();
            cvm.CatID = c.CategoryID;
            cvm.CatName = c.CategoryName;
            cvm.Description = c.Description;
            return Ok(cvm);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.CategoryID }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            
            ObjectParameter result = new ObjectParameter("outputresult",typeof(int));
            db.deleteCategory(id,result);
            int status = Convert.ToInt32(result.Value);
            if (status != 0)
            {
                return Ok();
            }
            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "Delete Failed"));
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

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.CategoryID == id) > 0;
        }
    }
}