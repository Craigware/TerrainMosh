using System;
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

        private Godot.Collections.Dictionary<string, WeatherTypes> biomeWeathers = new();

        //? Don't know if anything in the game will care about weather changes but this signal is for
        //? listening to those.
        [Signal]
        public delegate void WeatherChangedEventHandler(WeatherTypes newWeather);

        private WeatherTypes weather;
        public WeatherTypes Weather {
            get { return weather; }
            set {
                weather = value;
                EmitSignal(SignalName.WeatherChanged);
                Output("Weather changed to " + value);
            }
        }

        //TODO NEED TO HAVE A PLAY WHERE A PARTICLE SYSTEM CAN BE UPDATED

        public WeatherManager() {
            Name = "WeatherManager";
            //CHECK TO SEE IF SAVE EXISTS
            InstantiateChunkWeather();
            //RESET
        }

        public void InstantiateChunkWeather() {
            var Biomes = Enum.GetNames<WorldManager.Biomes>();
            foreach (string biome in Biomes) {
                var weather = DecideWeather(biome);
                biomeWeathers[biome] = weather;
            }

            Output("Biome weathers instanitated. \n" + biomeWeathers.ToString() + "\n---");
        }

        public WeatherTypes DecideWeather(string biomeType) {
            WeatherTypes[] possibleWeathers;
            
            switch (biomeType) {
                case "PALACE":
                    return WeatherTypes.SNOW;
                case "MOUNTAINS":
                    possibleWeathers = new WeatherTypes[3] { WeatherTypes.CLOUDY, WeatherTypes.SNOW, WeatherTypes.SNOWSTORM };
                    return possibleWeathers[new Random().Next(0, possibleWeathers.Length)];
                case "SEAS":
                    possibleWeathers = new WeatherTypes[4] { WeatherTypes.CLEAR, WeatherTypes.CLOUDY, WeatherTypes.RAIN, WeatherTypes.RAINSTORM };
                    return possibleWeathers[new Random().Next(0, possibleWeathers.Length)];
                case "PLAINS":
                    possibleWeathers = new WeatherTypes[4] { WeatherTypes.CLEAR, WeatherTypes.CLOUDY, WeatherTypes.SUN, WeatherTypes.RAIN };
                    return possibleWeathers[new Random().Next(0, possibleWeathers.Length)];
            }

            return WeatherTypes.CLEAR;
        }

        public WeatherTypes DecideWeather(WorldManager.Biomes biomeType) {
            return DecideWeather(Enum.GetName(biomeType));
        }

        //TODO NEED TO DECIDE A SAVE LOCAITON FOR ALL STUFF
        public override bool Load() {
            return base.Load();
        }

        public override bool Save() {
            return false;
        }
    }
}