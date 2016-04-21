using CoreTweet;
using NicoNocoApp.Common.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicoNocoApp.Common
{
    public class FakeStatusMessage
    {
        public FakeStatusMessage(Status s)
        {
            _targetStatus = s;
        }

        private Status _targetStatus;

        public Status Status
        {
            get { return _targetStatus.RetweetedStatus ?? _targetStatus; }
        }

        public bool IsRetweet
        {
            get { return _targetStatus.RetweetedStatus != null; }
        }

        public User RetweetUser
        {
            get { return IsRetweet ? _targetStatus.User : null; }
        }

        public string RetweetUserLabel
        {
            get { return IsRetweet ? String.Format(LocalizedString.RetweetBy, RetweetUser.Name, RetweetUser.ScreenName) : ""; }
        }
    }
}
