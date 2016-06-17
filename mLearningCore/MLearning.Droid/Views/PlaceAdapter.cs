using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Graphics;
using Android.Content;
using Android.Graphics.Drawables;
using Square.Picasso;

namespace MLearning.Droid.Views
{
	public class PlaceAdapter: BaseAdapter<PlaceItem>
	{
		Context context;
		List<PlaceItem> list;
		Drawable background_row;

		public PlaceAdapter (Context context, List<PlaceItem> list):base()
		{
			this.context = context;
			this.list = list;
			background_row = new BitmapDrawable (getBitmapFromAsset("images/1header.png"));
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override PlaceItem this[int position]{
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

			TextView txtName = new TextView (context);
			ImageView imgIcon = new ImageView (context);

			txtName.Text = item.titulo;
			//txtName.SetTextColor (Color.ParseColor ("#ffffff"));
			txtName.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			txtName.TextSize = Configuration.getHeight (20);
			//imgIcon.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset (item.Asset), Configuration.getWidth (30), Configuration.getWidth (30), true));

			linearItem.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (120));
			//linearItem.SetBackgroundDrawable (background_row);
			linearItem.Orientation = Orientation.Horizontal;
			linearItem.SetGravity (Android.Views.GravityFlags.CenterVertical);
			//linearItem.AddView (imgIcon);


			LinearLayout imageLayout = new LinearLayout (context);
			imageLayout.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (180), Configuration.getHeight (120));
			ImageView iconImage = new ImageView (context);
			Picasso.With (context).Load (item.pathIcon).Resize(Configuration.getWidth(180),Configuration.getHeight(120)).CenterCrop().Into (iconImage);
			imageLayout.AddView (iconImage);


			linearItem.AddView (imageLayout);
			linearItem.AddView (txtName);
			int space = Configuration.getWidth (30);
			//linearItem.SetPadding (space,0,space,0);
			//imgIcon.SetPadding (Configuration.getWidth(68), 0, 0, 0);
			txtName.SetPadding (Configuration.getWidth(10), 0, 0, 0);

			//if (position % 2 == 0) {
				//linearItem.SetBackgroundColor (Color.ParseColor ("#F0AE11"));
				//txtName.SetTextColor (Color.White);
			//} else {
				txtName.SetTextColor (Color.ParseColor("#F0AE11"));
			//}


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

