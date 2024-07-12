using AudioPlayer.API;
using AudioPlayer.Other;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Warhead;
using UnityEngine.Assertions.Must;
using VoiceChat;

namespace NightMode.Handlers;

public class Nuke
{
    public static void onNukeStart(StartingEventArgs e)
    {
        Log.Debug("starting to play");
        AudioController.SpawnDummy(99);
        AudioController.PlayAudioFromFile("Hi_Fi_Rush_OST_Buzzsaw.ogg");
    }

    public static void onNukeStop(StoppingEventArgs e)
    {
        Log.Debug("stopping");
        AudioController.StopAudio();
        AudioController.DisconnectDummy();
    }
}