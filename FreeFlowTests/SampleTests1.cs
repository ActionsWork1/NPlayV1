//using Microsoft.Playwright;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NPlayV1
//{


    
//    [TestFixture]
//    public class SampleTests1
//    {

//        IPlaywright? play;
//        IBrowser? browser;
//        IBrowserContext? context;
//        IPage? page;

//        [SetUp]
//        public async Task TestSetUp()
//        {
//            play = await Playwright.CreateAsync();
//            browser = await play.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
//            {
//                Headless = true,
//                Channel = "chrome",
//                SlowMo = 500,
//                Timeout = 35_000,
//                //Proxy = new Proxy { Server= "https://38.23.34.11:40003" }

//            });

//            context = await browser.NewContextAsync(new BrowserNewContextOptions
//            {
//                //Geolocation = { },
//                //Locale = "en-US",
//                //TimezoneId = "America/Denver",
//                ////Permissions=new[] { });
//                //StorageState = "C:/"
//            });

//            page = await browser.NewPageAsync();



//        }

//        [TearDown]
//        public async Task TearDown() { }

//        [Test]
//        public async Task TestSample() {

//            await page!.GotoAsync("https://duckduckgo.com/");


//        }








//    }
//}
