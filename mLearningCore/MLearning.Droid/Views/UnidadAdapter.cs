using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Content;

namespace MLearning.Droid.Views
{
	public class UnidadAdapter : BaseAdapter<UnidadItem>
	{
		List<UnidadItem> list;
		Context context;
		Bitmap iconImage;

		public UnidadAdapter (Context context, List<UnidadItem> list):base()
		{
			this.context = context;
			this.list = list;

			iconImage= Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/chat.png"),Configuration.getWidth (45), Configuration.getWidth (40),true);


		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override UnidadItem this[int position]{
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

			LinearLayout ListItem = new LinearLayout (context);



			LinearLayout horizontalSpace = new LinearLayout (context);
			horizontalSpace.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(200));
			horizontalSpace.Orientation = Orientation.Horizontal;

			ImageView icon = new ImageView (context);
			icon.SetImageBitmap (iconImage);

			horizontalSpace.AddView (icon);


			LinearLayout linearContenido = new LinearLayout (context);
			linearContenido.LayoutParameters = new LinearLayout.LayoutParams (-2, -1);
			linearContenido.Orientation = Orientation.Vertical;
			linearContenido.SetGravity (Android.Views.GravityFlags.CenterVertical);

			TextView title = new TextView(context);
			title.Text = item.Title;

			TextView descripcion = new TextView (context);
			descripcion.Text = item.Description;

			linearContenido.AddView (title);
			linearContenido.AddView (descripcion);

			horizontalSpace.AddView (linearContenido);

			ListItem.AddView (horizontalSpace);

			return ListItem;


		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

	}
}

