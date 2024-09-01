using AttendanceManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using AttendanceManagement.Handlers;
namespace AttendanceManagement.Models
{
    public partial class MyDbContext : DbContext
    {

        private readonly string _dbPath;

        public MyDbContext()
        {

            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            _dbPath = Path.Combine(path, "test4.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }


        public virtual DbSet<AcademicYear> AcademicYears { get; set; } = null!;
        public virtual DbSet<AttendanceRecord> AttendanceRecords { get; set; } = null!;
        public virtual DbSet<Institution> Institutions { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;

        public virtual DbSet<Leave> Leaves { get; set; } = null!;
        public virtual DbSet<Holiday> Holidays { get; set; } = null!;
        public virtual DbSet<Term> Terms { get; set; } = null!;
        public virtual DbSet<TypeOfStaff> TypeOfStaffs { get; set; } = null!;
        public virtual DbSet<LogEntry> LogEntries { get; set; } = null!;
        public virtual DbSet<TblRefreshtoken> TblRefreshtokens { get; set; } = null!;
        public virtual DbSet<UserAuth> UserAuths { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;
        public virtual DbSet<TenantUser> TenantUsers { get; set; } = null!;




        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changes = ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged && e.State != EntityState.Detached)
                .ToList();

            foreach (var change in changes)
            {
                var logEntry = new LogEntry
                {
                    Id = 0, // Set the Id as needed or remove if it's an identity column
                    TableName = change.Metadata.GetTableName(),
                    Action = GetActionFromState(change.State),
                    DateTime = DateTime.UtcNow,
                    OldValues = GetOldValues(change), // Retrieve the old values
                    NewValues = GetNewValues(change), // Retrieve the new values
                    AffectedColumns = GetAffectedColumns(change), // Retrieve the affected columns
                    Initiator = GetInitiatorInformation(change)! // Retrieve initiator information from the change
                };

                LogEntries.Add(logEntry);
            }

            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        private string GetOldValues(EntityEntry entry)
        {
            var oldValues = entry.OriginalValues;
            var propertyNames = oldValues.Properties.Select(p => p.Name).ToList();

            var oldValuesDictionary = propertyNames.ToDictionary(
                propertyName => propertyName,
                propertyName => oldValues[propertyName]?.ToString()
            );

            return JsonConvert.SerializeObject(oldValuesDictionary);
        }

        private string GetNewValues(EntityEntry entry)
        {
            var newValues = entry.CurrentValues;
            var propertyNames = newValues.Properties.Select(p => p.Name).ToList();

            var newValuesDictionary = propertyNames.ToDictionary(
                propertyName => propertyName,
                propertyName => newValues[propertyName]?.ToString()
            );

            return JsonConvert.SerializeObject(newValuesDictionary);
        }

        private string GetAffectedColumns(EntityEntry entry)
        {
            var propertyNames = entry.Properties.Select(p => p.Metadata.Name).ToList();
            return JsonConvert.SerializeObject(propertyNames);
        }

        private string GetActionFromState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Added:
                    return "Added";
                case EntityState.Modified:
                    return "Modified";
                case EntityState.Deleted:
                    return "Deleted";
                default:
                    return state.ToString();
            }
        }
        private string GetAffectedValue(EntityEntry change)
        {
            string? name = "";
            var entity = change.Entity;
            if (entity is Employee entityModel1)
            {
                name = entityModel1.FirstName + entityModel1.LastName;
            }
            else
            {
                name = null;
            }
            return name!;
        }
        private string? GetInitiatorInformation(EntityEntry change)
        {
            string? name = "";
            var entity = change.Entity;
            if (entity is TblRefreshtoken entityModel1)
            {
                name = entityModel1.UserId;
            }
            return name;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*  modelBuilder.Entity<TenantUser>(entity =>
              {
                  entity.ToTable("TenantUsers");
              });
              modelBuilder.Entity<LogEntry>(entity =>
              {
                  entity.ToTable("LogEntries");
              });
              modelBuilder.Entity<Tenant>(entity =>
              {
                  entity.ToTable("Tenants");

              });


              modelBuilder.Entity<TblRefreshtoken>(entity =>
              {
                  entity.HasKey(e => e.UserId);

                  entity.ToTable("tbl_refreshtoken");

                  entity.Property(e => e.UserId)
                      .HasMaxLength(50)
                      .IsUnicode(false);

                  entity.Property(e => e.TokenId)
                      .HasMaxLength(50)
                      .IsUnicode(false);
              });

              modelBuilder.Entity<AcademicYear>()
              .HasMany(e => e.Terms)
              .WithOne(e => e.AcademicYear)
              .HasForeignKey(e => e.AcademicYearId);

              modelBuilder.Entity<TypeOfStaff>()
              .HasMany(e => e.Employees)
              .WithOne(e => e.TypeOfStaff)
              .HasForeignKey(e => e.TypeOfStaffId);


              */
            modelBuilder.Entity<TblRefreshtoken>().HasNoKey();
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
