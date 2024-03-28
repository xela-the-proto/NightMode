using System.Linq;
using CommandSystem.Commands.RemoteAdmin.Cleanup;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PluginAPI.Core.Items;
using UnityEngine;
using Log = PluginAPI.Core.Log;
using Random = System.Random;


namespace NightMode.Handlers
{
    public class Player
    {
        public static void OnPlayerUsingItem(UsedItemEventArgs e)
        {
            Random rand = new Random();
            Log.Debug($"checking if item is throwable: {e.Item.IsThrowable}");
            if (e.Item.IsThrowable)
            {
                Log.Debug("Detected throwable");
                /*
                var rand_int = rand.Next(0, 100);
                if (rand_int >= 50)
                {
                */
                    e.Item.Destroy();
                    Log.Debug($"Destroyed {e.Item.ToString()}");
                //}
            }
        }
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
            if (!e.Player.IsScp && e.Player.IsAlive)
            {
                Log.Debug(Nightmode.Instance.Config.nightmode_toggled.ToString());
                Log.Debug("Player has not any form of illumination (minions heh)(btw idk why i wrote minions cringe ash shit but ill keep it)! giving one...");
                e.Player.AddItem(ItemType.Flashlight);
                e.Player.Broadcast(new Exiled.API.Features.Broadcast("You have been given a flashlight!"));
            }
        }

        public static void OnBallHit(HurtingEventArgs e)
        {
            if (e.DamageHandler.Type == DamageType.Scp018)
            {
                Random random = new Random();
                var rand = random.Next(1, 100);
                if (rand >= 51)
                {
                    
                }
            }
        }
    }
}