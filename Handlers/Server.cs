using System;
using AudioPlayer.API;
using AudioPlayer.Commands;
using Exiled.API.Features;
using Hints;
using MapGeneration;
using NightMode.Commands;
using PluginAPI.Commands;
using PluginAPI.Events;
using RemoteAdmin;

namespace NightMode.Handlers;

public class Server
{
    public static Nightmode Instance { get; } = new();
    
    /// <summary>
    /// disconnect the dummy 
    /// </summary>
    /// <param name="e"></param>
    public static void onServerStopping(RoundEndEvent e)
    {  
        AudioController.DisconnectDummy();
        AudioController.StopAudio();
    }
    
    /// <summary>
    /// Play something when in lobby
    /// </summary>
    public static void onServerStarting()
    {
        Random random = new Random();
        AudioController.SpawnDummy(99);
        if (Nightmode.Instance.Config.playOnLobby)
        {
            AudioController.PlayAudioFromFile(Nightmode.Instance.Config.lobbySong, true, 70f);
        }

        if (Nightmode.Instance.Config.eventRand)
        {
            var events = Nightmode.Instance.Config.events.Length;
            int rand = random.Next(1, events);

            string command = Nightmode.Instance.Config.events[rand - 1];

            Exiled.API.Features.Server.ExecuteCommand(command + " on");
        }
    }
    
    /// <summary>
    /// Stop any audio if we played before in lobby
    /// </summary>
    public static void onRoundStart()
    {
        if (Nightmode.Instance.Config.playOnLobby)
        {
            AudioController.StopAudio();
        }

        foreach (var player in PluginAPI.Core.Player.GetPlayers())
        {
            player.ReceiveHint("Test Hint");
        }
    }
}