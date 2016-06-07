using AutoMapper;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApp.Models.ListView;
using WebApp.Models.Edit;
using BL.DTO;
using DAL.Enums;
using System.Collections;
using BL.DTO.Filters;

namespace WebApp.Controllers
{
    [Authorize]
    public class IssueController : BaseController
    {
        private readonly IssueFacade issueFacade;
        private readonly ProjectFacade projectFacade;
        private readonly CommentFacade commentFacade;
        private readonly PersonFacade personFacade;
        private readonly NotificationFacade notificationFacade;

        public override void RegisterMapping(IConfiguration mapper)
        {
            base.RegisterMapping(mapper);
        }

        public IssueController(IssueFacade issueFacade, ProjectFacade projectFacade, CommentFacade commentFacade, PersonFacade personFacade, NotificationFacade notificationFacade)
        {
            this.issueFacade = issueFacade;
            this.projectFacade = projectFacade;
            this.commentFacade = commentFacade;
            this.personFacade = personFacade;
            this.notificationFacade = notificationFacade;
        }
        
        public ActionResult Issues(char? sortOrder, string projectName, string userName, string responsiblePerson)
        {
            IssueViewModel issueViewModel = new IssueViewModel();
            var myId = personFacade.GetPeople(new PersonFilter { Email = responsiblePerson }).First().Id;
            issueViewModel.Issues = issueFacade.GetAllIssues();

            if (!string.IsNullOrEmpty(responsiblePerson))
            {
                issueViewModel.Issues = issueViewModel.Issues.Where(y => y.ResponsiblePersonId == myId);
            }
            if (!string.IsNullOrEmpty(projectName))
            {
                issueViewModel.Issues = issueViewModel.Issues.Where(y => y.ProjectName == projectName);
            }
            if (!string.IsNullOrEmpty(userName))
            {
                issueViewModel.Issues = issueViewModel.Issues.Where(y => y.OwnerEmail == userName);
            }

            switch (sortOrder)
            {
                case 'o':
                    issueViewModel.Issues = issueViewModel.Issues.OrderBy(y => y.OwnerEmail);
                    break;
                case 'p':
                    issueViewModel.Issues = issueViewModel.Issues.OrderBy(y => y.ProjectName);
                    break;
                case 'n':
                    issueViewModel.Issues = issueViewModel.Issues.OrderBy(y => y.Name);
                    break;
                case 't':
                    issueViewModel.Issues = issueViewModel.Issues.OrderBy(y => y.Type);
                    break;
                case 's':
                    issueViewModel.Issues = issueViewModel.Issues.OrderBy(y => y.State);
                    break;
                default:
                    issueViewModel.Issues = issueViewModel.Issues.OrderByDescending(y => y.DateFiled);
                    break;
            }

            ViewBag.CurrentPerson = personFacade
                .GetPeople(new PersonFilter { Email = User.Identity.Name }).First();
            return View(issueViewModel);
        }

        public ActionResult Create()
        {
            var issueEditViewModel = new IssueEditViewModel()
            {
                Issue = new IssueDTO()
            };

            var projectNameList = projectFacade.GetProjects
                (new ProjectFilter { Owner = User.Identity.Name })
                .Select(y => y.Name).ToList();

            List<SelectListItem> sel = new List<SelectListItem>();

            foreach (string entry in projectNameList)
            {
                sel.Add(new SelectListItem{ Text = entry });
            }

            ViewBag.ListOfProjects = sel;
            ViewBag.CurrentPerson = personFacade.GetPeople
                (new PersonFilter { Email = User.Identity.Name }).First();
            return View(issueEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IssueEditViewModel model)
        {
            model.Issue.OwnerId = personFacade
                .GetPeople(new PersonFilter { Email = model.Issue.OwnerEmail })
                .First().Id;
            model.Issue.ProjectId = projectFacade
                .GetProjects(new ProjectFilter { Name = model.Issue.ProjectName })
                .First().Id;
            if (ModelState.IsValid)
            {
                model.Issue.State = IssueState.NEW;
                issueFacade.CreateIssue(model.Issue);
                return RedirectToAction("Issues");
            }

            var projectNameList = projectFacade.GetProjects
                (new ProjectFilter { Owner = User.Identity.Name })
                .Select(y => y.Name).ToList();

            List<SelectListItem> sel = new List<SelectListItem>();

            foreach (string entry in projectNameList)
            {
                sel.Add(new SelectListItem { Text = entry });
            }

            ViewBag.ListOfProjects = sel;
            ViewBag.CurrentPerson = personFacade.GetPeople
                (new PersonFilter { Email = User.Identity.Name }).First();
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            return View(new IssueEditViewModel()
                { Issue = issueFacade.GetIssueById(id)});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IssueEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Issue.State == IssueState.SOLVED)
                {
                    model.Issue.DateSolved = DateTime.Now;
                }
                notificationFacade.CreateNotification(new NotificationDTO
                {
                    PersonRecipientId = model.Issue.OwnerId,
                    Body = string.Format("{0} changed the state of your issue \"{1}\" to {2}",
                        personFacade.GetPeople(new PersonFilter
                            { Email = User.Identity.Name }).First().Name,
                        model.Issue.Name, 
                        model.Issue.State),
                    CreationTime = DateTime.Now,
                    Seen = false
                });
                issueFacade.UpdateIssue(model.Issue);
                return RedirectToAction("Issues");
            }
            return View(model);
        }

