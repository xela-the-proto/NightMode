using System;
using System.Collections.Generic;
using AudioPlayer.API;
using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using InventorySystem.Items.Usables;
using MEC;
using PlayerRoles;
using VoiceChat;
using broadcast = Exiled.API.Features.Broadcast;

namespace NightMode.Commands;
[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class Speedrun : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var countdown = new broadcast("",1,true,Broadcast.BroadcastFlags.Normal);
        foreach (var door in Door.List)
        {
            //ik door.lockall exists but i trust this more
            door.Lock(-1f,DoorLockType.AdminCommand);
        }
        foreach (var p in Player.List)
        {
            p.Role.Set(RoleTypeId.ClassD);
        }
        
        Timing.RunCoroutine(Start(countdown),"ev_speedrun");
        response = "running";
        return true;
    }

    public string Command { get; } = "speedrun";
    public string[] Aliases { get; }
    public string Description { get; }

    public IEnumerator<float> Start(broadcast br)
    {
        //just to be safe disconnect any previous audio bot
        AudioController.DisconnectDummy(99);
        AudioController.SpawnDummy(99,"ev_speedrun","red","run");
        Cassie.GlitchyMessage("bell_start ANNOUNCEMENT alpha warhead malfunction detected . " +
                              ". pitch_.2 .g4 . .g4 pitch_0.8 critical failure " +
                              "escape facility a s a p",0.5f,0.3f);
        
        for (int i = 4 - 1; i >= 1; i--)
        {
            br.Content = "<color=red> " + i;
            Map.Broadcast(br);  
            yield return Timing.WaitForSeconds(2);
        }
        
        PluginAPI.Core.Server.RunCommand("car 255 0 0");
        
        for (int i = 0; i < 5; i++)
        {
            Map.TurnOffAllLights(4);
            yield return Timing.WaitForSeconds(6); 
        }
        
        Map.TurnOffAllLights(5);
        Server.ExecuteCommand("car 255 0 0");
        
        foreach (var p in Player.List) p.EnableEffect<MovementBoost>(20, 0f);
        foreach (var door in Door.List) door.Unlock();
        AudioController.PlayAudioFromFile(Nightmode.Singleton.Config.ev_speedrun_song,true,150,VoiceChatChannel.Intercom);

    }

    public IEnumerator<float> Nuke()
    {
        //wait the time defined in the config before setting off the nuke
        yield return Timing.WaitForSeconds(Nightmode.Singleton.Config.ev_speedrun_time);
        Warhead.Detonate();
    }
}