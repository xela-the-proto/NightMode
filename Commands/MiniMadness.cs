using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using UnityEngine;
using Player = Exiled.API.Features.Player;
using Random = System.Random;

namespace NightMode.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class MiniMadness : ICommand
{
    public bool SanitizeResponse { get; }
    public string Command { get; } = "minimode";
    public string[] Aliases { get; } = ["minmode"];
    public string Description { get; } = "run a round of mini players";

    /// <summary>
    ///     Players get randomized heights (known glitch they fall out the map)
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="sender"></param>
    /// <param name="response"></param>
    /// <returns></returns>
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        //this shit so broken and so unfun imm ajust remove it lol
        response = "THIS EVENT IS DEPRECATED";
        return false;
        if (arguments.Array[1] == "on")
        {
            Timing.RunCoroutine(Rand_sizes(), "rand_coroutine");
            response = "Starting randomizer...";
            return true;
        }

        Log.Debug("Killing coroutine...");
        int value = Timing.KillCoroutines("rand_coroutine");
        Log.Debug($"killed {value} coroutine");
        foreach (var player in Player.List)
        {
            Log.Debug($"resetting size for {player.Nickname}");
            player.Scale = new Vector3(1f, 1f, 1f);
        }

        response = "Killed coroutine and reset everyone to scale 1";
        return true;
    }
    
    public IEnumerator<float> Rand_sizes()
    {
        Random random_int = new Random();
        for (;;)
        {
            Log.Debug("start for iteration");
            foreach (var player in Player.List)
            {
                float size = random_int.Next(0, 3);
                size = (float)(size * random_int.NextDouble());
                Log.Debug($"rand = {size}");
                if (size < 0.1f || size > 2.5f)
                {
                    size = 1f;
                    player.Broadcast(new Exiled.API.Features.Broadcast("invalid", 1));
                }
                else
                {
                    player.Broadcast(new Exiled.API.Features.Broadcast($"{size}", 1)); 
                    player.Scale = new Vector3(size, size, size);
                }   
            }
            yield return Timing.WaitForSeconds(Nightmode.Instance.Config.Time_switching);
        }
    }
}