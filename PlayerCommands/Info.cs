using System;
using CommandSystem;
using Exiled.API.Features;

namespace NightMode.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class Info : ICommand
{
    public bool SanitizeResponse { get; }
    public string Command { get; } = "Info";
    public string[] Aliases { get; }
    public string Description { get; } = "Gets the general inof of the server";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var player = Player.Get(sender);
        player.SendConsoleMessage("Current server tps is " + Server.Tps, "green");
        player.SendConsoleMessage("Server running game version " + Server.Version, "green");
        player.SendConsoleMessage("players currently online " + Server.PlayerCount, "green");
        player.SendConsoleMessage("Connected via port " + Server.Port, "green");
        player.SendConsoleMessage("max players is " + Server.MaxPlayerCount, "green");
        response = "";
        return true;
    }
}