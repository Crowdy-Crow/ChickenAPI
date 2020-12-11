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
    public class ChickenController : Controller
    {
        ChickensRep chickenRep;

        public ChickenController(ChickensRep chickenrep)
        {
            this.chickenRep = chickenrep;
        }

        [HttpGet(Name = "GetAllChickens")]
        public IEnumerable<Chicken> Get()
        {
            return chickenRep.Get();
        }

        [HttpGet("{id}", Name = "GetChickenFromAll")]
        public IActionResult Get(int Id)
        {
            Chicken chicken = chickenRep.Get(Id);

            if (chicken == null)
            {
                return NotFound();
            }

            return new ObjectResult(chicken);
        }
        [HttpGet("FromUser", Name = "GetEggsFromUser")]
        public IEnumerable<Chicken> Get(User userfromreq)
        {
            return chickenRep.Get(userfromreq);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Chicken chicken)
        {
            if (chicken == null)
            {
                BadRequest();
            }
            chickenRep.Create(chicken);
            return CreatedAtRoute("GetChickenFromAll", new { id = chicken.Id }, chicken);
        }

        [HttpPost("{id}")]
        public IActionResult Create(int id)
        {
            Chicken chicken = chickenRep.Create(id);
            if (chicken==null)
            {
                BadRequest();
            }
            return CreatedAtRoute("GetChickenFromAll", new { id = chicken.Id}, chicken);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int Id, [FromBody] Chicken updatedChicken)
        {
            if (updatedChicken == null || updatedChicken.Id != Id)
            {
                return BadRequest();
            }

            var todoItem = chickenRep.Get(Id);
            if (todoItem == null)
            {
                return NotFound();
            }

            chickenRep.Update(updatedChicken);
            return RedirectToRoute("GetChickens");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var deletedTodoItem = chickenRep.Delete(Id);

            if (deletedTodoItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedTodoItem);
        }
    }
}
