using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Helpers
{
    public static class Settings
    {
        private const string _customerImages = "CustomerImages";
        private const string _customer = "customer";
        private const string _token = "token";
        private const string _collector = "collector";
        private const string _isRemember = "IsRemember";
        private static readonly string _settingsDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string CustomerImages
        {
            get => AppSettings.GetValueOrDefault(_customerImages, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_customerImages, value);
        }

        public static string Customer
        {
            get => AppSettings.GetValueOrDefault(_customer, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_customer, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static string Collector
        {
            get => AppSettings.GetValueOrDefault(_collector, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_collector, value);
        }

        public static bool IsRemember
        {
            get => AppSettings.GetValueOrDefault(_isRemember, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isRemember, value);
        }
    }
}
