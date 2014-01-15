using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebMatrix.WebData;
using filesDatabase.Filters;
using filesDatabase.Models;
using System.Data.SqlTypes;
using System.Net;

namespace filesDatabase.Controllers
{
    public class HomeController : UserController
    {
        private readonly FilesDatabaseClass1DataContext _db = new FilesDatabaseClass1DataContext();
        public string username;

        [Authorize]
        [InitializeSimpleMembership]
        public ActionResult Index()
        {
            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "Name", autoCreateTables: true);
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            UpdateLastSeenOn();

            return View(new List<userFile>());
        }
        
        

        [HttpPost]
        public ActionResult Search(string type, string str)
        {
                if (type == "userShare")
                {
                    List<User> users = new List<User>();

                    if (str == "")return PartialView("_userSearchShare", users);

                    users = _db.Users.Where(x => x.userName.Contains(str)).OrderBy(x => x.userName).ToList();

                    return PartialView("_userSearchShare", users);
                }

                if (type == "userSearch")
                {
                    List<User> allUsers = new List<User>();
                    List<SearchedUser> users = new List<SearchedUser>();

                    if (str == "") return PartialView("_userSearchTpl", users);

                    allUsers = _db.Users.Where(x => x.userName.Contains(str)).OrderBy(x => x.userName).ToList();

                    foreach (var user in allUsers)
                    {
                        var isFriend = _db.subscribers.Any(x => x.subscriberId == GetUserId() && x.subscribedToId == user.userID);
                        users.Add(new SearchedUser(user, isFriend));
                    }

                    return PartialView("_userSearchTpl", users);
                }

                return PartialView("_userSearchShare", new List<User>());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
