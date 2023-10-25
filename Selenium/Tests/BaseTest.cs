using log4net;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.IO;

namespace Selenium.Framework.Tests
{
    public class BaseTest
    {
        private static readonly Driver LogginDriver;
        protected ILog Logger;

        static BaseTest()
        {
            LogginDriver = new LoggingDriver(new WebCoreDriver());
        }

        public BaseTest()
        {
            Driver = LogginDriver;
        }

        public Driver Driver { get; set; }
        
        [SetUp]
        public virtual void Init()
        {
            this.Logger = LogManager.GetLogger(GetType());
            this.Logger.Info("log4net initialized");
            Driver.Start(Settings.GetDriver());
            this.Driver.Manage().Window.Maximize();
            this.Logger.Info("Test started");
        }

        [TearDown]
        public virtual void Cleanup()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure || TestContext.CurrentContext.Result.Outcome == ResultState.Error)
            {
                var timestamp = DateTime.Now.ToString("MM-dd-yyyy-hh-mm-ss-ffff");
                var filename = $"{TestContext.CurrentContext.Test.Name}-failure-{timestamp}.png";
                var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                Directory.CreateDirectory(directory);
                var filepath = Path.Combine(directory, filename);
                Driver.CaptureScreenshot(filepath);                
            }
            this.Driver.Quit();
        }
    }
}
