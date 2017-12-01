using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProteinTrackerRedisDemo.Models;
using ServiceStack.Redis;

namespace ProteinTrackerRedisDemo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            using (IRedisClient client = new RedisClient())
            {
                var userClient = client.GetTypedClient<User>();
                var users = userClient.GetAll();
                var usersSelection = new SelectList(users, "Id", "Name", String.Empty);
                ViewBag.UserId = usersSelection;
            }
            
            return View();
        }

    }
}
