using UnityEngine;
using System.Collections.Generic;
using Smod2.API;

namespace TBC
{
    public class StaticBroadcast : MonoBehaviour
    {
        private float timer = 0f;
        private readonly float timeIsUp = 1.0f;

        public Dictionary<Vector, string[]> staticBroadcasts = new Dictionary<Vector, string[]>();

        public void Update()
        {
            timer = timer + Time.deltaTime;
            if (timer >= timeIsUp)
            {
                timer = 0f;
                foreach (KeyValuePair<Vector, string[]> kvp in staticBroadcasts)
                {
                    foreach (Player p in Global.plugin.Server.GetPlayers())
                    {
                        if (Vector.Distance(p.GetPosition(), kvp.Key) <= System.Convert.ToSingle(kvp.Value[0]))
                        {
                            p.PersonalClearBroadcasts();
                            p.PersonalBroadcast(1, kvp.Value[1], true);
                        }
                    }

                }
            }
        }
    }
}
