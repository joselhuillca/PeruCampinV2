
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MLearning.Droid
{
	public class VerticalScrollViewPager : ScrollView
	{
		private ScrollViewListenerPager scrollViewListener = null;
		Context context;
		public VerticalScrollViewPager (Context context) :
		base (context)
		{
			this.context = context;

		}

		public interface ScrollViewListenerPager
		{
			void OnScrollChangedPager(VerticalScrollViewPager v, int l, int t, int oldl, int oldt);
		}



		public void setOnScrollViewListener(ScrollViewListenerPager scrollViewListener) {
			this.scrollViewListener = scrollViewListener;
		}


		protected override void OnScrollChanged(int l, int t, int oldl, int oldt)
		{


			base.OnScrollChanged (l, t, oldl, oldt); 
			if (scrollViewListener != null) {
				scrollViewListener.OnScrollChangedPager (this, l, t, oldl, oldt);
			} 
		}
	}
}

