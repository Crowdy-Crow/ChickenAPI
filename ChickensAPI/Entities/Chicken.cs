using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChickensAPI.Entities
{
    public class Chicken
    {
        public int Id { get; set; }
        public double Mass { get; set; }
        [Required]
        public int UserId { get; set; }
        public User user { get; set; }

        public Egg CreateChild()
        {
            Random random = new Random();
            double minMass = Mass * 0.85;
            double maxMass = Mass * 1.1;
            double bornMass = random.NextDouble() * (maxMass - minMass) + minMass;
            return new Egg() { Mass = bornMass, UserId = this.UserId, user = this.user };
        }

    }
}
