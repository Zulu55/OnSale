using Foundation;
using Plugin.FacebookClient;
using Prism;
using Prism.Ioc;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfNumericTextBox.XForms.iOS;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.MaskedEdit;
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
            Xamarin.FormsMaps.Init();
            new SfNumericTextBoxRenderer();
            SfTextInputLayoutRenderer.Init();
            LoadApplication(new App(new iOSInitializer()));
            SfMaskedEditRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            new SfBusyIndicatorRenderer();
            new SfRotatorRenderer();
            FacebookClientManager.Initialize(app, options);
            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            FacebookClientManager.OnActivated();
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return FacebookClientManager.OpenUrl(app, url, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return FacebookClientManager.OpenUrl(application, url, sourceApplication, annotation);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
