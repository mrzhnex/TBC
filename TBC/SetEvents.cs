using Smod2.EventHandlers;
using Smod2.Events;
using UnityEngine;

namespace TBC
{
    internal class SetEvents : IEventHandler, IEventHandlerRoundStart, IEventHandlerSetRole
    {
        public SetEvents(MainSettings mainSettings)
        {
            Global.plugin = mainSettings;
        }

        public void OnRoundStart(RoundStartEvent ev)
        {
            GameObject.FindWithTag("FemurBreaker").AddComponent<StaticBroadcast>();
        }

        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            if ((ev.Player.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>() != null)
            {
                Object.Destroy((ev.Player.GetGameObject() as GameObject).GetComponent<DynamicBroadcast>());
            }
        }
    }
}