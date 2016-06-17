using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Graphics;
using Android.Content;
using MLearning.Droid;


namespace TaskView
{
	public class TaskViewAdapter: BaseAdapter<TaskViewItem>
	{
		Context context;
		List<TaskViewItem> list;
		public TaskViewAdapter(Context context, List<TaskViewItem> list):base(){
			
			this.context = context;
			this.list = list;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override TaskViewItem this[int position]{
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

			RelativeLayout relativeItem = new RelativeLayout (context);
			LinearLayout view = new LinearLayout (context);
			LinearLayout linearItem = new LinearLayout (context);
			TextView txtTask = new TextView (context);
			txtTask.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			ImageView img = new ImageView (context);

			linearItem.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(85));
			relativeItem.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);
			relativeItem.SetGravity (Android.Views.GravityFlags.CenterVertical);

			linearItem.Orientation = Orientation.Horizontal;
			linearItem.SetGravity (Android.Views.GravityFlags.Center);

			txtTask.Text = item.Tarea;
			txtTask.SetTextColor (Color.ParseColor ("#000000"));
			txtTask.Gravity = Android.Views.GravityFlags.CenterVertical;
			img.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset (item.Icon), Configuration.getWidth (30), Configuration.getHeight (30), true));


			img.SetX (Configuration.getWidth(61));
			txtTask.SetX (Configuration.getWidth(110));
			relativeItem.AddView (img);
			relativeItem.AddView (txtTask);
			linearItem.AddView (relativeItem);

			//linearItem.AddView (img);
//			linearItem.AddView (txtTask);

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

