using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;
using Player_exiled_handler = Exiled.Events.Handlers.Player;
using Player = NightMode.Handlers.Player;
using Nuke_exiled_handler = Exiled.Events.Handlers.Warhead;
using Nuke = NightMode.Handlers.Nuke;

namespace NightMode;

public class Nightmode : Plugin<Config>
{
    private int _patchesCounter;

    private Nightmode()
    {
    }

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
            var lastDebugStatus = Harmony.DEBUG;
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
        //TODO: when i will have to do events rewrite these if's
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

        if (Instance.Config.nightmode_toggled)
        {
            Player_exiled_handler.Spawned += Player.OnPlayerSpawned;
        }

        if (Instance.Config.FlipRand)
        {
            Player_exiled_handler.FlippingCoin += Player.FlippingCoin;
        }

        Nuke_exiled_handler.Starting += Nuke.onNukeStart;
        Nuke_exiled_handler.Stopping += Nuke.onNukeStop;
    }

    private void UnregisterEvents()
    {
        if (Instance.Config.RadioDrain) Player_exiled_handler.UsingRadioBattery -= Player.OnPlayerUsingRadioBattery;

        if (Instance.Config.UL) Player_exiled_handler.ChangingRadioPreset -= Player.OnPlayerChangingRadioRange;

        Player_exiled_handler.Spawned -= Player.OnPlayerSpawned;

        if (Instance.Config.FlipRand) Player_exiled_handler.FlippingCoin -= Player.FlippingCoin;
        Nuke_exiled_handler.Starting -= Nuke.onNukeStart;
        Nuke_exiled_handler.Stopping -= Nuke.onNukeStop;
    }
}