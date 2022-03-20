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
using System.Net;
using Leaf.xNet;
using System.Text.RegularExpressions;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;


namespace Streamify.NewFolder1
{
    class streamer
    {

        public static void start()
        {
            Console.Title = "Streamify - Streaming playlist";
            string accounts = File.ReadAllText("results/spotify.txt");
            foreach (string line in System.IO.File.ReadLines("results/spotify.txt"))
            {
                count_total++;
            }
            foreach (string line in System.IO.File.ReadLines("results/spotify.txt"))
            {
                Thread.Sleep(1000);
                string[] array = line.Split(':');
                Console.Title = $"Streamify - Total -> {count_total} | Listening -> {count}";
                login(array[0], array[1]);
            }
        }
        public static int count = 0;
        public static int count_total;
        public static bool proxies_tester(string proxy)
        {
            try
            {
                var httpRequest = new Leaf.xNet.HttpRequest();
                if (Program.proxy_type.ToLower().Contains("http"))
                {
                    httpRequest.Proxy = HttpProxyClient.Parse(proxy);
                }

                if (Program.proxy_type.ToLower().Contains("socks4"))
                {
                    httpRequest.Proxy = Socks4ProxyClient.Parse(proxy);
                }

                if (Program.proxy_type.ToLower().Contains("socks5"))
                {
                    httpRequest.Proxy = Socks5ProxyClient.Parse(proxy);
                }
                httpRequest.Proxy = httpRequest.Proxy;
                httpRequest.IgnoreProtocolErrors = true;
                httpRequest.UserAgent = Http.ChromeUserAgent();
                string text2 = httpRequest.Get("http://ip-api.com/json/", null).ToString();
                string region = "";
                string location = "";
                if (text2.Contains("\"status\":\"success\""))
                {
                    location = Regex.Match(text2, "\"countryCode\":\"(.*?)\"").Groups[1].Value.ToString();
                    region = Regex.Match(text2, "\"regionName\":\"(.*?)\"").Groups[1].Value.ToString();
                    Console.WriteLine(proxy + " - Country: " + location + " - Region: " + region);
                    return true;
                }
            }
            catch(Exception)
            {
                return false;
            }
            return false;
        }
        public static string proxy_grabber()
        {
            string[] proxies = File.ReadAllLines("proxies.txt");
            Random rd = new Random();
            return proxies[rd.Next(proxies.Length)];
        }
        public static string proxy;


        /* 
        
           for (; ;)
            { 
                proxy = proxy_grabber();
                bool tester = proxies_tester(proxy);
                if (tester == true)
                {
                    Console.WriteLine("Proxies works " + proxy);
                    break;
                }
                else
                {
                    Console.WriteLine(proxy + " Failed");
                }
            }
        
        
        
        
        */

        public static int starter = 0;
        public static bool login(string email, string password)
        {
            if (starter == Program.max_bots)
            {
                Console.WriteLine("I Hit the limit of max browers...");
                return false;
            }

            if (Program.proxy_type.ToLower() == "off")
            {
            }
            else
            {

                for (int i = 1; i <= 200; i++)
                {
                    proxy = proxy_grabber();
                    bool tester = proxies_tester(proxy);
                    if (tester == true)
                    {
                        Console.WriteLine("Proxies works " + proxy);
                        break;
                    }
                    else
                    {
                        Console.WriteLine(proxy + " Failed");
                    }
                }
            }

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.EnableVerboseLogging = false;
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            var option = new ChromeOptions();
            option.AddArguments("--disable-popup-blocking");
            option.AddArguments("--mute-audio");
            option.AddArguments("--no-sandbox");
            option.AddArguments("--log-level=3");
            if (Program.proxy_type.ToLower() == "off")
            {
            }
            if (Program.proxy_type.ToLower() == "http")
            {
                option.AddArguments("--proxy-server=http://" + proxy);
            }
            if (Program.proxy_type.ToLower() == "socks4")
            {
                option.AddArguments("--proxy-server=socks4://" + proxy);
            }
            if (Program.proxy_type.ToLower() == "socks5")
            {
                option.AddArguments("--proxy-server=socks5://" + proxy);
            }
            service.SuppressInitialDiagnosticInformation = true;
            IWebDriver driver = new ChromeDriver(service, option);

            try
            {

                driver.Navigate().GoToUrl("https://accounts.spotify.com/en/login/");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
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
            catch
            {

                driver.Close();
                return false;
            }
                try
                {

                WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait2.Until(ExpectedConditions.UrlContains("/status"));
                driver.Url = Program.url;
                starter++;
                try
                {
                    wait2.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"main\"]/div/div[2]/div[3]/main/div[2]/div[2]/div/div/div[2]/section/div[2]/div[2]/div/button[1]"))).Click();
                }
                catch
                {
                    wait2.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"main\"]/div/div[2]/div[3]/main/div[2]/div[2]/div/div/div[2]/section/div[3]/div/button[1]"))).Click();
                }
                count++;
                Console.WriteLine("[Playing playlist] Email: " + email + " Password: " + password, Color.FromArgb(30, 215, 96));
                return true;
            }
            catch
            {
                Console.WriteLine("FAILED....");
                driver.Close();
                return false;
            }
        }
    }
}
