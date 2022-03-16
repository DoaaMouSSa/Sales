﻿// <auto-generated />
using System;
using DataLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(AccountDbContext))]
    [Migration("20220308134029_CatItemTbls")]
    partial class CatItemTbls
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DataLayer.Tables.TblCategory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("cat_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("tblCategory");
                });

            modelBuilder.Entity("DataLayer.Tables.TblItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("bar_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("bit");

                    b.Property<bool>("is_modified")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("profit")
                        .HasColumnType("real");

                    b.Property<float>("purchase_price")
                        .HasColumnType("real");

                    b.Property<float>("sales_price")
                        .HasColumnType("real");

                    b.Property<int>("subcat_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("tblItem");
                });

            modelBuilder.Entity("DataLayer.Tables.TblSubCategory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("cat_id")
                        .HasColumnType("int");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("bit");

                    b.Property<bool>("is_modified")
                        .HasColumnType("bit");

                    b.Property<string>("subcat_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("tblSubCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
