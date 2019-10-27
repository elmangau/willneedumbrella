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
            userMB
                .HasData(
                    new User { Id = 1, Active = true, UserName = "administrator", Password = "$2y$10$nLgfDdhTjYdUH6wbEctoLe0Ua6yjzx8YCksWZ/aaVGLpAb0hmtddG", FirstName = "System", LastName = "Administrator" },
                    new User { Id = 2, Active = true, UserName = "test01", Password = "$2y$10$qrGKsfUDysr7fR18ZWlkxOYWMg6D.Of3CeCUzZLGC27xS4VV4AzqW", FirstName = "Test", LastName = "01" }
                    );

            var groupMB = modelBuilder.Entity<Group>();
            groupMB
                .Property(u => u.Active)
                .HasDefaultValue(true);
            groupMB
                .HasIndex(u => u.Name)
                .IsUnique(true);
            groupMB
                .HasData(
                    new Group { Id = 1, Active = true, Name = "Everyone", Description = "Everyone" },
                    new Group { Id = 2, Active = true, Name = "Administrators", Description = "System Administrators" }
                );

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
            groupsUsersMB
                .HasData(
                    new GroupUser { GroupId = 1, UserId = 1 },
                    new GroupUser { GroupId = 1, UserId = 2 },
                    new GroupUser { GroupId = 2, UserId = 1 }
                );

            var percatMB = modelBuilder.Entity<PermissionCategory>();
            percatMB
                .Property(u => u.Active)
                .HasDefaultValue(true);
            percatMB
                .HasIndex(u => u.Name)
                .IsUnique(true);
            percatMB
                .HasData(
                    new PermissionCategory { Id = 1, Name = "Users", Description = "User Management Permissions"},
                    new PermissionCategory { Id = 2, Name = "System", Description = "System Management Permissions" }
                );

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
            perMB
                .HasData(
                    new Permission { Id = 1, Name = "Users.Login", Description = "The user can Login in the System", PermissionCategoryId = 1 },
                    new Permission { Id = 2, Name = "Users.AddUser", Description = "The user can add other users to the System", PermissionCategoryId = 1 }
                );

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
            groupsPersMB
                .HasData(
                    new GroupPermission { GroupId = 1, PermissionId = 1},
                    new GroupPermission { GroupId = 2, PermissionId = 1 },
                    new GroupPermission { GroupId = 2, PermissionId = 2 }
                );
        }
    }
}
