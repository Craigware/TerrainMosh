using Godot;

namespace Model
{    
    [GlobalClass]
    public partial class Item : Resource 
    {
        public string Name;
        public Texture2D Sprite;
        public Texture2D Icon;

        public Item() : this("UNDEFINED", null, null) {}
        public Item(
            string name,
            Texture2D sprite,
            Texture2D icon
        ) {
            Name = name;
            Sprite = sprite;
            Icon = icon;
        }
    }

}