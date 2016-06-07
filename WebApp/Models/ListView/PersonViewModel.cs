using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ListView
{
    public class PersonViewModel
    {
        public IEnumerable<PersonDTO> People { get; set; }

        public PersonViewModel()
        {
            People = new List<PersonDTO>();
        }
    }
}