using Smod2.API;
using Smod2.Commands;

namespace TBC
{
    internal class RBCCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "description";
        }

        public string GetUsage()
        {
            return "Usage: rbc <radius> <time(sec)> <text>";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (args == null)
            {
                return new string[] { "Out of args." + GetUsage() };
            }
            if (args.Length < 3)
            {
                return new string[] { "Out of args." + GetUsage() };
            }
            if (!int.TryParse(args[0], out int radius))
            {
                return new string[] { "Wrong radius." + GetUsage() };
            }
            if (radius < 0)
            {
                return new string[] { "Wrong radius." + GetUsage() };
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
                if (Vector.Distance((sender as Player).GetPosition(), player.GetPosition()) <= radius)
                {
                    player.PersonalClearBroadcasts();
                    player.PersonalBroadcast(time, text, true);
                }
            }
            Global.plugin.Info("Admin " + (sender as Player).Name + " run rbc command: Broadcast radius: " + radius + ", time: " + time + ", text: " + text);
            return new string[] { "Broadcast radius: " + radius + ", time: " + time + ", text: " + text };
        }
    }
}