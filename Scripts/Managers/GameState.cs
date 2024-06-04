using Godot;

namespace Managers
{
    public partial class GameState : Model.Manager
    {
        //? Weather manager might want to be moved under world manager
        //? Maybe boss manager moves under it as well since they both
        //? Revolve around the world and its tiles
        public WeatherManager WeatherManager;
        public WorldManager WorldManager;
        public BossManager BossManager;
        public DialougeManager DialougeManager;

        public void SetUp() {
            WeatherManager = new();
            WorldManager = new();
            BossManager = new();
            DialougeManager = new();       
        }

        public override void _Ready() {
            SetUp();
            Output("Game state initialized");
        }
    }
}
