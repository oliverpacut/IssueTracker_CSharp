using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ListView
{
    public class IssueViewModel
    {
        public IEnumerable<IssueDTO> Issues { get; set; }

        public ProjectDTO Project { get; set; }

        public IssueViewModel()
        {
            Issues = new List<IssueDTO>();
        }
    }
}