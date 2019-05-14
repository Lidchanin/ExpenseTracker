using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Xamarin.Forms;

namespace ExpenseTracker.Helpers
{
    public class ExpensesDatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        private const string DbName = "ExpensesTracker.db";

        public ExpensesDatabaseContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    SQLitePCL.Batteries_V2.Init();
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..",
                        "Library", DbName);
                    break;
                case Device.Android:
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DbName);
                    break;
                default:
                    throw new NotImplementedException("Platform not supported");
            }

            optionsBuilder.UseSqlite($"Data Source={dbPath}", providerOptions => providerOptions.CommandTimeout(30));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired();

            modelBuilder.Entity<Expense>().HasKey(e => e.Id);
            modelBuilder.Entity<Expense>().Property(e => e.Name).IsRequired(false);
            modelBuilder.Entity<Expense>().Property(e => e.Cost).IsRequired();
            modelBuilder.Entity<Expense>().Property(e => e.Timestamp).IsRequired();

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired();
        }
    }
}