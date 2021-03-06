// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostFeedback.DAL.EF.Data;

namespace PostFeedback.DAL.EF.Migrations
{
    [DbContext(typeof(PostDbContext))]
    [Migration("20220222155040_PhotoTableAltered")]
    partial class PhotoTableAltered
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FacilityPost", b =>
                {
                    b.Property<long>("FacilitiesId")
                        .HasColumnType("bigint");

                    b.Property<long>("PostsId")
                        .HasColumnType("bigint");

                    b.HasKey("FacilitiesId", "PostsId");

                    b.HasIndex("PostsId");

                    b.ToTable("FacilityPost");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Booking", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("GuestId")
                        .HasColumnType("bigint");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("PostId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Category", b =>
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

            modelBuilder.Entity("PostFeedback.Domain.Entities.City", b =>
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

            modelBuilder.Entity("PostFeedback.Domain.Entities.Facility", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Feedback<PostFeedback.Domain.Entities.Post>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ItemId");

                    b.ToTable("PostFeedbacks");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Feedback<PostFeedback.Domain.Entities.User>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ItemId");

                    b.ToTable("UserFeedbacks");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Photo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("PhotoBytes")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BathroomsNo")
                        .HasColumnType("int");

                    b.Property<int>("BedsNo")
                        .HasColumnType("int");

                    b.Property<long?>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<long>("CityId")
                        .HasColumnType("bigint");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsWholeApartment")
                        .HasColumnType("bit");

                    b.Property<int>("MaxGuestsNo")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("MovingInTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("MovingOutTime")
                        .HasColumnType("time");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoomsNo")
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

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Rule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PostRule", b =>
                {
                    b.Property<long>("PostsId")
                        .HasColumnType("bigint");

                    b.Property<long>("RulesId")
                        .HasColumnType("bigint");

                    b.HasKey("PostsId", "RulesId");

                    b.HasIndex("RulesId");

                    b.ToTable("PostRule");
                });

            modelBuilder.Entity("FacilityPost", b =>
                {
                    b.HasOne("PostFeedback.Domain.Entities.Facility", null)
                        .WithMany()
                        .HasForeignKey("FacilitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PostFeedback.Domain.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Booking", b =>
                {
                    b.HasOne("PostFeedback.Domain.Entities.User", "Guest")
                        .WithMany("Bookings")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PostFeedback.Domain.Entities.Post", "Post")
                        .WithMany("Bookings")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Guest");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Feedback<PostFeedback.Domain.Entities.Post>", b =>
                {
                    b.HasOne("PostFeedback.Domain.Entities.User", "Creator")
                        .WithMany("FeedbacksForAccommodations")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("PostFeedback.Domain.Entities.Post", "Item")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Feedback<PostFeedback.Domain.Entities.User>", b =>
                {
                    b.HasOne("PostFeedback.Domain.Entities.User", "Creator")
                        .WithMany("FeedbacksForUsers")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("PostFeedback.Domain.Entities.User", "Item")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Photo", b =>
                {
                    b.HasOne("PostFeedback.Domain.Entities.Post", "Post")
                        .WithMany("Photos")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Post", b =>
                {
                    b.HasOne("PostFeedback.Domain.Entities.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("PostFeedback.Domain.Entities.City", "City")
                        .WithMany("Posts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PostFeedback.Domain.Entities.User", "Owner")
                        .WithMany("Posts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("City");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PostRule", b =>
                {
                    b.HasOne("PostFeedback.Domain.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PostFeedback.Domain.Entities.Rule", null)
                        .WithMany()
                        .HasForeignKey("RulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Category", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.City", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.Post", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Feedbacks");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("PostFeedback.Domain.Entities.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Feedbacks");

                    b.Navigation("FeedbacksForAccommodations");

                    b.Navigation("FeedbacksForUsers");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
