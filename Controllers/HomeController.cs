using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web_MehrTabellen.DAL;
using Web_MehrTabellen.DAL.Artikel;
using Web_MehrTabellen.Model;
using Web_MehrTabellen.Model.ViewModels;
using Web_TagHelper;


namespace Web_MehrTabellen.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArtikelRepository _artikelRepo;
        private readonly IKategorieRepository _kategorieRepo;

        public HomeController(IArtikelRepository artikelRepo, IKategorieRepository kategorieRepo)
        {
            _artikelRepo = artikelRepo;
            _kategorieRepo = kategorieRepo;
        }

       
        public IActionResult Index(int? kat)
        {
            var kategorien = _kategorieRepo.GetAllKategorien();
            var alleArtikel = _artikelRepo.GetAllArtikel();

            List<Artikels> artikel;
            int selectedKID = kat ?? 0;

            if (kat.HasValue && kat.Value > 0)
                artikel = alleArtikel.Where(a => a.KID == kat.Value).ToList();
            else
                artikel = alleArtikel;

            var vm = new HomeIndexViewModel
            {
                Artikel = artikel,
                Kategorien = kategorien,
                SelectedKID = selectedKID
            };

            return View(vm);
        }
        #region ARTIKEL CRUD

        [HttpGet]
        public IActionResult CreateArtikel()
        {
            ViewBag.Kategorien = _kategorieRepo.GetAllKategorien();
            return View("Insert_Art");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateArtikel(Artikels artikel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Kategorien = _kategorieRepo.GetAllKategorien();
                return View("Insert_Art", artikel);
            }

            _artikelRepo.InsertArtikel(artikel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EditArtikel(int id)
        {
            var artikel = _artikelRepo.GetArtikelById(id);
            if (artikel == null) return NotFound();

            ViewBag.Kategorien = _kategorieRepo.GetAllKategorien();
            return View("Update_Art", artikel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditArtikel(Artikels artikel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Kategorien = _kategorieRepo.GetAllKategorien();
                return View("Update_Art", artikel);
            }

            _artikelRepo.UpdateArtikel(artikel);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteArtikel(int id)
        {
            _artikelRepo.DeleteArtikel(id);
            return RedirectToAction(nameof(Index));
        }
#endregion
        #region KATEGORIE CRUD 

        [HttpGet]
        public IActionResult CreateKategorie()
        {
            return View("Insert_Kat");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateKategorie(Kategorien kategorie)
        {
            if (!ModelState.IsValid)
            {
                return View("Insert_Kat", kategorie);
            }

            _kategorieRepo.InsertKategorie(kategorie);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EditKategorie(int id)
        {
            var kat = _kategorieRepo.GetKategorieById(id);
            if (kat == null) return NotFound();
            return View("Update_Kat", kat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditKategorie(Kategorien kategorie)
        {
            if (!ModelState.IsValid)
            {
                return View("Update_Kat", kategorie);
            }

            _kategorieRepo.UpdateKategorie(kategorie);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteKategorie(int id)
        {
            _kategorieRepo.DeleteKategorie(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}


