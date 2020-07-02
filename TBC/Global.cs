using Smod2;
using Smod2.API;
using System;
using System.Collections.Generic;

namespace TBC
{
    public static class Global
    {
        public static Plugin plugin;

        public static readonly Dictionary<int, Smod2.API.Team> teamlist = new Dictionary<int, Smod2.API.Team>()
        {
            {1, Smod2.API.Team.SPECTATOR },
            {2, Smod2.API.Team.CLASSD },
            {3, Smod2.API.Team.SCIENTIST },
            {4, Smod2.API.Team.NINETAILFOX },
            {5, Smod2.API.Team.CHAOS_INSURGENCY },
            {6, Smod2.API.Team.SCP },
            {7, Smod2.API.Team.TUTORIAL }
        };

        public static Player GetPlayer(string args, out Player playerOut)
        {
            Player player = null;
            int id = -1;
            string name = "";
            try
            {
                id = Convert.ToInt32(args.ToLower());
            }
            catch (FormatException)
            {
                id = -1;
                name = args.ToLower();
            }
            foreach (Player player2 in plugin.Server.GetPlayers())
            {
                if (id == -1)
                {
                    if (player2.Name.ToLower().Contains(name.ToLower()) || player2.Name.ToLower() == name.ToLower())
                    {
                        player = player2;
                    }
                }
                else
                {
                    if (player2.PlayerId == id)
                    {
                        player = player2;
                    }
                }
            }
            playerOut = player;
            return playerOut;
        }

        public static readonly Dictionary<int, Role> rolelist = new Dictionary<int, Role>()
        {
            {0, Role.SCP_173 },
            {1, Role.CLASSD },
            {2, Role.SPECTATOR },
            {3, Role.SCP_106 },
            {4, Role.NTF_SCIENTIST },
            {5, Role.SCP_049 },
            {6, Role.SCIENTIST },
            {7, Role.SCP_079 },
            {8, Role.CHAOS_INSURGENCY },
            {9, Role.SCP_096 },
            {10, Role.SCP_049_2 },
            {11, Role.NTF_LIEUTENANT },
            {12, Role.NTF_COMMANDER },
            {13, Role.NTF_CADET },
            {14, Role.TUTORIAL },
            {15, Role.FACILITY_GUARD },
            {16, Role.SCP_939_53 },
            {17, Role.SCP_939_89 },

        };
    }
}
