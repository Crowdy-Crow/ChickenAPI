using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChickensAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Chicken chicken;
        public List<Egg> eggs = new List<Egg>();
        public Chicken FirstChicken()
        {
            return new Chicken() { Mass = 3, UserId = Id, user = this };
        }
    }
}
