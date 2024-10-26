using System.ComponentModel;
using Exiled.API.Interfaces;

namespace NightMode;

public class Config : IConfig
{
    [Description("Determines if the plugin should be enabled or disabled.")]
    public bool IsEnabled { get; set; } = true;

    [Description("Debug mode?")] public bool Debug { get; set; } = false;
    
    [Description("Sets if at the start of the round the server can roll a random event")]
    public bool eventRand { get; set; } = true;

    [Description("How possible is the event to trigger? (in %)")]
    public double percentage { get; set; } = 50;

    [Description("List of possible events")]
    public string[] events { get; set; } = { "nightmode", "speedrun" };

    [Description("Time in seconds before nuke explodes (ONLY AFFECTS SPEEDRUN EVENT)")]
    public float ev_speedrun_time { get; set; } = 300;

    [Description("Song that plays when nuke starts (ONLY AFFECTS SPEEDRUN EVENT)")]
    public string ev_speedrun_song { get; set; } = "song+siren.ogg";

    [Description("Play song on lobby")] public bool playOnLobby { get; set; } = false;

    public string lobbySong { get; set; } = "Hi_Fi_Rush_OST_Production_Destruction.ogg";

    [Description("Lock the radio on UL")] public bool UL { get; set; } = false;

    [Description("Make the battery of the radio not drain")]
    public bool RadioDrain { get; set; } = true;

    [Description("Seconds to wait before randomizing the players height (only used when the minimadness is on)")]
    public int Time_switching { get; set; } = 10;

    [Description("Should a player get a new item if he flips a coin?")]
    public bool FlipRand { get; set; } = true;


    [Description("from here on out all config will be used for internal stuff DO NOT EDIT")]
    public int literally_does_nothing_so_the_compiler_doesnt_fucking_complain = 0;

    [Description("nightmode_activation")] public bool nightmode_toggled { get; set; } = false;
    [Description("speedrun_activation")] public bool speedrun_toggled { get; set; } = false;


    
}