using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rat_Pack.Context;
using Rat_Pack.Models.Database;

namespace Rat_Pack.Controllers
{
    
    public class PackagesController : Controller
    {
        private DataContext db = new DataContext();

        [Authorize(Roles = "Klient")]
        public async Task<ActionResult> Index()
        {

            var packages = db.Packages.Include(p => p.Truck);
            return View(await packages.ToListAsync());
        }

        [Authorize(Roles = "Kierowca")]
        public async Task<ActionResult> Index2()
        {
            var packages = db.Packages.Include(p => p.Truck);
            return View(await packages.ToListAsync());
        }

        public async Task<ActionResult> MyAccount()
        {
            var currentUser = db.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var currentUserId = currentUser.Id;
            return View(await db.Packages.Where(p => p.CreatedBy.Id == currentUserId).ToListAsync());
        }

        public async Task<ActionResult> Details(Guid? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Package package= await db.Packages
                .Include(c => c.Messages)
                .Where(c => c.Id == id.Value).SingleOrDefaultAsync();
            if (package == null)
            {
                return HttpNotFound();
            }

            return View(package);
        }

        [Authorize(Roles = "Klient")]
        public ActionResult Create(Guid? id)
        {
            var c = db.Packages.SingleOrDefault(p =>  p.Id == id);
            ViewBag.Truck_Id = new SelectList(db.Trucks, "Id", "CarBrand");
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Klient")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Category,Description,CityStart,CityEnd,DateStart,DateEnd,Lenght,Width,Weight,Height,AdditionalInfo,Truck_Id")] Package package)
        {

            if (ModelState.IsValid)
            {
                package.CreatedBy = db.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                package.Id = Guid.NewGuid();
                db.Packages.Add(package);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.Truck_Id = new SelectList(db.Trucks, "Id", "CarBrand", package.Truck_Id);
            return View(package);
        }

        [Authorize(Roles = "Klient")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = await db.Packages.FindAsync(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            ViewBag.Truck_Id = new SelectList(db.Trucks, "Id", "CarBrand", package.Truck_Id);
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Category,Description,CityStart,CityEnd,DateStart,DateEnd,Lenght,Width,Weight,Height,AdditionalInfo,Truck_Id")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Truck_Id = new SelectList(db.Trucks, "Id", "CarBrand", package.Truck_Id);
            return View(package);
        }

        [Authorize(Roles = "Klient")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = await db.Packages.FindAsync(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Klient")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Package package = await db.Packages.FindAsync(id);
            db.Packages.Remove(package);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
