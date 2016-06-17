
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
using Android.Text;

namespace MLearning.Droid
{
	public class PhraseView : RelativeLayout
	{
		TextView txtPhrase;
		TextView txtAuthor;
		ImageView imgComilla;
		ImageView imgBarra;
		Context context;

		RelativeLayout mainLayout;
		LinearLayout contentLinearLayout;

		int Altura =0;

		public PhraseView (Context context) :
		base (context)
		{
			this.context = context;
			ini ();
			//Initialize ();
		}

		void ini()
		{
			var textFormatdip = Android.Util.ComplexUnitType.Dip;


			mainLayout = new RelativeLayout (context);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);
			int padW = Configuration.getWidth(30);
			int padH = Configuration.getHeight (0);
			mainLayout.SetPadding (padW,padH,padW,padH);
			mainLayout.SetGravity(GravityFlags.CenterVertical);


			contentLinearLayout = new LinearLayout (context);
			contentLinearLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);
			contentLinearLayout.Orientation = Orientation.Vertical;
			contentLinearLayout.SetGravity (GravityFlags.Center);


			txtPhrase = new TextView (context);
			txtPhrase.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (400), -2);
			txtPhrase.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");
			txtPhrase.SetTextSize (ComplexUnitType.Fraction, Configuration.getHeight(35));
			txtPhrase.SetTextColor (Color.Red);



			ImageView background = new ImageView (context);
			background.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("images/steps.png"), 250,390, true));

			LinearLayout backgroundLayout = new LinearLayout (context);
			backgroundLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			backgroundLayout.Orientation = Orientation.Vertical;
			backgroundLayout.SetGravity (GravityFlags.CenterHorizontal);
			backgroundLayout.AddView (background);

			contentLinearLayout.AddView (txtPhrase);

			mainLayout.AddView (backgroundLayout);
			mainLayout.AddView (contentLinearLayout);
			this.AddView (mainLayout);

		}



		private String _phrase;
		public String Phrase{
			get{ return _phrase;}
			set{ _phrase = value;
				txtPhrase.TextFormatted = Html.FromHtml (_phrase);
				//txtPhrase.Text = _phrase;
			}

		}

		private String _author;
		public String Author{
			get{ return _author;}
			set{ _author = value;
				txtAuthor.Text = _author;
				//Altura = linearTextContainer.LayoutParameters.Height;
			}

		}

		private String _imagenComilla;
		public String ImagenComilla{
			get{ return _imagenComilla;}
			set{ _imagenComilla = value;

				imgComilla.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset (_imagenComilla), Configuration.getWidth( 30), Configuration.getHeight (26), true));
			}
		}

		private String _imagenBarra;
		public String ImagenBarra{
			get{ return _imagenBarra;}
			set{ _imagenBarra = value;
				//	int valor = Altura;// - Configuration.getHeight (30);
				//	imgBarra.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset (_imagenBarra), Configuration.getWidth( 10),100, true));
			}
		}



		private String _colortext;
		public String ColorText{
			get{ return _colortext;}
			set{ _colortext = value;
				txtPhrase.SetTextColor(Color.ParseColor(_colortext));	}

		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}


	}
}

