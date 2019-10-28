using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;


namespace ChatBot
{
    
    class chat
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            Console.ReadLine();
        }

    }

    class Bot
    {
        TwitchClient client;

        public Bot()
        {
            ConnectionCredentials credentials = new ConnectionCredentials("chatcheat", "oauth:df6b12m7fmydpk57jy0namdlek47s5");

            client = new TwitchClient();
            client.Initialize(credentials, "osurce");

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnConnected += Client_OnConnected;

            client.Connect();
            
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("ChatCheat - connected!");
            client.SendMessage(e.Channel, "ChatCheat - connected!");
        }



        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if(e.ChatMessage.Message.Contains("test"))
            {

                string user = e.ChatMessage.DisplayName;
                string user_id = e.ChatMessage.Id;
                string message_name = e.ChatMessage.Message;

                //would add chat message to queue
                ChatCheat.cheat.State.queue.Enqueue(new ChatCheat.cheat.Message(user, user_id, message_name));

                client.SendMessage(e.ChatMessage.Channel, "debug - success");
            } 
            
        }

    }

}
