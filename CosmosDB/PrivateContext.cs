﻿#if COSMOSDB
using LivestreamRecorder.DB.Models;
using Microsoft.EntityFrameworkCore;

// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
#nullable disable warnings

namespace LivestreamRecorder.DB.CosmosDB;

public class PrivateContext : DbContext
{
    public PrivateContext()
    {
    }

    public PrivateContext(DbContextOptions<PrivateContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Users

        modelBuilder.Entity<User>()
                    .ToContainer("Users");

        modelBuilder.Entity<User>()
                    .HasNoDiscriminator();

        modelBuilder.Entity<User>()
                    .HasKey(nameof(User.id));

        modelBuilder.Entity<User>()
                    .HasPartitionKey(o => o.id);

        modelBuilder.Entity<User>()
                    .UseETagConcurrency();

        #endregion
    }
}
#endif
