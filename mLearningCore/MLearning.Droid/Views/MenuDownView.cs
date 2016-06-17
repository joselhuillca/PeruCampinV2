
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

namespace MLearning.Droid
{
	public class MenuDownView : LinearLayout
	{
		Context context;
		ImageButton imgOp1;
		ImageButton imgOp2;
		ImageButton imgOp3;
		ImageButton imgOp4;
		ImageButton imgOp5;
		ImageButton imgOp6;
		TextView txtTitle;

		LinearLayout linearText;
		LinearLayout linearButtons;

		public MenuDownView (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}


		void Initialize ()
		{
			this.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(84));
			this.SetBackgroundColor (Color.ParseColor ("#eeeeee"));
			this.Orientation = Orientation.Horizontal;
			this.SetGravity (GravityFlags.CenterVertical);
			imgOp1 = new ImageButton (context);
			imgOp2 = new ImageButton (context);
			imgOp3 = new ImageButton (context);
			imgOp4 = new ImageButton (context);
			imgOp5 = new ImageButton (context);
			imgOp6 = new ImageButton (context);
			txtTitle = new TextView (context);


			linearText = new LinearLayout (context);
			linearButtons = new LinearLayout (context);


			linearText.LayoutParameters = new LinearLayout.LayoutParams(Configuration.getWidth(340),-1);
			linearButtons.LayoutParameters = new LinearLayout.LayoutParams(Configuration.getWidth(300),-1);

			imgOp1.SetImageBitmap(Bitmap.CreateScaledBitmap(getBitmapFromAsset ("icons/conta.png"), Configuration.getWidth (35), Configuration.getHeight (35), true));
			imgOp2.SetImageBitmap(Bitmap.CreateScaledBitmap(getBitmapFromAsset ("icons/contb.png"), Configuration.getWidth (35), Configuration.getHeight (35), true));
			imgOp3.SetImageBitmap(Bitmap.CreateScaledBitmap(getBitmapFromAsset ("icons/contc.png"), Configuration.getWidth (35), Configuration.getHeight (35), true));
			imgOp4.SetImageBitmap(Bitmap.CreateScaledBitmap(getBitmapFromAsset ("icons/contd.png"), Configuration.getWidth (35), Configuration.getHeight (35), true));
			imgOp5.SetImageBitmap(Bitmap.CreateScaledBitmap(getBitmapFromAsset ("icons/conte.png"), Configuration.getWidth (35), Configuration.getHeight (35), true));
			imgOp6.SetImageBitmap(Bitmap.CreateScaledBitmap(getBitmapFromAsset ("icons/contf.png"), Configuration.getWidth (35), Configuration.getHeight (35), true));

			initButtonColor (imgOp1);
			initButtonColor (imgOp2);
			initButtonColor (imgOp3);
			initButtonColor (imgOp4);
			initButtonColor (imgOp5);
			initButtonColor (imgOp6);

			linearText.Orientation = Orientation.Horizontal;
			linearButtons.Orientation = Orientation.Horizontal;
			linearText.SetGravity(GravityFlags.CenterVertical);
			linearText.SetGravity(GravityFlags.CenterVertical);

			txtTitle.SetTextColor (Color.ParseColor ("#969696"));
			txtTitle.SetTextSize (ComplexUnitType.Px, Configuration.getHeight (35));
			txtTitle.SetPadding (Configuration.getWidth (10), 0, 0, 0);

			linearText.AddView(txtTitle);
			linearButtons.AddView(imgOp1);
			linearButtons.AddView(imgOp2);
			linearButtons.AddView(imgOp3);
			linearButtons.AddView(imgOp4);
			linearButtons.AddView(imgOp5);
			linearButtons.AddView(imgOp6);

			this.AddView (linearText);
			this.AddView (linearButtons);
		}
		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		private String _title;
		public String Title{
			get{ return _title;}
			set{ _title = value;
				txtTitle.Text = _title;	}

		}
		public  void initButtonColor(ImageButton btn){
			btn.Alpha = 255;
			//btn.SetAlpha(255);
			btn.SetBackgroundColor(Color.Transparent);
		}
	}
}

