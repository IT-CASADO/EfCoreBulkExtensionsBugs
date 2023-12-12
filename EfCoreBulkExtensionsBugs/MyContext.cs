using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreBulkExtensionsBugs
{
	public class MyContext : DbContext
	{
		public DbSet<SomeEntity> Entities { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			var connection = @"Server=(localdb)\mssqllocaldb;Database=EfCoreBulkExtensionsBugs;Trusted_Connection=True;ConnectRetryCount=0";

			optionsBuilder.UseSqlServer(connection, options =>
			{

				options.CommandTimeout(300);
			});
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new SomeEntityConfiguration());
		}
	}

	public class SomeEntityConfiguration : IEntityTypeConfiguration<SomeEntity>
	{
		public void Configure(EntityTypeBuilder<SomeEntity> builder)
		{
			builder.HasKey(i => i.Id);

			builder.Property(i => i.Name)
				.IsRequired();

			builder.OwnsOne<SomeField>(
				"_fieldA",
				b =>
				{
					b.Property("PropA")
						.HasColumnName("FieldAPropA")
						.IsRequired(true);

					b.Property("PropB")
						.HasColumnName("FieldAPropB")
						.IsRequired(true);
				}
			);

			builder.Navigation("_fieldA").IsRequired();

			builder.ToTable("SomeEntity");
		}
	}

	public class SomeEntity
	{
		private SomeField _fieldA;

		public int Id { get; set; }
		public string Name { get; set; }

		public string SomeOtherProp => _fieldA.Prop;

		public SomeEntity(string name)
		{
			Name = name;
			_fieldA = new SomeField(Id.ToString(), System.DateTime.Now.Second % 2 == 0 ? name.ToUpper() : null);
		}
	}

	public record SomeField(string PropA, string? PropB)
	{
		public string PropA { private get; init; } = PropA;
		public string? PropB { private get; init; } = PropB;

		public string Prop => PropB ?? PropA;
	};
}
