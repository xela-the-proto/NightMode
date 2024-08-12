using AudioPlayer.API;
using AudioPlayer.Commands;
using Hints;
using PluginAPI.Events;

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
        AudioController.SpawnDummy(99);
        if (Nightmode.Instance.Config.playOnLobby)
        {
            AudioController.PlayAudioFromFile(Nightmode.Instance.Config.lobbySong,true,70f);
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