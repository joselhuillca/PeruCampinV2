
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
using Square.Picasso;

namespace MLearning.Droid
{
	public class FrontContainerView : RelativeLayout
	{

		Drawable drBack;
		LinearLayout linearImageLO;
		LinearLayout linearLike;

		TextView txtNameLO;
		TextView txtChapter;
		TextView txtAuthor;
		TextView txtLike;

		int widthInDp;
		int heightInDp;

		ImageView imgHeart; 


		LinearLayout linearTextLO;

		LinearLayout linearContainerFisrst;
		//LinearLayout linearContainer;

		Context context;
		public FrontContainerView (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}


		public void setBack(Drawable dr, Bitmap bm)
		{
			//drBack = dr;
			linearContainerFisrst.SetBackgroundDrawable (dr);

			//Bitmap.CreateScaledBitmap (getBitmapFromAsset ("images/like.png"), Configuration.getWidth(43), Configuration.getHeight(43), true)
			imgHeart.SetImageBitmap (bm);


		}

		void Initialize ()
		{

			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);

			this.SetBackgroundColor (Color.ParseColor ("#000000"));


			linearContainerFisrst = new LinearLayout (context);
			//linearContainer = new LinearLayout (context);
			linearImageLO = new LinearLayout (context);
			linearTextLO = new LinearLayout (context);
			linearLike = new LinearLayout (context);

			txtAuthor = new TextView (context);
			txtChapter= new TextView (context);
			txtNameLO = new TextView (context);
			txtLike = new TextView (context);



			imgHeart= new ImageView (context);

			this.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(637));


			linearContainerFisrst.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);
			linearImageLO.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(637));
			linearTextLO.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(250));
			linearLike.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(120), Configuration.getHeight(80));


			//Drawable drBack =new BitmapDrawable(Bitmap.CreateScaledBitmap (getBitmapFromAsset ("images/fondocondiagonalm.png"), 640, 1136, true));
			//linearContainerFisrst.SetBackgroundDrawable (drBack);
			//drBack = null;


			linearTextLO.Orientation = Orientation.Vertical;
			linearTextLO.SetGravity(GravityFlags.Right);

			linearLike.Orientation = Orientation.Vertical;
			linearLike.SetGravity (GravityFlags.Center);



			linearContainerFisrst.Orientation = Orientation.Vertical;



			//Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("images/fondounidad.png"), 480, 640, true));
			//linearImageLO.SetBackgroundDrawable (d);

			//imgHeart.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("images/like.png"), Configuration.getWidth(43), Configuration.getHeight(43), true));



			txtNameLO.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (30));
			txtChapter.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (50));
			txtAuthor.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (30));
			txtNameLO.Typeface = Typeface.DefaultBold;


			txtAuthor.SetTextColor (Color.ParseColor("#ffffff"));
			txtChapter.SetTextColor (Color.ParseColor("#ffffff"));
			txtNameLO.SetTextColor (Color.ParseColor("#ffffff"));
			txtLike.SetTextColor (Color.ParseColor("#ffffff"));

			txtAuthor.Gravity = GravityFlags.Right;
			txtChapter.Gravity = GravityFlags.Right;
			txtNameLO.Gravity = GravityFlags.Right;
			txtLike.Gravity = GravityFlags.Center;

			linearTextLO.AddView (txtNameLO);
			linearTextLO.AddView (txtChapter);
			linearTextLO.AddView (txtAuthor);

			linearLike.AddView (imgHeart);
			linearLike.AddView (txtLike);

			linearTextLO.SetX (0); linearTextLO.SetY (Configuration.getHeight(398));

			linearLike.SetX (0); linearLike.SetY (Configuration.getHeight(438));
			linearContainerFisrst.SetX (0); linearContainerFisrst.SetY (0);

			linearImageLO.SetX (0); linearImageLO.SetY (0);

			linearTextLO.SetPadding (0, 0, Configuration.getWidth (35), 0);
			this.AddView (linearImageLO);
			this.AddView (linearTextLO);
			this.AddView (linearLike);
			this.AddView (linearContainerFisrst);

		}

		public LinearLayout Imagen{
			get {return linearImageLO; }
			//set { }

		}
	
		private String _author;
		public String Author{
			get{ return _author;}
			set{ _author = value;
				txtAuthor.Text = _author;	}

		}

		private String _chapter;
		public String Chapter{
			get{ return _chapter;}
			set{ _chapter = value;
				txtChapter.Text = _chapter;	}

		}

		private String _like;
		public String Like{
			get{ return _like;}
			set{ _like = value;
				txtLike.Text = _like;	}

		}

		private String _nameLO;
		public String NameLO{
			get{ return _nameLO;}
			set{ _nameLO = value;
				txtNameLO.Text = _nameLO;	}

		}

		private String _imageChapter;
		public String ImageChapter{
			get{ return _imageChapter;}
			set{ _imageChapter = value;
				/*
				Bitmap bm = Configuration.GetImageBitmapFromUrl (_imageChapter);
				Console.WriteLine ("ESTOy DESCARGANDOO IMAGENNNNNNNNNNnnnnn");
				Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (bm, 480, 640, true));
				linearImageLO.SetBackgroundDrawable (d);
				*/
				ImageView fondoChapter = new ImageView (context);
				//fondoChapter.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/imdownloading.png"), Configuration.getWidth (640), Configuration.getHeight (637), true));
				Picasso.With (context).Load (ImageChapter).Resize(Configuration.getWidth(640),Configuration.getHeight(640)).CenterCrop().Into (fondoChapter);
				linearImageLO.RemoveAllViews ();
				linearImageLO.AddView (fondoChapter);
				fondoChapter = null;

			}

		}

		private Bitmap _imageChapterBitmap;
		public Bitmap ImageChapterBitmap{
			set{ _imageChapterBitmap = value;
				Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (_imageChapterBitmap, 480, 640, true));
				linearImageLO.SetBackgroundDrawable (d);
				ImageChapterBitmap = null;
				}

		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

	}
}

