using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Application.Interfaces;
using AutoFixture;
using Bogus;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Application.IntegrationTests
{
    public class TestFixture : IDisposable
    {
        private static readonly object _lock = new();
        private static bool _databaseInitialized;
        private const string InMemoryConnectionString = "Datasource=test.db";
        public SqliteConnection Connection { get; }

        public TestFixture()
        {
            Connection = new SqliteConnection(InMemoryConnectionString);
            Seed();
            Connection.Open();
        }

        public AppDbContext CreateContext(DbTransaction? transaction = null)
        {
            var context = new AppDbContext
            (
                new DbContextOptionsBuilder<AppDbContext>().UseSqlite(Connection).Options,
                new DateTimeService(),
                new CurrentUserService(new HttpContextAccessor())
            );
            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        SeedData(context);
                    }

                    _databaseInitialized = true;
                }
            }
        }

        private static void SeedData(AppDbContext context)
        {
            var faker = new Faker();
            Fixture fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var fakeClassRoom = fixture
                .Build<ClassRoom>()
                .With(c => c.Grade, faker.Random.Byte(1, 13))
                .With(c => c.Group, RandomGrade(1))
                .Without(c => c.MainTeacher)
                .Without(c => c.MainTeacherId)
                .Without(c => c.Students)
                .Without(c => c.Id)
                .CreateMany(10);

            context.ClassRoom.AddRange(fakeClassRoom);
            context.SaveChanges();
        }

        public void Dispose()
        {
        }

        private static string RandomGrade(int length)
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}