using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using PayMeBack.Backend.Repository;

namespace PayMeBack.Backend.Repository.Migrations
{
    [DbContext(typeof(PayMeBackContext))]
    [Migration("20160605185955_LinkSplitToUser")]
    partial class LinkSplitToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PayMeBack.Backend.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Creation");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PasswordSalt");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("PayMeBack.Backend.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("Iban");

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("PayMeBack.Backend.Models.Split", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("PayMeBack.Backend.Models.SplitContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<int>("ContactId");

                    b.Property<decimal>("Owes");

                    b.Property<decimal>("Paid");

                    b.Property<int>("SplitId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("PayMeBack.Backend.Models.Split", b =>
                {
                    b.HasOne("PayMeBack.Backend.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PayMeBack.Backend.Models.SplitContact", b =>
                {
                    b.HasOne("PayMeBack.Backend.Models.Contact")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("PayMeBack.Backend.Models.Split")
                        .WithMany()
                        .HasForeignKey("SplitId");
                });
        }
    }
}
