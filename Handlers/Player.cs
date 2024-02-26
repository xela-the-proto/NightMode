using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;
using Log = PluginAPI.Core.Log;


namespace NightMode.Handlers
{
    public class Player
    {
        public static void OnPlayerUsingRadioBattery(UsingRadioBatteryEventArgs e)
        {
            e.Drain = 0;
            if (e.IsAllowed)
            {
                e.IsAllowed = false;
            }
        }

        public static void OnPlayerChangingRadioRange(ChangingRadioPresetEventArgs e)
        {
            if (e.NewValue != RadioRange.Ultra)
            {
                e.NewValue = RadioRange.Ultra;
            }
        }

        public static void OnPlayerSpawned(SpawnedEventArgs e)
        {
            if (Nightmode.Instance.Config.nightmode_toggled)
            {
                if (!e.Player.IsScp)
                {
                    Log.Debug(Nightmode.Instance.Config.nightmode_toggled.ToString());
                    Log.Debug("Player has not any form of illumination! giving one...");
                    e.Player.AddItem(ItemType.Flashlight);
                    e.Player.Broadcast(new Exiled.API.Features.Broadcast("You have been given a flashlight!"));
                }
            }
        }
    }
}