using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Graphics;

namespace MLearning.Droid.Views
{
	public class TaskAdapter: BaseAdapter<TaskItem>
	{
		Activity context;
		List<TaskItem> list;
		public TaskAdapter (Activity context, List<TaskItem> list):base()
		{
			this.context = context;
			this.list = list;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override TaskItem this[int position]{
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

			txtName.Text = item.Name;
			txtName.SetTextColor (Color.ParseColor ("#ffffff"));
			txtName.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			imgIcon.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset (item.Asset), Configuration.getWidth (30), Configuration.getWidth (30), true));

			linearItem.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (89));
			linearItem.Orientation = Orientation.Horizontal;
			linearItem.SetGravity (Android.Views.GravityFlags.CenterVertical);
			linearItem.AddView (imgIcon);
			linearItem.AddView (txtName);

			imgIcon.SetPadding (Configuration.getWidth(68), 0, 0, 0);
			txtName.SetPadding (Configuration.getWidth(48), 0, 0, 0);

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

