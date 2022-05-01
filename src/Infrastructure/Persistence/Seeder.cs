using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static class Seeder
{
    public static void Seed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
        var classRooms = GetAllClassRoom();
        if (context.ClassRoom.FirstOrDefault() is null)
        {
            context.ClassRoom.AddRange(classRooms);
            context.SaveChanges();
            SeedStudents(context);
        }
    }

    private static void SeedStudents(AppDbContext context)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false, Comment = '#', AllowComments = true, Delimiter = ","
        };
        const string path = "../Infrastructure/Persistence/students.csv";
        using var streamReader = File.OpenText(path);
        using var csvReader = new CsvReader(streamReader, csvConfig);


        while (csvReader.Read())
        {
            var schoolNumber = csvReader.GetField(1);
            var firstName = csvReader.GetField(2);
            var lastName = csvReader.GetField(3);
            var gender = csvReader.GetField(4);
            var grade = csvReader.GetField(5);
            var group = csvReader.GetField(6);

            var classRoom = context.ClassRoom.FirstOrDefault(x => x.Group.Equals(group) && x.Grade == byte.Parse(grade));
            if (classRoom is not null)
            {
                context.Students.Add(new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    SchoolNumber = schoolNumber,
                    Gender = GenderConverter(gender),
                    ClassRoomId = classRoom.Id
                });
            }

            context.SaveChanges();

            Console.WriteLine($"{firstName} {lastName} {schoolNumber} {gender}");
        }
    }

    private static IEnumerable<ClassRoom> GetAllClassRoom()
    {
        return new List<ClassRoom>
        {
            new() {Grade = 5, Group = "A"},
            new() {Grade = 5, Group = "B"},
            new() {Grade = 5, Group = "C"},
            new() {Grade = 5, Group = "D"},
            new() {Grade = 5, Group = "E"},
            new() {Grade = 5, Group = "F"},
            new() {Grade = 5, Group = "G"},
            new() {Grade = 6, Group = "A"},
            new() {Grade = 6, Group = "B"},
            new() {Grade = 6, Group = "C"},
            new() {Grade = 6, Group = "D"},
            new() {Grade = 6, Group = "F"},
            new() {Grade = 6, Group = "G"},
            new() {Grade = 7, Group = "A"},
            new() {Grade = 7, Group = "B"},
            new() {Grade = 7, Group = "C"},
            new() {Grade = 7, Group = "D"},
            new() {Grade = 7, Group = "E"},
            new() {Grade = 7, Group = "F"},
            new() {Grade = 7, Group = "G"},
            new() {Grade = 8, Group = "A"},
            new() {Grade = 8, Group = "B"},
            new() {Grade = 8, Group = "C"},
            new() {Grade = 8, Group = "D"},
            new() {Grade = 8, Group = "E"},
            new() {Grade = 8, Group = "F"},
            new() {Grade = 8, Group = "G"}
        };
    }


    private static Gender GenderConverter(string genderWithTurkish)
    {
        return genderWithTurkish.Equals("Erkek") ? Gender.MALE : Gender.FEMALE;
    }
}