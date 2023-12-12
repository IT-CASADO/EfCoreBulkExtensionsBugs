﻿// <auto-generated />
using EfCoreBulkExtensionsBugs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EfCoreBulkExtensionsBugs.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EfCoreBulkExtensionsBugs.SomeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SomeEntity", (string)null);
                });

            modelBuilder.Entity("EfCoreBulkExtensionsBugs.SomeEntity", b =>
                {
                    b.OwnsOne("EfCoreBulkExtensionsBugs.SomeField", "_fieldA", b1 =>
                        {
                            b1.Property<int>("SomeEntityId")
                                .HasColumnType("int");

                            b1.Property<string>("PropA")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("FieldAPropA");

                            b1.Property<string>("PropB")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("FieldAPropB");

                            b1.HasKey("SomeEntityId");

                            b1.ToTable("SomeEntity");

                            b1.WithOwner()
                                .HasForeignKey("SomeEntityId");
                        });

                    b.Navigation("_fieldA")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
