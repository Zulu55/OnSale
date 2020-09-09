using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace OnSale.Common.Helpers
{
    public static class Settings
    {
        private const string _token = "token";
        private const string _isLogin = "isLogin";
        private const string _product = "product";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }

        public static string Product
        {
            get => AppSettings.GetValueOrDefault(_product, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_product, value);
        }
    }
}
