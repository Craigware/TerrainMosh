using Godot;

namespace Model
{
    interface IInteractable
    {
        abstract void Interact();
    }

    public abstract partial class Interactable : Node, IInteractable 
    {
        public abstract void Interact();
    }
}