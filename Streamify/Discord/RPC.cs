using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;
using System.IO;

namespace Streamify.Discord
{
    class RPC
    {
        public static void RPCDiscord(string text)
        {
            try
            {
                discordRpcClient = new DiscordRpcClient("924864671792631868");
                discordRpcClient.Initialize();
                discordRpcClient.SetPresence(new RichPresence
                {
                    Details = "Spotify services",
                    State = text,
                    Timestamps = Timestamps.Now,
                    Assets = new Assets
                    {
                        LargeImageKey = "rpc",
                        LargeImageText = "Created by Vanix#9999"
                    }
                });
            }
            catch (Exception)
            {
                Console.WriteLine("[DISCORD-RPC] FAILED TO START");
            }

        }

        public static DiscordRpcClient discordRpcClient;
    }
}
