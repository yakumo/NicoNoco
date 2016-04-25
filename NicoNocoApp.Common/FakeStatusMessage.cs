using CoreTweet;
using NicoNocoApp.Common.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicoNocoApp.Common
{
    public class FakeStatusMessage : IFakeMessage
    {
        public FakeStatusMessage(Status s)
        {
            _targetStatus = s;
        }

        private Status _targetStatus;

        private Status Status
        {
            get { return _targetStatus.RetweetedStatus ?? _targetStatus; }
        }

        public long Id { get { return _targetStatus.Id; } }

        public User Writer
        {
            get { return Status.User; }
        }

        public string Text
        {
            get { return Status.Text; }
        }

        public DateTimeOffset Posted
        {
            get { return Status.CreatedAt; }
        }

        public bool IsRetweet
        {
            get { return _targetStatus.RetweetedStatus != null; }
        }

        public User RetweetUser
        {
            get { return IsRetweet ? _targetStatus.User : null; }
        }
    }
}
