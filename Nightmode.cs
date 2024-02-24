using System;
using System.Collections.Generic;
using HarmonyLib;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Loader;
using NightMode.API;
using server = Exiled.Events.Handlers.Server;
using player = Exiled.Events.Handlers.Player;
using Player = Exiled.Events.Handlers.Player;

namespace NightMode
{
    public class Nightmode : Plugin<Config> 
    {
        
        public static Nightmode Instance { get; } = new Nightmode();
        private Nightmode() { }

        public override PluginPriority Priority { get; } = PluginPriority.Default;

        private Handlers.Server server;
        private Handlers.Player player;
        public static readonly Dictionary<string, PlayerData> PlayerData = new Dictionary<string, PlayerData>();

        private int _patchesCounter;
        private Harmony Harmony { get; set; }

        public override void OnEnabled()
        {
            base.OnEnabled();
            RegisterEvents();
            Patch();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            UnregisterEvents();
            Unpatch();
        }

        private void Patch()
        {
            try
            {
                Harmony = new Harmony($"NightMode.{++_patchesCounter}");
                bool lastDebugStatus = Harmony.DEBUG;
                Harmony.DEBUG = true;

                Harmony.PatchAll();

                Harmony.DEBUG = lastDebugStatus;
                Log.Debug("Patches applied successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void Unpatch()
        {
            Harmony.UnpatchAll();

            Log.Debug("Patches have been undone!");
        }
        
        private void RegisterEvents()
        {
            server = new Handlers.Server();
            player = new Handlers.Player();
                
            Player.Left += Handlers.Player.OnPlayerLeft;
            Player.Joined += Handlers.Player.OnPlayerJoin;
        }

        private void UnregisterEvents()
        {
            Player.Left -= Handlers.Player.OnPlayerLeft;
            Player.Joined -= Handlers.Player.OnPlayerJoin;
            server = null;
            player = null;  
        }
            
    }
}