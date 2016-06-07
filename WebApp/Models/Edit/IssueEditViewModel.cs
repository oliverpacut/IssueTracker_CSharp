using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Enums;

namespace WebApp.Models.Edit
{
    public class IssueEditViewModel
    {
        
        public ProjectDTO Project { get; set; }
        public string ProjectName { get; set; }
        
        public string Owner { get; set; }

        public string Name { get; set; }

        public IssueDTO Issue { get; set; }

        public string Description { get; set; }

        public List<CommentDTO> Comments { get; set; }

        public IssueType Type { get; set; }

        public IssueState State { get; set; }

        public IssueEditViewModel()
        {
            //Issue = new IssueDTO();
        }
    }
}