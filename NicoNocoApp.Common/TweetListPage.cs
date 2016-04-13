using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NicoNocoApp.Common
{
    public class TweetListPage : ContentPage
    {
        public TweetListPage()
        {
            Content = new ListView()
            {
                ItemsSource = CommonData.Instance.TweetList,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label()
                    {
                    };
                    nameLabel.SetBinding(Label.TextProperty, new Binding("Name"));
                    ViewCell ret = new ViewCell()
                    {
                        View = new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical,
                            Children =
                            {
                                new StackLayout()
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    Children =
                                    {
                                        nameLabel,
                                    }
                                },
                            },
                        },
                    };
                    return ret;
                }),
            };
        }
    }
}
