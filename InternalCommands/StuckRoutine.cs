using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using GameCore;
using MEC;
using Log = PluginAPI.Core.Log;

namespace NightMode.InternalCommands;

[CommandHandler(typeof(GameConsoleCommandHandler))]
[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class StuckRoutine : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        Timing.RunCoroutine(GetLastRoom(), "Room");
        response = "Starting stuck coroutine";
        return true;
    }

    public string Command { get; } = "stuckService";
    public string[] Aliases { get; }
    public string Description { get; } = "Stuck service";
    
    public IEnumerator<float> GetLastRoom()
    {
        foreach (var p in Exiled.API.Features.Player.List)
        {
            p.SessionVariables.Add("LastRoom", p.CurrentRoom);
        }
        Log.Info("Storing latest room player was in...");
        yield return Timing.WaitForSeconds(5);
    }
}

