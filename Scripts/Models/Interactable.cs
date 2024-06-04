using Godot;

namespace Model
{
    interface IInteractable
    {
        abstract void Interact();
        void Output(string message);
    }

    public abstract partial class Interactable : Node, IInteractable 
    {
        public abstract void Interact();

        public void Output(string message) {
            GD.Print(Name + " - " + Time.GetTimeStringFromSystem() + " - " +  message);
        }
    }
}