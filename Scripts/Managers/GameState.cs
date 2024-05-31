using Godot;

namespace Managers
{
    public partial class GameState : Model.Manager
    {
        public override void _Ready() {
            Output("Game state initialized");
        }
    }
}
