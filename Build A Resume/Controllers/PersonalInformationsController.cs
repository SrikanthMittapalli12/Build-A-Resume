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
    public class PersonalInformationsController : Controller
    {
        private readonly ResumeBuildContext _context;

        public PersonalInformationsController(ResumeBuildContext context)
        {
            _context = context;
        }

        // GET: PersonalInformations
        public async Task<IActionResult> Index()
        {
            TempData["pi"] = JsonConvert.SerializeObject( JsonConvert.DeserializeObject(TempData["personalInformation"].ToString()));
            return RedirectToAction("Create", "Educations");

        }

        // GET: PersonalInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInformation = await _context.PersonalInformations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalInformation == null)
            {
                return NotFound();
            }

            return View(personalInformation);
        }

        // GET: PersonalInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonalInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Phone,Address,Summary")] PersonalInformation personalInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalInformation);
                await _context.SaveChangesAsync();
                TempData["personalInformation"] = JsonConvert.SerializeObject(personalInformation);
                return RedirectToAction(nameof(Index));
            }
           

            return View(personalInformation);
        }




        public ActionResult SubmitPersonalInfo(PersonalInformation personalInfo)
        {
            TempData["PersonalInfo"] = personalInfo;
            return RedirectToAction("Preview");
        }

        // GET: PersonalInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInformation = await _context.PersonalInformations.FindAsync(id);
            if (personalInformation == null)
            {
                return NotFound();
            }
            return RedirectToAction("Create", "Education", new { personalInfoId = personalInformation.Id });
        }

        // POST: PersonalInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Phone,Address,Summary")] PersonalInformation personalInformation)
        {
            if (id != personalInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalInformationExists(personalInformation.Id))
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
            return View(personalInformation);
        }

        // GET: PersonalInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalInformation = await _context.PersonalInformations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalInformation == null)
            {
                return NotFound();
            }

            return View(personalInformation);
        }

        // POST: PersonalInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalInformation = await _context.PersonalInformations.FindAsync(id);
            _context.PersonalInformations.Remove(personalInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalInformationExists(int id)
        {
            return _context.PersonalInformations.Any(e => e.Id == id);
        }
    }
}
