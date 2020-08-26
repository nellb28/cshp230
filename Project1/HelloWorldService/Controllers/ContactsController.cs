using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorldService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authenticator]
    public class ContactsController : ControllerBase
    {
        public static List<Contact> contacts = new List<Contact>();
        public static int currentId = 101;

        // GET: api/<ContactsController>
        [HttpGet]
        public IActionResult Get()
        {
            //int x = 1;

            //x = x / (x - 1);

            return Ok(contacts);
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var contact = contacts.FirstOrDefault(t => t.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            return new OkObjectResult(contact);
        }

        [HttpGet]
        public IActionResult GetCustomers(int customerId)
        {
            var contact = contacts.FirstOrDefault(t => t.Id == customerId);

            if (contact == null)
            {
                return NotFound();
            }

            return new OkObjectResult(contact);
        }

        // POST api/<ContactsController>
        [HttpPost]
        public IActionResult Post([FromBody] Contact value)
        {
            if (value == null)
            {
                return new BadRequestResult();
            }

            value.Id = currentId++;
            value.DateAdded = DateTime.Now;

            contacts.Add(value);

            //var result = new { Id = value.Id, Candy = true };

            return CreatedAtAction(nameof(Get),
                new { id = value.Id },
                value);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Contact value)
        {
            var contact = contacts.FirstOrDefault(t => t.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.Id = id;
            contact.Name = value.Name;
            contact.Phones = value.Phones;

            return Ok(contact);
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contactsRemoved = contacts.RemoveAll(t => t.Id == id);

            if (contactsRemoved == 0)
            {
                return NotFound(); //404
            }

            return Ok(); //200
        }
    }
}
