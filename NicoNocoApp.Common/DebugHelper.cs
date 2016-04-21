using CoreTweet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicoNocoApp.Common
{
    static class DebugHelper
    {
        public static void Dump(this Status s)
        {
            Debug.WriteLine("status:"+s.Id);
            Debug.WriteLine(" created:" + s.CreatedAt);
            Debug.WriteLine(" retweetuser:" + s.CurrentUserRetweet);
            Debug.WriteLine(" fav:" + s.IsFavorited);
            Debug.WriteLine(" fav count:" + s.FavoriteCount);
            Debug.WriteLine(" reply screen name:" + s.InReplyToScreenName);
            Debug.WriteLine(" reply status id:" + s.InReplyToStatusId);
            Debug.WriteLine(" is retweeted:" + s.IsRetweeted);
            Debug.WriteLine(" retweet count:" + s.RetweetCount);
        }
    }
}
