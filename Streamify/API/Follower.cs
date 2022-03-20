using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streamify.API.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using SeleniumExtras.WaitHelpers;

namespace Streamify.API
{
    class Follower
    {
        public static string Token = "";
        public static string Token_user = "";
        public static string auth = "";
        public static string refreshToken = "";
        public static void main()
        {
            foreach (string line in System.IO.File.ReadLines("results/spotify.txt"))
            {
                LocalHost.host(0);
                string[] array = line.Split(':');
                string password = array[1];
                string email = array[0];


                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.EnableVerboseLogging = false;
                service.SuppressInitialDiagnosticInformation = true;
                service.HideCommandPromptWindow = true;

                var option = new ChromeOptions();
                option.AddArguments("--disable-popup-blocking");
                option.AddArguments("--mute-audio");

                service.SuppressInitialDiagnosticInformation = true;
                IWebDriver driver = new ChromeDriver(service, option);
                try
                {

                    driver.Navigate().GoToUrl("https://accounts.spotify.com/login?client_id=dcf7c38d3cb0434fb4fdb6ac3f736f04&response_type=code&redirect_uri=http%3A%2F%2Flocalhost%3A8080%2F&callback&scope=user-read-private%20user-read-email%20user-top-read%20user-read-currently-playing&state=34fFs29kd09");
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until<IWebElement>((d) =>
                    {
                        IWebElement element = d.FindElement(By.Id("login-username"));
                        if (element.Displayed &&
                            element.Enabled &&
                            element.GetAttribute("aria-disabled") == null)
                        {
                            return element;
                        }

                        return null;
                    });



                    driver.FindElement(By.Id("login-username")).SendKeys(email);
                    driver.FindElement(By.Id("login-password")).SendKeys(password);
                    driver.FindElement(By.Id("login-button")).Click();
                }
                catch { }
                try
                {
                    WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                    wait2.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"auth - accept\"]"))).Click();
                }
                catch { }

                }
        }
    }
}
