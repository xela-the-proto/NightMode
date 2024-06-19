using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;
using player = Exiled.Events.Handlers.Player;
using Player = NightMode.Handlers.Player;

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
        if (Instance.Config.RadioDrain)
        {
            Log.Debug("Registering battery usage...");
            player.UsingRadioBattery += Player.OnPlayerUsingRadioBattery;
        }

        if (Instance.Config.UL)
        {
            Log.Debug("Registering radio preset...");
            player.ChangingRadioPreset += Player.OnPlayerChangingRadioRange;
        }

        if (Instance.Config.nightmode_toggled)
        {
            player.Spawned += Player.OnPlayerSpawned;
        }

        if (Instance.Config.FlipRand)
        {
            player.FlippingCoin += Player.FlippingCoin;
        }
    }

    private void UnregisterEvents()
    {
        if (Instance.Config.RadioDrain) player.UsingRadioBattery -= Player.OnPlayerUsingRadioBattery;

        if (Instance.Config.UL) player.ChangingRadioPreset -= Player.OnPlayerChangingRadioRange;

        player.Spawned -= Player.OnPlayerSpawned;

        if (Instance.Config.FlipRand) player.FlippingCoin -= Player.FlippingCoin;
    }
}