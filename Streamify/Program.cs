using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streamify.NewFolder1;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;
using Streamify.Discord;
using System.Diagnostics;
using System.ComponentModel;

namespace Streamify
{
    class Program
    {
        public static string url;
        public static string proxy_type;
        public static int max_bots;
       public static void Main()
        {
            Console.Title = "Streamify";
            RPC.RPCDiscord("Doing somethings i guess");
            logo();
            Console.Write("[");
            Console.Write("1", Color.FromArgb(30, 215, 96));
            Console.Write("] ");
            Console.WriteLine("Start streaming PLAYLIST");
            Console.Write("[");
            Console.Write("2", Color.FromArgb(30, 215, 96));
            Console.Write("] ");
            Console.WriteLine("Start Following account");
            Console.Write("[");
            Console.Write("3", Color.FromArgb(30, 215, 96));
            Console.Write("] ");
            Console.WriteLine("Start following playlist");
            Console.Write("[");
            Console.Write("4", Color.FromArgb(30, 215, 96));
            Console.Write("] ");
            Console.WriteLine("Start streaming artist");
            Console.Write("[");
            Console.Write("5", Color.FromArgb(30, 215, 96));
            Console.Write("] ");
            Console.WriteLine("Exit");
            Console.WriteLine();

            Console.Write("[");
            Console.Write("INPUT", Color.FromArgb(30, 215, 96));
            Console.Write("] ");
            try
            {
                int input = Convert.ToInt32(Console.ReadLine());
                if (input == 1)
                {
                    Console.Write("Playlist -> ");
                    url = Console.ReadLine();
                    Console.WriteLine("Only use good and proxy that are not banned on spotify otherwise you write \"off\" ");
                    Console.Write("Proxy type -> ");
                    proxy_type = Console.ReadLine();
                    Console.Write("Bots limit -> ");
                    try
                    {
                        max_bots = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Failed so i use the default -> \"10\" ");
                        max_bots = 10;
                    }
                    streamer.start();
                }
                if (input == 2)
                {
                    Console.Write("Account -> ");
                    url = Console.ReadLine();
                    Console.Write("Follow limit -> ");
                    try
                    {
                        max_bots = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Failed so i use the default -> \"10\" ");
                        max_bots = 10;
                    }
                    follow.start();
                }
                if (input == 3)
                {
                    Console.Write("Playlist -> ");
                    url = Console.ReadLine();
                    Console.Write("Bots limit -> ");
                    try
                    {
                        max_bots = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Failed so i use the default -> \"10\" ");
                        max_bots = 10;
                    }
                    playlist_follow.start();
                }
                if (input == 4)
                {
                    Console.Write("Artist  -> ");
                    url = Console.ReadLine();
                    Console.WriteLine("Only use good and proxy that are not banned on spotify otherwise you write \"off\" ");
                    Console.Write("Proxy type -> ");
                    proxy_type = Console.ReadLine();
                    Console.Write("Bots limit -> ");
                    try
                    {
                        max_bots = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Failed so i use the default -> \"10\" ");
                        max_bots = 10;
                    }
                    Artist_player.start();
                }
                if (input == 5)
                {
                    Process[] chromeInstances = Process.GetProcessesByName("chromedriver");
                    foreach (Process p in chromeInstances)
                        p.Kill();
                    Environment.Exit(0);
                }
            }
            catch 
             {
                Console.Clear();
                Console.WriteLine("[Error input] failed to read input", ColorTranslator.FromHtml("#cc0000"));
                Main();
            }
            Console.ReadLine();
        }

        public static void logo()
        {
            Console.WriteLine(@"

███████╗████████╗██████╗ ███████╗ █████╗ ███╗   ███╗██╗███████╗██╗   ██╗
██╔════╝╚══██╔══╝██╔══██╗██╔════╝██╔══██╗████╗ ████║██║██╔════╝╚██╗ ██╔╝
███████╗   ██║   ██████╔╝█████╗  ███████║██╔████╔██║██║█████╗   ╚████╔╝ 
╚════██║   ██║   ██╔══██╗██╔══╝  ██╔══██║██║╚██╔╝██║██║██╔══╝    ╚██╔╝  
███████║   ██║   ██║  ██║███████╗██║  ██║██║ ╚═╝ ██║██║██║        ██║   
╚══════╝   ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝╚═╝        ╚═╝   
                                                                        

             [ Created by Vanix#9999 ]



", Color.FromArgb(30, 215, 96));




        }
    }
}
