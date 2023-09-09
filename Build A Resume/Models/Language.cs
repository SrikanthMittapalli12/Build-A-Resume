using System;
using System.Collections.Generic;

#nullable disable

namespace Build_A_Resume.Models
{
    public partial class Language
    {
        public int Id { get; set; }
        public int? PersonalInfoId { get; set; }
        public string LanguageName { get; set; }
        public string ProficiencyLevel { get; set; }

        public virtual PersonalInformation PersonalInfo { get; set; }
    }
}
