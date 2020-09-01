using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNetCore.Http;
using SimpleCstructor.Models;

namespace SimpleCstructor.Controllers
{
	public class HomeController : Controller
	{

		private Cstructor db = new Cstructor();

		// GET: Home/Classlist
		public ActionResult Classlist()
		{
			return View(db.Classes.ToList());
		}


		// GET: Home/StudentClasses
		public ActionResult StudentClasses()
		{
			if (Session["User"] == null)
			{ 
				return  RedirectToAction("LogOn", new { returnUrl = "~/Home/StudentClasses" });
			}

			var tempUser = (User)Session["User"];
			return View("Classlist", db.Classes.Where(t => t.Users
							.Any(x => x.UserId == tempUser.UserId))
							.ToList());
		}

		// GET: Home/Register
		public ActionResult Register()
		{
			return View();
		}

		// POST: Home/Register
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register([Bind(Include = "UserId,UserEmail,UserPassword,UserIsAdmin")] User user)
		{
			if (ModelState.IsValid)
			{
				db.Users.Add(user);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(user);
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult LogOn()
		{
			return View();
		}

		[HttpPost]
		public ActionResult LogOn(LogOnModel loginModel, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (loginModel.UserEmail != null)
				{
					
					User user = db.Users.Where(t => t.UserEmail == loginModel.UserEmail
									&& t.UserPassword == loginModel.UserPassword).FirstOrDefault();
					if (user == null)
					{
						ModelState.AddModelError("loginFailure", "Invalid user login");
						return View();
					}

					Session["User"] = user;
					System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserEmail, loginModel.RememberMe);
					
					if (returnUrl == null)
					{
						return Redirect("~/");
					}
					else
					{
						return Redirect(returnUrl);
					}
				}
				else
				{
					ModelState.AddModelError("", "The user name or password provided is incorrect.");
				}
			}

			return View(loginModel);
		}

		// GET: Home/StudentClasses
		public ActionResult LogOut()
		{
			Session["User"] = null;
			return Redirect("~/");
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult EnrollInClass()
		{
			if (Session["User"] == null)
			{
				return RedirectToAction("LogOn", new { returnUrl = "~/Home/EnrollInClass" });
			}
			return View(db.Classes.ToList());
			//TODO - write querry to filter list where you eliminate 
			//classes that have already been enrolled
			//return View(db.Classes.Where(t => t.Users
			//	.Any(x => x.UserId == tempUser.UserId))
			//	.ToList());
		}

		// POST: Home/Enroll
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EnrollInClass(int EnrollInClass)
		{
			if (ModelState.IsValid)
			{
				var tempUserID = ((User)Session["User"]).UserId;
				db.Users.Find(tempUserID).Classes.Add(db.Classes.Find(EnrollInClass));
				db.SaveChanges();
				return RedirectToAction("StudentClasses");
			}

			return View("EnrollInClass");
		}
	}
}