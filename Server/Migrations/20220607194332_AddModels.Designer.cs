﻿// <auto-generated />
using System;
using EventCachingDemo.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventCachingDemo.Server.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20220607194332_AddModels")]
    partial class AddModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EventCachingDemo.Server.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EventCachingDemo.Server.Models.Report", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Agent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MostSoldProduct")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SalesAgentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<int>("TotalProducts")
                        .HasColumnType("int");

                    b.Property<int>("Week")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("ReportId");

                    b.HasIndex("SalesAgentId");

                    b.HasIndex("Year", "Week");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("EventCachingDemo.Server.Models.SalesAgent", b =>
                {
                    b.Property<Guid>("SalesAgentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SalesDepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SalesAgentId");

                    b.HasIndex("SalesDepartmentId");

                    b.ToTable("SalesAgents");
                });

            modelBuilder.Entity("EventCachingDemo.Server.Models.SalesDepartment", b =>
                {
                    b.Property<Guid>("SalesDepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SalesDepartmentId");

                    b.ToTable("SalesDepartments");
                });

            modelBuilder.Entity("EventCachingDemo.Server.Models.SalesLog", b =>
                {
                    b.Property<Guid>("SalesLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DayOfSale")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("Price")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("SalesAgentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SalesLogId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SalesAgentId");

                    b.ToTable("SalesLogs");
                });

            modelBuilder.Entity("EventCachingDemo.Server.Models.Report", b =>
                {
                    b.HasOne("EventCachingDemo.Server.Models.SalesAgent", "SalesAgent")
                        .WithMany()
                        .HasForeignKey("SalesAgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SalesAgent");
                });

            modelBuilder.Entity("EventCachingDemo.Server.Models.SalesAgent", b =>
                {
                    b.HasOne("EventCachingDemo.Server.Models.SalesDepartment", "SalesDepartment")
                        .WithMany("SalesAgents")
                        .HasForeignKey("SalesDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SalesDepartment");
                });

            modelBuilder.Entity("EventCachingDemo.Server.Models.SalesLog", b =>
                {
                    b.HasOne("EventCachingDemo.Server.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventCachingDemo.Server.Models.SalesAgent", "SalesAgent")
                        .WithMany()
                        .HasForeignKey("SalesAgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SalesAgent");
                });

            modelBuilder.Entity("EventCachingDemo.Server.Models.SalesDepartment", b =>
                {
                    b.Navigation("SalesAgents");
                });
#pragma warning restore 612, 618
        }
    }
}