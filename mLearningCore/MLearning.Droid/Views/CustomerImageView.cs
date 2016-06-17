
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
using Android.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Android.Text.Util;

namespace MLearning.Droid
{
	public class CustomerImageView : RelativeLayout
	{
		Context context;
		LinearLayout image;
		LinearLayout background;
		TextView txtDescription;
		TextView txtTitle;
		ImageView imBack;

		LinearLayout relTemp;

		public CustomerImageView (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}


		void Initialize ()
		{
			this.LayoutParameters = new RelativeLayout.LayoutParams(-1,-2);// LinearLayout.LayoutParams (Configuration.getWidth (582), Configuration.getHeight (394));
			this.SetGravity(GravityFlags.CenterHorizontal);


			imBack = new ImageView (context);

			//imBack.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);

			image = new LinearLayout(context);
			txtDescription = new TextView (context);
			txtTitle = new TextView (context);
			background = new LinearLayout (context);
			relTemp = new LinearLayout(context);


			//LinearLayout.LayoutParams paramL = new new LinearLayout.LayoutParams (Configuration.getWidth (530), Configuration.getHeight (356));

			background.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);

			background.Orientation = Orientation.Vertical;



			//background.SetBackgroundColor (Color.ParseColor ("#50000000"));
			//background.BaselineAligned = true;

			image.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			image.Orientation = Orientation.Vertical;
			//image.SetGravity (GravityFlags.Center);

			relTemp.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			//relTemp.SetGravity (GravityFlags.Center);

			//RelativeLayout.LayoutParams param = new RelativeLayout.LayoutParams(Configuration.getWidth (530), Configuration.getHeight (356));

			//param.AddRule (LayoutRules.CenterInParent);

			relTemp.AddView (background);
			int space = Configuration.getWidth (30);
			relTemp.SetPadding (space, 0, space, 0);

			//txtTitle.SetTextColor (Color.ParseColor("#424242"));
			//txtDescription.SetTextColor(Color.ParseColor("#424242"));
			//txtTitle.SetTextSize (ComplexUnitType.Px, Configuration.getHeight (40));
			//txtDescription.SetTextSize (ComplexUnitType.Px, Configuration.getHeight (30));

			txtTitle.SetTextSize (ComplexUnitType.Fraction, Configuration.getHeight(38));
			txtDescription.SetTextSize (ComplexUnitType.Fraction, Configuration.getHeight(32));
			txtTitle.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");
			txtDescription.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");


			background.AddView (txtTitle);
			background.AddView (txtDescription);



			image.AddView (relTemp);
			image.AddView (imBack);
			imBack.SetPadding(space,space,space,space);
			this.AddView (image);
			//this.AddView (background);


		}

		private String _title;
		public String Title{
			get{ return _title;}
			set{ _title = value;
				if (_title == null) {
					background.RemoveView (txtTitle);
				}
				txtTitle.Text = _title;
				Linkify.AddLinks (txtTitle, MatchOptions.All);//HUILLCA
			}

		}

		private String _description;
		public String Description{
			get{ return _description;}
			set{ _description = value;

				if (_description == null) {
					background.RemoveView (txtDescription);
				}

				txtDescription.TextFormatted = Html.FromHtml (_description);
				Linkify.AddLinks (txtDescription, MatchOptions.All);//HUILLCA
				//txtDescription.Text = _description;
			}

		}

		private String _imagen;
		public String Imagen{
			get{ return _imagen;}
			set{ _imagen = value;
				//Bitmap bm = Configuration.GetImageBitmapFromUrl (_imagen);
				//Drawable dr = new BitmapDrawable (Bitmap.CreateScaledBitmap (bm, Configuration.getWidth (582), Configuration.getHeight (394), true));

				//image.SetBackgroundDrawable (dr);
				//Bitmap  bm = GetImageBitmapFromUrlAsync(_imagen);

				//imBack.SetImageBitmap (Bitmap.CreateScaledBitmap (bm, Configuration.getWidth (582), Configuration.getHeight (394), true));
				//bm = null;

				Picasso.With (context).Load (_imagen).Resize(Configuration.getWidth(640),Configuration.getHeight(640)).CenterInside().Into (imBack);

				/*Task task = new Task (DownloadImage);
				task.Start();
				task.Wait ();*/

			}


		}

		public async void  DownloadImage(){

			Bitmap  bm = await GetImageBitmapFromUrlAsync (_imagen);
			imBack.SetImageBitmap (Bitmap.CreateScaledBitmap (bm, Configuration.getWidth (582), Configuration.getHeight (394), true));
			bm = null;
		}

		private string _color;
		public string ColorTexto{
			get{return _color; }
			set{_color = value;
				txtTitle.SetTextColor(Color.ParseColor(_color));
			}

		}


		private Bitmap _imageBitmap;
		public Bitmap ImageBitmap{
			get{ return _imageBitmap;}
			set{ _imageBitmap = value;			

				Drawable dr = new BitmapDrawable (Bitmap.CreateScaledBitmap (_imageBitmap, Configuration.getWidth (582), Configuration.getHeight (394), true));
				image.SetBackgroundDrawable (dr);
			}

		}

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}


		public async Task<Bitmap> GetImageBitmapFromUrlAsync(string url)
		{
			Bitmap imageBitmap = null;

			using (var httpClient = new HttpClient())
			{
				var imageBytes = await httpClient.GetByteArrayAsync(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;
		}


	}
}

