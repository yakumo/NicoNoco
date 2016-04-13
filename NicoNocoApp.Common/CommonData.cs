using CoreTweet;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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

        public ReactiveCollection<object> TweetList { get; private set; }

        public ReactiveProperty<bool> IsAuthorized { get; set; }
        public ReactiveProperty<Tokens> Tokens { get; set; }
        [DataMember]
        public string AccessToken { get; set; }
        [DataMember]
        public string AccessTokenSecret { get; set; }

        #endregion

        #region codes

        public CommonData()
        {
            TweetList = new ReactiveCollection<object>();
            IsAuthorized = new ReactiveProperty<bool>(false);
            Tokens = new ReactiveProperty<Tokens>();

            Tokens.Subscribe((tok) =>
            {
                if (tok?.Account != null)
                {
                    IsAuthorized.Value = true;
                    AccessToken = tok.AccessToken;
                    AccessTokenSecret = tok.AccessTokenSecret;
                }
            });
        }

        public Task StartUpdator()
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    Debug.WriteLine("Start update");
                    await Task.Delay(TimeSpan.FromSeconds(60));
                }
            });
        }

        #endregion
    }
}
