using System.Web.Mvc;
using ProteinTrackerRedisDemo.Models;
using ServiceStack.Redis;

namespace ProteinTrackerRedisDemo.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult NewUser()
        {
            return View();
        }

        public ActionResult Save(string userName, int goal, long? userId)
        {
            using (IRedisClient client = new RedisClient())
            {
                var userClient = client.GetTypedClient<User>();

                User user;
                if (userId != null)
                {
                    user = userClient.GetById(userId);
                    client.RemoveItemFromSortedSet("urn:leaderboard", user.Name);
                }
                else
                {
                    user = new User
                               {
                                   Id = userClient.GetNextSequence()
                               };
                }

                user.Name = userName;
                user.Goal = goal;
                userClient.Store(user);
                userId = user.Id;
                client.AddItemToSortedSet("urn:leaderboard", userName, user.Total);
            }

            return RedirectToAction("Index", "Tracker", new { userId });
        }

        public ActionResult Edit(int userid)
        {
            using (IRedisClient client = new RedisClient())
            {
                var userClient = client.GetTypedClient<User>();
                var user = userClient.GetById(userid);
                ViewBag.UserName = user.Name;
                ViewBag.Goal = user.Goal;
                ViewBag.UserId = user.Id;
            }

            return View("NewUser");
        }
    }
}