using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCVShop.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics.Contracts;
using System.ComponentModel.DataAnnotations;

namespace MCVShop.Controllers
{
    public class AdminsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();
        public static bool logged = false;
       
        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        
        private string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private bool IsValidAccount(string loginName, string password)
        {
            bool isValid = false;
            Admin admin = null;
            foreach(Admin user in db.Users)
            {
                if (user.loginName == loginName)
                {
                   admin = user;
                }
            }
           
            if (admin != null)
            {
                string cryptedPass = getMd5Hash(password);
                if (cryptedPass == admin.Parola)
                    isValid = true;
            }
            return isValid;
        }


        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.Admin user)
        {
            if(ModelState.IsValid)
            {
                if (IsValidAccount(user.loginName, user.Parola))
                {
                    logged = true;
                    Response.Redirect("../Admins/Index");
                    return null;
                }
            }

            Response.Redirect("../Admins/Login");
            return View();    
        }

        public ActionResult LogOff()
        {
            logged = false;
            return RedirectToAction("Index");
        }

        // GET: Admins/Details/5
        [Admin(Logged = true)]
        public ActionResult Details(int? id)
        {
            if (logged == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Users.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            else return  RedirectToAction("Index");
          //  return null;
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (logged == true)
            {
                return View();
            }
            else return RedirectToAction("Index");
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,loginName,Nume,Prenume,Parola,Logged")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.Parola = getMd5Hash(admin.Parola);
                db.Users.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
     
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (logged == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Users.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            else return RedirectToAction("Index");
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,loginName,Nume,Prenume,Parola,Logged")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (logged == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Users.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            else return RedirectToAction("Index");
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Users.Find(id);
            db.Users.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
