using AudioPlayer.API;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Warhead;

namespace NightMode.Handlers;

public class Nuke
{
    public static Nightmode Instance { get; }

    /// <summary>
    ///     Plays audio when the nuke stops
    /// </summary>
    /// <param name="e"></param>
    public static void onNukeStop(StoppingEventArgs e)
    {
        Log.Debug("stopping");
        AudioController.StopAudio();
    }
}