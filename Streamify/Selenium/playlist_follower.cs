using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;

namespace Streamify.NewFolder1
{
    class playlist_follow
    {

        public static void start()
        {
            Console.Title = "Streamify - follow playlist";
            string accounts = File.ReadAllText("results/spotify.txt");
            foreach (string line in System.IO.File.ReadLines("results/spotify.txt"))
            {
                count_total++;
            }
            foreach (string line in System.IO.File.ReadLines("results/spotify.txt"))
            {
                Thread.Sleep(1000);
                Console.Title = $"Streamify - Total -> {count_total} | Done -> {count}";
                string[] array = line.Split(':');
                login(array[0], array[1]);
            }
            Console.WriteLine("FINISHED");
            Program.Main();
        }
        public static int count = 0;
        public static int count_total;
        public static int starter = 0;
        public static bool login(string email, string password)
        {
            if (starter == Program.max_bots)
            {
                Console.WriteLine("I Hit the limit of max browers...");
                return false;
            }



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

                driver.Navigate().GoToUrl("https://accounts.spotify.com/en/login/");
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
            catch {
                Console.WriteLine("[Error login] Email: " + email + " Password: " + password, ColorTranslator.FromHtml("#cc0000"));
                driver.Close();
                return false;
            }

            try
            {
                WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait2.Until(ExpectedConditions.UrlContains("/status"));
                driver.Url = Program.url;
                wait2.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"main\"]/div/div[2]/div[3]/main/div[2]/div[2]/div/div/div[2]/section/div[2]/div[2]/div/button[2]"))).Click();
                count++;
                starter++;
                Thread.Sleep(1000);
                Console.WriteLine("[Playlist Followed] Email: " + email + " Password: " + password, Color.FromArgb(30, 215, 96));
                driver.Close();
                return true;
            }
            catch
            {
                Console.WriteLine("[Error following] Email: " + email + " Password: " + password, ColorTranslator.FromHtml("#cc0000"));
                driver.Close();
                return false;
            }
        }
    }
}
