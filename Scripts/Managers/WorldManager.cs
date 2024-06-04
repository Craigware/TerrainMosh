using Godot;

namespace Managers
{
    //! The world can't really be broken up into different scenes
    //! Because then the bosses would stop moving around and i want them
    //! To interact with one another.

    //* The palace can probably be put into a different scene because it will
    //* Be frozen anyways so makes sense
    public partial class WorldManager : Node
    {
        public enum Biomes {
            PALACE,
            PLAINS,
            SEAS,
            MOUNTAINS
        }
    }
}