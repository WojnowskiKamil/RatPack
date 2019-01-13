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
    public class MessagesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Messages
        public async Task<ActionResult> Index()
        {
            var messages = db.Messages.Include(m => m.Package);
            return View(await messages.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create(Guid packageId)
        {
            var message = new Message
            {
                Content = "Nowy wpis",
                CreatedDate = DateTime.Now,
                Package_Id = packageId
            };
            return View(message);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Content,CreatedDate,Package_Id")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.Id = Guid.NewGuid();
                message.CreatedBy = db.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                db.Messages.Add(message);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Packages", new { id = message.Package_Id });
            }

            ViewBag.Package_Id = new SelectList(db.Packages, "Id", "Description", message.Package_Id);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.Package_Id = new SelectList(db.Packages, "Id", "Description", message.Package_Id);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Content,CreatedDate,Package_Id")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Package_Id = new SelectList(db.Packages, "Id", "Description", message.Package_Id);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Message message = await db.Messages.FindAsync(id);
            db.Messages.Remove(message);
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
