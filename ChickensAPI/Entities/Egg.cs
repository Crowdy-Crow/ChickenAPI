using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChickensAPI.Entities
{
    public class Egg
    {
        public int Id { get; set; }
        public double Mass { get; set; }
        [Required]
        public int UserId { get; set; }
        public User user { get; set; }
       public Chicken Born()
        {
            return new Chicken() { Mass = this.Mass, UserId = this.UserId, user = this.user };
        }
    }
}
