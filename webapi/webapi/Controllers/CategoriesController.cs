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
        public IHttpActionResult PutCategory(int id, CategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          
            Category cat = new Category();
            cat.CategoryID = category.CatID;
            cat.CategoryName = category.CatName;
            cat.Description = category.Description;
            db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();
           

            //return StatusCode(HttpStatusCode.NoContent);
            return Ok();
        }

        // POST: api/Categories
        [ResponseType(typeof(CategoryViewModel))]
        public IHttpActionResult PostCategory(CategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category cat = new Category();
            cat.CategoryName = category.CatName;
            cat.Description = category.Description;
            db.Categories.Add(cat);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cat.CategoryID }, category);
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