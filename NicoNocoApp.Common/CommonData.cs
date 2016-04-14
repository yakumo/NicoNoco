using CoreTweet;
using CoreTweet.Streaming;
using Reactive;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NicoNocoApp.Common
{
    [DataContract]
    public class CommonData
    {
        #region instance
        private static CommonData _instance = null;
        public static CommonData Instance
        {
            get { return _instance ?? (_instance = new CommonData()); }
        }
        #endregion

        #region properties

        public ObservableCollection<StatusMessage> TweetList { get; private set; }

        public ReactiveProperty<bool> IsAuthorized { get; set; }
        public ReactiveProperty<Tokens> Tokens { get; set; }
        [DataMember]
        public string AccessToken { get; set; }
        [DataMember]
        public string AccessTokenSecret { get; set; }

        public ReactiveProperty<bool> IsConnect { get; set; }

        private IConnectableObservable<StreamingMessage> _StreamingMessage { get; set; }
        private IDisposable _StreamingDisposable { get; set; }

        #endregion

        #region codes

        public CommonData()
        {
            TweetList = new ReactiveCollection<StatusMessage>();
            IsAuthorized = new ReactiveProperty<bool>(false);
            Tokens = new ReactiveProperty<Tokens>();
            IsConnect = new ReactiveProperty<bool>(false);

            _StreamingMessage = null;
            _StreamingDisposable = null;

            Tokens.Subscribe((tok) =>
            {
                if (tok?.Account != null)
                {
                    IsAuthorized.Value = true;
                    AccessToken = tok.AccessToken;
                    AccessTokenSecret = tok.AccessTokenSecret;
                }
            });

            IsConnect.Subscribe((flg) =>
            {
            Debug.WriteLine("IsConnect.Subscribe:" + flg);
            if (flg)
            {
                if (_StreamingMessage == null)
                {
                    _StreamingMessage = this.Tokens.Value.Streaming.UserAsObservable().Publish();
                    _StreamingMessage.OfType<StatusMessage>().Subscribe(x =>
                    {
                        Debug.WriteLine(String.Format("{0}: {1}", x.Status.User.ScreenName, x.Status.Text));
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            TweetList.Insert(0, x);
                        });
                    });
                    }
                    if (_StreamingDisposable == null)
                    {
                        _StreamingDisposable = _StreamingMessage.Connect();
                    }
                }
                else
                {
                    if (_StreamingDisposable != null)
                    {
                        _StreamingDisposable.Dispose();
                        _StreamingDisposable = null;
                    }
                }
            });
        }

        public async Task<bool> LoadAsync()
        {
            return false;
        }

        public async Task<bool> SaveAsync()
        {
            return false;
        }

        #endregion
    }
}
