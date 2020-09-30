using System.Collections.Generic;
using System.Threading.Tasks;
using OnSale.Common.Responses;
using OnSale.Common.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Position = Xamarin.Forms.Maps.Position;

namespace OnSale.Prism.Views
{
    public partial class MapPage : ContentPage
    {
        private readonly IGeolocatorService _geolocatorService;
        private readonly IApiService _apiService;

        public MapPage(IGeolocatorService geolocatorService, IApiService apiService)
        {
            InitializeComponent();
            _geolocatorService = geolocatorService;
            _apiService = apiService;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MoveMapToCurrentPositionAsync();
            LoadUsersAsync();
        }

        private async void LoadUsersAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<UserResponse>(url, "api", "/Account");

            if (!response.IsSuccess)
            {
                return;
            }

            List<UserResponse> users = (List<UserResponse>)response.Result;
            foreach (UserResponse user in users)
            {
                MyMap.Pins.Add(new Pin
                {
                    Address = user.Address,
                    Label = user.FullName,
                    Position = new Position(user.Latitude, user.Logitude),
                    Type = PinType.Place
                });
            }
        }

        private async void MoveMapToCurrentPositionAsync()
        {
            bool isLocationPermision = await CheckLocationPermisionsAsync();

            if (isLocationPermision)
            {
                MyMap.IsShowingUser = true;

                await _geolocatorService.GetLocationAsync();
                if (_geolocatorService.Latitude != 0 && _geolocatorService.Longitude != 0)
                {
                    Position position = new Position(
                        _geolocatorService.Latitude,
                        _geolocatorService.Longitude);
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                        position,
                        Distance.FromKilometers(.5)));
                }
            }
        }

        private async Task<bool> CheckLocationPermisionsAsync()
        {
            Plugin.Permissions.Abstractions.PermissionStatus permissionLocation = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            Plugin.Permissions.Abstractions.PermissionStatus permissionLocationAlways = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
            Plugin.Permissions.Abstractions.PermissionStatus permissionLocationWhenInUse = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            bool isLocationEnabled = permissionLocation == Plugin.Permissions.Abstractions.PermissionStatus.Granted ||
                                        permissionLocationAlways == Plugin.Permissions.Abstractions.PermissionStatus.Granted ||
                                        permissionLocationWhenInUse == Plugin.Permissions.Abstractions.PermissionStatus.Granted;
            if (isLocationEnabled)
            {
                return true;
            }

            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);

            permissionLocation = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            permissionLocationAlways = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
            permissionLocationWhenInUse = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            return permissionLocation == Plugin.Permissions.Abstractions.PermissionStatus.Granted ||
                    permissionLocationAlways == Plugin.Permissions.Abstractions.PermissionStatus.Granted ||
                    permissionLocationWhenInUse == Plugin.Permissions.Abstractions.PermissionStatus.Granted;
        }
    }
}
