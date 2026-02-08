using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPlayV1
{
    public class FakeDataTests
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

        private static IEnumerable<UserTestData> RegistrationData = DataGenerator.GetUserTests(5);

        [Test, TestCaseSource(nameof(RegistrationData))]
        public async Task UserShouldBeAbleToRegister(UserTestData testData)
        {
            await _page!.GotoAsync("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");

            await _page.FillAsync("input[name='username']", testData.FirstName);
            await _page.FillAsync("input[name = 'password']", testData.Email);
            Thread.Sleep(3000);

        }





    }//test-class

    public static class DataGenerator
    {
        public static List<UserTestData> GetUserTests(int count)
        {
            return new Bogus.Faker<UserTestData>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password(10))
                .Generate(count);
        }
    }
    public class UserTestData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}//namespace
