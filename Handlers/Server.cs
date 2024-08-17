using AudioPlayer.API;
using Exiled.API.Features;
using PluginAPI.Events;
using Random = System.Random;

namespace NightMode.Handlers;

public class Server
{

    /// <summary>
    ///     disconnect the dummy
    /// </summary>
    /// <param name="e"></param>
    public static void onServerStopping(RoundEndEvent e)
    {
        AudioController.DisconnectDummy();
        AudioController.StopAudio();
    }

    /// <summary>
    ///     Play something when in lobby
    /// </summary>
    public static void onServerStarting()
    {
        AudioController.SpawnDummy(99);
        if (Nightmode.Instance.Config.playOnLobby)
            AudioController.PlayAudioFromFile(Nightmode.Instance.Config.lobbySong, true, 70f);
    }

    /// <summary>
    ///     Stop any audio if we played before in lobby
    /// </summary>
    public static void onRoundStart()
    {
        Random random = new Random();

        if (Nightmode.Instance.Config.playOnLobby) AudioController.StopAudio();

        if (Nightmode.Instance.Config.eventRand)
        {
            int events = Nightmode.Instance.Config.events.Length;
            Log.Debug("array length" + events);
            int rand = random.Next(0, events);

            string command = Nightmode.Instance.Config.events[rand];
            Log.Debug("command from list: " + command);
            Log.Debug("Index: " + rand);
            Exiled.API.Features.Server.ExecuteCommand(command + " on");
        }
    }
}