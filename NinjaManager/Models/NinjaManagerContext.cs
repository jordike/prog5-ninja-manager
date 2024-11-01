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

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<NinjaHasEquipment>()
            .HasKey(nhe => new { nhe.NinjaId, nhe.EquipmentId });

        modelBuilder.Entity<Equipment>().HasData(
                new Equipment { Id = 1, Name = "Ninja Hood of Shadows", Type = "Head", Strength = 2, Agility = 6, Intelligence = 5, Value = 80 },
                new Equipment { Id = 2, Name = "Samurai Helm", Type = "Head", Strength = 5, Agility = 4, Intelligence = 1, Value = 100 },
                new Equipment { Id = 3, Name = "Cursed Mask of Stealth", Type = "Head", Strength = 3, Agility = 10, Intelligence = 4, Value = 90 },

                new Equipment { Id = 4, Name = "Dragon Scale Armor", Type = "Chest", Strength = 10, Agility = 3, Intelligence = 2, Value = 200 },
                new Equipment { Id = 5, Name = "Leather Tunic of Agility", Type = "Chest", Strength = 4, Agility = 12, Intelligence = 2, Value = 120 },
                new Equipment { Id = 6, Name = "Mystic Robe of Wisdom", Type = "Chest", Strength = 1, Agility = 5, Intelligence = 10, Value = 150 },

                new Equipment { Id = 7, Name = "Bracers of Strength", Type = "Hand", Strength = 8, Agility = 4, Intelligence = 1, Value = 110 },
                new Equipment { Id = 8, Name = "Gloves of Precision", Type = "Hand", Strength = 2, Agility = 9, Intelligence = 4, Value = 95 },
                new Equipment { Id = 9, Name = "Assassin's Gloves", Type = "Hand", Strength = 3, Agility = 11, Intelligence = 2, Value = 100 },

                new Equipment { Id = 10, Name = "Boots of Silent Steps", Type = "Feet", Strength = 1, Agility = 15, Intelligence = 2, Value = 130 },
                new Equipment { Id = 11, Name = "Warrior's Sandals", Type = "Feet", Strength = 5, Agility = 7, Intelligence = 2, Value = 90 },
                new Equipment { Id = 12, Name = "Frostwalker Boots", Type = "Feet", Strength = 4, Agility = 8, Intelligence = 3, Value = 140 },

                new Equipment { Id = 13, Name = "Ring of Strength", Type = "Ring", Strength = 5, Agility = 1, Intelligence = 0, Value = 70 },
                new Equipment { Id = 14, Name = "Ring of Intelligence", Type = "Ring", Strength = 0, Agility = 1, Intelligence = 5, Value = 80 },
                new Equipment { Id = 15, Name = "Ring of Agility", Type = "Ring", Strength = 1, Agility = 5, Intelligence = 0, Value = 75 },

                new Equipment { Id = 16, Name = "Amulet of the Night", Type = "Necklace", Strength = 2, Agility = 2, Intelligence = 8, Value = 110 },
                new Equipment { Id = 17, Name = "Necklace of the Elements", Type = "Necklace", Strength = 3, Agility = 3, Intelligence = 5, Value = 120 },
                new Equipment { Id = 18, Name = "Talisman of Protection", Type = "Necklace", Strength = 1, Agility = 6, Intelligence = 3, Value = 100 }
        );
    
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
