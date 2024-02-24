using Exiled.API.Features;

namespace NightMode.Handlers
{
    public class Server
    {
        public void OnWaitingForPlayers()
        {
            Log.Info("waiting for players...");
        }

        public void OnRoundStarted(string round_type)
        {
            Map.Broadcast(10,$"Tipo round: {round_type}");
        }
    }
}