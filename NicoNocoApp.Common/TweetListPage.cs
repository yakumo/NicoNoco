using NicoNocoApp.Common.Strings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;

namespace NicoNocoApp.Common
{
    public class TweetListPage : CarouselPage
    {
        ToolbarItem streamMenuItem;
        public string TweetText { get; set; }
        public ObservableCollection<TweetListTabItem> TabItems { get; } = new ObservableCollection<TweetListTabItem>();

        public static IValueConverter userLabelConverter = new MessageUserLabelConverter();
        public static IValueConverter retweetLabelConverter = new RetweetFromLabelConverter();
        public static IValueConverter postedDateFormatConverter = new PostedDateFormatConverter();

        public TweetListPage()
        {
            TabItems.Add(new TweetListTabItem()
            {
                TabIdentify = "timeline",
                TabName = LocalizedString.Timeline,
                TabItemSource = CommonData.Instance.TweetList,
            });
            TabItems.Add(new TweetListTabItem()
            {
                TabIdentify = "mensions",
                TabName = LocalizedString.Mensions,
                TabItemSource = CommonData.Instance.ReplyList,
            });
            TabItems.Add(new TweetListTabItem()
            {
                TabIdentify = "direct message",
                TabName = LocalizedString.DirectMessage,
                TabItemSource = CommonData.Instance.DMList,
            });

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

            ItemsSource = TabItems;
            ItemTemplate = new DataTemplate(() =>
            {
                Label tabNameLabel = new Label()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.End,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                };
                ListView itemsList = new ListView()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HasUnevenRows = true,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        Label nameLabel = new Label()
                        {
                            HorizontalOptions = LayoutOptions.Start,
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalTextAlignment = TextAlignment.Start,
                            VerticalTextAlignment = TextAlignment.Center,
                            FontSize = 11,
                            FontAttributes = FontAttributes.Bold,
                        };
                        Label postedDateLabel = new Label()
                        {
                            HorizontalOptions = LayoutOptions.EndAndExpand,
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalTextAlignment = TextAlignment.End,
                            VerticalTextAlignment = TextAlignment.Center,
                            TextColor = Color.FromRgb(0.8, 0.8, 0.9),
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
                            VerticalOptions = LayoutOptions.Start,
                        };
                        Label retweetUserLabel = new Label()
                        {
                            HorizontalOptions = LayoutOptions.End,
                            VerticalOptions = LayoutOptions.Center,
                            FontSize = 10,
                            TextColor = Color.FromRgb(0.5, 0.5, 0.8),
                        };
                        ViewCell cell = new ViewCell()
                        {
                            View = new StackLayout()
                            {
                                Padding = new Thickness(8, 3),
                                Orientation = StackOrientation.Horizontal,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.Start,
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
                                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                                VerticalOptions=LayoutOptions.Start,
                                                Children =
                                                {
                                                    nameLabel,
                                                    postedDateLabel,
                                                }
                                            },
                                            new Frame()
                                            {
                                                Padding = new Thickness(10, 5),
                                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                                VerticalOptions=LayoutOptions.End,
                                                OutlineColor = Color.FromRgb(0.7, 0.7, 0.85),
                                                HasShadow = true,
                                                Content = statusLabel,
                                            },
                                            retweetUserLabel,
                                        }
                                    },
                                },
                            },
                        };
                        nameLabel.SetBinding(Label.TextProperty, new Binding("Writer", BindingMode.OneWay, userLabelConverter));
                        postedDateLabel.SetBinding(Label.TextProperty, new Binding("Posted", BindingMode.OneWay, postedDateFormatConverter));
                        statusLabel.SetBinding(Label.TextProperty, new Binding("Text"));
                        iconImage.SetBinding(Image.SourceProperty, new Binding("Writer.ProfileImageUrl"));
                        retweetUserLabel.SetBinding(Label.IsVisibleProperty, new Binding("IsRetweet"));
                        retweetUserLabel.SetBinding(Label.TextProperty, new Binding("RetweetUser", BindingMode.OneWay, retweetLabelConverter));
                        return cell;
                    }),
                };

                Page ret = new ContentPage()
                {
                    BackgroundColor = Color.White,
                    Content = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            itemsList,
                            new StackLayout()
                            {
                                Padding = new Thickness(0, 1, 0, 0),
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.End,
                                BackgroundColor = Color.FromRgb(0.6,0.6,0.8),
                                Children =
                                {
                                    new StackLayout()
                                    {
                                        Padding = new Thickness(0, 15),
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.FillAndExpand,
                                        BackgroundColor = Color.White,
                                        Children =
                                        {
                                            tabNameLabel,
                                        },
                                    },
                                }
                            },
                        }, // Children
                    }, // Content
                };

                itemsList.SetBinding(ListView.ItemsSourceProperty, new Binding("TabItemSource"));
                tabNameLabel.SetBinding(Label.TextProperty, new Binding("TabName"));

                return ret;
            });

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

    public class TweetListTabItem
    {
        public string TabIdentify { get; set; }
        public string TabName { get; set; }
        public IEnumerable TabItemSource { get; set; }
    }
}
