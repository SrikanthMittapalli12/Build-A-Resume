using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Build_A_Resume.Models;

namespace Build_A_Resume.Controllers
{
    public class WorkExperiencesController : Controller
    {
        private readonly ResumeBuildContext _context;

        public WorkExperiencesController(ResumeBuildContext context)
        {
            _context = context;
        }

        // GET: WorkExperiences
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Create", "Projects");

        }

        // GET: WorkExperiences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workExperience = await _context.WorkExperiences
                .Include(w => w.PersonalInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workExperience == null)
            {
                return NotFound();
            }

            return View(workExperience);
        }

        // GET: WorkExperiences/Create
        public IActionResult Create()
        {
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName");
            return View();
        }

        // POST: WorkExperiences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonalInfoId,Company,JobTitle,StartDate,EndDate,Description")] WorkExperience workExperience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workExperience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", workExperience.PersonalInfoId);
            return View(workExperience);
        }


        public ActionResult SubmitSkill(WorkExperience workExperience)
        {
            TempData["WorkExperience"] = workExperience;
            return RedirectToAction("Preview");
        }

        // GET: WorkExperiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workExperience = await _context.WorkExperiences.FindAsync(id);
            if (workExperience == null)
            {
                return NotFound();
            }
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", workExperience.PersonalInfoId);
            return View(workExperience);
        }

        // POST: WorkExperiences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonalInfoId,Company,JobTitle,StartDate,EndDate,Description")] WorkExperience workExperience)
        {
            if (id != workExperience.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workExperience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkExperienceExists(workExperience.Id))
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
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", workExperience.PersonalInfoId);
            return View(workExperience);
        }

        // GET: WorkExperiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workExperience = await _context.WorkExperiences
                .Include(w => w.PersonalInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workExperience == null)
            {
                return NotFound();
            }

            return View(workExperience);
        }

        // POST: WorkExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workExperience = await _context.WorkExperiences.FindAsync(id);
            _context.WorkExperiences.Remove(workExperience);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkExperienceExists(int id)
        {
            return _context.WorkExperiences.Any(e => e.Id == id);
        }
    }
}
