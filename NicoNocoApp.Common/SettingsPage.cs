using NicoNocoApp.Common.Strings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;
using System.Reflection;
using Plugin.Settings.Abstractions;
using Plugin.Settings;
using System.Linq.Expressions;

namespace NicoNocoApp.Common
{
    class SettingsPage : ContentPage
    {
        enum SettingMode
        {
            Switch,
        }

        public SettingsPage()
        {
            StackLayout baseView = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            View v = SettingItemBuilder(LocalizedString.SettingsRememberReceiveSwitch, SettingMode.Switch, x => x.RememberReceiveSwitch);
            baseView.Children.Add(v);

            Content = baseView;

            this.BindingContext = CommonData.Instance;
        }

        View SettingItemBuilder(string label, SettingMode mode, Expression<Func<CommonData,object>> sourceProperty)
        {
            StackLayout v = new StackLayout()
            {
                Padding = new Thickness(10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            Label settingLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                Text = label,
            };
            v.Children.Add(settingLabel);
            switch (mode)
            {
                case SettingMode.Switch:
                    Switch switchView = new Switch()
                    {
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    };
                    switchView.SetBinding<CommonData>(Switch.IsToggledProperty, sourceProperty, BindingMode.TwoWay);
                    v.Children.Add(switchView);
                    break;
            }

            return v;
        }
    }
}
