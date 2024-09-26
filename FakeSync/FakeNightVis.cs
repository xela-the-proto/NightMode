using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;
using UnityEngine;
using Log = PluginAPI.Core.Log;

namespace NightMode.FakeSync;

[CommandHandler(typeof(ClientCommandHandler))]
public class FakeNightVis : ICommand
{
    public string Command { get; } = "nightv";
    public string[] Aliases { get; } = { "nightvison" };
    public string Description { get; } = "activate nightvision";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        //i will prolly use this for something eventually
        var player = Player.Get(sender);
        //Timing.RunCoroutine(ChangeRoom(player), "CheckRoom");
        if (player.CheckPermission("nightmode.nightv"))
        {
            if (!player.SessionVariables.ContainsKey("NightV"))
            {
                player.SessionVariables.Add("NightV", true);
                Timing.RunCoroutine(CheckRoom(player), "CheckRoom");
                response = "running...";
                return true;
            }

            switch ((bool)player.SessionVariables["NightV"])
            {
                case false:
                    Timing.RunCoroutine(CheckRoom(player), "CheckRoom");
                    response = "running...";
                    return true;
                case true:
                    Timing.KillCoroutines("CheckRoom");
                    player.SessionVariables.Remove("NightV");
                    player.SessionVariables.Add("NightV", false);
                    response = "stopping...";
                    return true;
            }
        }

        response = "insufficient permissions!";
        return false;
    }

    public IEnumerator<float> CheckRoom(Player p)
    {
        var lastRoom = p.CurrentRoom;
        for (;;)
        {
            var room = p.CurrentRoom;
            //sometimes moving too fast between room breaks it 
            //set the last room to off
            if (lastRoom.Identifier != room.Identifier)
            {
                lastRoom.SetRoomColorForTargetOnly(p, Color.white);
                lastRoom.SetRoomLightsForTargetOnly(p, false);
            }

            //Set the current room to whatever we want
            room.SetRoomLightsForTargetOnly(p, true);
            room.SetRoomColorForTargetOnly(p, Color.green);
            lastRoom = room;

            yield return Timing.WaitForSeconds(1);
        }
    }
}