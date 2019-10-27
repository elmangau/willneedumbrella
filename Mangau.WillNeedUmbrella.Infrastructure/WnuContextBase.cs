using Mangau.WillNeedUmbrella.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mangau.WillNeedUmbrella.Infrastructure
{
    public abstract class WnuContextBase : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupUser> GroupsUsers { get; set; }

        public DbSet<PermissionCategory> PermissionCategories { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<GroupPermission> GroupsPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userMB = modelBuilder.Entity<User>();
            userMB
                .Property(u => u.Active)
                .HasDefaultValue(false);
            userMB
                .Property(u => u.Recover)
                .HasDefaultValue(false);
            userMB
                .HasIndex(u => u.UserName)
                .IsUnique(false);
            userMB
                .HasIndex(u => u.FirstName)
                .IsUnique(false);
            userMB
                .HasIndex(u => u.LastName)
                .IsUnique(false);

            var groupMB = modelBuilder.Entity<Group>();
            groupMB
                .Property(u => u.Active)
                .HasDefaultValue(true);
            groupMB
                .HasIndex(u => u.Name)
                .IsUnique(true);

            var groupsUsersMB = modelBuilder.Entity<GroupUser>();
            groupsUsersMB
                .HasKey(gu => new { gu.GroupId, gu.UserId });
            groupsUsersMB
                .HasOne(gu => gu.Group)
                .WithMany(g => g.GroupsUsers)
                .HasForeignKey(g => g.GroupId);
            groupsUsersMB
                .HasOne(gu => gu.User)
                .WithMany(g => g.GroupsUsers)
                .HasForeignKey(g => g.UserId);

            var percatMB = modelBuilder.Entity<PermissionCategory>();
            percatMB
                .Property(u => u.Active)
                .HasDefaultValue(true);
            percatMB
                .HasIndex(u => u.Name)
                .IsUnique(true);

            var perMB = modelBuilder.Entity<Permission>();
            perMB
                .Property(u => u.Active)
                .HasDefaultValue(true);
            perMB
                .HasIndex(u => u.Name)
                .IsUnique(true);
            perMB
                .HasOne(p => p.PermissionCategory)
                .WithMany(pc => pc.Permissions)
                .HasForeignKey(p => p.PermissionCategoryId);

            var groupsPersMB = modelBuilder.Entity<GroupPermission>();
            groupsPersMB
                .HasKey(gp => new { gp.GroupId, gp.PermissionId });
            groupsPersMB
                .HasOne(gp => gp.Group)
                .WithMany(g => g.GroupsPermissions)
                .HasForeignKey(g => g.GroupId);
            groupsPersMB
                .HasOne(gp => gp.Permission)
                .WithMany(g => g.GroupsPermissions)
                .HasForeignKey(g => g.PermissionId);
        }
    }
}
