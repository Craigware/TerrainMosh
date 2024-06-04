using Godot;

namespace Model 
{
    interface IManager
    {
        public void Output(string message);
        public bool Save();
        public bool Load();
    }

    public partial class Manager : Node, IManager
    {
        public virtual bool Save() {return false;}
        public virtual bool Load() {return false;}

        public void Output(string message) {
            GD.Print(Name + " - "+ Time.GetTimeStringFromSystem() + " - " + message);
        }
    }
}