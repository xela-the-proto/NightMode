using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Log = PluginAPI.Core.Log;


namespace NightMode.Handlers
{
    public class Player
    {
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
            
            if (e.NewValue != RadioRange.Ultra)
            {
                e.NewValue = RadioRange.Ultra;
            }
            
        }
    }
}