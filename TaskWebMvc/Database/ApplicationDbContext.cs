﻿using Microsoft.EntityFrameworkCore;
using TaskWebMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TaskWebMvc.Database
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<WorkTask> WorkTasks { get; set; }
        public DbSet<TaskUser> TaskUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir chave primária composta para TaskUser (tabela de associação)
            modelBuilder.Entity<TaskUser>()
                .HasKey(t => new { t.TaskId, t.UserId});

            // Configurar relacionamento Task <-> TaskUser
            modelBuilder.Entity<TaskUser>()
                .HasOne(t => t.Task)
                .WithMany(tu => tu.TaskUsers)
                .HasForeignKey(t => t.TaskId);

            // Configurar relacionamento User <-> TaskUser
            modelBuilder.Entity<TaskUser>()
                .HasOne(t => t.User)
                .WithMany(tu => tu.TaskUsers)
                .HasForeignKey(t => t.UserId);

        }

    }
}
