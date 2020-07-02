using Smod2.API;
using UnityEngine;
using RemoteAdmin;

namespace TBC
{
    public class DynamicBroadcast : MonoBehaviour
    {
        private float timer = 0f;
        private readonly float timeIsUp = 1.0f;
        public float radius;
        public string text;
        public float time;
        private float progress = 0f;
        private Player target;

        public void Start()
        {
            foreach (Player p in Global.plugin.Server.GetPlayers())
            {
                if (p.PlayerId == gameObject.GetComponent<QueryProcessor>().PlayerId)
                {
                    target = p;
                    break;
                }
            }
        }

        public void Update()
        {
            timer = timer + Time.deltaTime;
            if (timer >= timeIsUp)
            {
                timer = 0f;
                progress = progress + timeIsUp;
                if (progress >= time && time != -1)
                {
                    Destroy(gameObject.GetComponent<DynamicBroadcast>());
                }
                foreach (Player p in Global.plugin.Server.GetPlayers())
                {
                    if (Vector.Distance(p.GetPosition(), target.GetPosition()) <= radius)
                    {
                        p.PersonalClearBroadcasts();
                        p.PersonalBroadcast(1, text, true);
                    }
                }
            }
        }
    }
}
