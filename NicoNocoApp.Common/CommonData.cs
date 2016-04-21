using CoreTweet;
using CoreTweet.Streaming;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
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
        public ObservableCollection<StatusMessage> ReplyList { get; private set; }
        public ObservableCollection<DirectMessage> DMList { get; private set; }

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
                if (flg)
                {
                    if (_StreamingMessage == null)
                    {
                        _StreamingMessage = this.Tokens.Value.Streaming.UserAsObservable().Publish();
                        _StreamingMessage.Subscribe(x =>
                        {
                            Debug.WriteLine("type:" + x.Type);
                        });
                        _StreamingMessage.OfType<StatusMessage>().Subscribe(x =>
                        {
                            Debug.WriteLine(String.Format("{0}: {1}", x.Status.User.ScreenName, x.Status.Text));
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                TweetList.Insert(0, x);
                            });
                        });
                        _StreamingMessage.OfType<EventMessage>().Subscribe(x =>
                        {
                            Debug.WriteLine(String.Format("event: {0}: {1}", x.Source.Name, x.Target.Name));
                        });
                        _StreamingMessage.OfType<WarningMessage>().Subscribe(x =>
                        {
                            Debug.WriteLine(String.Format("warning: {0}: {1}", x.Code, x.Message));
                        });
                        _StreamingMessage.OfType<DeleteMessage>().Subscribe(x =>
                        {
                            Debug.WriteLine(String.Format("delete: {0}: {1}", x.Id, x.UserId));
                        });
                        _StreamingMessage.OfType<DirectMessageMessage>().Subscribe(x =>
                        {
                            Debug.WriteLine(String.Format("DM: {0}: {1}", x.DirectMessage.Sender.Name, x.DirectMessage.Text));
                        });
                        _StreamingMessage.OfType<DisconnectMessage>().Subscribe(x =>
                        {
                            Debug.WriteLine(String.Format("disconnect: {0}: {1}", x.Code, x.Reason));
                        });
                    }
                    if (_StreamingDisposable == null)
                    {
                        try
                        {
                            Debug.WriteLine("start connection");
                            _StreamingDisposable = _StreamingMessage.Connect();
                            Debug.WriteLine("connected");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("connecting exception");
                            Debug.WriteLine(ex);
                            _StreamingDisposable = null;
                        }
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

        const string AccessTokenKey = "AccessToken";
        const string AccessTokenSecretKey = "AccessTokenSecret";

        public async Task<bool> LoadAsync()
        {
            bool ret = false;
            try
            {
                ISettings settings = CrossSettings.Current;
                string appAccessToken = settings.GetValueOrDefault<string>(AccessTokenKey, String.Empty);
                if (!String.IsNullOrEmpty(appAccessToken))
                {
                    AccessToken = appAccessToken;
                }
                string appAccessSecret = settings.GetValueOrDefault<string>(AccessTokenSecretKey, String.Empty);
                if (!String.IsNullOrEmpty(appAccessSecret))
                {
                    AccessTokenSecret = appAccessSecret;
                }
                if (!String.IsNullOrEmpty(AccessToken) && !String.IsNullOrEmpty(AccessTokenSecret))
                {
                    Tokens t = CoreTweet.Tokens.Create(Constants.Internal.ConsumerKey, Constants.Internal.ConsumerSecret, AccessToken, AccessTokenSecret);
                    if (t != null)
                    {
                        this.Tokens.Value = t;
                    }
                }
                if (this.Tokens.Value?.Account != null)
                {
                    IsAuthorized.Value = true;
                    AccessToken = this.Tokens.Value.AccessToken;
                    AccessTokenSecret = this.Tokens.Value.AccessTokenSecret;
                }
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }

        public async Task<bool> SaveAsync()
        {
            ISettings settings = CrossSettings.Current;
            settings.AddOrUpdateValue<string>(AccessTokenKey, AccessToken);
            settings.AddOrUpdateValue<string>(AccessTokenSecretKey, AccessTokenSecret);
            return false;
        }

        #endregion
    }
}
