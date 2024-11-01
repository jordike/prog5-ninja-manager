using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NinjaManager.Models;

public partial class NinjaManagerContext : DbContext
{
    public DbSet<Ninja> Ninjas { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<NinjaHasEquipment> NinjaHasEquipment { get; set; }

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

        modelBuilder.Entity<NinjaHasEquipment>()
            .HasKey(nhe => new { nhe.NinjaId, nhe.EquipmentId });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
