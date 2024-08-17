using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using Exiled.API.Features;

namespace NightMode.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class Stuck : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        Player p = Player.Get(sender);
        Room room = new Room();
        if (!p.SessionVariables.ContainsKey("LastRoom"))
        {
            response = "Your last room wasnt stored yet! sending to heavy...";
            return false;
        }
        p.Teleport(p.SessionVariables["LastRoom"]);
        Log.Warn("Player " + p.CustomName +" with id " + p.Id + " with auth " + p.AuthenticationType + " with userid " + p.UserId + " unstuck himself");
        response = "Unstuck! (probably)";
        return true;
    }

    public string Command { get; } = "stuck";
    public string[] Aliases { get; }
    public string Description { get; } = "Unstuck yourself";
}