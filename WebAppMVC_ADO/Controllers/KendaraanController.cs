using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVC_ADO.DAL;
using WebAppMVC_ADO.Models;

namespace WebAppMVC_ADO.Controllers
{
    public class KendaraanController : Controller
    {
        Kendaraan_DAL _kendaraanDAL = new Kendaraan_DAL();

        // GET: Kendaraan
        public ActionResult Index()
        {
            var kendaraanList = _kendaraanDAL.GetKendaraans();

            if (kendaraanList.Count == 0)
            {
                TempData["InfoMessage"] = "Database Tabel Kendaraan Masih Kosong";
            }

            return View(kendaraanList);
        }

        // GET: Kendaraan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kendaraan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kendaraan/Create
        [HttpPost]
        public ActionResult Create(Kendaraan_Model kendaraan)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _kendaraanDAL.InsertKendaraan(kendaraan);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Tabel Kendaraan Detail sudah Tersimpam !";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Tabel Kendaraan Gagal Tersimpan !";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return View();
            }

        }

        // GET: Kendaraan/Edit/5
        public ActionResult Edit(int id)
        {
            var kendaraans = _kendaraanDAL.GetKendaraanID(id).FirstOrDefault();

            if (kendaraans == null)
            {
                TempData["InfoMessage"] = "Database Tabel Kendaraan Tdk ada dgn ID " + id.ToString();

                return RedirectToAction("Index");
            }

            return View(kendaraans);
        }

        // POST: Kendaraan/Edit/5
        [HttpPost]
        public ActionResult Edit(Kendaraan_Model kendaraan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdate = _kendaraanDAL.UpdateKendaraan(kendaraan);

                    if (IsUpdate)
                    {
                        TempData["SuccessMessage"] = "Tabel Kendaraan Detail sudah TerUpdate !";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Tabel Kendaraan Gagal TerUpdate !";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return View();
            }
        }

        // GET: Kendaraan/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var kendaraans = _kendaraanDAL.GetKendaraanID(id).FirstOrDefault();

                if (kendaraans == null)
                {
                    TempData["InfoMessage"] = "Database Tabel Kendaraan Tdk ada dgn ID " + id.ToString();

                    return RedirectToAction("Index");
                }

                return View(kendaraans);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return View();
            }
        }

        // POST: Kendaraan/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _kendaraanDAL.DeleteKendaraan(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                return View();
            }
        }
    }
}
