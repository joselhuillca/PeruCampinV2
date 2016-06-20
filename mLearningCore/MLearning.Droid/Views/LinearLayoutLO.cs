
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
	public class LinearLayoutLO : LinearLayout
	{
		Context context;
		public UnidadItem content; 

		public int index;

		

		public LinearLayoutLO (Context context) :
		base (context)
		{
			this.context = context;

		}





	}
}

