
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
using Android.Graphics;
using Android.Graphics.Drawables;
using Square.Picasso;

namespace MLearning.Droid
{
	public class ImageIconMap : ImageView
	{
		Context context;


		public int index;


		public ImageIconMap (Context context) :
		base (context)
		{
			this.context = context;

		}




	}
}

