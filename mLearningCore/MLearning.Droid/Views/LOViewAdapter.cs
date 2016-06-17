
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
using Android.Support.V4.View;

namespace MLearning.Droid
{
	public class LOViewAdapter : PagerAdapter
	{
		List<VerticalScrollViewPager> listScroll;
		Context context;
		public LOViewAdapter (Context context,List<VerticalScrollViewPager> listScroll) 

		{
			this.context = context;
			this.listScroll = listScroll;

		}


		public override bool IsViewFromObject (View view, Java.Lang.Object @object)
		{
			return view == ((VerticalScrollViewPager)@object);
		}

		public override int Count {
			get {
				return listScroll.Count;
			}
		}

		public override void DestroyItem (ViewGroup container, int position, Java.Lang.Object objectValue)
		{
			((ViewPager)container).RemoveView ((VerticalScrollViewPager)@objectValue);
		}

		public override Java.Lang.Object InstantiateItem (ViewGroup container, int position)
		{
			VerticalScrollViewPager scroll = new VerticalScrollViewPager (context);


			scroll = listScroll [position];

			((ViewGroup) container).AddView(scroll);
			return scroll;
		}
	}

}

