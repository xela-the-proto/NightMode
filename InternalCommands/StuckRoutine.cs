using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using MEC;
using Log = PluginAPI.Core.Log;

namespace NightMode.InternalCommands;

[CommandHandler(typeof(GameConsoleCommandHandler))]
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
        Log.Info("entering routine");
        for (;;)
        {
            foreach (var p in Exiled.API.Features.Player.List)
            {
                Log.Info("running");
                p.SessionVariables.Add("LastRoom", p.CurrentRoom.Position);
                Log.Info(p.CurrentRoom.Name);
            }
            Log.Info("Storing latest room player was in...");
            yield return Timing.WaitForSeconds(5);
        }
    }
}

