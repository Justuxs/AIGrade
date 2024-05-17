using LLMEducation.Data;
using LLMEducation.Data.Entity;
using LLMEducation.Data.UIModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace LLMEducation.Repos
{
    public class UserRepo
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public UserRepo(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Save(User data)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                data.Email = data.Email.ToLower();
                if (context.Clients.Where(e=> e.Email == data.Email).Any())
                {
                    throw new InvalidDataException("Email is already registered");
                }
                await context.Clients.AddAsync(data);
                context.SaveChanges();
            }
        }

        public async Task<User?> GetUser(string email, string password)
        {
            email = email.ToLower();

            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Clients.Where(e => e.Email == email && password == e.Password).FirstOrDefaultAsync();
            }
        }
    }
}
