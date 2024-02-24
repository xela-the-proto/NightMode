using System.ComponentModel;
using Exiled.API.Interfaces;

namespace NightMode
{
    public class Config : IConfig
    {
        [Description("Determines if the plugin should be enabled or disabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Debug mode?")] 
        public bool Debug { get; set; } = true;

        [Description("Sets the message for when someone joins the server. {player} will be replaced with the players name.")]
        public string JoinedMessage { get; set; } = "{player} has joined the server.";

        [Description("Sets the message for when someone leaves the server. {player} will be replaced with the players name.")]
        public string LeftMessage { get; set; } = "{player} has left the server.";

        [Description("Sets the message to be played when a round has been started.")]
        public string RoundStartedMessage { get; set; } = "Get ready for carnage!";

        [Description("Sets the message for when someone triggers a booby trap.")]
        public string BoobyTrapMessage { get; set; } = "This door has been booby trapped!";

        [Description(
            "Amount of time to cache players after they have left. (Best to keep resonable to avoid disk read on round restarts)")]
        public float PlayerCacheTime { get; set; } = 120;
    }
}