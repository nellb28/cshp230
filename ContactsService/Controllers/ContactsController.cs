using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactsService.Models;
using System.Collections;

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
    }
}
