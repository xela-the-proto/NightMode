using System;
using Exiled.API.Features;
using HarmonyLib;
using Player_exiled_handler = Exiled.Events.Handlers.Player;
using Player = NightMode.Handlers.Player;
using Server_exiled_handler = Exiled.Events.Handlers.Server;
using Server = NightMode.Handlers.Server;
using Nuke_exiled_handler = Exiled.Events.Handlers.Warhead;
using Nuke = NightMode.Handlers.Nuke;
using PluginPriority = Exiled.API.Enums.PluginPriority;

namespace NightMode;

public class Nightmode : Plugin<Config>
{
    private int _patchesCounter;

    public static Nightmode Singleton { get; private set; }
    public override PluginPriority Priority { get; } = PluginPriority.Default;
    private Harmony Harmony { get; set; }

    public override void OnEnabled()
    {
        Singleton = this;
        RegisterEvents();
        Patch();
    }

    public override void OnDisabled()
    {
        Singleton = null;
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
        if (Singleton.Config.RadioDrain)
        {
            Player_exiled_handler.UsingRadioBattery += Player.OnPlayerUsingRadioBattery;
        }

        if (Singleton.Config.UL)
        {
            Player_exiled_handler.ChangingRadioPreset += Player.OnPlayerChangingRadioRange;
        }

        if (Singleton.Config.nightmode_toggled) Player_exiled_handler.Spawned += Player.OnPlayerSpawned;

        if (Singleton.Config.FlipRand) Player_exiled_handler.FlippingCoin += Player.FlippingCoin;

        Nuke_exiled_handler.Starting += Nuke.onNukeStart;
        Nuke_exiled_handler.Stopping += Nuke.onNukeStop;
        Server_exiled_handler.WaitingForPlayers += Server.onServerStarting;
        Server_exiled_handler.RoundStarted += Server.onRoundStart;
    }

    private void UnregisterEvents()
    {
        if (Singleton.Config.RadioDrain) Player_exiled_handler.UsingRadioBattery -= Player.OnPlayerUsingRadioBattery;

        if (Singleton.Config.UL) Player_exiled_handler.ChangingRadioPreset -= Player.OnPlayerChangingRadioRange;

        if (Singleton.Config.FlipRand) Player_exiled_handler.FlippingCoin -= Player.FlippingCoin;

        Player_exiled_handler.Spawned -= Player.OnPlayerSpawned;
        Nuke_exiled_handler.Starting -= Nuke.onNukeStart;
        Nuke_exiled_handler.Stopping -= Nuke.onNukeStop;
        Server_exiled_handler.WaitingForPlayers -= Server.onServerStarting;
        Server_exiled_handler.RoundStarted -= Server.onRoundStart;
    }
    
    
}