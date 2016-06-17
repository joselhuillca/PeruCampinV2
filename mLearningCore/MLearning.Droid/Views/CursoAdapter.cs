using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;

namespace MLearning.Droid.Views
{
	public class CursoAdapter : BaseAdapter<CursoItem>
	{
		List<CursoItem> list;
		Activity context;

		public CursoAdapter (Activity context, List<CursoItem> list):base()
		{
			this.context = context;
			this.list = list;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override CursoItem this[int position]{
			get{ return list [position];}
		}

		public override int Count {
			get {
				return list.Count;
			}
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			var item = list [position];
			var metrics = context.Resources.DisplayMetrics;
			float  widthInDp = ((int)metrics.WidthPixels);
			float heightInDp = ((float)metrics.HeightPixels);
			RelativeLayout mainItem = new RelativeLayout (context);
			LinearLayout view = new LinearLayout (context);
			LinearLayout linearItem = new LinearLayout (context);
			LinearLayout linearCurso = new LinearLayout (context);
			LinearLayout linearNumLO = new LinearLayout (context);
			LinearLayout linearContentNumLO = new LinearLayout (context);



			TextView txtCurso = new TextView (context);
			TextView txtCursoNombre = new TextView (context);
			TextView txtNumLo = new TextView (context);

			txtCurso.Text = "Curso:";
			txtCurso.SetTextColor (Color.ParseColor ("#ffffff"));
			txtCurso.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
		

			txtCursoNombre.Text = item.CursoName;
			txtCursoNombre.SetTextColor (Color.ParseColor ("#ffffff"));
			txtCursoNombre.Ellipsize = Android.Text.TextUtils.TruncateAt.End;
			txtCursoNombre.SetSingleLine (true);
			txtCursoNombre.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			//txtCurso.SetLines (1);
			txtNumLo.Text = item.NumLO.ToString();
			txtNumLo.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (22));
			txtNumLo.SetTextColor (Color.ParseColor ("#ffffff"));
			txtNumLo.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");

			mainItem.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);
			mainItem.SetGravity (Android.Views.GravityFlags.CenterVertical);


			linearItem.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(89));
			linearItem.Orientation = Orientation.Horizontal;
			linearItem.SetGravity (Android.Views.GravityFlags.Center);

			linearCurso.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(250),-2);
			linearCurso.Orientation = Orientation.Vertical;
			linearCurso.SetGravity (Android.Views.GravityFlags.Center);

			linearNumLO.LayoutParameters = new LinearLayout.LayoutParams (-2,-1);
			linearNumLO.Orientation = Orientation.Vertical;
			linearNumLO.SetGravity (Android.Views.GravityFlags.CenterVertical);

			linearContentNumLO.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(33),Configuration.getWidth(33));
			linearContentNumLO.Orientation = Orientation.Horizontal;
			linearContentNumLO.SetGravity (Android.Views.GravityFlags.Center);
			linearContentNumLO.SetBackgroundColor (Color.Gray);
		//	linearContentNumLO.SetVerticalGravity (Android.Views.GravityFlags.FillVertical);

			linearItem.AddView (mainItem);

			linearCurso.AddView (txtCurso);
			linearCurso.AddView (txtCursoNombre);
			linearContentNumLO.AddView (txtNumLo);
			linearNumLO.AddView (linearContentNumLO);

			//linearCurso.SetBackgroundColor (Color.Red);
			linearCurso.SetX (Configuration.getWidth(130));
			linearNumLO.SetX (Configuration.getWidth(428));

			mainItem.AddView (linearCurso);
			mainItem.AddView (linearNumLO);

			view.AddView (linearItem);
			return view;

		}
	}
}

