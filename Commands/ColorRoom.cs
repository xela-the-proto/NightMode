using System;
using CommandSystem;
using Exiled.API.Features;
using UnityEngine;

namespace NightMode.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]

public class ColorRoom : ICommand
{
    public bool SanitizeResponse { get; }
    public string Command { get; } = "colorallrooms";
    public string[] Aliases { get; } = { "car", "colallroo" };
    public string Description { get; } = "color all rooms in the facility with a specified rgb color";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        try
        {
            if (arguments.Count <= 1)
            {
                Map.ChangeLightsColor(Color.white);
                response = "Color reset!";
                return true;  
            }
            float value;
            float[] userColor = [0, 0, 0];

            for (int i = 0; i < 4; i++)
            {
                if (i != 0)
                {
                    if (float.TryParse(arguments.Array[i], out value))
                    {
                        userColor[i - 1] += value / 255.0F; 
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
            }
            Color color = new Color(userColor[0], userColor[1], userColor[2]);
            //Bruh i never knew this shit even existed :(
            Map.ChangeLightsColor(color);
            response = "Color changed in all the facility!";
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}