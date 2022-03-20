using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Web;
using System.Text.RegularExpressions;
using Colorful;
using System.Drawing;
using Console = Colorful.Console;
using Streamify.API;

namespace Streamify.API.Support
{
    class LocalHost
    {
        static HttpListener _httpListener = new HttpListener();
        public static void host(int arg)
        {
            checker = arg;
            _httpListener.Prefixes.Add("http://localhost:8080/");
            _httpListener.Start();
            Thread _responseThread = new Thread(ResponseThread);
            _responseThread.Start(); // start the response thread
        }

        public static string Splitter(string orignal, string left, string right)
        {
            return orignal.Split(new string[]
            {
                left
            }, StringSplitOptions.None)[1].Split(new string[]
            {
                right
            }, StringSplitOptions.None)[0];
        }

        public static HttpListenerContext context;
        public static string Context = "";
        public static int checker;
        static void ResponseThread()
        {
            while (true)
            {
                context = _httpListener.GetContext();
                var request = context.Request;
                Context = request.Url.PathAndQuery;

                if (Context.Contains("/?code"))
                {
                    Follower.Token_user = Splitter(Context, "/?code=", "&state");
                    checker = 1;
                }
                byte[] _responseArray = Encoding.UTF8.GetBytes(@"
<html>
<head>
<style>
@import url('https://fonts.googleapis.com/css2?family=Raleway:wght@100&family=Source+Sans+Pro:ital,wght@0,200;1,300&display=swap');
body{
    background-color: #1DB954;
   font-family: 'Source Sans Pro', sans-serif;
}
p{
    color: #191414;
   font-size: 30px;
}
img{
  width: 150px;
  text-align:center;
}
</style>
  <title> SpotifyX - Key system </title>
</head>
<body>
<p style='text-align:center;'><img src='https://cdn.discordapp.com/attachments/792479438532509697/827120284959113246/Spotify_Logo_RGB_White.png' alt='logo'></p>
  <p style='text-align:center'> Please exit the website. SpotifyX got the key! - SpotifyX Key system<p>
</html>");
                context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length);
                context.Response.KeepAlive = false;
                context.Response.Close();
            }
        }
    }
}
