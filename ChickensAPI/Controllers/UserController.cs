using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChickensAPI.Entities;
using Microsoft.EntityFrameworkCore;
using ChickensAPI.Rep;

namespace ChickensAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        UsersRep usersRep;
        public UserController(UsersRep usersRep)
        {
            this.usersRep = usersRep;
        }

        [HttpGet(Name = "GetAllUsers")]
        public IEnumerable<User> Get()
        {
            return usersRep.Get();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int Id)
        {
            User user = usersRep.Get(Id);

            if (user == null)
            {
                return NotFound();
            }

            return new ObjectResult(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            usersRep.Create(user);
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int Id, [FromBody] User updatedUser)
        {
            if (updatedUser == null || updatedUser.Id != Id)
            {
                return BadRequest();
            }

            var todoItem = usersRep.Get(Id);
            if (todoItem == null)
            {
                return NotFound();
            }

            usersRep.Update(updatedUser);
            return RedirectToRoute("GetUsers");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var deletedTodoItem = usersRep.Delete(Id);

            if (deletedTodoItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedTodoItem);
        }
    }
}
