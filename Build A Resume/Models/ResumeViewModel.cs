using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Build_A_Resume.Models
{
    public class ResumeViewModel
    {
        public PersonalInformation PersonalInfo { get; set; }
        public Education Education { get; set; }
        public WorkExperience WorkExperience { get; set; }
        public Skill Skill { get; set; }
        public Project Project { get; set; }
        public Language Language { get; set; }
        public Certification Certification { get; set; }
        public AwardsAndHonor AwardsAndHonor { get; set; }
    }
}
