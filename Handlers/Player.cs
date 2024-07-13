using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Random = System.Random;


namespace NightMode.Handlers;

public class Player
{
    
    /// <summary>
    /// Disable radio drain
    /// </summary>
    /// <param name="e"></param>
    public static void OnPlayerUsingRadioBattery(UsingRadioBatteryEventArgs e)
    {
        e.Drain = 0;
        if (e.IsAllowed) e.IsAllowed = false;
    }
    
    /// <summary>
    /// Locks radio to a specific channel
    /// </summary>
    /// <param name="e"></param>
    public static void OnPlayerChangingRadioRange(ChangingRadioPresetEventArgs e)
    {
        if (e.NewValue != RadioRange.Ultra) e.NewValue = RadioRange.Ultra;
    }
    
    /// <summary>
    /// What to do when the player spawns based on if an event is active
    /// </summary>
    /// <param name="e"></param>
    public static void OnPlayerSpawned(SpawnedEventArgs e)
    {
        
        if (!e.Player.IsScp && e.Player.IsAlive)
        {
            Log.Debug(Nightmode.Instance.Config.nightmode_toggled.ToString());
            Log.Debug(
                "Player has not any form of illumination (minions heh)(btw idk why i wrote minions cringe ash shit but ill keep it)! giving one...");
            e.Player.AddItem(ItemType.Flashlight);
            e.Player.Broadcast(new Exiled.API.Features.Broadcast("You have been given a flashlight!"));
        }
    }
    
    /// <summary>
    /// if enabled from config give player a random item ONCE per round
    /// </summary>
    /// <param name="e"></param>
    public static void FlippingCoin(FlippingCoinEventArgs e)
    {
        var flipped = false;
        var rand = new Random();
        var val = Enum.GetValues(typeof(ItemType));
        if (!e.Player.SessionVariables.TryGetValue("flipped_success", out var flip_obj))
            e.Player.SessionVariables.Add("flipped_success", false);
        if (!e.IsTails && !(bool)e.Player.SessionVariables["flipped_success"])
        {
            var item = (ItemType)val.GetValue(rand.Next(val.Length));
            Log.Debug("item is " + item);
            e.Player.AddItem(item);
            e.Player.SessionVariables["flipped_success"] = true;
            e.Player.Broadcast(new Exiled.API.Features.Broadcast("Luck smiles on you you flip heads and you get " +
                                                                 item, 5));
        }
    }
}