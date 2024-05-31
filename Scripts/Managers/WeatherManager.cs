using Godot;

namespace Managers
{
    public partial class WeatherManager : Model.Manager
    {
        public enum WeatherTypes
        {
            CLEAR       = 0 ,
            CLOUDY      = 1 ,
            SUN         = 2 ,
            RAIN        = 3 ,
            SNOW        = 4 ,
            SNOWSTORM   = 5 ,
            RAINSTORM   = 6 ,
            SANDSTORM   = 7 ,
        }

        //? Don't know if anything in the game will care about weather changes but this signal is for
        //? listening to those.
        [Signal]
        public delegate void WeatherChangedEventHandler(WeatherTypes newWeather);

        private WeatherTypes current;
        public WeatherTypes Current {
            get { return current; }
            set {
                current = value;
                EmitSignal(SignalName.WeatherChanged);
                Output("Weather changed to " + value);
            }
        }

        public override void _Ready()
        {
            Current = WeatherTypes.SUN;
        }

        //TODO NEED TO HAVE A PLAY WHERE A PARTICLE SYSTEM CAN BE UPDATED
    }
}