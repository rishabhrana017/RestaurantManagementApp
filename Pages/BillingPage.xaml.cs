using Android.Locations;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Controls.Maps;

using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using Location = Microsoft.Maui.Devices.Sensors.Location;

namespace RestaurantManagementApp.Pages;


    public partial class BillingPage : ContentPage
    {
        private readonly CartViewModel _cartViewModel;
        private readonly Location storeLocation = new Location(30.639781039829426, 76.80324656443854); // Example Store Location

        private const string GoogleApiKey = "5b3ce3597851110001cf62481889674ba2574cc0b16ca79dcf454f64"; // Replace with your API key

        public BillingPage(CartViewModel cartViewModel)
        {
            _cartViewModel = cartViewModel;
            InitializeComponent();
            BindingContext = _cartViewModel;
            LoadUserLocation();
        }

        private async void LoadUserLocation()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High));
                }

                if (location != null)
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)
                    {
                        address.Text = $"{placemark.Thoroughfare}, {placemark.Locality}, {placemark.AdminArea}, {placemark.PostalCode}";
                    }

                    await DisplayRoute(location, storeLocation);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Unable to get location. Please ensure location services are enabled and permissions are granted.", "OK");
            }
        }

        private async Task DisplayRoute(Location userLocation, Location storeLocation)
        {
            // Clear existing pins
            map.Pins.Clear();
            //map.Shapes.Clear(); // Clear previous routes if any

            // Add the store pin to the map
            var storePin = new Pin
            {
                Label = "Store Location",
                Address = "Static Store Address",
                Location = storeLocation
            };
            map.Pins.Add(storePin);

            // Add user's location pin
            var userPin = new Pin
            {
                Label = "Your Location",
                Location = userLocation
            };
            map.Pins.Add(userPin);

            // Calculate distance and duration
            var result = await GetDistanceAndDurationWithORS(userLocation, storeLocation);

            var (distance, duration) = result;

            // Update the UI with distance and duration
            await DisplayAlert("Route Information", $"Distance: {distance}, Duration: {duration}", "OK");

            // Update the CartViewModel with distance and delivery charges
            if (double.TryParse(distance.Replace(" km", ""), out var distanceInKm))
            {
                _cartViewModel.UpdateDistance(distanceInKm);
            }

            // Draw the route on the map

            DrawRouteOnMap(userLocation, storeLocation);


        }

        private async Task<(string Distance, string Duration)> GetDistanceAndDurationWithORS(Location origin, Location destination)
        {
            try
            {
                var httpClient = new HttpClient();
                var requestUrl = $"https://api.openrouteservice.org/v2/directions/driving-car?api_key={GoogleApiKey}&start={origin.Longitude},{origin.Latitude}&end={destination.Longitude},{destination.Latitude}&format=json";
                var response = await httpClient.GetStringAsync(requestUrl);
                var jsonResponse = JObject.Parse(response);

                // Extract distance and duration from the summary
                var feature = jsonResponse["features"]?.FirstOrDefault();
                if (feature != null)
                {
                    var properties = feature["properties"];
                    if (properties != null)
                    {
                        var segments = properties["segments"]?.FirstOrDefault();
                        if (segments != null)
                        {
                            var distance = segments["distance"]?.ToString();
                            var duration = segments["duration"]?.ToString();

                            // Convert duration from seconds to minutes
                            if (duration != null)
                            {
                                double durationInMinutes = Convert.ToDouble(duration) / 60;
                                duration = $"{durationInMinutes:0} mins";
                            }
                            if (distance != null)
                            {
                                double distanceInMeters = Convert.ToDouble(distance);
                                distance = $"{distanceInMeters / 1000:0.0} km";
                            }

                            // Return distance and duration
                            return (distance, duration);
                        }
                    }
                }

                // In case of missing summary
                return ("Unknown", "Unknown");
            }
            catch (Exception ex)
            {
                // Handle potential errors
                await DisplayAlert("Error", "Unable to retrieve distance and duration.", "OK");
                return ("Unknown", "Unknown");
            }
        }


        private async Task DrawRouteOnMap(Location userLoc, Location storeLoc)
        {
            try
            {
                var httpClient = new HttpClient();
                var requestUrl = $"https://api.openrouteservice.org/v2/directions/driving-car?api_key={GoogleApiKey}&start={userLoc.Longitude},{userLoc.Latitude}&end={storeLoc.Longitude},{storeLoc.Latitude}&format=geojson";
                var response = await httpClient.GetStringAsync(requestUrl);
                var jsonResponse = JObject.Parse(response);

                // Extract route coordinates
                var routeCoordinates = jsonResponse["features"]?
                    .FirstOrDefault()?
                    ["geometry"]?["coordinates"]?
                    .ToObject<List<List<double>>>();

                if (routeCoordinates != null)
                {
                    // Create a polyline
                    var polyline = new Microsoft.Maui.Controls.Maps.Polyline
                    {
                        StrokeColor = Colors.Blue,
                        StrokeWidth = 5
                    };

                    // Add individual points to Geopath
                    foreach (var coord in routeCoordinates)
                    {
                        var location = new Location(coord[1], coord[0]); // Note: [1] is latitude, [0] is longitude
                        polyline.Geopath.Add(location);
                    }

                    map.MapElements.Add(polyline);
                }
            }
            catch (Exception ex)
            {
                // Handle potential errors
                await DisplayAlert("Error", "Unable to draw the route on the map.", "OK");
            }
        }

    }
