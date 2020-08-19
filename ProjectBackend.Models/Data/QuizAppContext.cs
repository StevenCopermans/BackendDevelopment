using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectBackend.Models.Data
{
    public partial class QuizAppContext : IdentityDbContext<IdentityUser>
    { 
        //ctor met opties(!) 
        public QuizAppContext(DbContextOptions<QuizAppContext> options) : base(options) 
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //must voor identity 
        }

        //blijf consequent: alle models -> tabellen (met hun naam in het meervoud zoals op de SQL server) 
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
    }
}
