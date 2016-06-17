using System;
using Android.Widget;
using System.Collections.Generic;
using Android.Graphics;
using Android.Views;
using Android.Content;
using Android.Graphics.Drawables;

namespace MLearning.Droid
{
	public class CommentListViewAdapter:BaseAdapter<CommentDataRow>
	{

		public List<CommentDataRow> mItems;
		private Context mContext;

		public CommentListViewAdapter (Context context, List<CommentDataRow> items)
		{
			mContext = context;
			mItems = items;
		}


		public override int Count
		{
			get {return mItems.Count; } 
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override CommentDataRow this[int position]
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

			if (row == null) {
				row = LayoutInflater.From (mContext).Inflate (Resource.Layout.row_template, parent, false);
			}

			row.SetMinimumHeight (Configuration.getHeight (130));

			ImageView im_profie = row.FindViewById<ImageView> (Resource.Id.imageView_Row_TemplateList);

			ImageView row_image = row.FindViewById<ImageView> (Resource.Id.imageView_point_TemplateList);
			//row_image.SetX (Configuration.getWidth (102));

			LinearLayout ln_notif_info_row = row.FindViewById<LinearLayout> (Resource.Id.info_Row_TemplateList_LinearLayout_INFO);
			ln_notif_info_row.SetX (Configuration.getWidth(80));

			TextView name = row.FindViewById<TextView> (Resource.Id.textView_name_TemplateList);
			name.Text = mItems [position].name;
			//name.SetX (Configuration.getWidth (48));

			name.SetTypeface (null, TypefaceStyle.Bold);
			name.SetTextSize (Android.Util.ComplexUnitType.Px,Configuration.getHeight (26));


			TextView date = row.FindViewById<TextView> (Resource.Id.textView_date_TemplateList);
			date.Text = mItems [position].date;
			date.SetTextColor (Color.ParseColor ("#D5D5D5"));
			date.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (24));


			TextView comment = row.FindViewById<TextView> (Resource.Id.textView_comment_TemplateList);
			comment.Text = mItems [position].comment;
			comment.SetX(Configuration.getWidth(118));


			string path = mItems [position].im_profile;
			Bitmap bm;
			if (path==null) {
				bm = getBitmapFromAsset ("icons/nouser.png");


			} else {
				bm=Configuration.GetImageBitmapFromUrl (path);
			}

			 


			Bitmap newbm = Configuration.getRoundedShape(Bitmap.CreateScaledBitmap( bm, Configuration.getWidth(200), Configuration.getHeight(200), true)
				,Configuration.getWidth(60),Configuration.getHeight(60));



			im_profie.SetImageBitmap (newbm);
			im_profie.SetX (Configuration.getWidth (60));

			if (position % 2 == 0) 
			{
				Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/circplomo.png"), 100, 100, true));
				row_image.SetImageDrawable (d);
				row.SetBackgroundColor (Color.ParseColor ("#ffffff"));
				d = null;

			} else {
				Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/circplomo.png"), 100, 100, true));
				row_image.SetImageDrawable (d);
				d = null;
				row.SetBackgroundColor (Color.ParseColor ("#eeeeee"));
			}



			return row;

		}
	}
}

