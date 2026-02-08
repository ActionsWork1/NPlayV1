using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPlayV1.TestWork
{
    [AllureSuite("Authentication")]
    public class LoginTests : BaseTest
    {
        [Test]
        [AllureName("Verify Valid Login")]
        [AllureFeature("Login Functionality")]
        public async Task TestLogin()
        {
            await Page.GotoAsync("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login", new() { Timeout=35_000});
            await AllureApi.Step("Enter Credentials", async () => {
                await Page.FillAsync("input[name='username']", "Admin");
                TestContext.Out.WriteLine("Enter Username data");

                await Page.FillAsync("input[name='password']", "admin123");
                TestContext.Out.WriteLine("Enter Password data");
            });
            await Page.ClickAsync("button[type='submit']");
            TestContext.Out.WriteLine("cliks on submit button");


            await Expect(Page).ToHaveURLAsync(new Regex("dashboard"));
            TestContext.Out.WriteLine("wait for dashboard page");
        }
    }
}
