using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using intento1;
using intento1.Models;

namespace intento1.Controllers
{
    public class ProductosController : Controller
    {
        private intento1Entities db = new intento1Entities();

        public ActionResult AddCart(int id, CarritoCompra cc)
        {
            Productos p = db.Productos.Find(id);
            cc.Add(p);
            return RedirectToAction("Index");
        }

        public ActionResult AddOrder(CarritoCompra cc)
        {
            try
            {
                // Crear un nuevo pedido
                Pedidos pedido = new Pedidos
                {
                    email = "hola " + User.Identity.Name
                };

                // Agregar el pedido a la base de datos
                db.Pedidos.Add(pedido);

                // Guardar los cambios en la base de datos para obtener el ID del nuevo pedido
                db.SaveChanges();

                // Cargar los productos desde el contexto actual
                foreach (var producto in cc)
                {
                    // Cargar el producto desde el contexto actual
                    var productoEnContexto = db.Productos.Find(producto.Id);

                    // Verificar si el producto fue encontrado
                    if (productoEnContexto != null)
                    {
                        // Agregar el producto al pedido
                        pedido.Productos.Add(productoEnContexto);

                        productoEnContexto.Cantidad--;

                        if (productoEnContexto.Cantidad <= 2)
                        {
                            StockAlerts stockAlerts = db.StockAlerts.First();
                            stockAlerts.Productos.Add(db.Productos.Find(producto.Id));
                        }
                    }
                }

                // Guardar los cambios en la base de datos para asociar los productos al pedido
                db.SaveChanges();

                // Redirigir a la página de índice u otra página de destino
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Manejar excepciones, por ejemplo, redirigir a una página de error
                return RedirectToAction("Error");
            }
        }

        // GET: Productos
        public ActionResult Index()
        {
            var productos = db.Productos.Include(p => p.StockAlerts);
            return View(productos.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.StockAlert_Id = new SelectList(db.StockAlerts, "Id", "Id");
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cantidad,StockAlert_Id")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StockAlert_Id = new SelectList(db.StockAlerts, "Id", "Id", productos.StockAlert_Id);
            return View(productos);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockAlert_Id = new SelectList(db.StockAlerts, "Id", "Id", productos.StockAlert_Id);
            return View(productos);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cantidad,StockAlert_Id")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StockAlert_Id = new SelectList(db.StockAlerts, "Id", "Id", productos.StockAlert_Id);
            return View(productos);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productos productos = db.Productos.Find(id);
            db.Productos.Remove(productos);
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
