using System;
using System.IO;
using YamlDotNet.Serialization;

namespace NightMode.API
{
    public class PlayerData
    {
        private static readonly string PlayersPath =
            Path.Combine(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "YouTubeTutorialPlugin"), "Players");

        public int Kills { get; internal set; }
        public int Deaths { get; internal set; }
        public int DoorInteractions { get; set; }
        public int RoundsPlayed { get; set; }

        public void SaveData(string userId)
        {
            string playerPath = Path.Combine(PlayersPath, userId);

            ISerializer serializer = new SerializerBuilder().Build();
            string yaml = serializer.Serialize(this);

            Directory.CreateDirectory(playerPath);

            File.WriteAllText(Path.Combine(playerPath, "data.yml"), yaml);
        }

        public static void LoadData(string userId)
        {
            string dataPath = Path.Combine(Path.Combine(PlayersPath, userId), "data.yml");

            if (!File.Exists(dataPath)) return;
			
            string data = File.ReadAllText(dataPath);

            IDeserializer deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();

            var playerData = deserializer.Deserialize<PlayerData>(data);

            Nightmode.PlayerData.GetOrAdd(userId, () => playerData);
        }
    }
}