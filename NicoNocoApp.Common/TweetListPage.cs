using NicoNocoApp.Common.Strings;
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
        ToolbarItem streamMenuItem;
        public string TweetText { get; set; }

        public TweetListPage()
        {
            ToolbarItems.Add(new ToolbarItem()
            {
                Text = LocalizedString.Tweet,
                Icon = "Images/tweet.png",
                Command = new Command(() => { ShowTweetPage(); }),
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
            });
            streamMenuItem = new ToolbarItem()
            {
                Text = LocalizedString.Stream,
                Icon = "Images/play.png",
                Command = new Command(() => { ToggleConnection(); }),
                Order = ToolbarItemOrder.Primary,
                Priority = 2,
            };
            ToolbarItems.Add(streamMenuItem);

            ToolbarItems.Add(new ToolbarItem()
            {
                Text = LocalizedString.Settings,
                Icon = "Images/settings.png",
                Command = new Command(() => { ShowSettingsPage(); }),
                Order = ToolbarItemOrder.Secondary,
                Priority = 1,
            });

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
                        HasUnevenRows = true,
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
                            Label retweetUserLabel=new Label()
                            {
                                HorizontalOptions = LayoutOptions.End,
                                VerticalOptions = LayoutOptions.Center,
                                FontSize = 10,
                                TextColor = Color.FromRgb(0.5,0.5,0.8),
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
                                                    HasShadow = true,
                                                    Content = statusLabel,
                                                },
                                                retweetUserLabel,
                                            }
                                        },
                                    },
                                },
                            };
                            nameLabel.SetBinding(Label.TextProperty, new Binding("Status.User.Name"));
                            idLabel.SetBinding(Label.TextProperty,new Binding("Status.User.ScreenName"));
                            statusLabel.SetBinding(Label.TextProperty, new Binding("Status.Text"));
                            iconImage.SetBinding(Image.SourceProperty, new Binding("Status.User.ProfileImageUrl"));
                            retweetUserLabel.SetBinding(Label.IsVisibleProperty, new Binding("IsRetweet"));
                            retweetUserLabel.SetBinding(Label.TextProperty, new Binding("RetweetUserLabel"));
                            return ret;
                        }),
                    },
                }
            };

            UpdateIcons();
            CommonData.Instance.IsConnect.Subscribe((flg) =>
            {
                UpdateIcons();
            });
        }

        private void ToggleConnection()
        {
            CommonData.Instance.IsConnect.Value = !CommonData.Instance.IsConnect.Value;
        }

        private void ShowTweetPage()
        {
            Navigation.PushAsync(new TweetInputPagecs(), true).ContinueWith((res) => { });
        }

        private void ShowSettingsPage()
        {
            Navigation.PushAsync(new SettingsPage(), true).ContinueWith((res) => { });
        }

        private void UpdateIcons()
        {
            streamMenuItem.Icon = (CommonData.Instance.IsConnect.Value) ? "Images/pause.png" : "Images/play.png";
        }

        private void StreamMenuItem_Clicked(object sender, EventArgs e)
        {
            CommonData.Instance.IsConnect.Value = !CommonData.Instance.IsConnect.Value;
        }
    }
}
