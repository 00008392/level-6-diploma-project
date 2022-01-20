﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Post.DAL.EF.Data;

namespace Post.DAL.EF.Migrations
{
    [DbContext(typeof(PostDbContext))]
    [Migration("20220120180401_BookingsModified")]
    partial class BookingsModified
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Post.Domain.Entities.Accommodation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BathroomsNo")
                        .HasColumnType("int");

                    b.Property<int?>("BedsNo")
                        .HasColumnType("int");

                    b.Property<long?>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CityId")
                        .HasColumnType("bigint");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsWholeApartment")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MaxGuestsNo")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("MovingInTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("MovingOutTime")
                        .HasColumnType("time");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ReferencePoint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomsNo")
                        .HasColumnType("int");

                    b.Property<int?>("SquareMeters")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CityId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Accommodations");
                });

            modelBuilder.Entity("Post.Domain.Entities.AccommodationFacility", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccommodationId")
                        .HasColumnType("bigint");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<string>("OtherItem")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.HasIndex("ItemId", "AccommodationId")
                        .IsUnique()
                        .HasFilter("OtherItem is null");

                    b.HasIndex("ItemId", "AccommodationId", "OtherItem")
                        .IsUnique()
                        .HasFilter("[OtherItem] IS NOT NULL");

                    b.ToTable("AccommodationFacilities");
                });

            modelBuilder.Entity("Post.Domain.Entities.AccommodationPhoto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccommodationId")
                        .HasColumnType("bigint");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.ToTable("AccommodationPhotos");
                });

            modelBuilder.Entity("Post.Domain.Entities.AccommodationRule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccommodationId")
                        .HasColumnType("bigint");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<string>("OtherItem")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.HasIndex("ItemId", "AccommodationId")
                        .IsUnique()
                        .HasFilter("OtherItem is null");

                    b.HasIndex("ItemId", "AccommodationId", "OtherItem")
                        .IsUnique()
                        .HasFilter("[OtherItem] IS NOT NULL");

                    b.ToTable("AccommodationRules");
                });

            modelBuilder.Entity("Post.Domain.Entities.AccommodationSpecificity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccommodationId")
                        .HasColumnType("bigint");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<string>("OtherItem")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.HasIndex("ItemId", "AccommodationId")
                        .IsUnique()
                        .HasFilter("OtherItem is null");

                    b.HasIndex("ItemId", "AccommodationId", "OtherItem")
                        .IsUnique()
                        .HasFilter("[OtherItem] IS NOT NULL");

                    b.ToTable("AccommodationSpecificities");
                });

            modelBuilder.Entity("Post.Domain.Entities.Booking", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long>("AccommodationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Post.Domain.Entities.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Post.Domain.Entities.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Post.Domain.Entities.Facility", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsOther")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("Post.Domain.Entities.Feedback<Post.Domain.Entities.Accommodation>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("AccommodationFeedbacks");
                });

            modelBuilder.Entity("Post.Domain.Entities.Feedback<Post.Domain.Entities.User>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFeedbacks");
                });

            modelBuilder.Entity("Post.Domain.Entities.Rule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsOther")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("Post.Domain.Entities.Specificity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsOther")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specificities");
                });

            modelBuilder.Entity("Post.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Post.Domain.Entities.Accommodation", b =>
                {
                    b.HasOne("Post.Domain.Entities.Category", "Category")
                        .WithMany("Accommodations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Post.Domain.Entities.City", "City")
                        .WithMany("Accommodations")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Post.Domain.Entities.User", "Owner")
                        .WithMany("Accommodations")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("City");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Post.Domain.Entities.AccommodationFacility", b =>
                {
                    b.HasOne("Post.Domain.Entities.Accommodation", "Accommodation")
                        .WithMany("AccommodationFacilities")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Post.Domain.Entities.Facility", "Item")
                        .WithMany("AccommodationItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accommodation");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Post.Domain.Entities.AccommodationPhoto", b =>
                {
                    b.HasOne("Post.Domain.Entities.Accommodation", "Accommodation")
                        .WithMany("AccommodationPhotos")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accommodation");
                });

            modelBuilder.Entity("Post.Domain.Entities.AccommodationRule", b =>
                {
                    b.HasOne("Post.Domain.Entities.Accommodation", "Accommodation")
                        .WithMany("AccommodationRules")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Post.Domain.Entities.Rule", "Item")
                        .WithMany("AccommodationItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accommodation");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Post.Domain.Entities.AccommodationSpecificity", b =>
                {
                    b.HasOne("Post.Domain.Entities.Accommodation", "Accommodation")
                        .WithMany("AccommodationSpecificities")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Post.Domain.Entities.Specificity", "Item")
                        .WithMany("AccommodationItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accommodation");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Post.Domain.Entities.Booking", b =>
                {
                    b.HasOne("Post.Domain.Entities.Accommodation", "Accommodation")
                        .WithMany("Bookings")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Post.Domain.Entities.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Accommodation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Post.Domain.Entities.Feedback<Post.Domain.Entities.Accommodation>", b =>
                {
                    b.HasOne("Post.Domain.Entities.Accommodation", "Item")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Post.Domain.Entities.User", "FeedbackOwner")
                        .WithMany("FeedbacksAsOwnerForAccommodations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("FeedbackOwner");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Post.Domain.Entities.Feedback<Post.Domain.Entities.User>", b =>
                {
                    b.HasOne("Post.Domain.Entities.User", "Item")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Post.Domain.Entities.User", "FeedbackOwner")
                        .WithMany("FeedbacksAsOwnerForUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("FeedbackOwner");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Post.Domain.Entities.Accommodation", b =>
                {
                    b.Navigation("AccommodationFacilities");

                    b.Navigation("AccommodationPhotos");

                    b.Navigation("AccommodationRules");

                    b.Navigation("AccommodationSpecificities");

                    b.Navigation("Bookings");

                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("Post.Domain.Entities.Category", b =>
                {
                    b.Navigation("Accommodations");
                });

            modelBuilder.Entity("Post.Domain.Entities.City", b =>
                {
                    b.Navigation("Accommodations");
                });

            modelBuilder.Entity("Post.Domain.Entities.Facility", b =>
                {
                    b.Navigation("AccommodationItems");
                });

            modelBuilder.Entity("Post.Domain.Entities.Rule", b =>
                {
                    b.Navigation("AccommodationItems");
                });

            modelBuilder.Entity("Post.Domain.Entities.Specificity", b =>
                {
                    b.Navigation("AccommodationItems");
                });

            modelBuilder.Entity("Post.Domain.Entities.User", b =>
                {
                    b.Navigation("Accommodations");

                    b.Navigation("Bookings");

                    b.Navigation("Feedbacks");

                    b.Navigation("FeedbacksAsOwnerForAccommodations");

                    b.Navigation("FeedbacksAsOwnerForUsers");
                });
#pragma warning restore 612, 618
        }
    }
}