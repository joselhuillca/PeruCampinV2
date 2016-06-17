
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
using Square.Picasso;
using Android.Text.Util;

namespace MLearning.Droid
{
	public class Template1 : RelativeLayout
	{
		RelativeLayout mainLayout;
		LinearLayout mainLinearLayout;
		LinearLayout mainHeaderLinearLayout;
		LinearLayout headerLinearLayout;
		LinearLayout contentLinearLayout;

		ImageView imHeader;
		TextView titleHeader;
		TextView AutorHeader;
		TextView content;


		int widthInDp;
		int heightInDp;

		Context context;

		public Template1 (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}

		void Initialize ()
		{
			var metrics = Resources.DisplayMetrics;
			widthInDp = metrics.WidthPixels;
			heightInDp = metrics.HeightPixels;
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);

			//ini ();
			ini2 ();
			//iniNotifList ();
			this.AddView (mainLayout);

		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}
		public void ini2(){

			mainLayout = new RelativeLayout (context);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);
			int padW = Configuration.getWidth(30);
			int padH = Configuration.getHeight (5);
			mainLayout.SetPadding (padW,padH,padW,padH);

			contentLinearLayout = new LinearLayout (context);
			contentLinearLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			contentLinearLayout.Orientation = Orientation.Vertical;


			imHeader = new ImageView (context);
			titleHeader = new TextView (context);
			AutorHeader = new TextView (context);
			content = new TextView (context);

			titleHeader.LayoutParameters = new LinearLayout.LayoutParams (-2, -2);
			AutorHeader.LayoutParameters = new LinearLayout.LayoutParams (-2, -2);
			content.LayoutParameters = new LinearLayout.LayoutParams (-2, -2);

			titleHeader.SetTextSize (ComplexUnitType.Fraction, Configuration.getHeight(38));
			titleHeader.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");

			content.SetTextSize (ComplexUnitType.Fraction, Configuration.getHeight(32));
			content.Typeface = Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");

			contentLinearLayout.AddView (titleHeader);
			contentLinearLayout.AddView (imHeader);
			contentLinearLayout.AddView (content);

			//titleHeader.SetBackgroundColor (Color.Red);
			//imHeader.SetBackgroundResource (Color.Green);
			//content.SetBackgroundColor (Color.Blue);

