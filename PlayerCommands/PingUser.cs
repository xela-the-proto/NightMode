using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using Discord;
using Exiled.API.Features;
using PlayerRoles.FirstPersonControl.Thirdperson;

namespace NightMode.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class PingUser : ICommand
{
    public string Command { get; } = "ping";
    public string[] Aliases { get; } = ["p"];
    public string Description { get; } = "pong! returns your ping from the server";
    public bool SanitizeResponse { get; } = true;

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        Player player = Player.Get(sender);
        int ping = player.Ping;
        response = "Pong! your ping is " + ping;
        return true;
    }
}   