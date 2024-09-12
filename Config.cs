﻿using System.ComponentModel;
using Exiled.API.Interfaces;

namespace NightMode;

public class Config : IConfig
{
    [Description("Determines if the plugin should be enabled or disabled.")]
    public bool IsEnabled { get; set; } = true;

    [Description("Debug mode?")] 
    public bool Debug { get; set; } = false;
    
    [Description("Sets if at the start of the round the server can roll a random event")]
    public bool eventRand { get; set; } = true;
    
    [Description("How possible is the event to trigger? (in %)")]
    public double percentage { get; set; } = 50;
    
    [Description("List of possible events")]
    public string[] events { get; set; } = { "nightmode" };
    
    [Description("Play song when nuke starts")]
    public bool playOnNukeStart { get; set; } = false;

    public string nukeSong { get; set; } = "Hi_Fi_Rush_OST_Buzzsaw.ogg";

    [Description("Play song on lobby")] 
    public bool playOnLobby { get; set; } = false;

    public string lobbySong { get; set; } = "Hi_Fi_Rush_OST_Production_Destruction.ogg";
    
    [Description("Lock the radio on UL")] 
    public bool UL { get; set; } = false;

    [Description("Make the battery of the radio not drain")]
    public bool RadioDrain { get; set; } = true;
    
    [Description("Seconds to wait before randomizing the players height (only used when the minimadness is on)")]
    public int Time_switching { get; set; } = 10;

    [Description("Should a player get a new item if he flips a coin?")]
    public bool FlipRand { get; set; } = true;
    
    

    [Description("USED FOR EVENTS")]
    public bool nightmode_toggled { get; set; } = false;

    

    
}