using Smod2.API;
using Smod2.Commands;
using UnityEngine;

namespace TBC
{
    internal class DBCCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "description";
        }

        public string GetUsage()
        {
            return "Usage: dbc <player> <radius> <time> <text>";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (args == null)
            {
                return new string[] { "Out of args." + GetUsage() };
            }
            if (args.Length < 4)
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
            if (!float.TryParse(args[2], out float time))
            {
                return new string[] { "Wrong time." + GetUsage() };
            }
            string text = "";

            for (int i = 3; i < args.Length; i++)
            {
                text = text + args[i] + " ";
            }

            if ((target.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>() != null)
            {
                return new string[] { target.Name + " already broadcast: " + (target.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>().text };
            }
            (target.GetGameObject() as GameObject).AddComponent<DynamicBroadcast>();
            (target.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>().radius = radius;
            (target.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>().text = text;
            (target.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>().time = time;

            Global.plugin.Info("Admin " + (sender as Player).Name + " run dbc command: target: " + target.Name + ", radius: " + radius + ", time: " + time + ", text: " + text);
            return new string[] { "Broadcast add: target: " + target.Name + ", radius: " + radius + ", text: " + text };
        }
    }
}