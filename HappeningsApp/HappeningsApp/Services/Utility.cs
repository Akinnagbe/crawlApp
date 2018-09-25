﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace HappeningsApp.Services
{
    public class Utility
    {
        public class StringCaseConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {

                string param = System.Convert.ToString(parameter) ?? "u";

                switch (param.ToUpper())
                {
                    case "U":
                        return ((string)value).ToUpper();
                    case "L":
                        return ((string)value).ToLower();
                    default:
                        return ((string)value);
                }

            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotSupportedException();
            }
        }


        public bool CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
                return true;
            else
                return false;
        }


        public void PersistUsername()
        {
            
        }


    }
}
