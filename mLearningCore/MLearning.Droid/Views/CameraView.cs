
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using Android.Graphics;
using Android.Graphics.Drawables;
using MLearning.Core;
using Core.ViewModels;

namespace MLearning.Droid
{
	[Activity(Label = "View for CameraViewModel")]		
	public class CameraView : MvxActivity
	{
		RelativeLayout mainLayout;

		ImageView imgCamera;
		ImageView imgLineal;
		ImageButton btnCamera;
		ImageButton btnRepository;
		ImageButton btnDone;
		TextView txtCamera;

		LinearLayout linearButtons;
		LinearLayout linearImageCamera;
		LinearLayout LinearTextCamera;
		LinearLayout LinearImageText;


		protected override void OnCreate (Bundle bundle)
		{
			this.Window.AddFlags(WindowManagerFlags.Fullscreen);
			base.OnCreate (bundle);

			init ();

			SetContentView (mainLayout);

			// Create your application here
		}
		public void init(){
			mainLayout = new RelativeLayout (this);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);
			linearButtons = new LinearLayout (this);
			linearImageCamera = new LinearLayout (this);
			LinearImageText = new LinearLayout (this);
			LinearTextCamera = new LinearLayout (this);

			linearButtons.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearImageCamera.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			LinearImageText.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			LinearTextCamera.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);

			linearButtons.Orientation = Orientation.Horizontal;
			linearImageCamera.Orientation = Orientation.Horizontal;
			LinearImageText.Orientation = Orientation.Horizontal;
			LinearTextCamera.Orientation = Orientation.Vertical;

			//linearButtons.SetGravity (GravityFlags.Center);
			linearImageCamera.SetGravity (GravityFlags.Center);
			LinearImageText.SetGravity (GravityFlags.Center);
			LinearTextCamera.SetGravity (GravityFlags.Center);

			Drawable dr = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/bfondo.png"), 1024, 768, true));
			mainLayout.SetBackgroundDrawable (dr);

			imgCamera = new ImageView (this);
			imgLineal = new ImageView (this);
			btnCamera = new ImageButton (this);
			btnRepository = new ImageButton (this);
			btnDone = new ImageButton (this);
			txtCamera = new TextView (this);

			txtCamera.Text = "CHOOSE A PICTURE AND \n     SELECT A COLOUR"; 
			txtCamera.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			txtCamera.SetTextSize(Android.Util.ComplexUnitType.Px,Configuration.getHeight(30));

			txtCamera.SetTextColor (Color.ParseColor ("#ffffff"));

			Button bt = new Button (this);


			imgCamera.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/camara.png"), Configuration.getWidth(164),Configuration.getHeight(164),true));
			imgLineal.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/colores.png"), Configuration.getWidth(542), 5,true));
			btnCamera.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/loadcamara.png"), Configuration.getWidth(58),Configuration.getHeight(50),true));
			btnRepository.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/loadbiblioteca.png"), Configuration.getWidth(58),Configuration.getHeight(50),true));
			btnDone.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/adelante.png"), Configuration.getWidth(20),Configuration.getWidth(30),true));

			imgCamera.Click += delegate {
				var com = ((CameraViewModel)this.DataContext).TakePictureCommand;
				com.Execute (null);
			};

			linearButtons.AddView (btnCamera);
			linearButtons.AddView (btnRepository);

			btnCamera.SetPadding (Configuration.getWidth(122),0,0,0);
			btnRepository.SetPadding (Configuration.getWidth(300),0,0,0);

			linearImageCamera.AddView (imgCamera);
			txtCamera.SetPadding (0, Configuration.getHeight(70), 0, 0);
			LinearImageText.AddView (txtCamera);


			LinearTextCamera.AddView (linearImageCamera);
			LinearTextCamera.AddView (LinearImageText);

			LinearTextCamera.SetX (0); LinearTextCamera.SetY (Configuration.getHeight(295));
			linearButtons.SetX (0); linearButtons.SetY (Configuration.getHeight(926));
			imgLineal.SetX (Configuration.getWidth(61)); imgLineal.SetY (Configuration.getHeight(1019));
			btnDone.SetX (Configuration.getWidth(550)); btnDone.SetY (Configuration.getHeight(40));

			mainLayout.AddView (btnDone);
			mainLayout.AddView (LinearTextCamera);
			mainLayout.AddView (linearButtons);
			mainLayout.AddView (imgLineal);
			initButtonColor (btnCamera);
			initButtonColor (btnRepository);
			initButtonColor (btnDone);

			btnCamera.Click += delegate {
				var com = ((CameraViewModel)this.DataContext).TakePictureCommand;
				com.Execute(null);
			};

			btnRepository.Click += delegate {
				var com = ((CameraViewModel)this.DataContext).ChoosePictureCommand;
				com.Execute(null);
			};


			Bitmap bm;
			var vm = this.ViewModel as CameraViewModel;
			if (vm.Bytes != null)
			{
				bm= BitmapFactory.DecodeByteArray(vm.Bytes, 0, vm.Bytes.Length);
				imgCamera.SetImageBitmap (bm); 

			}

			vm.PropertyChanged += Vm_PropertyChanged;


			btnDone.Click += delegate {
				var com = ((CameraViewModel)this.DataContext).RegisterCommand;
				com.Execute(null);
			};


		}

		void Vm_PropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{

			var vm = this.ViewModel as CameraViewModel;

			string property = e.PropertyName;
			switch (property) {

			case "Bytes":
				if (vm.Bytes != null) {
					Bitmap bm = BitmapFactory.DecodeByteArray (vm.Bytes, 0, vm.Bytes.Length);
					imgCamera.SetImageBitmap (bm);
					//Bitmap newbm = Utilities.getRoundedShape (bm);
					//imgUser.SetImageBitmap (newbm);
				}

				break;
			}
		}



		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =this.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}
		public  void initButtonColor(ImageButton btn){
			btn.Alpha = 255;
			//btn.SetAlpha(255);
			btn.SetBackgroundColor(Color.Transparent);
		}

	}
}

