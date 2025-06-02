using Api.Data.Entities;
using Api.Models.AuditableEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context;

public class MendalaApiContext(DbContextOptions<MendalaApiContext> options)
	: IdentityDbContext<User, IdentityRole, string>(options)
{
	public DbSet<Issue> Issues { get; set; }
	public DbSet<Invoice> Invoices { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<IssueStatusHistory> IssueStatusHistory { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Issue>(entity =>
		{
			entity.HasOne(i => i.Invoice)
				.WithMany(inv => inv.Issues) 
				.HasForeignKey(i => i.InvoiceId)
				.IsRequired();

			entity.HasOne(i => i.Creator)
				.WithMany(u => u.Issues)
				.HasForeignKey(i => i.CreatorId)
				.IsRequired();
		});

		modelBuilder.Entity<Customer>()
			.HasMany(c => c.Invoices)
			.WithOne(i => i.Customer) 
			.HasForeignKey(i => i.CustomerId) 
			.IsRequired();
		
		modelBuilder.Entity<Product>()
			.HasMany(p => p.InvoiceItems)
			.WithOne(ii => ii.Product)
			.HasForeignKey(ii => ii.ProductId)
			.IsRequired();
		
		modelBuilder.Entity<InvoiceItem>()
			.HasOne(ii => ii.Product)
			.WithMany(p => p.InvoiceItems)
			.HasForeignKey(ii => ii.ProductId)
			.IsRequired();
		
		modelBuilder.Entity<Invoice>(entity =>
		{
			entity.HasOne(i => i.Customer)
				.WithMany(c => c.Invoices)
				.HasForeignKey(i => i.CustomerId)
				.IsRequired();

			entity.HasMany(i => i.Issues)
				.WithOne(i => i.Invoice)
				.HasForeignKey(i => i.InvoiceId)
				.IsRequired();

			entity.HasMany(i => i.InvoiceItems)
				.WithOne(ii => ii.Invoice)
				.HasForeignKey(ii => ii.InvoiceId)
				.IsRequired();
		});
		
		modelBuilder.Entity<IssueStatusHistory>(b =>
		{
			b.HasOne(h => h.Issue)
				.WithMany(i => i.StatusHistory)
				.HasForeignKey(h => h.IssueId)
				.IsRequired();
		});
		
		modelBuilder.Entity<User>()
			.HasMany(u => u.Customers)
			.WithOne(c => c.User)
			.HasForeignKey(c => c.UserId)
			.IsRequired(false);

		
	}

	public override int SaveChanges()
	{
		AddStatusHistoryEntries();
		UpdateTimestamps();
		return base.SaveChanges();
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		AddStatusHistoryEntries();
		UpdateTimestamps();
		return base.SaveChangesAsync(cancellationToken);
	}

	private void UpdateTimestamps()
	{
		var entries = ChangeTracker.Entries()
			.Where(e => e.Entity is IAuditable && 
			            (e.State == EntityState.Added || e.State == EntityState.Modified));


		foreach (var entry in entries)
		{
			if (entry.State == EntityState.Added)
			{
				entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
			}

			entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
		}
	}
	
	private void AddStatusHistoryEntries()
	{
		AddHistoryForCreated();
		AddHistoryForModified();
	}

	private void AddHistoryForModified()
	{
		var entries = ChangeTracker.Entries<Issue>()
			.Where(e => e.State == EntityState.Modified)
			.Where(e => e.Property(i => i.Status).IsModified);

		foreach (var entry in entries)
		{
			var history = new IssueStatusHistory
			{
				Id = Guid.NewGuid(),
				IssueId = entry.Entity.Id,
				Status = entry.Entity.Status,
				ChangedAt = DateTime.UtcNow
			};
			
			IssueStatusHistory.Add(history);
		}
	}

	private void AddHistoryForCreated()
	{
		var addedEntries = ChangeTracker.Entries<Issue>()
			.Where(e => e.State == EntityState.Added);

		foreach (var entry in addedEntries)
		{
			var history = new IssueStatusHistory
			{
				Id = Guid.NewGuid(),
				IssueId = entry.Entity.Id,
				Status = entry.Entity.Status,
				ChangedAt = DateTime.UtcNow
			};

			IssueStatusHistory.Add(history);
		}
	}
}