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
    public class LanguagesController : Controller
    {
        private readonly ResumeBuildContext _context;

        public LanguagesController(ResumeBuildContext context)
        {
            _context = context;
        }

        // GET: Languages
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Details", "Languages");
        }

        // GET: Languages/Details/5
        public async Task<IActionResult> Details()
        {
            var personalInfo = TempData["PersonalInfo"] as PersonalInformation;
            var educationData = TempData["Education"] as Education;
            var experienceData = TempData["WorkExperience"] as WorkExperience;
            var skillData = TempData["Skill"] as Skill;
            var projectData = TempData["Project"] as Project;
            var languageData = TempData["Language"] as Language;
            var certificationData = TempData["Certification"] as Certification;
            var awardData = TempData["AwardsAndHonor"] as AwardsAndHonor;

            // Combine data and pass it to the view
            var resumeData = new ResumeViewModel
            {
                PersonalInfo = personalInfo,
                Education = educationData,
                WorkExperience = experienceData,
                Skill = skillData,
                Project = projectData,
                Language = languageData,
                Certification = certificationData,
                AwardsAndHonor = awardData
            };

            return View(resumeData);
        }

        // GET: Languages/Create
        public IActionResult Create()
        {
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName");
            return View();
        }

        // POST: Languages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonalInfoId,LanguageName,ProficiencyLevel")] Language language)
        {
            if (ModelState.IsValid)
            {
                _context.Add(language);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", language.PersonalInfoId);
            return View(language);
        }

        public ActionResult SubmitSkill(Language language)
        {
            TempData["Language"] = language;
            return RedirectToAction("Preview");
        }

        // GET: Languages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", language.PersonalInfoId);
            return View(language);
        }

        // POST: Languages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonalInfoId,LanguageName,ProficiencyLevel")] Language language)
        {
            if (id != language.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(language.Id))
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
            ViewData["PersonalInfoId"] = new SelectList(_context.PersonalInformations, "Id", "FullName", language.PersonalInfoId);
            return View(language);
        }

        // GET: Languages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages
                .Include(l => l.PersonalInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var language = await _context.Languages.FindAsync(id);
            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanguageExists(int id)
        {
            return _context.Languages.Any(e => e.Id == id);
        }
    }
}
