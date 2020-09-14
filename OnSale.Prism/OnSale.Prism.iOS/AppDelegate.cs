using Foundation;
using Prism;
using Prism.Ioc;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfNumericTextBox.XForms.iOS;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.TextInputLayout;
using UIKit;

namespace OnSale.Prism.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            new SfNumericTextBoxRenderer();
            SfTextInputLayoutRenderer.Init();
            LoadApplication(new App(new iOSInitializer()));
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            new SfBusyIndicatorRenderer();
            new SfRotatorRenderer();
            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
