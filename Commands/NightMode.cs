﻿using System;
using CommandSystem;
using Exiled.API.Features;
using PlayerRoles;

namespace NightMode.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class NightMode : ICommand
{
    public string Command { get; } = "nightmode";
    public string[] Aliases { get; } = { "gn" };
    public string Description { get; } = "Turn off all lights and give a flashlight to everyone";
    public bool SanitizeResponse { get; }

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var toggle = false;
        var message_cassie =
            "Bell_start Attention all personnel an electric failure has been detected . . " +
            ". . . . . . pitch_.2 .g4 . .g4 pitch_0.7 Danger . all generators of the facility are " +
            "shut pitch_0.5 jam_5_3 down pitch_.2 .g4 . .g4 pitch_0.9 All remaining personnel are " +
            "advised to enter the entrance zone until an M T F squad come to escort jam_5_3 you " +
            "pitch_.2 .g4 pitch_.5 .g4";
        var broadcast = new Exiled.API.Features.Broadcast("<color=green> You were " +
                                                          "switched to 939 automatically");
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
                    if (player.IsScp)
                    {
                        Log.Debug($"switching {player.Nickname} to 939");
                        player.Role.Set(RoleTypeId.Scp939);
                        player.Broadcast(broadcast);
                    }
                    else
                    {
                        Log.Debug($"giving {player.Nickname} a flashlight");
                        player.AddItem(ItemType.Flashlight);
                    }
            }
            else
            {
                toggle = false;
            }

            //iterate through every room and turn off the lights
            Log.Debug("Trying NetworkLightsEnabled...");
            foreach (var room in Room.List)
                if (toggle)
                {
                    if (!room.AreLightsOff) room.TurnOffLights();
                }
                else if (toggle == false)
                {
                    if (room.AreLightsOff) room.RoomLightController.NetworkLightsEnabled = true;
                }

            if (toggle)
            {
                //toggle to let the server know if we need to give a torch
                Nightmode.Instance.Config.nightmode_toggled = true;
                response = "Turning the lights off...";
                Log.Debug(Nightmode.Instance.Config.nightmode_toggled);
                return true;
            }

            Nightmode.Instance.Config.nightmode_toggled = false;
            response = "Turning the lights on...";
            Log.Debug(Nightmode.Instance.Config.nightmode_toggled);
            return true;
        }
        catch (Exception e)
        {
            Log.Error("Bad command!");
            response = "Bad command formatting! options are [on/off]";
            return false;
        }
    }
}