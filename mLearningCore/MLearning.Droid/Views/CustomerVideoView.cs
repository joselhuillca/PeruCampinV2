
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
using Android.Text.Util;

namespace MLearning.Droid
{
	public class CustomerVideoView : RelativeLayout
	{

		Context context;
		RelativeLayout image;
		LinearLayout background;
		LinearLayout linearTitle;
		LinearLayout linearButton;
		TextView txtTitle;
		ImageButton imgPlay;

		VideoView video;

		public CustomerVideoView (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}



		void Initialize ()
		{
			this.LayoutParameters = new RelativeLayout.LayoutParams(-1,Configuration.getHeight (412));// LinearLayout.LayoutParams (Configuration.getWidth (582), Configuration.getHeight (394));
			this.SetGravity(GravityFlags.Center);


			image = new RelativeLayout(context);

			txtTitle = new TextView (context);
			background = new LinearLayout (context);
			linearTitle = new LinearLayout (context);
			linearButton = new LinearLayout (context);
			imgPlay = new ImageButton (context);
			video = new VideoView (context);

			image.SetGravity (GravityFlags.Bottom);
			background.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (582), Configuration.getHeight (111));
			background.Orientation = Orientation.Horizontal;
			background.SetBackgroundColor (Color.ParseColor ("#50000000"));

			linearTitle.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (482), Configuration.getHeight (111));
			linearTitle.Orientation = Orientation.Horizontal;
			linearTitle.SetGravity (GravityFlags.Center);

			linearButton.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (100), Configuration.getHeight (111));
			linearButton.Orientation = Orientation.Horizontal;
			linearButton.SetGravity (GravityFlags.Center);


			image.LayoutParameters = new RelativeLayout.LayoutParams (Configuration.getWidth (582), Configuration.getHeight (394));
			//image.SetGravity (GravityFlags.Bottom);
			video.LayoutParameters = new ViewGroup.LayoutParams(Configuration.getWidth(582),Configuration.getHeight(394));


			imgPlay.Alpha = 255.0f;
			imgPlay.SetBackgroundColor (Color.Transparent);

			txtTitle.SetTextColor (Color.ParseColor("#ffffff"));
			txtTitle.SetPadding (Configuration.getWidth (20), 0, 0, 0);
			txtTitle.SetTextSize (ComplexUnitType.Px, Configuration.getHeight (40));
			txtTitle.Ellipsize = Android.Text.TextUtils.TruncateAt.End;
			txtTitle.SetMaxLines (2);

			//imgPlay.SetImageBitmap (Bitmap.CreateStxtcaledBitmap (getBitmapFromAsset ("icons/"), Configuration.getWidth (83), Configuration.getHeight (83), true));
			linearTitle.AddView (txtTitle);
			linearButton.AddView (imgPlay);

			background.AddView (linearTitle);
			background.AddView (linearButton);



			image.AddView (background);

			this.AddView (image);
			//this.AddView (background);


		}

		private String _title;
		public String Title{
			get{ return _title;}
			set{ _title = value;
				txtTitle.Text = _title;
				Linkify.AddLinks (txtTitle, MatchOptions.All);//HUILLCA
			}

		}


		private String _urlVideo;
		public String UrlVideo{
			get{ return _urlVideo;}
			set{ _urlVideo = value;
				//Drawable dr = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset (_imagen), Configuration.getWidth (582), Configuration.getHeight (394), true));
				//image.SetBackgroundDrawable (dr);

			}


		}


		private String _imagen;
		public String Imagen{
			get{ return _imagen;}
			set{ _imagen = value;
				Bitmap bm = Configuration.GetImageBitmapFromUrl (_imagen);
				Drawable dr = new BitmapDrawable (Bitmap.CreateScaledBitmap (bm, Configuration.getWidth (582), Configuration.getHeight (394), true));

				image.SetBackgroundDrawable (dr);
			}


		}


		private String _imagenPlay;
		public String ImagenPlay{
			get{ return _imagenPlay;}
			set{ _imagenPlay = value;
				imgPlay.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset (_imagenPlay), Configuration.getWidth (83), Configuration.getHeight (83), true));
			}


		}




		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}
	}
}

