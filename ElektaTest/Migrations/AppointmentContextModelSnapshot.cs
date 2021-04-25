﻿// <auto-generated />
using System;
using ElektaTest.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ElektaTest.Migrations
{
    [DbContext(typeof(AppointmentContext))]
    partial class AppointmentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("ElektaTest.Domain.Appointment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AppointmentTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Canceled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EequipmentId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Appointment");
                });
#pragma warning restore 612, 618
        }
    }
}
