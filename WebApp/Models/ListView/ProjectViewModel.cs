using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTO;

namespace WebApp.Models.ListView
{
    public class ProjectViewModel
    {
        public IEnumerable<ProjectDTO> Projects { get; set; }

        public ProjectViewModel()
        {
            Projects = new List<ProjectDTO>();
        }
    }
}