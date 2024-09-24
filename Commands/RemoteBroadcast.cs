using System;
using CommandSystem;
using Exiled.API.Features;
using UnityEngine;

namespace NightMode.Commands;

[CommandHandler(typeof(GameConsoleCommandHandler))]
public class RemoteBroadcast : ICommand
{
    public bool SanitizeResponse { get; }
    public string Command { get; } = "broadcast";
    public string[] Aliases { get; } = { "bc" };
    public string Description { get; } = "Broadcast to the server directly like if you were in remoteadmin";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        try
        {
            ushort time = 0;
            string message = "<color=red>[SERVER] <color=white>";
            ushort ushort_time = 0;
            int i = 0;
            
            foreach (var item in arguments)
            {
                if (i == 0)
                {
                    ushort_time = Convert.ToUInt16(item);
                    Log.Debug($"time we got {time} time converted {ushort_time}");
                }
                else
                {
                    message += " " + item;
                }

                i += 1;
            }

            response = $"Broadcasting {message} for {ushort_time} seconds to server...";
            Server.Broadcast.RpcAddElement(message, ushort_time, Broadcast.BroadcastFlags.Normal);
            Log.Debug("Broadcast should have been sent");
            return true;
        }
        catch (FormatException e)
        {
            Log.Error("Bad command format!");
            response = "Bad command formatting! either you inserted 0 as a time or a string!";
            return false;
        }
    }
}