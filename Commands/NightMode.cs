using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Permissions.Commands.Permissions.Group;
using PlayerRoles;

namespace NightMode.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class NightMode : ICommand
{
    public string Command { get; } = "nightmode";
    public string[] Aliases { get; } = new string[] { "gn" };
    public string Description { get; } = "Turn off all lights and give a flashligth to everyone";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        bool toggle = false;
        string message_cassie =
            "Bell_start Attention all personnel an electric failure has been detected . . " +
            ". . . . . . pitch_.2 .g4 . .g4 pitch_0.7 Danger . all generators of the facility are " +
            "shut pitch_0.5 jam_5_3 down pitch_.2 .g4 . .g4 pitch_0.9 All remaining personnel are " +
            "advised to enter the entrance zone until an M T F squad come to escort jam_5_3 you " +
            "pitch_.2 .g4 pitch_.5 .g4";
        Exiled.API.Features.Broadcast broadcast = new Exiled.API.Features.Broadcast("<color=green> You were " +
            "switched to 939 automatically", 10, true, Broadcast.BroadcastFlags.Normal);
        try
        {
            //check args
            if (arguments.Array[1] == "on")
            {
                toggle = true;
                //cassie message
                Cassie.Message(message_cassie,
                    true, true, true);

                //if a player is a scp we need to switch em to the dog
                //give every player a flashlight
                foreach (var player in Player.List)
                {
                    if (player.IsScp)
                    {
                        player.Role.Set(RoleTypeId.Scp939, SpawnReason.ForceClass);
                        player.Broadcast(broadcast);
                    }
                    else
                    {
                        player.AddItem(ItemType.Flashlight);
                    }
                }
            }
            else
            {
                toggle = false;
            }

            //iterate through every room and turn off the lights
            foreach (var room in Room.List)
            {
                if (toggle)
                {
                    if (!room.AreLightsOff)
                    {
                        room.TurnOffLights(-1F);
                    }
                }
                else if (toggle == false)
                {
                    if (room.AreLightsOff)
                    {
                        Log.Debug("Trying NetworkLightsEnabled...");
                        room.RoomLightController.NetworkLightsEnabled = true;
                    }
                }
            }

            if (toggle)
            {
                Nightmode.Instance.Config.nightmode_toggled = true;
                response = "Turning the lights off... (gn back to the lobby -xela)";
                Log.Debug(Nightmode.Instance.Config.nightmode_toggled);
                return true;
            }
            else
            {
                Nightmode.Instance.Config.nightmode_toggled = false;
                response = "Turning the lights on... (here comes the sun)";
                Log.Debug(Nightmode.Instance.Config.nightmode_toggled);
                return true;
            }
        }
        catch (Exception e)
        {
            Log.Error("Bad command!");
            response = "Bad command formatting! options are [on/off]";
            return false;
        }
    }
}