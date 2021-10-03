using DiscordRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudstorageEditorV3
{
    class DiscordRPC
    {
        public DiscordRpcClient client;
        public void Start()
        {
            new Thread(() =>
            {
				client = new DiscordRpcClient("831638911855755274");

				client.OnReady += (sender, e) =>
				{
					Console.WriteLine("Welcome " + e.User.Username);
				};

				client.OnPresenceUpdate += (sender, e) =>
				{};

				client.Initialize();

				client.SetPresence(new RichPresence()
				{
					Details = "Discord.gg/6sWyTUWRb8",
					State = "Playing",
				});
			}).Start();
        }
    }
}
