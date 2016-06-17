using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MLearning.Droid
{
	public class VerticalScrollView : ScrollView
	{
		private ScrollViewListener scrollViewListener = null;
		Context context;
		public VerticalScrollView (Context context) :
			base (context)
		{
			this.context = context;
		
		}

		public interface ScrollViewListener
		{
			void OnScrollChanged(VerticalScrollView v, int l, int t, int oldl, int oldt);
		}



		public void setOnScrollViewListener(ScrollViewListener scrollViewListener) {
			this.scrollViewListener = scrollViewListener;
		}


		protected override void OnScrollChanged(int l, int t, int oldl, int oldt)
		{

			this.VerticalScrollBarEnabled = false;
			base.OnScrollChanged (l, t, oldl, oldt); 
			if (scrollViewListener != null) {
				scrollViewListener.OnScrollChanged (this, l, t, oldl, oldt);
			} 
		}
	}
}

