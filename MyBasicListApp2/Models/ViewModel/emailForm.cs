using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyBasicListApp2.Models.ViewModel
{
    public class emailForm
    {
        [Required, Display(Name = "First Name:")]
        public string emailfirstName { get; set; }

        [Required, Display(Name = "Last Name:")]
        public string emaillastName { get; set; }

        [Required, Display(Name = "Email Address:")]
        [EmailAddress]
        public string emailEmail { get; set; }

        [Required, Display(Name = "Subject")]
        public string emailsubject { get; set; }

        [Required, Display(Name = "Message")]
        [UIHint("MultilineText")]
        public string emailmessage { get; set; }
    }
}