using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_Przychodnia_TsWM.Models
{
    public class Event
    {
        public int EventID { get; set; }

        [Display(Name = "Temat")]
        public string Subject { get; set; }
        [Display(Name = "Opis wizyty")]
        public string Description { get; set; }
        [Display(Name = "Data wizyty")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string ThemeColor { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        [Display(Name = "Czas trwania (min)")]
        public double DateDiff => End.HasValue ? (End.Value - Start).TotalMinutes : 0;
        [NotMapped]
        public string PatientName { get; set; }
    }

    public class UserDetailsViewModel
    {
        public ApplicationUser user { get; set; }

        public List<Event> events { get; set; }
    }
}