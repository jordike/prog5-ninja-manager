using Microsoft.EntityFrameworkCore;

namespace NinjaManager.Data.Models;

/// <summary>
/// Represents the database context for the Ninja Manager application.
/// </summary>
public partial class NinjaManagerContext : DbContext
{
    /// <summary>
    /// Gets or sets the Ninjas DbSet.
    /// </summary>
    public DbSet<Ninja> Ninjas { get; set; }

    /// <summary>
    /// Gets or sets the Equipment DbSet.
    /// </summary>
    public DbSet<Equipment> Equipment { get; set; }

    /// <summary>
    /// Gets or sets the EquipmentTypes DbSet.
    /// </summary>
    public DbSet<EquipmentType> EquipmentTypes { get; set; }

    /// <summary>
    /// Gets or sets the NinjaHasEquipment DbSet.
    /// </summary>
    public DbSet<NinjaHasEquipment> NinjaHasEquipment { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NinjaManagerContext"/> class.
    /// </summary>
    public NinjaManagerContext()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NinjaManagerContext"/> class with specified options.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    public NinjaManagerContext(DbContextOptions<NinjaManagerContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types
    /// exposed in <see cref="DbSet{TEntity}"/> properties on the derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<NinjaHasEquipment>()
            .HasKey(nhe => new { nhe.NinjaId, nhe.EquipmentId });

        modelBuilder.Entity<EquipmentType>().HasData(
            new EquipmentType { Id = 1, Name = "Head" },
            new EquipmentType { Id = 2, Name = "Chest" },
            new EquipmentType { Id = 3, Name = "Hand" },
            new EquipmentType { Id = 4, Name = "Feet" },
            new EquipmentType { Id = 5, Name = "Ring" },
            new EquipmentType { Id = 6, Name = "Necklace" });

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment { Id = 1, Name = "Ninja Hood of Shadows", EquipmentTypeId = 1, Strength = 2, Agility = 6, Intelligence = 5, Value = 80 },
            new Equipment { Id = 2, Name = "Samurai Helm", EquipmentTypeId = 1, Strength = 5, Agility = 4, Intelligence = 1, Value = 100 },
            new Equipment { Id = 3, Name = "Cursed Mask of Stealth", EquipmentTypeId = 1, Strength = 3, Agility = 10, Intelligence = 4, Value = 90 },

            new Equipment { Id = 4, Name = "Dragon Scale Armor", EquipmentTypeId = 2, Strength = 10, Agility = 3, Intelligence = 2, Value = 200 },
            new Equipment { Id = 5, Name = "Leather Tunic of Agility", EquipmentTypeId = 2, Strength = 4, Agility = 12, Intelligence = 2, Value = 120 },
            new Equipment { Id = 6, Name = "Mystic Robe of Wisdom", EquipmentTypeId = 2, Strength = 1, Agility = 5, Intelligence = 10, Value = 150 },

            new Equipment { Id = 7, Name = "Bracers of Strength", EquipmentTypeId = 3, Strength = 8, Agility = 4, Intelligence = 1, Value = 110 },
            new Equipment { Id = 8, Name = "Gloves of Precision", EquipmentTypeId = 3, Strength = 2, Agility = 9, Intelligence = 4, Value = 95 },
            new Equipment { Id = 9, Name = "Assassin's Gloves", EquipmentTypeId = 3, Strength = 3, Agility = 11, Intelligence = 2, Value = 100 },

            new Equipment { Id = 10, Name = "Boots of Silent Steps", EquipmentTypeId = 4, Strength = 1, Agility = 15, Intelligence = 2, Value = 130 },
            new Equipment { Id = 11, Name = "Warrior's Sandals", EquipmentTypeId = 4, Strength = 5, Agility = 7, Intelligence = 2, Value = 90 },
            new Equipment { Id = 12, Name = "Frostwalker Boots", EquipmentTypeId = 4, Strength = 4, Agility = 8, Intelligence = 3, Value = 140 },

            new Equipment { Id = 13, Name = "Ring of Strength", EquipmentTypeId = 5, Strength = 5, Agility = 1, Intelligence = 0, Value = 70 },
            new Equipment { Id = 14, Name = "Ring of Intelligence", EquipmentTypeId = 5, Strength = 0, Agility = 1, Intelligence = 5, Value = 80 },
            new Equipment { Id = 15, Name = "Ring of Agility", EquipmentTypeId = 5, Strength = 1, Agility = 5, Intelligence = 0, Value = 75 },

            new Equipment { Id = 16, Name = "Amulet of the Night", EquipmentTypeId = 6, Strength = 2, Agility = 2, Intelligence = 8, Value = 110 },
            new Equipment { Id = 17, Name = "Necklace of the Elements", EquipmentTypeId = 6, Strength = 3, Agility = 3, Intelligence = 5, Value = 120 },
            new Equipment { Id = 18, Name = "Talisman of Protection", EquipmentTypeId = 6, Strength = 1, Agility = 6, Intelligence = 3, Value = 100 }
        );
    }

    /// <summary>
    /// Allows for additional configuration of the model.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
