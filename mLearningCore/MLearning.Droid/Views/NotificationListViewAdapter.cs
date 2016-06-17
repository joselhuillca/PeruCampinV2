using System;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Android.Views;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace MLearning.Droid
{
	public class NotificationListViewAdapter:BaseAdapter<NotificationDataRow>
	{

		public List<NotificationDataRow> mItems;
		private Context mContext;

		public NotificationListViewAdapter (Context context, List<NotificationDataRow> items)
		{
			mItems = items;
			mContext = context;
		}

		public override int Count
		{
			get {return mItems.Count; } 
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override NotificationDataRow this[int position]
		{
			get{ return mItems [position];}
		}



		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = mContext.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}


		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;



			if (row == null) 
			{
				row = LayoutInflater.From (mContext).Inflate (Resource.Layout.row_notification_list, parent, false);
			}


			row.SetMinimumHeight (Configuration.getHeight (143));

			ImageView row_image = row.FindViewById<ImageView> (Resource.Id.imageView_Row_NL);
			row_image.SetX (Configuration.getWidth (52)-4);

			//row_image.SetImageResource (Resource.Id.icon);

			LinearLayout ln_chat_row = row.FindViewById<LinearLayout> (Resource.Id.info_Row_NL);
			//ln_chat_row.SetX (Configuration.getWidth(148));

			TextView date = row.FindViewById<TextView> (Resource.Id.textView_date_NL);
			date.Text = mItems [position].date;
			date.SetX (Configuration.getWidth (140));

			TextView comment = row.FindViewById<TextView> (Resource.Id.textView_comment_NL);
			comment.Text = mItems [position].comment;
			comment.SetX (Configuration.getWidth (140));

			comment.Typeface =  Typeface.CreateFromAsset(mContext.Assets, "fonts/HelveticaNeue.ttf");
			date.Typeface =  Typeface.CreateFromAsset(mContext.Assets, "fonts/HelveticaNeue.ttf");





			if (position % 2 == 0) 
			{

				Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/circplomo.png"), 100, 100, true));


				row_image.SetImageDrawable (d);

				row.SetBackgroundColor (Color.ParseColor ("#ffffff"));

			} else {

				Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/circplomo.png"), 100, 100, true));
				row_image.SetImageDrawable (d);
				row.SetBackgroundColor (Color.ParseColor ("#eeeeee"));
			}



			return row;

		}

	}
}

