using Smod2.API;
using Smod2.Commands;
using UnityEngine;
using System.Collections.Generic;

namespace TBC
{
    internal class LBCCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "description";
        }

        public string GetUsage()
        {
            return "Usage: lbc | lbc <remove> <id>";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (args == null)
            {
                return new string[] { "Out of args." + GetUsage() };
            }
            if (args.Length > 0)
            {
                if (args[0] != "remove")
                {
                    return new string[] { "Wrong args." + GetUsage() };
                }
                if (args.Length < 2)
                {
                    return new string[] { "Out of args." + GetUsage() };
                }
                if (!int.TryParse(args[1], out int id))
                {
                    return new string[] { "Wrong radius." + GetUsage() };
                }
                if (id < 0)
                {
                    return new string[] { "Wrong radius." + GetUsage() };
                }
                int counter = 1;
                foreach (Player p in Global.plugin.Server.GetPlayers())
                {
                    if ((p.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>() != null)
                    {
                        if (counter == id)
                        {
                            Object.Destroy((p.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>());
                            Global.plugin.Info("Admin " + (sender as Player).Name + " run lbc <remove> command with id: " + id);
                            return new string[] { "Delete broadcast with id: " + id.ToString() };
                        }
                        counter = counter + 1;
                    }
                }
                foreach (KeyValuePair<Vector, string[]> kvp in GameObject.FindWithTag("FemurBreaker").GetComponent<StaticBroadcast>().staticBroadcasts)
                {
                    if (counter == id)
                    {
                        GameObject.FindWithTag("FemurBreaker").GetComponent<StaticBroadcast>().staticBroadcasts.Remove(kvp.Key);
                        Global.plugin.Info("Admin " + (sender as Player).Name + " run lbc <remove> command with id: " + id);
                        return new string[] { "Delete broadcast with id: " + id.ToString() };
                    }
                    counter = counter + 1;
                }
                return new string[] { "Broadcast not found with id: " + id.ToString() };
            }
            string answer = "\n";
            int count = 1;
            foreach (Player p in Global.plugin.Server.GetPlayers())
            {
                if ((p.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>() != null)
                {
                    answer = answer + count + ": (dynamic), (" + p.Name + ") (" + (p.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>().radius + ") (" + (p.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>().text + ")" + "\n";
                    count = count + 1;
                }
            }
            foreach (KeyValuePair<Vector, string[]> kvp in GameObject.FindWithTag("FemurBreaker").GetComponent<StaticBroadcast>().staticBroadcasts)
            {
                answer = answer + count + ": (static), (" + kvp.Key.ToString() + ") (" + kvp.Value[0] + ") (" + kvp.Value[1] + ")" + "\n";
                count = count + 1;
            }

            Global.plugin.Info("Admin " + (sender as Player).Name + " run lbc command");
            return new string[] { "<size=20>broadcast list:" + "\n" + "id: (type) (pos/target) (radius) (text)" + "\n" + answer + "</size>"};
        }
    }
}