using NicoNocoApp.Common.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NicoNocoApp.Common
{
    class TweetInputPagecs : ContentPage
    {
        Editor tweetEntry;
        Button sendButton;

        public string TweetText { get; set; }

        public TweetInputPagecs()
        {
            tweetEntry = new Editor()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            sendButton = new Button()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                Text = LocalizedString.Tweet,
            };
            Content = new StackLayout()
            {
                Padding = new Thickness(5,5),
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    tweetEntry,
                    sendButton,
                }
            };

            tweetEntry.SetBinding<TweetInputPagecs>(Editor.TextProperty, x => x.TweetText, BindingMode.TwoWay);
            sendButton.Clicked += SendButton_Clicked;

            BindingContext = this;
        }

        private void SendButton_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TweetText))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    tweetEntry.IsEnabled = false;
                    sendButton.IsEnabled = false;
                    await CommonData.Instance.Tokens.Value.Statuses.UpdateAsync(status => TweetText);
                    tweetEntry.Text = TweetText = "";
                    tweetEntry.IsEnabled = true;
                    sendButton.IsEnabled = true;
                    await Navigation.PopAsync(true);
                });
            }
        }
    }
}
