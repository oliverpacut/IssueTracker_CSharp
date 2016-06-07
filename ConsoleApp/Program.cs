using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Facades;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {

                Console.WriteLine();
                foreach (var item in db.Users)
                {
                    Console.WriteLine("{0}, {1}, {2}", item.Id, item.UserName, item.Email);
                }

                Console.WriteLine();
                foreach (var item in db.Comments)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}", item.Id, item.OwnerId, 
                        item.OwnerName, item.IssueId, item.Body, item.CreationTime);
                    //db.Comments.Remove(item);
                }

                Console.WriteLine();
                foreach (var item in db.Issues)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                        item.Id, item.ResponsiblePersonEmail, item.OwnerEmail, item.ProjectName, item.Name, 
                        item.Description, item.DateFiled, item.DateSolved, item.Type, item.State);
                }

                Console.WriteLine();
                foreach (var item in db.Notifications)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}", item.Id, item.PersonRecipientId, 
                        item.Body, item.Seen, item.CreationTime);
                }

                Console.WriteLine();
                foreach (var item in db.People)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", item.Id, item.Name, item.Email, item.IsAdmin);
                }

                Console.WriteLine();
                foreach(var item in db.Projects)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", item.Id, item.OwnerEmail, item.Name, item.Description);
                    //db.Projects.Remove(item);
                }
                db.SaveChanges();

                Console.ReadKey();
            }
        }
    }
}
