using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Graphics;
using Android.Content;
using Square.Picasso;

namespace MLearning.Droid
{
	public class CommentAdapter:BaseAdapter<CommentDataRow>
	{


		Context context;
		List<CommentDataRow> list;

		public CommentAdapter (Context context, List<CommentDataRow> list):base()
		{
			this.context = context;
			this.list = list;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override CommentDataRow this[int position]{
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
			TextView txtDate = new TextView (context);
			TextView txtComment = new TextView (context);
			ImageView imgProfile = new ImageView (context);
			ImageView imgCircle = new ImageView (context);

			txtName.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			txtDate.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			txtComment.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");

			txtName.Text = item.name;
			txtDate.Text = item.date;
			txtComment.Text = item.comment;

			imgCircle.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/circplomo"), Configuration.getWidth (20), Configuration.getWidth (20), true));
			//imgProfile.SetImageBitmap(Configuration.getRoundedShape(Bitmap.CreateScaledBitmap( getBitmapFromAsset(item.im_profile), Configuration.getWidth(45), Configuration.getWidth(45), true),Configuration.getWidth(45),Configuration.getHeight(45)));
			Picasso.With(context).Load(item.im_profile).Resize(Configuration.getWidth(45),Configuration.getWidth(45)).CenterCrop().Into(imgProfile);

			linearItem.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearItem.Orientation = Orientation.Vertical;
			linearItem.SetMinimumHeight (Configuration.getHeight (168));



			LinearLayout contentHeader = new LinearLayout (context);
			contentHeader.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			contentHeader.Orientation = Orientation.Horizontal;
			contentHeader.SetGravity (Android.Views.GravityFlags.CenterVertical);

			LinearLayout imContentLayout = new LinearLayout (context);
			imContentLayout.LayoutParameters = new LinearLayout.LayoutParams (-2, -2);

			imContentLayout.AddView (imgProfile);


			contentHeader.AddView (imgProfile);
			contentHeader.AddView (txtName);
			contentHeader.AddView (txtDate);

			linearItem.AddView (contentHeader);
			linearItem.AddView (txtComment);

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

