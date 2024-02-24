using System;
using CommandSystem;
using PluginAPI.Core;

namespace NightMode.Commands
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    
    public class RemoteBroadcast : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if( arguments.Count > 3)
            {
                response = "Bad command formatting!";
                return false;
            }
            else
            {
                
                response = arguments.ToString();
                return true;
            }
            
        }

        public string Command { get; } = "broadcast";
        public string[] Aliases { get; } = new string[] { "bc" };
        public string Description { get; } = "Broadcast to the server directly";
    }
}