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
    public class ContactUsController : Controller
    {
        private readonly ResumeBuildContext _context;

        public ContactUsController(ResumeBuildContext context)
        {
            _context = context;
        }

        // GET: ContactUs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactUs.ToListAsync());
        }

        // GET: ContactUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs
                .FirstOrDefaultAsync(m => m.ContactUsId == id);
            if (contactU == null)
            {
                return NotFound();
            }

            return View(contactU);
        }

        // GET: ContactUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactUsId,FullName,Email,Message")] ContactU contactU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactU);
        }

        // GET: ContactUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs.FindAsync(id);
            if (contactU == null)
            {
                return NotFound();
            }
            return View(contactU);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactUsId,FullName,Email,Message")] ContactU contactU)
        {
            if (id != contactU.ContactUsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUExists(contactU.ContactUsId))
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
            return View(contactU);
        }

        // GET: ContactUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs
                .FirstOrDefaultAsync(m => m.ContactUsId == id);
            if (contactU == null)
            {
                return NotFound();
            }

            return View(contactU);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactU = await _context.ContactUs.FindAsync(id);
            _context.ContactUs.Remove(contactU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUExists(int id)
        {
            return _context.ContactUs.Any(e => e.ContactUsId == id);
        }
    }
}
