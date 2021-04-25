﻿// <auto-generated />
using System;
using ElektaTest.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ElektaTest.Migrations
{
    [DbContext(typeof(AppointmentContext))]
    [Migration("20210424173839_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("ElektaTest.Domain.Appointment", b =>
                {
                    b.Property<Guid>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AppointmentTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Canceled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EequipmentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PatientId");

                    b.ToTable("Appointment");
                });
#pragma warning restore 612, 618
        }
    }
}
