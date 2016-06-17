using System;
using Android.Widget;
using System.Collections.Generic;
using Android.Views;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Square.Picasso;

namespace MLearning.Droid
{
	public class ChatListViewAdapter:BaseAdapter<ChatDataRow>
	{

		public List<ChatDataRow> mItems;
		private Context mContext;

		public ChatListViewAdapter (Context context, List<ChatDataRow> items):base()
		{
			this.mItems = items;
			this.mContext = context;
		}

		public override int Count
		{
			get {return mItems.Count; } 
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override ChatDataRow this[int position]
		{
			get{ return mItems [position];}
		}


		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null) 
			{
				row = LayoutInflater.From (mContext).Inflate (Resource.Layout.row_chat_list, parent, false);
			}
			LinearLayout ln_chat_row = row.FindViewById<LinearLayout> (Resource.Id.info_Row_CL);
			ln_chat_row.SetX (Configuration.getHeight (80));

			TextView name = row.FindViewById<TextView> (Resource.Id.textView_name_CL);
			name.Text = mItems [position].name;
			name.SetWidth (Configuration.getWidth (350));
			name.Typeface =  Typeface.CreateFromAsset(mContext.Assets, "fonts/HelveticaNeue.ttf");
			name.Ellipsize = TextUtils.TruncateAt.End;
			name.SetMaxLines (1);


			ImageView imProfile = row.FindViewById<ImageView> (Resource.Id.imageView_Row_CL);

			string path = mItems [position].imageProfile;
			Bitmap bm;
			if (path==null) {
				bm = getBitmapFromAsset ("icons/nouser.png");
				imProfile.SetImageBitmap (Bitmap.CreateScaledBitmap (bm,Configuration.getWidth (52), Configuration.getWidth(52),true));
				bm.Recycle ();

			} else {
				//bm=Configuration.GetImageBitmapFromUrl (path);
				Picasso.With(mContext).Load(path).Resize(Configuration.getWidth (52), Configuration.getWidth (52)).Into(imProfile);
			}

			bm = null;
				
			//imProfile.SetImageBitmap (Bitmap.CreateScaledBitmap (bm,Configuration.getWidth (52), Configuration.getHeight (52),true));
			imProfile.SetX (Configuration.getHeight (75));


			TextView state = row.FindViewById<TextView> (Resource.Id.textView_status_CL);
			state.Typeface =  Typeface.CreateFromAsset(mContext.Assets, "fonts/HelveticaNeue.ttf");

			if (mItems [position].state == 1) 
			{
				state.SetTextColor (Color.ParseColor ("#2ECCFA"));
				state.Text = "online now";
			}
			if (mItems [position].state == 0) 
			{
				state.SetTextColor (Color.ParseColor ("#A4A4A4"));
				state.Text = "offline";
			}

			//state.Text = mItems [position].state;
			row.SetBackgroundColor(Color.Transparent);


			return row;

		}

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = mContext.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}


	}
}

