using Build_A_Resume.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Build_A_Resume.Controllers
{
    public class PreviewController : Controller
    {
        // GET: PreviewController
        public ActionResult Details()
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
                Language= languageData,
                Certification= certificationData,
                AwardsAndHonor= awardData
            };

            return View(resumeData);
        }

        //// GET: PreviewController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        
    }
}
