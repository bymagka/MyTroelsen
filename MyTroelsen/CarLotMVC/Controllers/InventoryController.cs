using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoLotDAL.EF;
using AutoLotDAL.Models;
using AutoLotDAL.Repos;
using System.Data.Entity.Infrastructure;

namespace CarLotMVC.Controllers
{
    public class InventoryController : Controller
    {
        private InventoryRepo db = new InventoryRepo();

        // GET: Inventory
        public ActionResult Index()
        {
            return View(db.GetAll());
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.GetOne(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Make,Color,PetName")] Inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);
            try
            {
                db.Add(inventory);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");

                return View(inventory);
            }

            return RedirectToAction("Index");
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.GetOne(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Make,Color,PetName,Timestamp")] Inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);

            try
            {
                db.Save(inventory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty, $"Entry was already edited");
                return View(inventory);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Can't edit the entry : {ex.Message}");
                return View(inventory);
            }
            return RedirectToAction("Index");
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.GetOne(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            else db.Delete(inventory);
            return View(inventory);
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "id,Timestamp")] Inventory car)
        {
            try
            {
                db.Delete(car);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty, $"Entry was already edited");
                return View(car);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Can't delete the entry : {ex.Message}");
                return View(car);
            }
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
