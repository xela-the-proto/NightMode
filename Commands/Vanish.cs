using System;
using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using Player = Exiled.API.Features.Player;

namespace NightMode.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class Vanish : ICommand
{
    public string Command { get; } = "vanish";
    public string[] Aliases { get; } = { "vn" };
    public string Description { get; } = "vanish like in minecraft (duh)";
    public bool SanitizeResponse { get; }

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        try
        {
            var playerid = arguments.Array[1];
            Log.Debug(playerid);
            foreach (var player in Player.List)
                if (player.Id == int.Parse(playerid))
                {
                    Log.Debug($"after if {playerid}");
                    if (!player.IsEffectActive<Invisible>())
                    {
                        player.Role.Set(RoleTypeId.Tutorial);
                        player.CustomName = "";
                        player.EnableEffect(EffectType.Invisible, 86400f);
                        response = $"hiding player {player.Nickname}";
                        return true;
                    }

                    if (player.IsEffectActive<Invisible>())
                    {
                        player.DisableEffect<Invisible>();
                        player.CustomName = null;
                        response = $"revealing player {player.Nickname}";
                        return true;
                    }
                }

            response = "couldnt find any player matching id";
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            response = "bad command format\n vn [player_id]";
            return false;
        }
    }
}