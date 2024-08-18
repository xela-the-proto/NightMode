using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;
using MEC;
using Player_exiled_handler = Exiled.Events.Handlers.Player;
using Player = NightMode.Handlers.Player;
using Server_exiled_handler = Exiled.Events.Handlers.Server;
using Server = NightMode.Handlers.Server;
using Nuke_exiled_handler = Exiled.Events.Handlers.Warhead;
using Nuke = NightMode.Handlers.Nuke;

namespace NightMode;

public class Nightmode : Plugin<Config>
{
    private int _patchesCounter;

    public static Nightmode Instance { get; } = new();

    public override PluginPriority Priority { get; } = PluginPriority.Default;
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
        if (Instance.Config.RadioDrain)
        {
            Log.Debug("Registering battery usage...");
            Player_exiled_handler.UsingRadioBattery += Player.OnPlayerUsingRadioBattery;
        }

        if (Instance.Config.UL)
        {
            Log.Debug("Registering radio preset...");
            Player_exiled_handler.ChangingRadioPreset += Player.OnPlayerChangingRadioRange;
        }

        if (Instance.Config.nightmode_toggled) Player_exiled_handler.Spawned += Player.OnPlayerSpawned;

        if (Instance.Config.FlipRand) Player_exiled_handler.FlippingCoin += Player.FlippingCoin;

        Nuke_exiled_handler.Starting += Nuke.onNukeStart;
        Nuke_exiled_handler.Stopping += Nuke.onNukeStop;
        Server_exiled_handler.WaitingForPlayers += Server.onServerStarting;
        Server_exiled_handler.RoundStarted += Server.onRoundStart;
        Server_exiled_handler.RestartingRound += Server.onRoundRestarting;
    }

    private void UnregisterEvents()
    {
        if (Instance.Config.RadioDrain) Player_exiled_handler.UsingRadioBattery -= Player.OnPlayerUsingRadioBattery;

        if (Instance.Config.UL) Player_exiled_handler.ChangingRadioPreset -= Player.OnPlayerChangingRadioRange;

        if (Instance.Config.FlipRand) Player_exiled_handler.FlippingCoin -= Player.FlippingCoin;

        Player_exiled_handler.Spawned -= Player.OnPlayerSpawned;
        Nuke_exiled_handler.Starting -= Nuke.onNukeStart;
        Nuke_exiled_handler.Stopping -= Nuke.onNukeStop;
        Server_exiled_handler.WaitingForPlayers -= Server.onServerStarting;
        Server_exiled_handler.RoundStarted -= Server.onRoundStart;
    }
    
    
}