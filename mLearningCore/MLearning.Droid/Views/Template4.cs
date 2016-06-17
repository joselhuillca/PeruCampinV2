
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
	public class Template4: RelativeLayout
	{

		RelativeLayout mainLayout;
		LinearLayout contenLayout;


		int widthInDp;
		int heightInDp;


		Context context;
		ListView contentList;
		List<ImageGallery> _dataImageItem = new List<ImageGallery> ();

		public Template4 (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}

		void Initialize ()
		{
			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);

			ini ();

			this.AddView (mainLayout);



		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		public void ini(){




			mainLayout = new RelativeLayout (context);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);

			contenLayout = new LinearLayout (context);
			contenLayout.LayoutParameters = new LinearLayout.LayoutParams (-2, -2);
			contenLayout.Orientation = Orientation.Vertical;

			contentList = new ListView (context);



			contentList.Adapter = new ImageAdapter (context, _dataImageItem);
			int pad_size = Configuration.getWidth (0);
			contentList.DividerHeight = Configuration.getWidth (pad_size);
			//contenLayout.SetY(Configuration.getHeight(100));
			contenLayout.AddView (contentList);

			mainLayout.AddView (contenLayout);
		}

		private List<Bitmap> _listBitmap;
		public List<Bitmap> ListaBitmap{
			get{return _listBitmap; }
			set{_listBitmap = value;
							
			}


		}

		public List<ImageGallery> getDataImagesGallery(List<string> images_pat,int n_divs)
		{
			List<ImageGallery> data_gallery = new List<ImageGallery> ();
			int n_rows = images_pat.Count / n_divs;

			int y = 0;
			for (; y < n_rows; y++) 
			{

				ImageGallery row_images = new ImageGallery();
				for (int j = 0; j < n_divs; j++) {
					row_images.new_item (images_pat [y * n_divs + j]);	
					Console.WriteLine (images_pat [y * n_divs + j]);
				}
				data_gallery.Add (row_images);
			}

			ImageGallery row_im = new ImageGallery();
			for (int i = 0; i < images_pat.Count % n_divs; i++) {
				row_im.new_item (images_pat [n_divs * y + i]);	
			}

			if (images_pat.Count % n_divs != 0) {
				data_gallery.Add (row_im);
			}
			return data_gallery;
		}
	}
}

