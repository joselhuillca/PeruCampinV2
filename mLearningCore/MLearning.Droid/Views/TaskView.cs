
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
using TaskView;
using Android.Graphics;
using Android.Graphics.Drawables;
using Square.Picasso;

namespace MLearning.Droid
{
	public class TaskView : RelativeLayout
	{

		RelativeLayout mainLayout;

		TextView txtNameLO;
		TextView txtChapter;
		TextView txtAuthor;
		TextView txtTaskComplete;
		TextView txtTaskIncomplete;


		List<TaskViewItem> _listTaskComplete = new List<TaskViewItem>();
		List<TaskViewItem> _listTaskIncomplete = new List<TaskViewItem>();

		ListView listTaskComplete;
		ListView listTaskIncomplete;

		LinearLayout linearContentTask;

		LinearLayout linearListTaskC;
		LinearLayout linearListTaskI;
		LinearLayout linearTaskComplete;
		LinearLayout linearTaskIncomplete;
		LinearLayout linearTextLO;

		int widthInDp;
		int heightInDp;

		ImageView imgLO;
		LinearLayout linearImageLO;
		ImageView imgLinea;


		Context context;
		public TaskView (Context context) :
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
			//iniTaskComplete ();
			//iniTaskIncomplete ();
			this.AddView (mainLayout);

		}

		public void ini(){
			mainLayout = new RelativeLayout (context);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);
			//Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/fondo.png"), 1024, 768, true));
			//mainLayout.SetBackgroundDrawable (d);
			mainLayout.SetBackgroundColor(Color.ParseColor("#ffffff"));

			txtAuthor = new TextView (context);txtAuthor.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			txtChapter= new TextView (context);txtChapter.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			txtNameLO = new TextView (context);txtNameLO.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			txtTaskComplete = new TextView (context);txtTaskComplete.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			txtTaskIncomplete = new TextView (context);txtTaskIncomplete.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");

			listTaskComplete = new ListView (context);
			listTaskIncomplete = new ListView (context);

			linearTaskComplete = new LinearLayout (context);
			linearTaskIncomplete = new LinearLayout (context);
			linearTextLO = new LinearLayout (context);
			linearContentTask = new LinearLayout (context);
			linearListTaskI = new LinearLayout (context);
			linearListTaskC = new LinearLayout (context);

			imgLinea = new ImageView (context);



			imgLO = new ImageView (context);
			linearImageLO = new LinearLayout (context);

