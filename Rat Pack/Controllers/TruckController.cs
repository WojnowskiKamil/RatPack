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
    [Authorize(Roles = "Kierowca")]
    public class TruckController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Truck
        public async Task<ActionResult> Index()
        {
            return View(await db.Trucks.ToListAsync());
        }

        // GET: Truck/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = await db.Trucks.FindAsync(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // GET: Truck/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Truck/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CarBrand,CarName,CarType,HomeCity,Lenght,Width,Load,Height,AdditionalInfo,Location")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                truck.Id = Guid.NewGuid();
                db.Trucks.Add(truck);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(truck);
        }

        // GET: Truck/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = await db.Trucks.FindAsync(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // POST: Truck/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CarBrand,CarName,CarType,HomeCity,Lenght,Width,Load,Height,AdditionalInfo,Location")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(truck).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(truck);
        }

        // GET: Truck/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = await db.Trucks.FindAsync(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // POST: Truck/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Truck truck = await db.Trucks.FindAsync(id);
            db.Trucks.Remove(truck);
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
