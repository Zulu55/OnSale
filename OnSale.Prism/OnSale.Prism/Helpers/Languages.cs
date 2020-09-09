using System.Globalization;
using OnSale.Common.Helpers;
using OnSale.Prism.Resources;
using Xamarin.Forms;

namespace OnSale.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;

        public static string Loading => Resource.Loading;

        public static string SearchProduct => Resource.SearchProduct;

        public static string Name => Resource.Name;

        public static string Description => Resource.Description;

        public static string Price => Resource.Price;

        public static string Category => Resource.Category;

        public static string IsStarred => Resource.IsStarred;

        public static string AddToCart => Resource.AddToCart;

        public static string Product => Resource.Product;

        public static string Products => Resource.Products;

        public static string Login => Resource.Login;

        public static string ShowShoppingCar => Resource.ShowShoppingCar;

        public static string ShowPurchaseHistory => Resource.ShowPurchaseHistory;

        public static string ModifyUser => Resource.ModifyUser;

        public static string Email => Resource.Email;

        public static string EmailError => Resource.EmailError;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string ForgotPassword => Resource.ForgotPassword;

        public static string LoginError => Resource.LoginError;

        public static string Logout => Resource.Logout;

        public static string LoginFirstMessage => Resource.LoginFirstMessage;

        public static string Qualification => Resource.Qualification;

        public static string Qualifications => Resource.Qualifications;

        public static string QualificationNumber => Resource.QualificationNumber;

        public static string Details => Resource.Details;

        public static string RemarksPlaceHolder => Resource.RemarksPlaceHolder;

        public static string QualificationError => Resource.QualificationError;

        public static string NewQualification => Resource.NewQualification;

        public static string Save => Resource.Save;

        public static string Register => Resource.Register;

        public static string Document => Resource.Document;

        public static string DocumentError => Resource.DocumentError;

        public static string DocumentPlaceHolder => Resource.DocumentPlaceHolder;

        public static string FirstName => Resource.FirstName;

        public static string FirstNameError => Resource.FirstNameError;

        public static string FirstNamePlaceHolder => Resource.FirstNamePlaceHolder;

        public static string LastName => Resource.LastName;

        public static string LastNameError => Resource.LastNameError;

        public static string LastNamePlaceHolder => Resource.LastNamePlaceHolder;

        public static string Address => Resource.Address;

        public static string AddressError => Resource.AddressError;

        public static string AddressPlaceHolder => Resource.AddressPlaceHolder;

        public static string Phone => Resource.Phone;

        public static string PhoneError => Resource.PhoneError;

        public static string PhonePlaceHolder => Resource.PhonePlaceHolder;

        public static string City => Resource.City;

        public static string CityError => Resource.CityError;

        public static string CityPlaceHolder => Resource.CityPlaceHolder;

        public static string Department => Resource.Department;

        public static string DepartmentError => Resource.DepartmentError;

        public static string DepartmentPlaceHolder => Resource.DepartmentPlaceHolder;

        public static string Country => Resource.Country;

        public static string CountryError => Resource.CountryError;

        public static string CountryPlaceHolder => Resource.CountryPlaceHolder;

        public static string PasswordConfirm => Resource.PasswordConfirm;

        public static string PasswordConfirmError1 => Resource.PasswordConfirmError1;

        public static string PasswordConfirmError2 => Resource.PasswordConfirmError2;

        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;

        public static string Error001 => Resource.Error001;

        public static string Error002 => Resource.Error002;

        public static string Error003 => Resource.Error003;

        public static string Error004 => Resource.Error004;

        public static string Ok => Resource.Ok;

        public static string RegisterMessge => Resource.RegisterMessge;
    }
}
