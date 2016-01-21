namespace Fluffy.ViewModels.Converters
{
    using Cirrious.MvvmCross.Localization;
    using Cirrious.MvvmCross.Plugins.Visibility;

    public class Converters
    {
        public readonly MvxVisibilityValueConverter Visibility = new MvxVisibilityValueConverter();
        public readonly MvxLanguageConverter Language = new MvxLanguageConverter();
    }
}