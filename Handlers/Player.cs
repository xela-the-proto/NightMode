using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace NightMode.Handlers
{
    public class Player
    {
        public static void OnPlayerLeft(LeftEventArgs e)
        {
            Log.Debug($"Leave detected by player {e.Player.CustomName} with id {e.Player.Id}");
            Map.Broadcast(5, $"{e.Player.CustomName} has left the server");
        }

        public static void OnPlayerJoin(JoinedEventArgs e)
        {
            Log.Debug($"join detected by player {e.Player.CustomName} with id {e.Player.Id}");
            Map.Broadcast(5, $"{e.Player.CustomName} has joined the server");
        }
    }
}