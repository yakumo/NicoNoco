using CoreTweet;
using NicoNocoApp.Common.Strings;
using System;
using Xamarin.Forms;
using System.Globalization;

namespace NicoNocoApp.Common
{
    public interface IFakeMessage
    {
        long Id { get; }

        User Writer { get; }
        string Text { get; }

        bool IsRetweet { get; }
        User RetweetUser { get; }
    }

    public static class MessageExtension
    {
        public static string LabelText(this User user)
        {
            return String.Format(LocalizedString.UserLabelFormat, user.Name, user.ScreenName);
        }
    }

    public class MessageUserLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is User && targetType == typeof(string))
            {
                User u = value as User;
                return String.Format(LocalizedString.UserLabelFormat, u.Name, u.ScreenName);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
