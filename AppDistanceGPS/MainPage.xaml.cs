using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppDistanceGPS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public static double CalculateDistance(Coordinate point1, Coordinate point2)
        {
            double earthEcuator = 40000.0; //in KM
            double distance = 0.0;

            //Calculate radians
            double latitude1 = DegreesToRadians(point1.Latitude);
            double longitude1 = DegreesToRadians(point1.Longitude);

            double latitude2 = DegreesToRadians(point2.Latitude);
            double longitude2 = DegreesToRadians(point2.Longitude);

            double longitudeDelta = Math.Abs(longitude1 - longitude2);

            if (longitudeDelta > Math.PI)
            {
                longitudeDelta = 2.0 * Math.PI - longitudeDelta;
            }

            double angleCalculation =
                Math.Acos(Math.Sin(latitude2) * Math.Sin(latitude1) +
                Math.Cos(latitude2) * Math.Cos(latitude1) * Math.Cos(longitudeDelta));

            distance = earthEcuator * angleCalculation / (2.0 * Math.PI);

            return distance;

        }
        public static double CalculateDistance(params Coordinate[] coordinates)
        {
            double totalDistance = 0.0;

            for (int i = 0; i < coordinates.Length - 1; i++)
            {
                Coordinate actual = coordinates[i];
                Coordinate next = coordinates[i + i];

                totalDistance += CalculateDistance(actual, next);

            }

            return totalDistance;

        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Coordinate point1 = new Coordinate();
            Coordinate point2 = new Coordinate();

            point1.Latitude = Convert.ToDouble(txtLatitude1.Text.ToString());
            point1.Longitude = Convert.ToDouble(txtLongitude1.Text.ToString());

            point2.Latitude = Convert.ToDouble(txtLatitude2.Text.ToString());
            point2.Longitude = Convert.ToDouble(txtLongitude2.Text.ToString());

            Distance.Text = CalculateDistance(point1, point2).ToString() + " KM";

        }
    }

    public class Coordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
 
}
