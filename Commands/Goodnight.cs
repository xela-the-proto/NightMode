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
public class Goodnight : ICommand
{
    public string Command { get; } = "nightmode";
    public string[] Aliases { get; } = new string[] { "gn" };
    public string Description { get; } = "Turn off all lights and give a flashligth to everyone";
    
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        bool toggle = false;
        Exiled.API.Features.Broadcast broadcast = new Exiled.API.Features.Broadcast("<color=green> You were " +
            "switched to 939 automatically", 10, true, Broadcast.BroadcastFlags.Normal);
        try
        {
            //check args
            if (arguments.Array[1] == "on")
            {
                toggle = true;
                //cassie message
                Cassie.Message("electric failure detected bell_start locate nearest checkpoint immediately BELL_end",
                    true,true,true);
                
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
            }else 
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
                else if(toggle == false)
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
                response = "Turning the lights off... (gn back to the lobby -xela)";
                return true;
            }
            else
            {
                response = "Turning the lights on... (here comes the sun)";
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