			mainLayout.AddView (contentLinearLayout);

		}
		public void ini(){


			var textFormat = Android.Util.ComplexUnitType.Px;

			var textFormatdip = Android.Util.ComplexUnitType.Dip;

			mainLayout = new RelativeLayout (context);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);

			mainLinearLayout = new LinearLayout (context);
			headerLinearLayout = new LinearLayout (context);
			contentLinearLayout = new LinearLayout (context);
			mainHeaderLinearLayout = new LinearLayout (context);


			imHeader = new ImageView (context);
			titleHeader = new TextView (context);
			AutorHeader = new TextView (context);
			content = new TextView (context);

			titleHeader.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			AutorHeader.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			content.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");


			mainLinearLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			mainHeaderLinearLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(125));
			contentLinearLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			headerLinearLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);

			mainLinearLayout.Orientation = Orientation.Vertical;
			mainHeaderLinearLayout.Orientation = Orientation.Horizontal;
			headerLinearLayout.Orientation = Orientation.Vertical;
			contentLinearLayout.Orientation = Orientation.Vertical;


			mainLinearLayout.AddView (mainHeaderLinearLayout);
			mainLinearLayout.AddView (contentLinearLayout);

			mainHeaderLinearLayout.AddView (imHeader);
			mainHeaderLinearLayout.AddView (headerLinearLayout);


			headerLinearLayout.AddView (titleHeader);
			headerLinearLayout.AddView (AutorHeader);

			headerLinearLayout.SetPadding (15, 0, 0, 10);
			AutorHeader.SetPadding (0, 15, 0, 0);

			contentLinearLayout.AddView (content);
			contentLinearLayout.SetPadding (0, 15, 0, 0);


			mainLinearLayout.SetBackgroundResource (Resource.Drawable.border);
			//			mainLinearLayout.SetX (Configuration.getHeight (45));
			//mainLinearLayout.SetY (Configuration.getWidth (500));



			//titleHeader.Text = "Diferentes tipos de aves en Perú";
			titleHeader.SetTextColor (Color.ParseColor ("#FF0080"));
			//titleHeader.SetTextSize (textFormat, Configuration.getHeight (38));
			titleHeader.SetTextSize (textFormatdip, 16.0f);

			titleHeader.SetMaxWidth (Configuration.getWidth (274));
			titleHeader.SetMaxHeight (Configuration.getHeight (80));
			//titleHeader.SetX (Configuration.getHeight (218));titleHeader.SetY (Configuration.getWidth (794-desviacion));
			titleHeader.Ellipsize = TextUtils.TruncateAt.End;
			titleHeader.SetMaxLines(2);


			//AutorHeader.Text = "Autor del Articulo";
			AutorHeader.SetTextColor(Color.ParseColor ("#424242"));
			AutorHeader.SetTextSize (textFormat, Configuration.getHeight (23));
			AutorHeader.SetMaxWidth (Configuration.getWidth (274));
			//AutorHeader.SetMaxHeight (Configuration.getHeight (25));
			//AutorHeader.SetX (Configuration.getHeight (218));AutorHeader.SetY (Configuration.getWidth (895-desviacion));
			AutorHeader.Ellipsize = TextUtils.TruncateAt.End;
			AutorHeader.SetMaxLines(1);

			//content.Text = "Los factores geográficos, climáticos y evolutivos  convierten al Perú en el mejor lugar para realizar la observacion de aves(birthwaching) Tiene 1830 especies de";
			//content.SetTextSize (textFormat, Configuration.getHeight (24));
			content.SetTextSize (textFormatdip, 12.0f);
			content.SetMaxWidth (Configuration.getWidth(501));
			//content.SetX (Configuration.getHeight (68));content.SetY (Configuration.getWidth (951-desviacion));
			//content.Ellipsize = TextUtils.TruncateAt.End;
			//content.SetMaxLines(4);

			//imHeader.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/user.png"),Configuration.getWidth (124), Configuration.getHeight (124),true));
			//imHeader.SetX (Configuration.getHeight (68));imHeader.SetY (Configuration.getWidth (792-desviacion));
			imHeader.SetMaxWidth (Configuration.getWidth (124));
			imHeader.SetMaxHeight (Configuration.getWidth (124));




			int padW = Configuration.getWidth(45);
			int padH = Configuration.getHeight (15);

			mainLayout.SetPadding (padW,padH,padW,padH);

			mainLayout.AddView (mainLinearLayout);

			/*
			mainLayout.AddView (titleHeader);
			mainLayout.AddView (AutorHeader);
			mainLayout.AddView (content);
			mainLayout.AddView (imHeader);
			*/
		}

		private string _color;
		public string ColorTexto{
			get{return _color; }
			set{_color = value;
				titleHeader.SetTextColor(Color.ParseColor(_color));
			}

		}

		private string _title;
		public string Title{
			get{return _title; }
			set{_title = value;
				if (_title == null) {
					contentLinearLayout.RemoveView (titleHeader);
				}
				_title = Configuration.quitarErrorTildes (_title);
				titleHeader.Text =  _title;
				Linkify.AddLinks (titleHeader, MatchOptions.All);//HUILLCA
			}

		}

		private string _author;
		public string Author{
			get{return _author; }
			set{_author = value;
				AutorHeader.Text = _author;
				//Linkify.AddLinks (AutorHeader, MatchOptions.All);//HUILLCA
			}

		}

		private string _content;
		public string Contenido{
			get{return _content; }
			set{_content = value;

				if (_content == null) {
					contentLinearLayout.RemoveView (content);
				}
				_content = Configuration.quitarErrorTildes (_content);
				content.TextFormatted = Html.FromHtml (_content);
				//Linkify.AddLinks(content,Java.Util.Regex.Pattern.Compile("\\W\\d+\\W\\s\\d+\\W\\d+"),"tel:");
				//Linkify.AddLinks(content,Java.Util.Regex.Pattern.Compile("\\d+\\W\\d+"),"tel:");
				//Linkify.AddLinks(content,Patterns.EmailAddress,"email:");
				Linkify.AddLinks(content,Patterns.WebUrl,"http://");//HUILLCA


				ViewTreeObserver vto = content.ViewTreeObserver;
				int H = 0;
				vto.GlobalLayout += (sender, args) =>
				{     
					H = content.Height;
					Console.WriteLine ("TAM:::1:" + H );
					content.LayoutParameters.Height = H/*-Configuration.getHeight(20)*/; //------ESPACIOS EN BLANCO

				};  
			}

		}

		private Bitmap _imageBitmap;
		public Bitmap Image{
			get{return _imageBitmap; }
			set{_imageBitmap = value;
				imHeader.SetImageBitmap (Bitmap.CreateScaledBitmap (_imageBitmap,Configuration.getWidth (60), Configuration.getHeight (60),true));

			}
		}

		private string _imageUrl;
		public string ImageUrl{
			get{return _imageUrl; }
			set{_imageUrl = value/*"/drawable/mapIcon.jpg"*/;
				/*
				Bitmap bm = Configuration.GetImageBitmapFromUrl (_imageUrl);
				imHeader.SetImageBitmap (Bitmap.CreateScaledBitmap (bm,Configuration.getWidth (60), Configuration.getHeight (60),true));
				bm = null;
				*/
				Picasso.With (context).Load (ImageUrl).Placeholder(context.Resources.GetDrawable (Resource.Drawable.progress_animation)).Resize(Configuration.getWidth(640),Configuration.getHeight(640)).CenterInside().Into (imHeader);
				//Picasso.With (context).Load (ImageUrl).Resize(Configuration.getWidth(640),Configuration.getHeight(640)).CenterCrop().Into (imHeader);
				//Picasso.With (context).Load (ImageUrl).CenterCrop().Into (imHeader);
			}

		}

	}
}

