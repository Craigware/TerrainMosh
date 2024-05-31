using Godot;

namespace Model 
{
    interface IManager
    {
        public void Output(string message);
    }

    public partial class Manager : Node, IManager
    {
        public void Output(string message) {
            GD.Print(Name + " - "+ Time.GetTimeStringFromSystem() + " - " + message);
        }
    }
}