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

namespace ChickensAPI.Rep
{
    public class UsersRep
    {
        ApplicationContext Context;
        public IEnumerable<User> Get()
        {
            return Context.Users;
        }
        public User Get(int Id)
        {
            return Context.Users.Find(Id);
        }
        public UsersRep(ApplicationContext context)
        {
            Context = context;
        }
        public void Create(User item)
        {
            Context.Chickens.Add(item.FirstChicken());
            Context.Users.Add(item);
            Context.SaveChanges();
        }
        public void Update(User updatedTodoItem)
        {
            User currentItem = Get(updatedTodoItem.Id);

            Context.Users.Update(currentItem);
            Context.SaveChanges();
        }

        public User Delete(int Id)
        {
            User todoItem = Get(Id);

            if (todoItem != null)
            {
                Context.Users.Remove(todoItem);
                Context.SaveChanges();
            }

            return todoItem;
        }
    }
}
