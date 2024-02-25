using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using PluginAPI.Core;


namespace NightMode.Handlers
{
    public class Player
    {
        public static void OnPlayerLeft(LeftEventArgs e)
        {
            Log.Debug($"Leave detected by player {e.Player.CustomName} with id {e.Player.Id}");
            PluginAPI.Core.Server.Broadcast.RpcAddElement($"{e.Player.CustomName} has left the server", 5, 
                Broadcast.BroadcastFlags.Normal);
        }

        public static void OnPlayerJoin(JoinedEventArgs e)
        {
            Log.Debug($"join detected by player {e.Player.CustomName} with id {e.Player.Id}");
            PluginAPI.Core.Server.Broadcast.RpcAddElement( $"{e.Player.CustomName} has joined the server",5,
                Broadcast.BroadcastFlags.Normal);
        }

        public static void OnPlayerUsingRadioBattery(UsingRadioBatteryEventArgs e)
        {
            e.Drain = 0;
            if (e.IsAllowed)
            {
                e.IsAllowed = false;
            }
        }

        public static void OnPlayerTogglingRadio(TogglingRadioEventArgs e)
        {
            if (e.Radio.Range != RadioRange.Ultra)
            {
                e.Radio.Range = RadioRange.Ultra;
            }
        }
    }
}