﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Alura.WebAPI.DAL.Usuarios
{
    public class AuthDbContext : IdentityDbContext<Usuario>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration<Usuario>(new UsuarioConfiguration());
        }
    }
}
