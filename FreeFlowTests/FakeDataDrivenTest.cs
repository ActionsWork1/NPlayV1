using Bogus; // Faker library
using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace NPlayV1
{

        public class DataDrivenTests
        {
            private IBrowser? _browser;
            private IPage? _page;

            [SetUp]
            public async Task Setup()
            {
                var playwright = await Playwright.CreateAsync();
                _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
                var context = await _browser.NewContextAsync();
                _page = await context.NewPageAsync();
            }

            [TearDown]
            public async Task Teardown()
            {
                await _browser!.CloseAsync();
            }


        //    //[TestCaseSource(nameof(GetFakeUsers))]
        ////[Test, TestCaseSource(typeof(TestDataGenerator), nameof(TestDataGenerator.UserData))]
        //[Test, TestCaseSource(typeof(TestData), nameof(TestData.UserData))]
        //public async Task RegisterUserTest(string name, string email, string password)
        //    {
        //        await _page!.GotoAsync("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");

        //        await _page.FillAsync("input[name='username']", name);
        //        await _page.FillAsync("input[name = 'password']", email);
        //        Thread.Sleep(3000);

        //        // Example assertion
        //        //Assert.That(await _page.InnerTextAsync(".success-message"), Does.Contain("Welcome"));
        //    }



        [Test, TestCaseSource(typeof(TestData), nameof(TestData.UserData))]
        public async Task RegisterUserTest(string name, string email, string password)
        {
            await _page.GotoAsync("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");

            await _page.FillAsync("input[name='username']", name);
            await _page.FillAsync("input[name='password']", password);

            await _page.WaitForTimeoutAsync(3000);
        }








        //// Data source using Faker
        //public static IEnumerable<object[]> GetFakeUsers()
        //{
        //   var faker = new Faker();
        //   for (int i = 0; i < 3; i++)
        //   {
        //       yield return new object[]
        //         {
        //           faker.Name.FullName(),
        //           faker.Internet.Email(),
        //           faker.Internet.Password()
        //         };
        //   }
        //}

        //public static IEnumerable<(string, string, string)> GetFakeUsers()
        //{
        //    var faker = new Faker();
        //    for (int i = 0; i < 3; i++)
        //    {
        //        yield return (
        //            faker.Name.FullName(),
        //            faker.Internet.Email(),
        //            faker.Internet.Password()
        //        );
        //    }
        //}

    }


    //public class TestDataGenerator
    //{
    //    public static IEnumerable<TestCaseData> UserData()
    //    {
    //        var faker = new Faker();

    //        for (int i = 0; i < 5; i++) // Generate 5 test cases
    //        {
    //            var firstName = faker.Name.FirstName();
    //            var email = faker.Internet.Email();
    //            var password = faker.Internet.Password();

    //            yield return new TestCaseData(firstName, email, password);
    //        }
    //    }
    //}


    //public class TestData
    //{
    //    public static IEnumerable<TestCaseData> UserData()
    //    {
    //        var faker = new Faker();

    //        for (int i = 0; i < 3; i++)
    //        {
    //            yield return new TestCaseData(
    //                faker.Name.FullName(),
    //                faker.Internet.Email(),
    //                faker.Internet.Password()
    //            );
    //        }
    //    }
    //}

    //public class TestData
    //{
    //    public static IEnumerable<TestCaseData> UserData()
    //    {
    //        var faker = new Faker();

    //        for (int i = 0; i < 3; i++)
    //        {
    //            var user = (
    //                name: faker.Name.FullName(),
    //                email: faker.Internet.Email(),
    //                password: faker.Internet.Password()
    //            );

    //            yield return new TestCaseData(user);
    //        }
    //    }



    public class TestData
    {
        public static IEnumerable<TestCaseData> UserData()
        {
            var faker = new Faker();

            for (int i = 0; i < 3; i++)
            {
                yield return new TestCaseData(
                    faker.Name.FullName(),
                    faker.Internet.Email(),
                    faker.Internet.Password()
                );
            }
        }
    }
}






