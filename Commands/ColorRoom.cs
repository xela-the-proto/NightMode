using System;
using CommandSystem;
using Exiled.API.Features;
using UnityEngine;

namespace NightMode.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class ColorRoom : ICommand
{
    public string Command { get; } = "colorallrooms";
    public string[] Aliases { get; } = { "car", "colallroo" };
    public string Description { get; } = "color all rooms in the facility of a rgb color specified";
    public bool SanitizeResponse { get; }

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        try
        {
            float value;
            float[] userColor = [0, 0, 0];

            for (var i = 0; i < 4; i++)
                //skip first arg
                if (i != 0)
                {
                    if (float.TryParse(arguments.Array[i], out value))
                        userColor[i - 1] += value / 255.0F;
                    else throw new ArgumentException();
                }

            var color = new Color(userColor[0], userColor[1], userColor[2]);
            foreach (var item in Room.List) item.Color = color;

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