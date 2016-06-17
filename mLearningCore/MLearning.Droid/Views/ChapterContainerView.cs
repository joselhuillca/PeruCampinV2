
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
	public class ChapterContainerView : LinearLayout
	{
		Context context;
		ImageView imgUser;
		TextView txtTitle;
		TextView txtAuthor;
		TextView txtContainer;
		HorizontalScrollView scrollImage;
		LinearLayout linearPanelScroll;

		Bitmap defaultUser;


		LinearLayout linearImage;
		LinearLayout linearContainer;
		LinearLayout linearAll;
		View linea_separador;

	

		public ChapterContainerView (Context context) :
		base (context)
		{
			this.context = context;
	
			Initialize ();
		}



		void Initialize ()
		{
			this.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			this.Orientation = Orientation.Vertical;

			linea_separador = new View (context);
			linea_separador.LayoutParameters = new ViewGroup.LayoutParams (-1, 5);
			linea_separador.SetBackgroundColor (Color.ParseColor ("#eeeeee"));

			imgUser = new ImageView (context);
			scrollImage = new HorizontalScrollView (context);
			scrollImage.HorizontalScrollBarEnabled = false;
			linearImage = new LinearLayout (context);
			linearPanelScroll = new LinearLayout (context);
			linearContainer = new LinearLayout (context);
			linearAll = new LinearLayout (context);

			txtAuthor = new TextView (context);
			txtContainer = new TextView (context);
			txtTitle = new TextView (context);


			linearAll.LayoutParameters = new LinearLayout.LayoutParams (-1,-2);
			linearImage.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(140),-2);
			linearContainer.LayoutParameters = new LinearLayout.LayoutParams(Configuration.getWidth(500),-2);
			linearPanelScroll.LayoutParameters = new LinearLayout.LayoutParams (-2,-2);
			scrollImage.LayoutParameters = new ViewGroup.LayoutParams (Configuration.getWidth (500), -2);


			linearAll.Orientation = Orientation.Horizontal;
			linearImage.Orientation = Orientation.Vertical;
			linearContainer.Orientation = Orientation.Vertical;

			txtTitle.SetTextSize (ComplexUnitType.Px, Configuration.getHeight(40));
			txtAuthor.SetTextSize (ComplexUnitType.Px, Configuration.getHeight (30));
			txtContainer.SetTextSize(ComplexUnitType.Px, Configuration.getHeight (45));
			txtAuthor.SetTextColor (Color.ParseColor("#3c3c3c"));
			txtContainer.SetTextColor (Color.ParseColor("#3c3c3c"));

			txtTitle.SetSingleLine (true);
			txtAuthor.SetSingleLine (true);
			txtContainer.SetSingleLine (false);
			txtContainer.SetPadding (0, 0, 20, 0);
			txtContainer.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeueLight.ttf");
			txtAuthor.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeueLight.ttf");

			scrollImage.AddView (linearPanelScroll);
			linearAll.AddView (linearImage);
			linearAll.AddView (linearContainer);

			linearImage.AddView (imgUser);

			linearContainer.AddView (txtTitle);
			linearContainer.AddView (txtAuthor);
			linearContainer.AddView (txtContainer);
			linearContainer.AddView (scrollImage);

			int space = Configuration.getHeight (50);



			scrollImage.SetPadding (0, 0, 0, space);
			this.AddView (linearAll);
			this.AddView (linea_separador);
			this.SetPadding (0, 0,0, space);


		}

		private int _indice;
		public int Indice{
			get{return _indice; }
			set{_indice = value;}
		}

		private String _imgUser;
		public String ImgUsuario{
			get{return _imgUser; }
			set{_imgUser = value;
				iniUserList (imgUser);}
		}

		private String _title;
		public String Title{
			get{ return _title;}
			set{ _title = value;
				txtTitle.Text = _title;	}

		}

		private String _author;
		public String Author{
			get{ return _author;}
			set{ _author = value;
				txtAuthor.Text = _author;	}

		}

		private String _container;
		public String Container{
			get{ return _container;}
			set{ _container = value;
				txtContainer.Text = _container;	}

		}

		private String _colortext;
		public String ColorText{
			get{ return _colortext;}
			set{ _colortext = value;
				txtTitle.SetTextColor(Color.ParseColor(_colortext));	}

		}

		private Bitmap _imageBitmap;
		public Bitmap ImageBitmap{
			get{ return _imageBitmap;}
			set{ _imageBitmap = value;
				LinearLayout la = new LinearLayout (context);
				la.LayoutParameters = new LinearLayout.LayoutParams(Configuration.getWidth(242),Configuration.getHeight(172));	
				la.Orientation = Orientation.Horizontal;
				la.SetGravity (GravityFlags.Center);

				ImageView img = new ImageView (context);
				img.SetImageBitmap (Bitmap.CreateScaledBitmap (_imageBitmap, Configuration.getWidth (232), Configuration.getHeight (162), true));
				la.AddView (img);
				linearPanelScroll.AddView (la);
				_imageBitmap = null;
			}

		}

		private String _image;
		public String Image{
			get{ return _image;}
			set{ _image = value;
				LinearLayout la = new LinearLayout (context);
				la.LayoutParameters = new LinearLayout.LayoutParams(Configuration.getWidth(242),Configuration.getHeight(172));	
				la.Orientation = Orientation.Horizontal;
				la.SetGravity (GravityFlags.Center);

				ImageView img = new ImageView (context);
				//img.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/imdownloading.png"), Configuration.getWidth (232), Configuration.getHeight (162), true));
				//img.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset (_image), Configuration.getWidth (232), Configuration.getHeight (162), true));
				Picasso.With (context).Load (Image).Resize(Configuration.getWidth(232),Configuration.getHeight(162)).Into (img);
				la.AddView (img);
				linearPanelScroll.AddView (la);
			}

		}

		public void setDefaultProfileUserBitmap (Bitmap bm)
		{
			defaultUser = bm;
			iniUserList (imgUser);
		}

		private void iniUserList(ImageView imguserlist){		
			imguserlist.SetImageBitmap (defaultUser);
			defaultUser = null;
		}

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}


	}
}

