﻿using System;
using AudioPlayer.API;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;
using Random = System.Random;


namespace NightMode.Handlers;

public class Player
{
    /// <summary>
    ///     Disable radio drain
    /// </summary>
    /// <param name="e"></param>
    public static void OnPlayerUsingRadioBattery(UsingRadioBatteryEventArgs e)
    {
        e.Drain = 0;
    }

    /// <summary>
    ///     Locks radio to a specific channel
    /// </summary>
    /// <param name="e"></param>
    public static void OnPlayerChangingRadioRange(ChangingRadioPresetEventArgs e)
    {
        if (e.NewValue != RadioRange.Ultra) e.NewValue = RadioRange.Ultra;
    }

    /// <summary>
    ///     What to do when the player spawns based on if an event is active
    /// </summary>
    /// <param name="e"></param>
    public static void OnPlayerSpawned(SpawnedEventArgs e)
    {
        if (!e.Player.IsScp && e.Player.IsAlive && Nightmode.Singleton.Config.nightmode_toggled)
        {
            Log.Debug(Nightmode.Singleton.Config.nightmode_toggled.ToString());
            Log.Debug(
                "Player hasn't got any flashlight giving one...");
            e.Player.AddItem(ItemType.Flashlight);
            e.Player.Broadcast(new Exiled.API.Features.Broadcast("You have been given a flashlight!"));
        }

        //save a players room in case they get stuck / clip out
        Exiled.API.Features.Server.ExecuteCommand("stuckService");
    }

    /// <summary>
    ///     if enabled from config give player a random item ONCE per round
    /// </summary>
    /// <param name="e"></param>
    public static void FlippingCoin(FlippingCoinEventArgs e)
    {
        var rand = new Random();
        var val = Enum.GetValues(typeof(ItemType));

        if (!e.Player.SessionVariables.ContainsKey("flipped_success"))
            e.Player.SessionVariables.Add("flipped_success", false);
        if (!e.IsTails && !(bool)e.Player.SessionVariables["flipped_success"])
        {
            var item = (ItemType)val.GetValue(rand.Next(val.Length));
            Log.Debug("item is " + item);
            e.Player.AddItem(item);
            e.Player.SessionVariables["flipped_success"] = true;
            e.Player.Broadcast(
                new Exiled.API.Features.Broadcast("Luck smiles on you you flip heads and you get an item", 5));
        }
    }

    public static void Escaping(EscapingEventArgs e)
    {
        if (e.EscapeScenario == EscapeScenario.ClassD && Nightmode.Singleton.Config.speedrun_toggled)
        {
            Timing.KillCoroutines("nuke_routine");
            AudioController.StopAudio();
            Map.ChangeLightsColor(Color.green);
            Round.EndRound();
        }
    }
    
}