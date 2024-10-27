using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NinjaManager.Models;

public partial class NinjaManagerContext : DbContext
{
    public DbSet<Ninja> Ninjas { get; set; }

    public NinjaManagerContext()
    {
    }

    public NinjaManagerContext(DbContextOptions<NinjaManagerContext> options)
        : base(options)
    {
    }

    // Seed data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
