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
    public class AwardsAndHonorsController : Controller
    {
        private readonly ResumeBuildContext _context;

        public AwardsAndHonorsController(ResumeBuildContext context)
        {
            _context = context;
        }

        // GET: AwardsAndHonors
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Create", "Languages");

        }

        // GET: AwardsAndHonors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awardsAndHonor = await _context.AwardsAndHonors
                .Include(a => a.PersonalInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (awardsAndHonor == null)
            {
                return NotFound();
            }

            return View(awardsAndHonor);
        }

        // GET: AwardsAndHonors/Create
        public IActionResult Create()
        {
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName");
            return View();
        }

        // POST: AwardsAndHonors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonalInfoId,AwardName,AwardDate,AwardingOrganization")] AwardsAndHonor awardsAndHonor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(awardsAndHonor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", awardsAndHonor.PersonalInfoId);
            return View(awardsAndHonor);
        }


        public ActionResult SubmitSkill(AwardsAndHonor awardsAndHonor)
        {
            TempData["AwardsAndHonor"] = awardsAndHonor;
            return RedirectToAction("Preview");
        }

        // GET: AwardsAndHonors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awardsAndHonor = await _context.AwardsAndHonors.FindAsync(id);
            if (awardsAndHonor == null)
            {
                return NotFound();
            }
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", awardsAndHonor.PersonalInfoId);
            return View(awardsAndHonor);
        }

        // POST: AwardsAndHonors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonalInfoId,AwardName,AwardDate,AwardingOrganization")] AwardsAndHonor awardsAndHonor)
        {
            if (id != awardsAndHonor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(awardsAndHonor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AwardsAndHonorExists(awardsAndHonor.Id))
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
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", awardsAndHonor.PersonalInfoId);
            return View(awardsAndHonor);
        }

        // GET: AwardsAndHonors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awardsAndHonor = await _context.AwardsAndHonors
                .Include(a => a.PersonalInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (awardsAndHonor == null)
            {
                return NotFound();
            }

            return View(awardsAndHonor);
        }

        // POST: AwardsAndHonors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var awardsAndHonor = await _context.AwardsAndHonors.FindAsync(id);
            _context.AwardsAndHonors.Remove(awardsAndHonor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AwardsAndHonorExists(int id)
        {
            return _context.AwardsAndHonors.Any(e => e.Id == id);
        }
    }
}
