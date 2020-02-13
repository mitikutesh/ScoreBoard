using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ScoreBoardService.Data;
using ScoreBoardService.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using Xunit;
using System.Linq;

namespace ScoreBoardServiceTest
{
    public class ScoreContextTest
    {
        [Fact]
        public void Add_writes_to_database()
        {
            // In-memory database only exists while the connection is open
            var connection = new SQLiteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ScoreContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ScoreContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new ScoreContext(options))
                {
                    var service = new ScoreBoardService.Services.ScoreBoardService(context);
                    service.Add("Shewawa");
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new ScoreContext(options))
                {
                    Assert.Equal(1, context.Players.Count());
                    Assert.Equal("Shewawa", context.Players.Single().Name);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Find_searches_name()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ScoreContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new ScoreContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Insert seed data into the database using one instance of the context
                using (var context = new ScoreContext(options))
                {
                    context.Players.Add(new Player { Name="John F."});
                    context.Players.Add(new Player { Name = "Ville T." });
                    context.Players.Add(new Player { Name = "Anders R." });
                    context.SaveChanges();
                }

                // Use a clean instance of the context to run the test
                using (var context = new ScoreContext(options))
                {
                    var service = new ScoreBoardService.Services.ScoreBoardService(context);
                    var result = service.Find("Anders");
                    Assert.Equal(1, result.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
