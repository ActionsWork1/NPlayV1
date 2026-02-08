using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPlayV1.TestWork
{
    [AllureNUnit]
    public class BaseTest : PageTest
    {


        
        [SetUp]
        public async Task StartTrace()

        {
            await Context.Tracing.StartAsync(new() { Screenshots = true, Snapshots = true });
        }

        [TearDown]
        public async Task EndTrace()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "trace.zip");
            await Context.Tracing.StopAsync(new() { Path = path });

            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                AllureApi.AddAttachment("Debug Trace", "application/zip", path);
            }
        }
    }


    






}
