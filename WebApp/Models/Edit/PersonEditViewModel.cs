using BL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.Edit
{
    public class PersonEditViewModel
    {
        public PersonDTO Person { get; set; }

        //[Required]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
        public string Name { get; set; }

        public int Id { get; set; }

        public string Email { get; set; }
        
        public bool IsAdmin { get; set; }

        public List<NotificationDTO> Notifications { get; set; }
        
        //public string Password { get; set; }

        //public string PasswordConfirmation { get; set; }

        public PersonEditViewModel()
        {

        }
    }
}