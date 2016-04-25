using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;

namespace NicoNocoApp.Common
{
    public class FakeDirectMessage : IFakeMessage
    {
        DirectMessage _message;

        public FakeDirectMessage(DirectMessage m)
        {
            _message = m;
        }

        public long Id { get { return _message.Id; } }

        public bool IsRetweet
        {
            get { return false; }
        }

        public DateTimeOffset Posted
        {
            get { return _message.CreatedAt; }
        }

        public User RetweetUser
        {
            get { return null; }
        }

        public string Text
        {
            get { return _message.Text; }
        }

        public User Writer
        {
            get { return _message.Sender; }
        }
    }
}
