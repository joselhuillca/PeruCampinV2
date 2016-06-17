
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

namespace MLearning.Droid
{
	public class NotificationView : RelativeLayout
	{

		RelativeLayout mainLayout;
		LinearLayout linearList;

		TextView title;

		List<NotificationDataRow> _listNotificationData = new List<NotificationDataRow>();

		ListView listNotification;

		int widthInDp;
		int heightInDp;

		ImageView imgLinea;
		ImageView imgPoint;


		Context context;
		public NotificationView (Context context) :
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
			iniNotifList ();
			this.AddView (mainLayout);

		}

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}


		public void ini(){

			var textFormat = Android.Util.ComplexUnitType.Px;

			mainLayout = new RelativeLayout (context);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);

			Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/fondo.png"), 1024, 768, true));
			mainLayout.SetBackgroundDrawable (d);


			title = new TextView (context);
			imgLinea = new ImageView (context);
			listNotification = new ListView (context);
			linearList = new LinearLayout (context);
			imgPoint = new ImageView (context);


			title.Text = "Notificaciones";
			title.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			title.SetTextColor (Color.ParseColor ("#ffffff"));
			title.SetTextSize (textFormat, Configuration.getHeight (48));
			title.SetX (Configuration.getHeight (35));
			title.SetY (Configuration.getWidth (142));

			linearList.SetBackgroundColor (Color.ParseColor ("#ffffff"));
			linearList.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (886));
			linearList.SetX (Configuration.getWidth (0));	linearList.SetY (Configuration.getHeight(250));

			listNotification.SetX (Configuration.getWidth (0));	listNotification.SetY (Configuration.getHeight(250));
			listNotification.LayoutParameters = new LinearLayout.LayoutParams(-1, Configuration.getHeight (886));

			imgLinea.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/lineanotificaciones.png"), 4,2000 , true));
			imgLinea.SetX (Configuration.getWidth (61));	imgLinea.SetY (Configuration.getHeight(240));

			imgPoint.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/circblanco.png"), 30,30 , true));
			imgPoint.SetX (Configuration.getWidth (52));	imgPoint.SetY (Configuration.getHeight(228));

			//linearList.AddView (listNotification);


			mainLayout.AddView (title);

			mainLayout.AddView(linearList);
			mainLayout.AddView (listNotification);
			mainLayout.AddView (imgLinea);
			mainLayout.AddView (imgPoint);


		}

		public void iniNotifList(){

			_listNotificationData = new List<NotificationDataRow> ();
			_listNotificationData.Add (new NotificationDataRow (){ date = "10:00pm", comment = "Esto es un comentario" });
			_listNotificationData.Add (new NotificationDataRow (){ date = "10:00pm", comment = "Esto es un comentario" });
			_listNotificationData.Add (new NotificationDataRow (){ date = "10:00pm", comment = "Esto es un comentario" });
			_listNotificationData.Add (new NotificationDataRow (){ date = "10:00pm", comment = "Esto es un comentario" });
			_listNotificationData.Add (new NotificationDataRow (){ date = "10:00pm", comment = "Esto es un comentario" });
			_listNotificationData.Add (new NotificationDataRow (){ date = "10:00pm", comment = "Esto es un comentario" });
			_listNotificationData.Add (new NotificationDataRow (){ date = "10:00pm", comment = "Esto es un comentario" });
			_listNotificationData.Add (new NotificationDataRow (){ date = "10:00pm", comment = "Esto es un comentario" });
			_listNotificationData.Add (new NotificationDataRow (){ date = "10:00pm", comment = "Esto es un comentario" });
			_listNotificationData.Add (new NotificationDataRow (){ date = "10:00pm", comment = "Comentario Final" });

			listNotification.Adapter = new NotificationListViewAdapter(context,_listNotificationData);
				
		}
	}
}

