using EFCore.BulkExtensions;
using EfCoreBulkExtensionsBugs;


using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EfCoreBulkExtensionsBugsTest
{
	public class UnitTest1
	{
		[Fact]
		public void BulkInsert()
		{
			// arrange
			MigrateDatabase();

			var context = new MyContext();
			var newEntity =
				new SomeEntity("abc");

			// act

			var action = () => context.BulkInsert(new[] { newEntity });

			// assert
			action.Should().NotThrow();
		}

		[Fact]
		public void InsertOverEfCore()
		{
			// arrange
			MigrateDatabase();

			var context = new MyContext();
			var newEntity =
				new SomeEntity("abc");

			// act
			context.Entities.AddRange(newEntity);
			var action = () => context.SaveChanges();

			// assert
			action.Should().NotThrow();
		}

		private void MigrateDatabase()
		{
			var context = new MyContext();


			if (context.Database.GetDbConnection().Database != "")
			{
				context.Database.Migrate();
			}
		}
	}
}