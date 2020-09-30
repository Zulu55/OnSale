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

        public static string PictureSource => Resource.PictureSource;

        public static string Cancel => Resource.Cancel;

        public static string FromCamera => Resource.FromCamera;

        public static string FromGallery => Resource.FromGallery;

        public static string NoCameraSupported => Resource.NoCameraSupported;

        public static string NoGallerySupported => Resource.NoGallerySupported;

        public static string RecoverPassword => Resource.RecoverPassword;

        public static string RecoverPasswordMessage => Resource.RecoverPasswordMessage;

        public static string ChangePassword => Resource.ChangePassword;

        public static string ChangeUserMessage => Resource.ChangeUserMessage;

        public static string ConfirmNewPassword => Resource.ConfirmNewPassword;

        public static string ConfirmNewPasswordError1 => Resource.ConfirmNewPasswordError1;

        public static string ConfirmNewPasswordError2 => Resource.ConfirmNewPasswordError2;

        public static string ConfirmNewPasswordPlaceHolder => Resource.ConfirmNewPasswordPlaceHolder;

        public static string CurrentPassword => Resource.CurrentPassword;

        public static string CurrentPasswordError => Resource.CurrentPasswordError;

        public static string CurrentPasswordPlaceHolder => Resource.CurrentPasswordPlaceHolder;

        public static string NewPassword => Resource.NewPassword;

        public static string NewPasswordError => Resource.NewPasswordError;

        public static string NewPasswordPlaceHolder => Resource.NewPasswordPlaceHolder;

        public static string Error005 => Resource.Error005;

        public static string ChangePassworrdMessage => Resource.ChangePassworrdMessage;

        public static string Quantity => Resource.Quantity;

        public static string QuantityError => Resource.QuantityError;

        public static string QuantityPlaceHolder => Resource.QuantityPlaceHolder;

        public static string AddToCartMessage => Resource.AddToCartMessage;

        public static string ProductExistInOrder => Resource.ProductExistInOrder;

        public static string Value => Resource.Value;

        public static string FinishOrder => Resource.FinishOrder;

        public static string Yes => Resource.Yes;

        public static string No => Resource.No;

        public static string ClearAllConfirm => Resource.ClearAllConfirm;

        public static string Total => Resource.Total;

        public static string Items => Resource.Items;

        public static string Delete => Resource.Delete;

        public static string DeleteProductInOrderConfirm => Resource.DeleteProductInOrderConfirm;

        public static string ModifyOrder => Resource.ModifyOrder;

        public static string FinishOrderMessage => Resource.FinishOrderMessage;

        public static string PaymentMethod => Resource.PaymentMethod;

        public static string PaymentMethodError => Resource.PaymentMethodError;

        public static string PaymentMethodPlaceHolder => Resource.PaymentMethodPlaceHolder;

        public static string Cash => Resource.Cash;

        public static string CreditCard => Resource.CreditCard;

        public static string DeliveryAddress => Resource.DeliveryAddress;

        public static string DeliveryAddressError => Resource.DeliveryAddressError;

        public static string DeliveryAddressPlaceHolder => Resource.DeliveryAddressPlaceHolder;

        public static string Expiry => Resource.Expiry;

        public static string ExpiryError => Resource.ExpiryError;

        public static string CreditCardError => Resource.CreditCardError;

        public static string CVV => Resource.CVV;

        public static string CVVError => Resource.CVVError;

        public static string CreditCardNoValid => Resource.CreditCardNoValid;

        public static string PayNoOk => Resource.PayNoOk;

        public static string Order => Resource.Order;

        public static string UpdateRemarks => Resource.UpdateRemarks;

        public static string CancelOrder => Resource.CancelOrder;

        public static string OrderStatus => Resource.OrderStatus;

        public static string SearchOrder => Resource.SearchOrder;

        public static string DateSent => Resource.DateSent;

        public static string DateConfirmed => Resource.DateConfirmed;

        public static string RemarksError => Resource.RemarksError;

        public static string OrderUpdatedOk => Resource.OrderUpdatedOk;

        public static string CancelOrdenConfirm => Resource.CancelOrdenConfirm;

        public static string Question => Resource.Question;

        public static string LoginFacebook => Resource.LoginFacebook;

        public static string ChangeOnSocialNetwork => Resource.ChangeOnSocialNetwork;
    
        public static string Buyers => Resource.Buyers;
    }
}
