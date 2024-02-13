using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using intento1;

namespace intento1.Controllers
{
    public class StockAlertsController : Controller
    {
        private intento1Entities1 db = new intento1Entities1();

        // GET: StockAlerts
        public ActionResult Index()
        {
            return View(db.StockAlerts.ToList());
        }

        // GET: StockAlerts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockAlerts stockAlerts = db.StockAlerts.Find(id);
            if (stockAlerts == null)
            {
                return HttpNotFound();
            }
            //Añadido
            ICollection<Productos> productos = stockAlerts.Productos;
            //List<Productos> productos = stockAlerts.Productos.ToList();
            //return View(stockAlerts);
            return View(productos);
        }

        // GET: StockAlerts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockAlerts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] StockAlerts stockAlerts)
        {
            if (ModelState.IsValid)
            {
                db.StockAlerts.Add(stockAlerts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockAlerts);
        }

        // GET: StockAlerts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockAlerts stockAlerts = db.StockAlerts.Find(id);
            if (stockAlerts == null)
            {
                return HttpNotFound();
            }
            return View(stockAlerts);
        }

        // POST: StockAlerts/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] StockAlerts stockAlerts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockAlerts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockAlerts);
        }

        // GET: StockAlerts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockAlerts stockAlerts = db.StockAlerts.Find(id);
            if (stockAlerts == null)
            {
                return HttpNotFound();
            }
            return View(stockAlerts);
        }

        // POST: StockAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockAlerts stockAlerts = db.StockAlerts.Find(id);
            db.StockAlerts.Remove(stockAlerts);
            db.SaveChanges();
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
