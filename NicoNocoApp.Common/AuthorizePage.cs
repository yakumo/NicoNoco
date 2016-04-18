using CoreTweet;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NicoNocoApp.Common
{
    public class AuthorizePage : ContentPage
    {
        WebView codeWebView;
        Entry codeInput;
        Button enterButton;
        OAuth.OAuthSession session;
        public Tokens AccessToken { get; set; }

        public AuthorizePage()
        {
            codeWebView = new WebView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            codeInput = new Entry()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Placeholder = "Pin code",
            };
            enterButton = new Button()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Text = "Auth",
            };
            enterButton.Clicked += EnterButton_Clicked;
            Content = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    new StackLayout()
                    {
                        Padding = new Thickness(5,5),
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Start,
                        Children=
                        {
                            codeInput,
                            enterButton,
                        },
                        BackgroundColor = Color.FromRgb(0.9,0.9,1.0),
                    },
                    codeWebView,
                },
            };

            this.Appearing += AuthorizePage_Appearing;
        }

        private void AuthorizePage_Appearing(object sender, EventArgs e)
        {
            OAuth.AuthorizeAsync(Constants.Internal.ConsumerKey, Constants.Internal.ConsumerSecret).ContinueWith((res1) =>
            {
                session = res1.Result;
                string url = session.AuthorizeUri.ToString();
                Device.BeginInvokeOnMainThread(() =>
                {
                    codeWebView.Source = url;
                });
            });
        }

        private void EnterButton_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                string pin = codeInput.Text;
                codeInput.IsEnabled = false;
                enterButton.IsEnabled = false;
                session.GetTokensAsync(pin).ContinueWith((res) =>
                {
                    CommonData.Instance.Tokens.Value = Tokens.Create(Constants.Internal.ConsumerKey, Constants.Internal.ConsumerSecret, res.Result.AccessToken, res.Result.AccessTokenSecret);
                });
            });
        }
    }
}
