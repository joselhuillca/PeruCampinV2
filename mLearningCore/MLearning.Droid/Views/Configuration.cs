using System;
using Android.Graphics;
using System.Net;
using Android.Graphics.Drawables;
using Android.Widget;
using Android.Content;
using MLearning.Core.Services;
using System.Collections.Generic;

namespace MLearning.Droid
{
	public class Configuration
	{
		public static IMLearningService _mLearningService;
		public static int DIMENSION_DESING_WIDTH= 640;
		public static int DIMENSION_DESING_HEIGHT= 1136; 
		public static int WIDTH_PIXEL;
		public static int HEIGHT_PIXEL;


		public static int IndiceActual=0;

		public static String azul= "#00c6ff";
		public static String lila = "#de2ef6";
		public static String verde ="#65c921";
		public static String amarillo = "#ffd200";
		public static String naranja = "#ff9600";
		public static String rosa = "#ff3891";

		public static List<Bitmap> adds;

		public static String[] ListaColores = {azul,lila,verde,amarillo,naranja,rosa};

		public static int TYPE_TEXT = 1;
		public static int TYPE_IMAGE = 2;



		public static int getHeight(int value){

			return HEIGHT_PIXEL*value/DIMENSION_DESING_HEIGHT;
		}

		public static int getWidth(int value){
			return WIDTH_PIXEL*value/DIMENSION_DESING_WIDTH;
		}

		public static void setWidthPixel(int value){
			WIDTH_PIXEL=value;
		}

		public static void setHeigthPixel(int value){
			HEIGHT_PIXEL=value;
		}

		public static Intent getOpenFacebookIntent(Context context, string fb_url, string web_url) {

			try {
				return new Intent(Intent.ActionView,Android.Net.Uri.Parse(fb_url) );
			} catch (Exception e) {
				return new Intent(Intent.ActionView, Android.Net.Uri.Parse(web_url));
			}
		}


		public static Bitmap getRoundedShape(Bitmap scaleBitmapImage, int targetWidth, int targetHeight)
		{
			
			Bitmap targetBitmap = Bitmap.CreateBitmap(targetWidth,
				targetHeight, Bitmap.Config.Argb8888);

			Canvas canvas = new Canvas(targetBitmap);
			Android.Graphics.Path path = new Android.Graphics.Path();
			path.AddCircle(((float)targetWidth - 1) / 2,
				((float)targetHeight - 1) / 2,
				(Math.Min(((float)targetWidth),
					((float)targetHeight)) / 2),
				Android.Graphics.Path.Direction.Ccw);


			canvas.ClipPath(path);
			Bitmap sourceBitmap = scaleBitmapImage;
			canvas.DrawBitmap(sourceBitmap,
				new Rect(0, 0, sourceBitmap.Width,
					sourceBitmap.Height),
				new Rect(0, 0, targetWidth, targetHeight), null);
			return targetBitmap;
		}

		public static Bitmap GetRoundedCornerBitmap(Bitmap bitmap) {
			Bitmap output = Bitmap.CreateBitmap(bitmap.Width,
				bitmap.Height,Bitmap.Config.Argb8888);
			Canvas canvas = new Canvas(output);

			//int color = 0xff424242;
			Paint paint = new Paint();
			Rect rect = new Rect(0, 0, bitmap.Width, bitmap.Height);
			RectF rectF = new RectF(rect);
			float roundPx = 12.0f;

			paint.AntiAlias=true;
			canvas.DrawARGB(0, 0, 0, 0);
			paint.Color = Color.Red; //color;//Color.Transparent;
			canvas.DrawRoundRect(rectF, roundPx, roundPx, paint);

			paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));
			canvas.DrawBitmap(bitmap, rect, rect, paint);
			return output;
		}

		public static Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient())
			{
				var imageBytes = webClient.DownloadData(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;
		}

		public async static void GetImageBitmapFromUrl(string url,Bitmap img_bm)
		{
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient())
			{
				byte[] imageBytes = await webClient.DownloadDataTaskAsync (url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					//imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
					img_bm = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			//img_bm = imageBitmap;

			//return imageBitmap;
		}

		public static string quitarErrorTildes(string Texto)
		{
			//Minusculas
			if (Texto != null && Texto.Length > 0)
			{
				Texto = Texto.Replace("á", "á");
				Texto = Texto.Replace("é", "é");
				Texto = Texto.Replace("í", "í");
				Texto = Texto.Replace("ó", "ó");
				Texto = Texto.Replace("ú", "ú");
				Texto = Texto.Replace("ñ", "ñ");
				//Mayusculas
				Texto = Texto.Replace("Á", "Á");
				Texto = Texto.Replace("É", "É");
				Texto = Texto.Replace("Í", "Í");
				Texto = Texto.Replace("Ó", "Ó");
				Texto = Texto.Replace("Ú", "Ú");
				Texto = Texto.Replace ("Ñ","Ñ");
			}
			if (Texto != null) {
				//Minusculas
				Texto =Texto.Replace ("á","á");
				Texto =Texto.Replace ("é","é");
				Texto =Texto.Replace ("í","í");
				Texto =Texto.Replace ("ó","ó");
				Texto =Texto.Replace ("ú","ú");
				Texto =Texto.Replace ("ñ","ñ");
				//Mayusculas
				Texto =Texto.Replace ("Á","Á");
				Texto =Texto.Replace ("É","É");
				Texto =Texto.Replace ("Í","Í");
				Texto =Texto.Replace ("Ó","Ó");
				Texto =Texto.Replace ("Ú","Ú");
				Texto =Texto.Replace ("Ñ","Ñ");
			}

			return Texto;
		}
	}
}

