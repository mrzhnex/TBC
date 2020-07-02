using Smod2.API;
using Smod2.Commands;
using System.Collections.Generic;

namespace TBC
{
    internal class BSpawnCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "description";
        }

        public string GetUsage()
        {
            return "Usage: bspawn <teamID> <roleID> <player> | bspawn <teamlist>";
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
                Global.plugin.Info("Admin " + (sender as Player).Name + " run bspawn command: <teamlist>");
                return new string[] { answer };
            }
            if (args.Length < 2)
            {
                return new string[] { "Out of args." + GetUsage() };
            }
            if (!int.TryParse(args[0], out int teamId))
            {
                return new string[] { "Wrong teamId." + GetUsage() };
            }
            if (!Global.teamlist.ContainsKey(teamId))
            {
                return new string[] { "Wrong teamId." + GetUsage() };
            }
            if (!int.TryParse(args[1], out int roleId))
            {
                return new string[] { "Wrong roleId." + GetUsage() };
            }
            if (roleId < 0 || roleId > 17)
            {
                return new string[] { "Wrong roleId." + GetUsage() };
            }
            bool spawn_on_player = false;
            Player spawn_player = null;
            if (args.Length > 2)
            {
                spawn_on_player = true;
                spawn_player = Global.GetPlayer(args[2], out spawn_player);
                if (spawn_player == null)
                {
                    return new string[] { "SpawnPlayer not found." + GetUsage() };
                }
            }
            
            foreach (Player player in Global.plugin.Server.GetPlayers())
            {
                if (player.TeamRole.Team == Global.teamlist[teamId])
                {
                    player.ChangeRole(Global.rolelist[roleId]);
                    if (spawn_on_player)
                    {
                        player.Teleport(spawn_player.GetPosition(), false);
                    }
                }
            }
            if (spawn_on_player)
            {
                Global.plugin.Info("Admin " + (sender as Player).Name + " run bspawn command: team: " + Global.teamlist[teamId] + ", role: " + Global.rolelist[roleId] + ", onPlayerSpawn: " + spawn_player.Name);
                return new string[] { "ForceClass: team: " + Global.teamlist[teamId] + ", role: " + roleId + ", onPlayerSpawn: " + spawn_player.Name };
            }
            else
            {
                Global.plugin.Info("Admin " + (sender as Player).Name + " run bspawn command: team: " + Global.teamlist[teamId] + ", role: " + Global.rolelist[roleId] + ", onPlayerSpawn: none");
                return new string[] { "ForceClass: team: " + Global.teamlist[teamId] + ", role: " + roleId + ", onPlayerSpawn: none" };
            }
        }

    }
}