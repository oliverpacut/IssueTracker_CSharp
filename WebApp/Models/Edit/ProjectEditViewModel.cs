using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTO;

namespace WebApp.Models.Edit
{
    public class ProjectEditViewModel
    {
        public ProjectDTO Project { get; set; }

        public string ProjectName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ProjectEditViewModel()
        {

        }
    }
}