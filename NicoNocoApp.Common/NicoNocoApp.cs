﻿using System;
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
            CommonData.Instance.SaveAsync().ContinueWith((res) => { });
        }

        protected override void OnResume()
        {
            base.OnResume();
            CommonData.Instance.LoadAsync().ContinueWith((res) => { });
        }
    }
}
