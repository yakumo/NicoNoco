using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NicoNocoApp.Common
{
    public class TweetListPage : ContentPage
    {
        Entry tweetEntry;
        Button tweetButton;
        public string TweetText { get; set; }

        public TweetListPage()
        {
            ToolbarItems.Add(new ToolbarItem("Streaming", null, () => { CommonData.Instance.IsConnect.Value = !CommonData.Instance.IsConnect.Value; }));
            tweetEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Placeholder = "What do you do",
            };
            tweetButton = new Button()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Text = "Tweet",
            };
            Content = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new ListView()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        ItemsSource = CommonData.Instance.TweetList,
                        ItemTemplate = new DataTemplate(() =>
                        {
                            Label nameLabel = new Label()
                            {
                                HorizontalOptions = LayoutOptions.Start,
                                VerticalOptions = LayoutOptions.Start,
                                HorizontalTextAlignment = TextAlignment.Start,
                                VerticalTextAlignment = TextAlignment.Center,
                                FontSize = 11,
                            };
                            Label idLabel=new Label()
                            {
                                HorizontalOptions = LayoutOptions.Start,
                                VerticalOptions = LayoutOptions.Start,
                                HorizontalTextAlignment = TextAlignment.Start,
                                VerticalTextAlignment = TextAlignment.Center,
                                FontSize = 11,
                            };
                            Label statusLabel = new Label()
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.Start,
                                HorizontalTextAlignment = TextAlignment.Start,
                                VerticalTextAlignment = TextAlignment.Start,
                                MinimumWidthRequest = 10,
                                LineBreakMode = LineBreakMode.CharacterWrap,
                            };
                            Image iconImage = new Image()
                            {
                                WidthRequest = 48,
                                HeightRequest = 48,
                                HorizontalOptions = LayoutOptions.Start,
                                VerticalOptions=LayoutOptions.Start,
                            };
                            ViewCell ret = new ViewCell()
                            {
                                View = new StackLayout()
                                {
                                    Padding = new Thickness(3,8),
                                    Orientation = StackOrientation.Horizontal,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    VerticalOptions=LayoutOptions.Start,
                                    Children =
                                    {
                                        iconImage,
                                        new StackLayout()
                                        {
                                            Orientation = StackOrientation.Vertical,
                                            HorizontalOptions = LayoutOptions.FillAndExpand,
                                            VerticalOptions=LayoutOptions.Start,
                                            Children =
                                            {
                                                new StackLayout()
                                                {
                                                    Orientation = StackOrientation.Horizontal,
                                                    HorizontalOptions = LayoutOptions.Start,
                                                    VerticalOptions=LayoutOptions.Start,
                                                    Children =
                                                    {
                                                        nameLabel,
                                                        new Label()
                                                        {
                                                            HorizontalOptions = LayoutOptions.Start,
                                                            VerticalOptions=LayoutOptions.Start,
                                                            HorizontalTextAlignment = TextAlignment.Start,
                                                            VerticalTextAlignment = TextAlignment.Start,
                                                            Text = " / ",
                                                            FontSize = 11,
                                                        },
                                                        idLabel,
                                                    }
                                                },
                                                new Frame()
                                                {
                                                    Padding = new Thickness(15, 10),
                                                    OutlineColor = Color.FromRgb(0.6, 0.8, 1.0),
                                                    Content = statusLabel,
                                                }
                                            }
                                        },
                                    },
                                },
                            };
                            nameLabel.SetBinding(Label.TextProperty, new Binding("Status.User.Name"));
                            idLabel.SetBinding(Label.TextProperty,new Binding("Status.User.ScreenName"));
                            statusLabel.SetBinding(Label.TextProperty, new Binding("Status.Text"));
                            iconImage.SetBinding(Image.SourceProperty, new Binding("Status.User.ProfileImageUrl"));
                            return ret;
                        }),
                    },
                    new StackLayout()
                    {
                        BindingContext = this,
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        Children =
                        {
                            tweetEntry,
                            tweetButton,
                        },
                    },
                }
            };
            tweetEntry.SetBinding(Entry.TextProperty, new Binding("TweetText", BindingMode.TwoWay));
            tweetEntry.TextChanged += TweetEntry_TextChanged;
            tweetButton.Clicked += TweetButton_Clicked;
        }

        private void TweetEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.NewTextValue))
            {
                tweetButton.IsEnabled = false;
            }
            else
            {
                tweetButton.IsEnabled = true;
            }
        }

        private void TweetButton_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TweetText))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    tweetEntry.IsEnabled = false;
                    tweetButton.IsEnabled = false;
                });
                CommonData.Instance.Tokens.Value.Statuses.UpdateAsync(status => TweetText).ContinueWith((res) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        tweetEntry.Text = TweetText = "";
                        tweetEntry.IsEnabled = true;
                        tweetButton.IsEnabled = true;
                    });
                });
            }
        }
    }
}
