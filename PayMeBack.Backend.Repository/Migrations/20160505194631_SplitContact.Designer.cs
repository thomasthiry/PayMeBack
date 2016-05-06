using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using PayMeBack.Backend.Contracts;

namespace PayMeBack.Backend.Repository.Migrations
{
    [DbContext(typeof(PayMeBackContext))]
    [Migration("20160505194631_SplitContact")]
    partial class SplitContact
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PayMeBack.Backend.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<int>("SplitId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("PayMeBack.Backend.Models.Split", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("PayMeBack.Backend.Models.Contact", b =>
                {
                    b.HasOne("PayMeBack.Backend.Models.Split")
                        .WithMany()
                        .HasForeignKey("SplitId");
                });
        }
    }
}
