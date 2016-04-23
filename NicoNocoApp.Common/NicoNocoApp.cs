using CoreTweet;
using CoreTweet.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NicoNocoApp.Common
{
    public class NicoNocoApp : Xamarin.Forms.Application
    {
        AuthorizePage authPage = null;

        public NicoNocoApp()
        {
            CommonData d = CommonData.Instance;

            MainPage = new NavigationPage(new TweetListPage()
            {
            });

            CommonData.Instance.IsAuthorized.Subscribe((val) =>
            {
                if (val)
                {
                    if (authPage != null)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MainPage.Navigation.PopModalAsync(true).ContinueWith((res) =>
                            {
                                authPage = null;
                            });
                        });
                    }
                }
                else
                {
                }
            });
            MainPage.Appearing += MainPage_Appearing;
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            if (!CommonData.Instance.IsAuthorized.Value)
            {
                CommonData.Instance.LoadAsync().ContinueWith((res) =>
                {
                    if (res.Result)
                    {
                        if (!CommonData.Instance.IsAuthorized.Value)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                authPage = new AuthorizePage();
                                MainPage.Navigation.PushModalAsync(authPage, true).ContinueWith((res1) =>
                                {
                                });
                            });
                        }
                        else
                        {
                            CommonData.Instance.Tokens.Value.Statuses.HomeTimelineAsync(count => 25).ContinueWith((res2) =>
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    foreach (var t in res2.Result.Reverse())
                                    {
                                        CommonData.Instance.TweetList.Insert(0, new FakeStatusMessage(t));
                                        CommonData.Instance.LastReceivedTweetId = t.Id;
                                    }
                                    if (CommonData.Instance.RememberReceiveSwitch)
                                    {
                                        CommonData.Instance.IsConnect.Value = CommonData.Instance.LastStreamSwitchValue;
                                    }
                                });
                            });
                        }
                    }
                });
            }
            else
            {
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            CommonData.Instance.LastStreamSwitchValue = CommonData.Instance.IsConnect.Value;
            CommonData.Instance.IsConnect.Value = false;
            CommonData.Instance.SaveAsync().ContinueWith((res) => { });
        }

        protected override void OnResume()
        {
            base.OnResume();
            CommonData.Instance.LoadAsync().ContinueWith((res) =>
            {
                if (CommonData.Instance.RememberReceiveSwitch)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        CommonData.Instance.IsConnect.Value = CommonData.Instance.LastStreamSwitchValue;
                    });
                }
            });
        }
    }
}