			linearTaskComplete.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(84));
			linearTaskIncomplete.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(84));
			linearImageLO.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(372));
			linearTextLO.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(250));
			linearContentTask.LayoutParameters = new LinearLayout.LayoutParams(-1,-2);
			linearListTaskC.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(255));
			linearListTaskI.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(255));


			linearTaskComplete.Orientation = Orientation.Horizontal;
			linearTaskComplete.SetGravity(GravityFlags.Center);

			linearTaskIncomplete.Orientation = Orientation.Horizontal;
			linearTaskIncomplete.SetGravity(GravityFlags.Center);

			linearTextLO.Orientation = Orientation.Vertical;
			linearTextLO.SetGravity(GravityFlags.Right);

			linearListTaskC.Orientation = Orientation.Vertical;
			linearListTaskI.Orientation = Orientation.Vertical;
			//linearListTaskC.SetGravity(Gravity

			linearContentTask.Orientation = Orientation.Vertical;


			txtAuthor.Text = "Author : David Spencer";
			txtChapter.Text = "Flora y Fauna";
			txtNameLO.Text = "Camino Inca";


			txtAuthor.SetPadding (0, 0, Configuration.getWidth (30), 0);
			txtChapter.SetPadding (0, 0, Configuration.getWidth (30), 0);
			txtNameLO.SetPadding (0, 0, Configuration.getWidth (30), 0);


			txtTaskComplete.Text = "Tareas Completadas";
			txtTaskIncomplete.Text = "Tareas por completar";

			txtAuthor.SetTextColor (Color.ParseColor("#ffffff"));
			txtChapter.SetTextColor (Color.ParseColor("#ffffff"));
			txtNameLO.SetTextColor (Color.ParseColor("#ffffff"));

			txtNameLO.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (30));
			txtChapter.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (50));
			txtAuthor.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (30));
			txtNameLO.Typeface = Typeface.DefaultBold;


			txtAuthor.Gravity = GravityFlags.Right;
			txtChapter.Gravity = GravityFlags.Right;
			txtNameLO.Gravity = GravityFlags.Right;

			txtTaskComplete.SetTextColor (Color.ParseColor ("#999999"));
			txtTaskIncomplete.SetTextColor (Color.ParseColor ("#999999"));
			linearTaskComplete.SetBackgroundColor (Color.ParseColor ("#eeeeee"));
			linearTaskIncomplete.SetBackgroundColor (Color.ParseColor ("#eeeeee"));

			//Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("images/desert.jpg"), 480, 640, true));
			//linearImageLO.SetBackgroundDrawable (d);

			//imgLinea.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/lineatareas.png"), 4,2000 , true));

			linearTaskComplete.AddView (txtTaskComplete);
			linearTaskIncomplete.AddView (txtTaskIncomplete);

			linearTextLO.AddView (txtNameLO);
			linearTextLO.AddView (txtChapter);
			linearTextLO.AddView (txtAuthor);


			linearListTaskC.AddView (listTaskComplete);
			linearListTaskI.AddView (listTaskIncomplete);

			linearContentTask.AddView (linearImageLO);
			linearContentTask.AddView (linearTaskComplete);
			linearContentTask.AddView (linearListTaskC);
			linearContentTask.AddView (linearTaskIncomplete);
			linearContentTask.AddView (linearListTaskI);


			linearTextLO.SetX (0); linearTextLO.SetY (Configuration.getHeight(200));
			//linearImageLO.SetX (0); linearImageLO.SetY (0);

			linearContentTask.SetX(0); linearContentTask.SetY(0);

			imgLinea.SetX (Configuration.getWidth(75));  imgLinea.SetY (Configuration.getHeight(350));



			mainLayout.AddView (imgLinea);
			mainLayout.AddView (linearContentTask);
			mainLayout.AddView (linearTextLO);





		}

		private String _coverUrl;
		public String CoverUrl{
			get{ return _coverUrl;}
			set{ _coverUrl = value;

				ImageView cover = new ImageView (context);
				Picasso.With (context).Load (CoverUrl).Resize(Configuration.getWidth(640),Configuration.getHeight(372)).CenterCrop().Into (cover);
				linearImageLO.RemoveAllViews ();
				linearImageLO.AddView (cover);

			}
		}

		private String _author;
		public String Author{
			get{return _author; }
			set{_author = value;
				txtAuthor.Text=_author;}
		}

		private String _chapter;
		public String Chapter{
			get{ return _chapter;}
			set{ _chapter = value;
				txtChapter.Text = _chapter;	}

		}

		private String _nameLO;
		public String NameLO{
			get{ return _nameLO;}
			set{ _nameLO = value;
				txtNameLO.Text = _nameLO;	}

		}


		public List<TaskViewItem> ListaTareasCompletas{
			get{ return _listTaskComplete;}
			set{ _listTaskComplete = value;
				iniTaskComplete ();
				}
		}

		public List<TaskViewItem> ListaTareasIncompletas{
			get{ return _listTaskIncomplete;}
			set{ _listTaskIncomplete = value;
				iniTaskIncomplete ();
			}
		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		public void iniTaskComplete (){


			//String icon = "icons/tareacompleta.png";
			/*TaskViewItem item1 = new TaskViewItem ();
			TaskViewItem item2 = new TaskViewItem ();
			TaskViewItem item3 = new TaskViewItem ();

			item1.Tarea = "Tarea 001: Camino del inca";
			item2.Tarea = "Tarea 002: Tipos de aprendizaje";
			item3.Tarea = "Tarea 003: Programación movil";
			item1.Icon = icon;
			item2.Icon = icon;
			item3.Icon = icon;
			*/
			//_listTaskComplete.Add (item1);
			//_listTaskComplete.Add (item2);
			//_listTaskComplete.Add (item3);

			listTaskComplete.Adapter = new TaskViewAdapter (context, _listTaskComplete); 

		}

		public void iniTaskIncomplete (){

		/*	String icon = "icons/tareaincompleta.png";
			TaskViewItem item1 = new TaskViewItem ();
			TaskViewItem item2 = new TaskViewItem ();
			TaskViewItem item3 = new TaskViewItem ();
			TaskViewItem item4 = new TaskViewItem ();

			item1.Tarea = "Tarea 001: Camino del inca";
			item2.Tarea = "Tarea 002: Tipos de aprendizaje";
			item3.Tarea = "Tarea 003: Programación movil";
			item4.Tarea = "Tarea 004: Ecologia";

			item1.Icon = icon;
			item2.Icon = icon;
			item3.Icon = icon;
			item4.Icon = icon;
			_listTaskIncomplete.Add (item1);
			_listTaskIncomplete.Add (item2);
			_listTaskIncomplete.Add (item3);
			_listTaskIncomplete.Add (item4);
*/
			listTaskIncomplete.Adapter = new TaskViewAdapter (context, _listTaskIncomplete); 


		}
	}
}

