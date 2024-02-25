using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Log = PluginAPI.Core.Log;


namespace NightMode.Handlers
{
    public class Player
    {
        public static void OnPlayerLeft(LeftEventArgs e)
        {
            Log.Debug($"Leave detected by player {e.Player.CustomName} with id {e.Player.Id}");
        }

        public static void OnPlayerJoin(JoinedEventArgs e)
        {
            Log.Debug($"join detected by player {e.Player.CustomName} with id {e.Player.Id}");
        }

        public static void OnPlayerUsingRadioBattery(UsingRadioBatteryEventArgs e)
        {
            e.Drain = 0;
            if (e.IsAllowed)
            {
                e.IsAllowed = false;
            }
        }

        public static void OnPlayerChangingRadioRange(ChangingRadioPresetEventArgs e)
        {
            if (Nightmode.Instance.Config.UL)
            {
                if (e.NewValue != RadioRange.Ultra)
                {
                    e.NewValue = RadioRange.Ultra;
                }
            }
        }
    }
}