        public ActionResult Details(int id)
        {
            ViewBag.CurrentPerson = personFacade
                    .GetPeople(new PersonFilter { Email = User.Identity.Name }).First();
            return View(new IssueEditViewModel()
                            {
                                Issue = issueFacade.GetIssueById(id),
                                Comments = commentFacade.Get(new CommentFilter { IssueId = id })
                                                .OrderBy(y => y.CreationTime).ToList()
                            });
        }

        public ActionResult AddComment(int issueId)
        {
            var person = personFacade.GetPeople(new PersonFilter { Email = User.Identity.Name }).First();
            var commentEditViewModel = new CommentEditViewModel()
            {
                Comment = new CommentDTO() { OwnerName = person.Name,
                    IssueId = issueId, CreationTime = DateTime.Now, OwnerId = person.Id},
            };
            return View(commentEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                commentFacade.Create(model.Comment);

                //  Notifying the users was not an easy task. We managed to make the code as readable as possible,
                //  but it could be better. We hope the reader doesn't develop a hatred for spaghetti because
                //  of the following block. First, we get the issue object we added a comment to.
                var targetIssue = issueFacade.GetIssueById(model.Comment.IssueId);

                //  This dictionary will store all the notifications to be sent like so: <Target User ID, Notification Message>
                Dictionary<int, string> notificationMap = new Dictionary<int, string>();

                //  Add issue owner and responsible person to the map, but only if they are different and if the users exist.
                try
                {
                    notificationMap.Add(personFacade.GetPersonById(targetIssue.OwnerId).Id, 
                        string.Format("{0} commented on your issue \"{1}\".", 
                                        model.Comment.OwnerName, 
                                        targetIssue.Name));
                } catch (Exception)
                {
                }

                try {
                    if ((!notificationMap.ContainsKey(personFacade.GetPersonById(targetIssue.ResponsiblePersonId).Id)))
                    {
                        notificationMap.Add(personFacade.GetPersonById(targetIssue.ResponsiblePersonId).Id, 
                            string.Format("{0} commented on an issue you are working on: \"{1}\".", 
                                            model.Comment.OwnerName, 
                                            targetIssue.Name));
                    }
                }
                catch (Exception)
                {
                }

                // Add all the prior commenters to the notificationMap
                var distinctCommenters = commentFacade.Get(new CommentFilter { IssueId = model.Comment.IssueId })
                    .Where(y => !notificationMap.ContainsKey(y.OwnerId)).DistinctBy(y => y.OwnerId).Select(y => y.OwnerId).ToList();

                if (distinctCommenters.Count() > 0)
                {
                    distinctCommenters.ForEach(
                        y => notificationMap.Add(
                            y,
                            string.Format("{0} also commented on issue \"{1}\".", model.Comment.OwnerName,targetIssue.Name)));
                }

                //  Don't send a notification to the comment poster
                var currentUserId = personFacade.GetPeople(new PersonFilter { Email = User.Identity.Name }).First().Id;
                if (notificationMap.ContainsKey(currentUserId))
                {
                    notificationMap.Remove(currentUserId);
                }

                // generate notifications from the map
                notificationMap.ToList().ForEach(y => notificationFacade
                                .CreateNotification(new NotificationDTO
                                {
                                    PersonRecipientId = y.Key,
                                    Body = y.Value,
                                    CreationTime = DateTime.Now
                                }));

                return RedirectToAction("Details", new { id = model.Comment.IssueId });
            }
            return View(model);
        }

        public ActionResult DeleteComment(int id)
        {
            var issueId = commentFacade.GetById(id).IssueId;
            commentFacade.DeleteById(id);
            return RedirectToAction("Details", new { id = issueId });
        }


        public ActionResult Delete(int id)
        {
            issueFacade.DeleteById(id);
            return RedirectToAction("Issues");
        }

        public ActionResult IssueTakeOver(string adminName, int adminId, int issueId)
        {
            IssueDTO issue = issueFacade.GetIssueById(issueId);
            issue.ResponsiblePersonId = adminId;
            issue.ResponsiblePersonEmail = adminName;
            issueFacade.UpdateIssue(issue);
            return RedirectToAction("Details", new { id = issueId });
        }
    }
}