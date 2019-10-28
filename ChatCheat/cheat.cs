using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using System.Windows.Forms;
using System.Drawing;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace ChatCheat
{

    public class cheat : Script
    {
        public static State state = new State();

        private const int bufSize = 8 * 1024;

        public cheat()
        {
            state = new State();

            ShowText("ChatCheat Reloaded");
        }
        public class State
        {
            public static byte[] buffer = new byte[bufSize];
            public static ConcurrentQueue<string> queue = new ConcurrentQueue<string>();

        }


        private void OnTick(object sender, EventArgs e)
        {
            CheckQueue();
            
        }

        private void CheckQueue()
        {
            if(State.queue.IsEmpty)
            {
                return;
            }

            string line;
            while(State.queue.TryDequeue(out line))
            {
                var parts = line.Trim().Split(' ');

                if(parts.Length < 3)
                {
                    return;
                }

                var from = parts[0];
                var id = parts[1];
                var name = parts[2];

                Call(from, id, name, parts.Skip(3));
            }
        }

        public void Call(String from, String id, String name, IEnumerable<String> rest)
        {
            Ped player = Game.Player.Character;

            if(player == null)
            {
                return;
            }

            player.Health = 100;
        }

        public void ShowText(String text)
        {
            GTA.UI.Notification.Show(text);
        }
    }


}
