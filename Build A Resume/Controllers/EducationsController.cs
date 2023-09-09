using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Build_A_Resume.Models;
using Newtonsoft.Json;

namespace Build_A_Resume.Controllers
{
    public class EducationsController : Controller
    {
        private readonly ResumeBuildContext _context;

        public EducationsController(ResumeBuildContext context)
        {
            _context = context;
        }

        // GET: Educations
        public async Task<IActionResult> Index(int? personalInfoId)
        {
            // return RedirectToAction("Create", "Skills");
            var education = _context.Educations.Where(e => e.PersonalInfoId == personalInfoId);
            return View(education.ToList());
        }

        // GET: Educations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Educations
                .Include(e => e.PersonalInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // GET: Educations/Create
        public IActionResult Create()
        {
           // var data = TempData["pi"];
            //ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName") ;
            ViewData["PersonalInfoId"] = new SelectList(JsonConvert.DeserializeObject(TempData["pi"]), "Id", "FullName");

            return View();
        }

       


        // POST: Educations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonalInfoId,Institution,Degree,Major,GraduationDate")] Education education)
        {
            if (ModelState.IsValid)
            {
                _context.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", education.PersonalInfoId);
            return View(education);
        }


        public ActionResult SubmitSkill(Education education)
        {
            TempData["Education"] = education;
            return RedirectToAction("Preview");
        }

        // GET: Educations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Educations.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", education.PersonalInfoId);
            return View(education);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonalInfoId,Institution,Degree,Major,GraduationDate")] Education education)
        {
            if (id != education.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(education);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(education.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", education.PersonalInfoId);
            return View(education);
        }

        // GET: Educations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Educations
                .Include(e => e.PersonalInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationExists(int id)
        {
            return _context.Educations.Any(e => e.Id == id);
        }
    }
}
