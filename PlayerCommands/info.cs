﻿using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using Exiled.API.Features;

namespace NightMode.Commands;
[CommandHandler(typeof(ClientCommandHandler))]
public class info : ICommand
{
    public string Command { get; } = "info";
    public string[] Aliases { get; }
    public string Description { get; } = "Gets the general inof of the server";
    public bool SanitizeResponse { get; }
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        Player player = Player.Get(sender);
        player.SendConsoleMessage("Current server tps is " + Server.Tps, "green");
        player.SendConsoleMessage("Server running game version " + Server.Version,"green");
        player.SendConsoleMessage("players currently online " + Server.PlayerCount, "green");
        player.SendConsoleMessage("Connected via port " + Server.Port, "green");
        player.SendConsoleMessage("max players is " + Server.MaxPlayerCount,"green");
        response = "";
        return true;
        
    }

    
}