﻿// <auto-generated />
using System;
using DataLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(AccountDbContext))]
    partial class AccountDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("tblCategory");
                });

            modelBuilder.Entity("DataLayer.Tables.TblProduct", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("purchase_price")
                        .HasColumnType("real");

                    b.Property<float>("sale_price")
                        .HasColumnType("real");

                    b.Property<int>("sub_cat_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("sub_cat_id");

                    b.ToTable("tblProduct");
                });

            modelBuilder.Entity("DataLayer.Tables.TblPurchaseInvoice", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("purchase_Added_Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("store_id")
                        .HasColumnType("int");

                    b.Property<float>("total")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.HasIndex("store_id");

                    b.ToTable("tblPurchase");
                });

            modelBuilder.Entity("DataLayer.Tables.TblPurchaseInvoiceDetails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("notes")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.Property<int>("purchase_inv_id")
                        .HasColumnType("int");

                    b.Property<float>("purchase_price_one_product")
                        .HasColumnType("real");

                    b.Property<int>("qty")
                        .HasColumnType("int");

                    b.Property<float>("total_purchase_price_one_product")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.HasIndex("product_id");

                    b.HasIndex("purchase_inv_id");

                    b.ToTable("tblPurchaseDetails");
                });

            modelBuilder.Entity("DataLayer.Tables.TblStore", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("store_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("tblStore");
                });

            modelBuilder.Entity("DataLayer.Tables.TblStoreDetails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.Property<int>("qty")
                        .HasColumnType("int");

                    b.Property<int>("store_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("product_id");

                    b.HasIndex("store_id");

                    b.ToTable("tblStoreDetails");
                });

            modelBuilder.Entity("DataLayer.Tables.TblSubCategory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("cat_id")
                        .HasColumnType("int");

                    b.Property<string>("subcat_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("cat_id");

                    b.ToTable("tblSubCategory");
                });

            modelBuilder.Entity("DataLayer.Tables.TblUser", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("tblUser");
                });

            modelBuilder.Entity("DataLayer.Tables.TblProduct", b =>
                {
                    b.HasOne("DataLayer.Tables.TblSubCategory", "TblSubCategory")
                        .WithMany("TblProduct")
                        .HasForeignKey("sub_cat_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TblSubCategory");
                });

            modelBuilder.Entity("DataLayer.Tables.TblPurchaseInvoice", b =>
                {
                    b.HasOne("DataLayer.Tables.TblStore", "TblStore")
                        .WithMany()
                        .HasForeignKey("store_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TblStore");
                });

            modelBuilder.Entity("DataLayer.Tables.TblPurchaseInvoiceDetails", b =>
                {
                    b.HasOne("DataLayer.Tables.TblProduct", "TblProduct")
                        .WithMany()
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Tables.TblPurchaseInvoice", "TblPurchaseInvoice")
                        .WithMany("PurchaseInvoiceDetailsLst")
                        .HasForeignKey("purchase_inv_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TblProduct");

                    b.Navigation("TblPurchaseInvoice");
                });

            modelBuilder.Entity("DataLayer.Tables.TblStoreDetails", b =>
                {
                    b.HasOne("DataLayer.Tables.TblProduct", "TblProduct")
                        .WithMany()
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Tables.TblStore", "TblStore")
                        .WithMany()
                        .HasForeignKey("store_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TblProduct");

                    b.Navigation("TblStore");
                });

            modelBuilder.Entity("DataLayer.Tables.TblSubCategory", b =>
                {
                    b.HasOne("DataLayer.Tables.TblCategory", "TblCategory")
                        .WithMany("TblSubCategory")
                        .HasForeignKey("cat_id");

                    b.Navigation("TblCategory");
                });

            modelBuilder.Entity("DataLayer.Tables.TblCategory", b =>
                {
                    b.Navigation("TblSubCategory");
                });

            modelBuilder.Entity("DataLayer.Tables.TblPurchaseInvoice", b =>
                {
                    b.Navigation("PurchaseInvoiceDetailsLst");
                });

            modelBuilder.Entity("DataLayer.Tables.TblSubCategory", b =>
                {
                    b.Navigation("TblProduct");
                });
#pragma warning restore 612, 618
        }
    }
}
