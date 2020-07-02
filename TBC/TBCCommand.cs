using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;

namespace TBC
{
    internal class TBCCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "description";
        }

        public string GetUsage()
        {
            return "Usage: tbc <teamID> <time(sec)> <text> | tbc <teamlist>";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (args == null)
            {
                return new string[] { "Out of args." + GetUsage() };
            }
            if (args.Length < 1)
            {
                return new string[] { "Out of args." + GetUsage() };
            }
            if (args[0] == "teamlist")
            {
                string answer = "\n";
                foreach (KeyValuePair<int, Smod2.API.Team> kvp in Global.teamlist)
                {
                    answer = answer + kvp.Value + ": " + kvp.Key + "\n";
                }
                Global.plugin.Info("Admin " + (sender as Player).Name + " run tbc command: <teamlist>");
                return new string[] { answer };
            }
            if (args.Length < 3)
            {
                return new string[] { "Out of args." + GetUsage() };
            }
            if (!int.TryParse(args[0], out int id))
            {
                return new string[] { "Wrong id." + GetUsage() };
            }
            if (!Global.teamlist.ContainsKey(id))
            {
                return new string[] { "Wrong id." + GetUsage() };
            }
            if (!uint.TryParse(args[1], out uint time))
            {
                return new string[] { "Wrong time." + GetUsage() };
            }
            if (time < 0)
            {
                return new string[] { "Wrong time." + GetUsage() };
            }
            string text = "";
            for (int i = 2; i < args.Length; i++)
            {
                text = text + args[i] + " ";
            }
            foreach (Player player in Global.plugin.Server.GetPlayers())
            {
                if (player.TeamRole.Team == Global.teamlist[id])
                {
                    player.PersonalClearBroadcasts();
                    player.PersonalBroadcast(time, text, true);
                }
            }
            Global.plugin.Info("Admin " + (sender as Player).Name + " run tbc command: Broadcast to: " + Global.teamlist[id] + ", time: " + time + ", text: " + text);
            return new string[] { "Broadcast to: " + Global.teamlist[id] + ", time: " + time + ", text: " + text };
        }

    }
}