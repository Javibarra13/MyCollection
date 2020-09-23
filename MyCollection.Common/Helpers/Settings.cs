﻿using Plugin.Settings;
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
        private static readonly string _settingsDefault = string.Empty;

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
    }
}
