using System;
using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Features;
using Player = Exiled.API.Features.Player;

namespace NightMode.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class Vanish : ICommand
{
    public bool SanitizeResponse { get; }
    public string Command { get; } = "vanish";
    public string[] Aliases { get; } = { "vn" };
    public string Description { get; } = "vanish like in minecraft (duh)";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        try
        {
            Player player = Player.Get(sender);

            if (!player.IsEffectActive<Invisible>())
            {
                player.EnableEffect<Invisible>();
                Log.Info("hiding " + player.Nickname + "with id" + player.Id);
                response = "hiding...";
                return true;
            }

            player.DisableEffect<Invisible>();
            Log.Info("revealing " + player.Nickname + "with id" + player.Id);
            response = "revealing...";
            return false;
        }
        catch (Exception e)
        {
            response = e.Message;
            return false;
        }
    }
}