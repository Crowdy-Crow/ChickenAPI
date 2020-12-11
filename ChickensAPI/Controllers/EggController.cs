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
    public class EggController : Controller
    {
        EggsRep eggsRep;
        public EggController(EggsRep eggsRep)
        {
            this.eggsRep = eggsRep;
        }

        [HttpGet(Name = "GetAllEggs")]
        public IEnumerable<Egg> Get()
        {
            return eggsRep.Get();
        }

        [HttpGet("{id}", Name = "GetEggFromAll")]
        public IActionResult Get(int Id)
        {
            Egg egg = eggsRep.Get(Id);

            if (egg == null)
            {
                return NotFound();
            }

            return new ObjectResult(egg);
        }
        [HttpGet("FromUser", Name = "GetEggsFromUser")]
        public IEnumerable<Egg> Get(User userfromreq)
        {
            return eggsRep.Get(userfromreq);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Egg egg)
        {
            if (egg == null)
            {
                return BadRequest();
            }
            eggsRep.Create(egg);
            return CreatedAtRoute("GetEggFromAll", new { id = egg.Id }, egg);
        }
        [HttpPost("{id}")]
        public IActionResult Create(int id)
        {
            Egg egg = eggsRep.Create(id);
            return CreatedAtRoute("GetEggFromAll", new { id = egg.Id }, egg);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int Id, [FromBody] Egg updatedEgg)
        {
            if (updatedEgg == null || updatedEgg.Id != Id)
            {
                return BadRequest();
            }

            var todoItem = eggsRep.Get(Id);
            if (todoItem == null)
            {
                return NotFound();
            }

            eggsRep.Update(updatedEgg);
            return RedirectToRoute("GetEggFromAll", new { id = updatedEgg.Id }, updatedEgg.ToString());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var deletedTodoItem = eggsRep.Delete(Id);

            if (deletedTodoItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedTodoItem);
        }
    }
}
