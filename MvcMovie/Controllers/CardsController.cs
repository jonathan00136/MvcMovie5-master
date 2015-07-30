using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class CardsController : Controller
    {
        private CardDBContext db = new CardDBContext();

        // GET: Cards
        public ActionResult Index(string cardGenre, string searchString)
        {
            var GenreList = new List<string>();
            var GenreQry = from d in db.Cards
                           orderby d.Genre
                           select d.Genre;
            GenreList.AddRange(GenreQry.Distinct());
            ViewBag.cardGenre = new SelectList(GenreList);

            var cards = from m in db.Cards
                        select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                cards = cards.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(cardGenre))
            {
                cards = cards.Where(x => x.Genre == cardGenre);
            }

            return View(cards); 
        }

        // GET: Cards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // GET: Cards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,pic")] Card card , HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                  + file.FileName);
                   
                    string path = Server.MapPath("~/Images/"+ file.FileName);
                    card.pic = file.FileName;
                }
                
                db.Cards.Add(card);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(card);
        }

        // GET: Cards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,pic")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(card);
        }

        // GET: Cards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card card = db.Cards.Find(id);
            db.Cards.Remove(card);
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
