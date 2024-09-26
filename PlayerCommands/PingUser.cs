using System;
using CommandSystem;
using Exiled.API.Features;

namespace NightMode.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class PingUser : ICommand
{
    public bool SanitizeResponse { get; } = true;
    public string Command { get; } = "ping";
    public string[] Aliases { get; } = ["p"];
    public string Description { get; } = "pong! returns your ping from the server";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var player = Player.Get(sender);
        var ping = player.Ping;
        response = "Pong! your ping is " + ping;
        return true;
    }
}