﻿using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Model;

namespace WebApi.infrastructure
{
    public class ConnectionContext : DbContext
    {
        // DbSet<Tabela>
        public DbSet<InfoApi> infoApis { get; set; }

        // Conexão com o banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Server=localhost;" +
                "Port=5432;DataBase=Tabelas;" +
                "User Id=postgres;" +
                "Password=marlinho;");
    }
}
