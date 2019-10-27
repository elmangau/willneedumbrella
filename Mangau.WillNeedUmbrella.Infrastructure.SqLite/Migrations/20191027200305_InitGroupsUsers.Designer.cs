﻿// <auto-generated />
using Mangau.WillNeedUmbrella.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mangau.WillNeedUmbrella.Infrastructure.SqLite.Migrations
{
    [DbContext(typeof(WnuContext))]
    [Migration("20191027200305_InitGroupsUsers")]
    partial class InitGroupsUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("Mangau.WillNeedUmbrella.Entities.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("secgroup");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = true,
                            Description = "Everyone",
                            Name = "Everyone"
                        },
                        new
                        {
                            Id = 2L,
                            Active = true,
                            Description = "System Administrators",
                            Name = "Administrators"
                        });
                });

            modelBuilder.Entity("Mangau.WillNeedUmbrella.Entities.GroupPermission", b =>
                {
                    b.Property<long>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PermissionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GroupId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("secgrouppermission");
                });

            modelBuilder.Entity("Mangau.WillNeedUmbrella.Entities.GroupUser", b =>
                {
                    b.Property<long>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("secgroupuser");

                    b.HasData(
                        new
                        {
                            GroupId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            GroupId = 1L,
                            UserId = 2L
                        },
                        new
                        {
                            GroupId = 2L,
                            UserId = 1L
                        });
                });

            modelBuilder.Entity("Mangau.WillNeedUmbrella.Entities.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.Property<long>("PermissionCategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("PermissionCategoryId");

                    b.ToTable("secpermission");
                });

            modelBuilder.Entity("Mangau.WillNeedUmbrella.Entities.PermissionCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("secpermissioncategory");
                });

            modelBuilder.Entity("Mangau.WillNeedUmbrella.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<bool>("Recover")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("FirstName");

                    b.HasIndex("LastName");

                    b.HasIndex("UserName");

                    b.ToTable("secuser");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = true,
                            FirstName = "System",
                            LastName = "Administrator",
                            Password = "$2y$10$nLgfDdhTjYdUH6wbEctoLe0Ua6yjzx8YCksWZ/aaVGLpAb0hmtddG",
                            Recover = false,
                            UserName = "administrator"
                        },
                        new
                        {
                            Id = 2L,
                            Active = true,
                            FirstName = "Test",
                            LastName = "01",
                            Password = "$2y$10$qrGKsfUDysr7fR18ZWlkxOYWMg6D.Of3CeCUzZLGC27xS4VV4AzqW",
                            Recover = false,
                            UserName = "test01"
                        });
                });

            modelBuilder.Entity("Mangau.WillNeedUmbrella.Entities.GroupPermission", b =>
                {
                    b.HasOne("Mangau.WillNeedUmbrella.Entities.Group", "Group")
                        .WithMany("GroupsPermissions")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mangau.WillNeedUmbrella.Entities.Permission", "Permission")
                        .WithMany("GroupsPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mangau.WillNeedUmbrella.Entities.GroupUser", b =>
                {
                    b.HasOne("Mangau.WillNeedUmbrella.Entities.Group", "Group")
                        .WithMany("GroupsUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mangau.WillNeedUmbrella.Entities.User", "User")
                        .WithMany("GroupsUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mangau.WillNeedUmbrella.Entities.Permission", b =>
                {
                    b.HasOne("Mangau.WillNeedUmbrella.Entities.PermissionCategory", "PermissionCategory")
                        .WithMany("Permissions")
                        .HasForeignKey("PermissionCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
