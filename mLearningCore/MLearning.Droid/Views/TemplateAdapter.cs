using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Graphics;
using Android.Content;

namespace MLearning.Droid
{
	public class TemplateAdapter:BaseAdapter<TemplateItem>
	{


		Context context;
		List<TemplateItem> list;

		public TemplateAdapter (Context context, List<TemplateItem> list):base()
		{
			this.context = context;
			this.list = list;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override TemplateItem this[int position]{
			get{ return list [position];}
		}

		public override int Count {
			get {
				return list.Count;
			}
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			var item = list [position];
			var metrics = context.Resources.DisplayMetrics;
			float  widthInDp = ((int)metrics.WidthPixels);
			float heightInDp = ((float)metrics.HeightPixels);
			LinearLayout view = new LinearLayout (context);
			LinearLayout linearItem = new LinearLayout (context);

			view.SetBackgroundColor (Color.White);

			TextView txtName = new TextView (context);
			ImageView imgIcon = new ImageView (context);

			txtName.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");

			txtName.Text = item.content;
			txtName.SetPadding (30, 0, 0, 0);
			txtName.SetTextColor (Color.Blue);
			imgIcon.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset (item.im_vinheta), Configuration.getWidth (20), Configuration.getWidth (20), true));

			linearItem.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearItem.Orientation = Orientation.Horizontal;
			linearItem.SetGravity (Android.Views.GravityFlags.CenterVertical);
			linearItem.AddView (imgIcon);
			linearItem.AddView (txtName);


			view.AddView (linearItem);
			return view;

		}	

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

	}
}

