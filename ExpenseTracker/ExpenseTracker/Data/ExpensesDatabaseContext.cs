using ExpenseTracker.Helpers;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Xamarin.Forms;

namespace ExpenseTracker.Data
{
    public sealed class ExpensesDatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryIcon> CategoryIcons { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        private const string DbName = "ExpensesTracker.db";

        public ExpensesDatabaseContext()
        {
            Database.EnsureCreated();
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

            modelBuilder.Entity<CategoryIcon>().HasKey(ci => ci.Id);
            modelBuilder.Entity<CategoryIcon>().HasIndex(ci => ci.FilenameOrFilepath);

            modelBuilder.Entity<Expense>().HasKey(e => e.Id);
            modelBuilder.Entity<Expense>().Property(e => e.Name).IsRequired(false);
            modelBuilder.Entity<Expense>().Property(e => e.Cost).IsRequired();
            modelBuilder.Entity<Expense>().Property(e => e.Timestamp).IsRequired();

            modelBuilder.Entity<Category>()
                .HasOne(c => c.CategoryIcon)
                .WithMany(ci => ci.Categories)
                .HasForeignKey(c => c.CategoryIconId)
                .IsRequired();

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired();

#if DEBUG
            InitializeDefaultCategories(modelBuilder);
            InitializeDefaultCategoryIcons(modelBuilder);
            InitializeDefaultExpenses(modelBuilder);
#endif
        }

        #region Private methods

        private static void InitializeDefaultCategories(ModelBuilder modelBuilder)
        {
            var defaultCategories = new[]
            {
                new Category {Id = 1, Name = "Transport", HexColor = "#A9EB3C", CategoryIconId = 1},
                new Category {Id = 2, Name = "Entertainment", HexColor = "#EB513C", CategoryIconId = 2},
                new Category {Id = 3, Name = "Food", HexColor = "#3C74EB", CategoryIconId = 3},
                new Category {Id = 4, Name = "House", HexColor = "#DAF7A6", CategoryIconId = 4},
                new Category {Id = 5, Name = "Medicine", HexColor = "#DB13BA", CategoryIconId = 5},
                new Category {Id = 6, Name = "Taxi", HexColor = "#DB1353", CategoryIconId = 6},
            };

            modelBuilder.Entity<Category>().HasData(defaultCategories);
        }

        private static void InitializeDefaultCategoryIcons(ModelBuilder modelBuilder)
        {
            var defaultCategoryIcons = new[]
            {
                new CategoryIcon {Id = 1, FilenameOrFilepath = ConstantHelper.CarIcon},
                new CategoryIcon {Id = 2, FilenameOrFilepath = ConstantHelper.CarouselIcon},
                new CategoryIcon {Id = 3, FilenameOrFilepath = ConstantHelper.CutleryIcon},
                new CategoryIcon {Id = 4, FilenameOrFilepath = ConstantHelper.HouseIcon},
                new CategoryIcon {Id = 5, FilenameOrFilepath = ConstantHelper.MedicineIcon},
                new CategoryIcon {Id = 6, FilenameOrFilepath = ConstantHelper.TaxiIcon}
            };

            modelBuilder.Entity<CategoryIcon>().HasData(defaultCategoryIcons);
        }

        private static void InitializeDefaultExpenses(ModelBuilder modelBuilder)
        {
            var defaultExpenses = new[]
            {
                new Expense{Id = 1, Name = "milk", Cost = 1.23, Timestamp = new DateTime(2019, 1, 1), CategoryId = 3}, 
                new Expense{Id = 2, Name = "meat", Cost = 5.27, Timestamp = new DateTime(2019, 1, 31), CategoryId = 3}, 

                new Expense{Id = 3, Name = "meat", Cost = 1.23, Timestamp = new DateTime(2019, 3, 1), CategoryId = 3}, 
                new Expense{Id = 4, Cost = 150, Timestamp = new DateTime(2019, 3, 2), CategoryId = 1}, 
                new Expense{Id = 5, Name = "vegetables", Cost = 1.23, Timestamp = new DateTime(2019, 3, 3), CategoryId = 3},

                new Expense{Id = 6, Cost = 15, Timestamp = new DateTime(2019, 5, 15), CategoryId = 5}, 
                new Expense{Id = 7, Name = "Pills", Cost = 1.23, Timestamp = new DateTime(2019, 5, 15), CategoryId = 5}, 
                new Expense{Id = 8, Name = "meat", Cost = 1.23, Timestamp = new DateTime(2019, 5, 15), CategoryId = 3},
                new Expense{Id = 9, Cost = 123, Timestamp = new DateTime(2019, 5, 16), CategoryId = 5},
                new Expense{Id = 10, Name = "Rent", Cost = 2230, Timestamp = new DateTime(2019, 5, 16), CategoryId = 4}, 
            };

            modelBuilder.Entity<Expense>().HasData(defaultExpenses);
        }

        #endregion Private methods
    }
}