using BL.Facades;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApp.Models.ListView;
using WebApp.Models.Edit;
using BL.DTO;
using BL.DTO.Filters;

namespace WebApp.Controllers
{
    [Authorize]
    public class PersonController : BaseController
    {
        private readonly PersonFacade personFacade;
        private readonly ApplicationUserFacade applicationUserFacade;
        private readonly NotificationFacade notificationFacade;
        
        public PersonController(PersonFacade personFacade, ApplicationUserFacade applicationUserFacade, NotificationFacade notificationFacade)
        {
            this.personFacade = personFacade;
            this.applicationUserFacade = applicationUserFacade;
            this.notificationFacade = notificationFacade;
        }

        public ActionResult Account(string userName)
        {
            notificationFacade.DeleteStaleNotifications();
            var prs = personFacade.GetPeople(new PersonFilter { Email = userName }).First();
            var personEditViewModel = new PersonEditViewModel()
                {
                    Person = personFacade.GetPeople(new PersonFilter { Email = userName }).First(),
                    Notifications = notificationFacade
                        .GetNotifications(new NotificationFilter { PersonRecipient = prs.Id, Seen = false })
                        .OrderBy(y => y.CreationTime).ToList()
                };

            personEditViewModel.Notifications.ForEach(y => notificationFacade.MarkAsSeen(y.Id));

            notificationFacade.DeleteByIds(personEditViewModel.Notifications.Select(y => y.Id).ToArray());
            return View(personEditViewModel);
        }

        public ActionResult People(char? sortOrder)
        {
            PersonViewModel personViewModel = new PersonViewModel();
            personViewModel.People = personFacade.GetAllPeople();
            switch (sortOrder)
            {
                case 'e':
                    personViewModel.People = personViewModel.People.OrderBy(y => y.Email);
                    break;
                case 't':
                    personViewModel.People = personViewModel.People.OrderByDescending(y => y.IsAdmin);
                    break;
                default:
                    personViewModel.People = personViewModel.People.OrderBy(y => y.Name);
                    break;
            }
            ViewBag.CurrentPerson = personFacade
                .GetPeople(new PersonFilter { Email = User.Identity.Name }).First();
            return View(personViewModel);
        }

        public ActionResult Edit(int id)
        {
            //  Catch any intruders
            var currentUser = personFacade.GetPeople(new PersonFilter { Email = User.Identity.Name }).First();
            if (!(currentUser.IsAdmin))
            {
                return RedirectToAction("People");
            }
            
            ViewBag.CurrentPerson = currentUser;

            return View(new PersonEditViewModel()
                { Person = personFacade.GetPersonById(id) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonEditViewModel model)
        {
            var currentUser = personFacade.GetPeople(new PersonFilter { Email = User.Identity.Name }).First();
            if (!(currentUser.IsAdmin))
            {
                return RedirectToAction("People");
            }
            if (ModelState.IsValid)
            {
                personFacade.UpdatePerson(model.Person);
                return RedirectToAction("People");
            }
            ViewBag.CurrentPerson = currentUser;
            return View(model);
        }
        
        public ActionResult Details(int id)
        {
            ViewBag.CurrentPerson = personFacade
                .GetPeople(new PersonFilter { Email = User.Identity.Name }).First();

            return View(new PersonEditViewModel()
                { Person = personFacade.GetPersonById(id) });
        }

        public ActionResult Delete(int id)
        {
            applicationUserFacade.DeleteByEmail(personFacade.GetPersonById(id).Email);
            personFacade.DeleteById(id);
            return RedirectToAction("People");
        }
    }
}