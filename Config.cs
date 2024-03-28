using System.ComponentModel;
using Exiled.API.Interfaces;

namespace NightMode
{
    public sealed class Config : IConfig
    {
        [Description("Determines if the plugin should be enabled or disabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Debug mode?")] 
        public bool Debug { get; set; } = false;

        [Description("Amount of time to cache players after they have left. (Best to keep resonable to avoid disk read on round restarts)")]
        public float PlayerCacheTime { get; set; } = 120;

        [Description("Lock the radio on UL")] 
        public bool UL { get; set; } = true;

        [Description("Make the battery of the radio not drain")]
        public bool RadioDrain { get; set; } = true;

        [Description("Seconds to wait before randomizing the players height (only used when the minimadness is on)")]
        public int Time_switching { get; set; } = 60;

        [Description("DO NOT TOUCH USED FOR INTERNAL SHENANIGANS")]
        public bool nightmode_toggled { get; set; } = false;
    }
}