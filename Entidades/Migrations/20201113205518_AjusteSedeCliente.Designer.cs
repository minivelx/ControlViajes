﻿// <auto-generated />
using System;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entidades.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201113205518_AjusteSedeCliente")]
    partial class AjusteSedeCliente
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entidades.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Entidades.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("Activo");

                    b.Property<string>("Cedula")
                        .HasMaxLength(20);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nombre")
                        .HasMaxLength(100);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Entidades.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolUsuarios");
                });

            modelBuilder.Entity("Entidades.Camion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo");

                    b.Property<bool>("EsPropio");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(7);

                    b.Property<string>("Remolque")
                        .HasMaxLength(7);

                    b.Property<string>("UsuarioRegistro")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("UsuarioRegistro");

                    b.ToTable("Camiones");
                });

            modelBuilder.Entity("Entidades.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("UsuarioRegistro")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("UsuarioRegistro");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Entidades.Sede", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("IdCliente");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("IdCliente");

                    b.ToTable("Sedes");
                });

            modelBuilder.Entity("Entidades.Viaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha");

                    b.Property<DateTime>("FechaRegistro");

                    b.Property<DateTime?>("FinRuta");

                    b.Property<int>("IdCamion");

                    b.Property<int>("IdCliente");

                    b.Property<string>("IdCoductor")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<int>("IdDestino");

                    b.Property<int>("IdOrigen");

                    b.Property<DateTime?>("InicioRuta");

                    b.Property<string>("NumeroCuenta")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("NumeroManifiesto")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("UsuarioRegistro")
                        .HasMaxLength(450);

                    b.Property<decimal>("ValorAnticipo");

                    b.HasKey("Id");

                    b.HasIndex("IdCamion");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdCoductor");

                    b.HasIndex("IdDestino");

                    b.HasIndex("IdOrigen");

                    b.HasIndex("UsuarioRegistro");

                    b.ToTable("Viajes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Entidades.ApplicationUserRole", b =>
                {
                    b.HasOne("Entidades.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entidades.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entidades.Camion", b =>
                {
                    b.HasOne("Entidades.ApplicationUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioRegistro");
                });

            modelBuilder.Entity("Entidades.Cliente", b =>
                {
                    b.HasOne("Entidades.ApplicationUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioRegistro");
                });

            modelBuilder.Entity("Entidades.Sede", b =>
                {
                    b.HasOne("Entidades.Cliente", "Cliente")
                        .WithMany("lstSedes")
                        .HasForeignKey("IdCliente");
                });

            modelBuilder.Entity("Entidades.Viaje", b =>
                {
                    b.HasOne("Entidades.Camion", "Camion")
                        .WithMany()
                        .HasForeignKey("IdCamion")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entidades.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entidades.ApplicationUser", "Conductor")
                        .WithMany()
                        .HasForeignKey("IdCoductor")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entidades.Sede", "SedeDestino")
                        .WithMany()
                        .HasForeignKey("IdDestino")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entidades.Sede", "SedeOrigen")
                        .WithMany()
                        .HasForeignKey("IdOrigen")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entidades.ApplicationUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioRegistro");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Entidades.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Entidades.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Entidades.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Entidades.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
