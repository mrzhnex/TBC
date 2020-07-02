using Smod2.API;
using Smod2.Commands;
using UnityEngine;

namespace TBC
{
    internal class SBCCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "description";
        }

        public string GetUsage()
        {
            return "Usage: sbc <player> <radius> <text>";
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
            Player target = null;
            target = Global.GetPlayer(args[0], out target);
            if (target == null)
            {
                return new string[] { "Player not found." + GetUsage() };
            }
            if (target.TeamRole.Team == Smod2.API.Team.SPECTATOR)
            {
                return new string[] { "Player is spectator." + GetUsage() };
            }
            if (!float.TryParse(args[1], out float radius))
            {
                return new string[] { "Wrong radius." + GetUsage() };
            }
            if (radius < 0)
            {
                return new string[] { "Wrong radius." + GetUsage() };
            }
            string text = "";

            for (int i = 2; i < args.Length; i++)
            {
                text = text + args[i] + " ";
            }

            GameObject.FindWithTag("FemurBreaker").GetComponent<StaticBroadcast>().staticBroadcasts.Add(target.GetPosition(), new string[]
            {
                radius.ToString(),
                text
            });


            Global.plugin.Info("Admin " + (sender as Player).Name + " run sbc command: target_position: " + target.Name + ", radius: " + radius + ", text: " + text);
            return new string[] { "Broadcast add: target_position: " + target.Name + ", radius: " + radius + ", text: " + text };
        }
    }
}