﻿using System.Windows.Media;
using PropertyChanged;
using Reloaded.Mod.Loader.IO;
using Reloaded.Mod.Loader.IO.Config;
using Reloaded.WPF.MVVM;

namespace Reloaded.Mod.Launcher.Models.Model
{
    public class ImageApplicationPathTuple : ObservableObject
    {
        private static readonly ConfigReader<ApplicationConfig> ConfigReader = new ConfigReader<ApplicationConfig>();

        /// <summary>
        /// The image to represent the application by in the GUI.
        /// </summary>
        [DoNotCheckEquality]
        public ImageSource Image { get; set; }

        /// <summary>
        /// The application configuration.
        /// </summary>
        public ApplicationConfig Config { get; set; }

        /// <summary>
        /// The full path to the application configuration file.
        /// </summary>
        public string ConfigPath { get; set; }

        public ImageApplicationPathTuple(ImageSource image, ApplicationConfig config, string configPath)
        {
            Image = image;
            Config = config;
            ConfigPath = configPath;
        }

        public void Save()
        {
            ConfigReader.WriteConfiguration(ConfigPath, Config);
        }

        public override string ToString()
        {
            return Config.AppName;
        }

        /* Autogenerated by R# */
        protected bool Equals(ImageApplicationPathTuple other)
        {
            return Equals(Config, other.Config) &&
                   string.Equals(ConfigPath, other.ConfigPath);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ImageApplicationPathTuple)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Config != null ? Config.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ConfigPath != null ? ConfigPath.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
