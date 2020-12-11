using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ChickensAPI.Entities;
using Microsoft.EntityFrameworkCore.SqlServer;
using ChickensAPI.Rep;

namespace ChickensAPI.Rep
{
    public class EggsRep
    {
        ApplicationContext Context;
        public IEnumerable<Egg> Get()
        {
            return Context.Eggs;
        }
        public IEnumerable<Egg> Get(User userfromreq)
        {
            var Eggs = from egg in Context.Eggs where egg.UserId == userfromreq.Id select egg;
            return Eggs;
        }
        public Egg Get(int Id)
        {
            return Context.Eggs.Find(Id);
        }
        public EggsRep(ApplicationContext context)
        {
            Context = context;
        }
        public void Create(Egg egg)
        {
            Context.Eggs.Add(egg);
            Context.SaveChanges();
        }
        public Egg Create(int id)
        {
            Chicken chicken = Context.Chickens.Find(id);
            Egg egg = chicken.CreateChild();
            Context.Eggs.Add(egg);
            Context.SaveChanges();
            return egg;
        }
        public void Update(Egg updatedEgg)
        {
            Egg currentItem = Get(updatedEgg.Id);
            Context.Eggs.Update(currentItem);
            Context.SaveChanges();
        }

        public Egg Delete(int Id)
        {
            Egg egg = Context.Eggs.Find(Id);

            if (egg != null)
            {
                Context.Eggs.Remove(egg);
                Context.SaveChanges();
            }

            return egg;
        }
    }
}
