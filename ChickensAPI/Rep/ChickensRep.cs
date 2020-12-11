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

    public class ChickensRep 
    {
        ApplicationContext Context;
        
        public IEnumerable<Chicken> Get()
        {
            return Context.Chickens;
        }
        public IEnumerable<Chicken> Get(User userfromreq)
        {
            var chickens = from chicken in Context.Chickens where chicken.UserId == userfromreq.Id select chicken;
            return chickens;
        }
        public Chicken Get(int Id)
        {
            return Context.Chickens.Find(Id);
        }
        public ChickensRep(ApplicationContext context)
        {
            Context = context;
        }
        public Chicken Create(int id)
        {
            Egg egg = Context.Eggs.Find(id);
            if (egg == null) return null;
            Chicken chicken = egg.Born();
            Context.Chickens.Add(chicken);
            Context.Eggs.Remove(egg);
            Context.SaveChanges();
            return chicken;
        }
        public void Create(Chicken chicken)
        {
            Context.Chickens.Add(chicken);
            Context.SaveChanges();
        }

        public void Update(Chicken updatedTodoItem)
        {
            Chicken currentItem = Get(updatedTodoItem.Id);

            Context.Chickens.Update(currentItem);
            Context.SaveChanges();
        }

        public Chicken Delete(int Id)
        {
            Chicken todoItem = Get(Id);

            if (todoItem != null)
            {
                Context.Chickens.Remove(todoItem);
                Context.SaveChanges();
            }

            return todoItem;
        }
    }
}

