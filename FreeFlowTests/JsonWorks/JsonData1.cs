using Bogus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NPlayV1.FreeFlowTests.JsonWorks
{
    public class JsonData1
    {
        [Test]
        public void Test1()
        {
            Faker fake = new Bogus.Faker();
            var manager = new TestDataManager();

            // Example: Adding a new record
            var newUser = new UserData1
            {
                Id = "TC00"+fake.Random.Number(100,300).ToString(),
                Name = "New Test",
                Email = "new@example.com",
                Password = "abc"
            };
            manager.SaveTestData(newUser);

            // Example: Updating an existing record (TC001)
            var updateRecord = new UserData1
            {
                Id = "TC001",
                Name = "Updated Test1",
                Email = "fixed@example.com",
                Password = "999"
            };
            manager.SaveTestData(updateRecord);

            Console.WriteLine("Process complete. Check testdata.json.");
        }
    }








public class TestDataManager
    {
        private readonly string _filePath = Directory.GetCurrentDirectory().Split("bin")[0]+"/testdata.json";

        // Setting up options to make the JSON look "pretty" in the file
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions{ WriteIndented = true };

        //public void SaveTestData(UserData1 input)
        //{
        //    TestDataRoot1 root;

        //    // 1. CAPTURE all existing data from the file
        //    if (File.Exists(_filePath))
        //    {
        //        string existingJson = File.ReadAllText(_filePath);
        //        // Deserialize handles converting the text back into a C# List
        //        root = JsonSerializer.Deserialize<TestDataRoot1>(existingJson) ?? new TestDataRoot1();
        //    }
        //    else
        //    {
        //        // If file doesn't exist, start fresh
        //        root = new TestDataRoot1();
        //    }

        //    // 2. CHECK if the ID exists to decide between Update or Append
        //    var existingRecord = root.TestData1.FirstOrDefault(x => x.Id == input.Id);

        //    if (existingRecord != null)
        //    {
        //        // UPDATE: Record found, modify its properties
        //        existingRecord.Name = input.Name;
        //        existingRecord.Email = input.Email;
        //        existingRecord.Password = input.Password;
        //        Console.WriteLine($"Updated existing record: {input.Id}");
        //    }
        //    else
        //    {
        //        // APPEND: No record found, add it to the list
        //        root.TestData1.Add(input);
        //        Console.WriteLine($"Appended new record: {input.Id}");
        //    }

        //    // 3. WRITE the whole updated list back to the file
        //    string finalJson = JsonSerializer.Serialize(root, _options);
        //    File.WriteAllText(_filePath, finalJson);
        //}


        public void SaveTestData(UserData1 input)
        {
            TestDataRoot1 root;

            string fullPath = Path.GetFullPath(_filePath);
            Console.WriteLine($"Current file Path: {fullPath}");


            // 1. Load the file
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                root = JsonSerializer.Deserialize<TestDataRoot1>(json) ?? new TestDataRoot1();
            }
            else
            {
                root = new TestDataRoot1();
            }

            // 2. DEBUG: Check current count before doing anything
            Console.WriteLine($"Current records in file: {root.TestData1.Count}");

            // 3. Search for ID - Trim and Ignore Case to be safe
            var existingRecord = root.TestData1.FirstOrDefault(x =>
                x.Id.Trim().Equals(input.Id.Trim(), StringComparison.OrdinalIgnoreCase));

            if (existingRecord != null)
            {
                // If it enters here, it will NOT increment the count
                Console.WriteLine($"Match found for ID: {input.Id}. Updating existing record...");
                existingRecord.Name = input.Name;
                existingRecord.Email = input.Email;
                existingRecord.Password = input.Password;
            }
            else
            {
                // If it enters here, the count WILL increase
                Console.WriteLine($"No match for ID: {input.Id}. Appending new record...");
                root.TestData1.Add(input);
            }

            // 4. Save
            string finalJson = JsonSerializer.Serialize(root, _options);
            File.WriteAllText(_filePath, finalJson);

            Console.WriteLine($"Process finished. Total records now: {root.TestData1.Count}");
        }




    }


}
