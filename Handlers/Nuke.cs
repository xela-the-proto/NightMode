using AudioPlayer.API;
using AudioPlayer.Other;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Warhead;
using UnityEngine.Assertions.Must;
using VoiceChat;

namespace NightMode.Handlers;

public class Nuke
{
    
    public static Nightmode Instance { get; } = new();
    /// <summary>
    /// Plays audio when the nuke starts
    /// </summary>
    /// <param name="e"></param>
    public static void onNukeStart(StartingEventArgs e)
    {
        if (Nightmode.Instance.Config.playOnNukeStart)
        {
            AudioController.PlayAudioFromFile(Nightmode.Instance.Config.nukeSong);
        }
        
    }
    
    /// <summary>
    /// Plays audio when the nuke stops
    /// </summary>
    /// <param name="e"></param>
    public static void onNukeStop(StoppingEventArgs e)
    {
        Log.Debug("stopping");
        AudioController.StopAudio();
    }
}