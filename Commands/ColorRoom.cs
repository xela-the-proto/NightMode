using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using UnityEngine;

namespace NightMode.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ColorRoom : ICommand
    {
        public string Command { get; } = "colorallrooms";
        public string[] Aliases { get; } = new string[] { "car", "colallro" };
        public string Description { get; } = "color all rooms in the facility of a rgb color specified";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            try
            {
                float value;
                float[] userColor = [0, 0, 0];
                
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                        //skip first arg
                    }
                    else
                    {
                        if (float.TryParse(arguments.Array[i], out value))
                        {
                            userColor[i - 1] += value / 255.0F;
                        }
                        else throw new ArgumentException();
                    }
                }
                Color color = new Color(userColor[0],userColor[1],userColor[2]);
                foreach (var item in Room.List)
                {
                    item.Color = color;
                }

                response = "Color changed!";
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        
    }
}