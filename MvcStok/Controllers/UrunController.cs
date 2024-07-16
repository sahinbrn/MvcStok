using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities1 db= new MvcDbStokEntities1();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle() 
        {
         List<SelectListItem> degerler =(from i in db.TBLKATEGORILER.ToList()
                                         select new SelectListItem
                                         {
                                             Text =i.KATEGORIAD,
                                             Value =i.KATEGORIID.ToString()
                                         }).ToList();
            ViewBag.dgr = degerler;             
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {
            var ktgr = db.TBLKATEGORILER.Where(m=> m.KATEGORIID==p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktgr;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL (int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urn = db.TBLURUNLER.Find(id);

			List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
											 select new SelectListItem
											 {
												 Text = i.KATEGORIAD,
												 Value = i.KATEGORIID.ToString()
											 }).ToList();
			ViewBag.dgr = degerler;

			return View("UrunGetir",urn);
        }
		public ActionResult Guncelle(TBLURUNLER p)
		{
			var urun = db.TBLURUNLER.Find(p.URUNID);
			urun.URUNAD = p.URUNAD;
			urun.MARKA = p.MARKA;
			urun.STOK = p.STOK;
			urun.FIYAT = p.FIYAT;

			var ktgr = db.TBLKATEGORILER.FirstOrDefault(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID);
			if (ktgr != null)
			{
				urun.TBLKATEGORILER = ktgr;
			}
			else
			{
				urun.TBLKATEGORILER = null; // veya uygun bir işlem
			}

			db.SaveChanges();
			return RedirectToAction("Index");
		}

	}
}