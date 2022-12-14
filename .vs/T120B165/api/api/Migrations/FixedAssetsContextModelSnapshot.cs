// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Context;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(FixedAssetsContext))]
    partial class FixedAssetsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("api.Domain.Employee", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime2");

                    b.HasKey("Username");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("api.Domain.FixedAsset", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssignedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignedTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BoughtAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManagedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("FixedAssets", (string)null);
                });

            modelBuilder.Entity("api.Domain.FixedAssetEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AssignedTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FixedAssetCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FixedAssetCode");

                    b.ToTable("FixedAssetEvents", (string)null);

                    b.HasDiscriminator<int>("Type").HasValue(0);
                });

            modelBuilder.Entity("api.Domain.FixedAssetManager", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FACategory")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Username", "FACategory");

                    b.ToTable("FixedAssetsManagers", (string)null);
                });

            modelBuilder.Entity("api.Domain.AcceptedByUserEvent", b =>
                {
                    b.HasBaseType("api.Domain.FixedAssetEvent");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("api.Domain.AssignedToUserEvent", b =>
                {
                    b.HasBaseType("api.Domain.FixedAssetEvent");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("api.Domain.RejectedByUserEvent", b =>
                {
                    b.HasBaseType("api.Domain.FixedAssetEvent");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("api.Domain.ReturnInitiatedByManagerEvent", b =>
                {
                    b.HasBaseType("api.Domain.FixedAssetEvent");

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("api.Domain.StoredByManagerEvent", b =>
                {
                    b.HasBaseType("api.Domain.FixedAssetEvent");

                    b.HasDiscriminator().HasValue(5);
                });

            modelBuilder.Entity("api.Domain.FixedAssetEvent", b =>
                {
                    b.HasOne("api.Domain.FixedAsset", null)
                        .WithMany("Events")
                        .HasForeignKey("FixedAssetCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Domain.FixedAsset", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
