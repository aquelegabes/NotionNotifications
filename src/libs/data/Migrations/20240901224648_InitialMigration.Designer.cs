﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotionNotifications.Data;

#nullable disable

namespace NotionNotifications.Data.Migrations
{
    [DbContext(typeof(NotionNotificationsContext))]
    [Migration("20240901224648_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("NotionNotifications.Domain.Entities.NotificationRoot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("AlreadyNotified")
                        .HasColumnType("int");

                    b.Property<string>("Categories")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("EventDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IntegrationId")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("NotionIdProperty")
                        .HasColumnType("int");

                    b.Property<int>("Occurence")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("NotificationRoot", (string)null);
                });

            modelBuilder.Entity("NotionNotifications.Domain.Entities.TransactionEventRoot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("EventDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("NewObjectAsJsonString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldObjectAsJsonString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TransactionEventRoot", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
