using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using UnityEngine;

namespace NightMode.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class Stuck : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        Player p = Player.Get(sender);
        /*
        if (!p.SessionVariables.ContainsKey("LastRoom"))
        {
            Room room = Room.Get(RoomType.HczServers);
            response = "Your last room wasnt stored yet! sending to heavy...";
            p.Position = room.Position;
            return false;
        }
        */

        p.Position = (Vector3)p.SessionVariables["LastRoom"];
        Log.Warn("Player " + p.CustomName +" with id " + p.Id + " with auth " + p.AuthenticationType + " with userid " + p.UserId + " unstuck himself");
        response = "Unstuck! (probably)";
        return true;
    }

    public string Command { get; } = "stuck";
    public string[] Aliases { get; }
    public string Description { get; } = "Unstuck yourself";
}