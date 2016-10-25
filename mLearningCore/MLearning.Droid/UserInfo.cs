using System;
using MLearning.Core;
using Android.Content.Res;

namespace MLearning.Droid
{
	public class UserInfo : IUserInfo
	{
		Android.Content.Context context;
		public UserInfo(Android.Content.Context context)
		{
			this.context = context;
		}

		public string user()
		{
			// Resources.GetText
			//var lang = Resources.Configuration.Locale;
			int val = Resource.String.LOGIN_USER;

			return this.context.Resources.GetText(val);
		}
		public string pass()
		{
			return "1";
		}
	}
}