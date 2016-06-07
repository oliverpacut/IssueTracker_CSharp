using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BL.DTO;
using WebApp.Models.Edit;
using WebApp.Models.ListView;
using BL.Facades;
using AutoMapper;
using BL.DTO.Filters;

namespace WebApp.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly ProjectFacade projectFacade;
        private readonly IssueFacade issueFacade;
        private readonly CommentFacade commentFacade;
        private readonly PersonFacade personFacade;

        public override void RegisterMapping(IConfiguration mapper)
        {
            base.RegisterMapping(mapper);
        }

        public ProjectController(ProjectFacade prFac, IssueFacade iFac, CommentFacade cFac, PersonFacade pFac)
        {
            projectFacade = prFac;
            issueFacade = iFac;
            commentFacade = cFac;
            personFacade = pFac;
        }

        public ActionResult Projects(char? sortOrder, string userName)
        {
            ProjectViewModel projectViewModel = new ProjectViewModel();
            projectViewModel.Projects = projectFacade.GetProjects(new ProjectFilter { Owner = userName });

            switch (sortOrder)
            {
                case 'n':
                    projectViewModel.Projects = projectViewModel.Projects.OrderBy(y => y.Name);
                    break;
                case 'o':
                    projectViewModel.Projects = projectViewModel.Projects.OrderBy(y => y.OwnerEmail);
                    break;
                default:
                    projectViewModel.Projects = projectViewModel.Projects.OrderBy(y => y.Name);
                    break;
            }

            ViewBag.CurrentPerson = personFacade
                 .GetPeople(new PersonFilter { Email = User.Identity.Name }).First();
            return View(projectViewModel);
        }

        public ActionResult Create()
        {
            ViewBag.UserId = personFacade
                .GetPeople(new PersonFilter { Email = User.Identity.Name })
                .First().Id;
            var model = new ProjectEditViewModel() { Project = new ProjectDTO() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                projectFacade.CreateProject(model.Project);
                return RedirectToAction("Projects");
            }
            ViewBag.UserName = User.Identity.Name;
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            return View(new ProjectEditViewModel()
                { Project = projectFacade.GetProjectById(id) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                projectFacade.UpdateProject(model.Project);
                return RedirectToAction("Projects");
            }
            return View(model);
        }

        public ActionResult Details(int id)
        {
            ViewBag.CurrentPerson = personFacade
                    .GetPeople(new PersonFilter { Email = User.Identity.Name }).First();
            return View(new ProjectEditViewModel()
                                { Project = projectFacade.GetProjectById(id) });
        }

        public ActionResult Delete(int id)
        {
            foreach(IssueDTO issue in issueFacade.GetIssues(new IssueFilter { ProjectId = id }))
            {
                commentFacade.DeleteIssueComments(issue.Id);
                issueFacade.DeleteById(issue.Id);
            }
            projectFacade.DeleteById(id);
            return RedirectToAction("Projects");
        }
    }
}