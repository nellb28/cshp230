using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactsService.Models;
using System.Collections;
using System.Security.Cryptography;

namespace ContactsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        public static List<Contact> contacts = new List<Contact>(); //TODO xfer into a dictionary
        public static int currentId = 101;

        // GET: api/Contacts
        [HttpGet]
        public IEnumerable Get()
        {
            return contacts;
        }

        // GET: api/Contacts/5
        [HttpGet("{id}", Name = "Get")]
        public Contact Get(int id)
        {
            var temp = contacts.FirstOrDefault(x => x.Id == id);
            return temp;
        }

        // POST: api/Contacts
        [HttpPost]
        public void Post([FromBody] Contact value)
        {
            //TODO wrap in try /catch
            value.Id = currentId;
            value.DateAdded = value.DateModified = DateTime.Now;

            value.Password = saltPassword(value.Password);

            contacts.Add(value);
            currentId++;
        }

		// PUT: api/Contacts/5
		[HttpPut("{id}")]
        public void Put(int id, [FromBody] Contact value)
        {
            var temp = contacts.FirstOrDefault(x => x.Id == id);
            if (temp != null)
            {
                temp.Email = value.Email;
                temp.Password = value.Password;
                temp.DateModified = DateTime.Now;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var temp = contacts.FirstOrDefault(x => x.Id == id);
            temp = null;
        }


        private string saltPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
