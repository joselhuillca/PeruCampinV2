using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Android.Widget;
using Android.Graphics;
using Android.Views;
using Core.ViewModels;
using System;
using Android.Graphics.Drawables;
using Android.Views.Animations;
using Android.Text;
using Cirrious.MvvmCross.Binding.BindingContext;
using Android.Content.PM;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Core.Repositories;
using Cirrious.CrossCore;
using Java.IO;
using System.IO;
using Android.Content;
using Android.Net;

namespace MLearning.Droid.Views
{
	[Activity(Label = "View for FirstViewModel", ScreenOrientation = ScreenOrientation.Portrait)]
	public class LoginView : MvxActivity
	{

		int widthInDp;
		int heightInDp;

		RelativeLayout mainLayout;

		#region signUp
		RelativeLayout relSingup;
		RelativeLayout relLogin;
		ImageButton btnLogin;
		ImageButton btnSingUp;
		ImageButton btnFacebook;
		ImageView imgLogo;
		CheckBox chkLogin;
		TextView txtLicencia_a;
		TextView txtLicencia_b;
		LinearLayout linearLogo;
		LinearLayout linearLicencia;
		LinearLayout linearLogin;
		LinearLayout linearSingup;

		ProgressDialog _dialog;
		TextView txtlogin_a1;
		TextView txtlogin_a2;
		TextView txtlogin_b1;
		TextView txtlogin_b2;
		TextView txtlogin_c1;
		TextView txtlogin_c2;

		LinearLayout linearTxta;
		LinearLayout linearTxtb;
		LinearLayout linearTxtc;
		LinearLayout linearContentText;
		#endregion
		#region login
		EditText etxtUser;
		EditText etxtPassword;
		ImageButton btnLoginInto;
		TextView txtLogin_a;
		TextView txtLogin_b;
		TextView txtInicioSesion;

		LinearLayout linearButtonLogin;
		LinearLayout linearEditTextLogin;
		LinearLayout linearTextLogin;
		#endregion


		protected override void OnCreate(Bundle bundle)
		{
			

			base.OnCreate(bundle);
			this.Window.AddFlags(WindowManagerFlags.Fullscreen);
			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);

			//START PARA COPIAR LA BASE DE DATOS LOCAL

			copyDataBase ();

			//END
		


			LinearLayout test = new LinearLayout (this);
			test.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);
			test.SetBackgroundResource (Resource.Drawable.splash);
			SetContentView (test);


			var com = ((LoginViewModel)this.DataContext).LoginCommand;
			com.Execute(null);
			//init ();

			//SetContentView (mainLayout);
			//SetContentView(Resource.Layout.LoginView);
		}

		private void copyDataBase()
		{

			Stream iStream = Assets.Open("database/cache.db");
			string dbPath = "/data/data/camping.Droid/files/cache.db";
			    	//path  "/data/data/camping.Droid/files/cache.db"   

			var oStream = new FileOutputStream (dbPath);

			bool exists = System.IO.File.Exists(dbPath);
			long size = 0;
			Android.Net.Uri uri = Android.Net.Uri.FromFile(new Java.IO.File(dbPath));

			using (var fd = ContentResolver.OpenFileDescriptor(uri, "r"))
				size = fd.StatSize;

			if ( size == 0 ) {
				byte[] buffer = new byte[2048];
				int length = 2048;
				//    length = Convert.ToInt16(length2);
				 
				while (iStream.Read(buffer, 0, length) > 0)
				{
					oStream.Write(buffer, 0, length);
				}
				oStream.Flush();
				oStream.Close();
				iStream.Close();

			}

		

		}

		public void init(){



			var textFormat = Android.Util.ComplexUnitType.Px;

			_dialog = new ProgressDialog(this);


			//popupBuilder.SetView(_dialog);


			_dialog.SetMessage("Espere por favor...");
			_dialog.SetCancelable(false);
			//_dialog.Show();

			mainLayout = new RelativeLayout (this);
			relSingup = new RelativeLayout (this);
			relLogin = new RelativeLayout (this);
			linearLicencia = new LinearLayout (this);
			linearLogin = new LinearLayout (this);
			linearSingup = new LinearLayout (this);
			linearLogo = new LinearLayout (this);

			imgLogo = new ImageView (this);
			btnLogin = new ImageButton (this);
			btnSingUp = new ImageButton (this);
			btnFacebook = new ImageButton (this);
			chkLogin = new CheckBox (this);
			txtLicencia_a = new TextView (this);
			txtLicencia_b = new TextView (this);

			txtlogin_a1 = new TextView (this);
			txtlogin_a2 = new TextView (this);
			txtlogin_b1 = new TextView (this);
			txtlogin_b2 = new TextView (this);
			txtlogin_c1 = new TextView (this);
			txtlogin_c2 = new TextView (this);

			initText (txtlogin_a1, "", "#00c6ff");
			txtlogin_a1.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			initText (txtlogin_a2, "", "#ffffff");
			txtlogin_a2.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			initText (txtlogin_b1, "", "#00c6ff");
			txtlogin_b1.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			initText (txtlogin_b2, "","#ffffff");
			txtlogin_b2.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			initText (txtlogin_c1, "", "#00c6ff");
			txtlogin_c1.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			initText (txtlogin_c2, "", "#ffffff");
			txtlogin_c2.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
		

			linearTxta = new LinearLayout (this);
			linearTxtb = new LinearLayout (this);
			linearTxtc = new LinearLayout (this);
			linearContentText = new LinearLayout (this);

			linearTxta.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearTxtb.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearTxtc.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearContentText.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);

			linearTxta.Orientation = Orientation.Horizontal;
			linearTxta.SetGravity (GravityFlags.Center);
			linearTxtb.Orientation = Orientation.Horizontal;
			linearTxtb.SetGravity (GravityFlags.Center);
			linearTxtc.Orientation = Orientation.Horizontal;
			linearTxtc.SetGravity (GravityFlags.Center);

			linearContentText.Orientation = Orientation.Vertical;
			linearContentText.SetGravity (GravityFlags.Center);


			linearTxta.AddView (txtlogin_a1); linearTxta.AddView (txtlogin_a2);
			linearTxtb.AddView (txtlogin_b1); linearTxtb.AddView (txtlogin_b2);
			linearTxtc.AddView (txtlogin_c1); linearTxtc.AddView (txtlogin_c2);

			linearContentText.AddView (linearTxta);
			linearContentText.AddView (linearTxtb);
			linearContentText.AddView (linearTxtc);


			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);
			Drawable d = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/afondo.png"), 768, 1024, true));
			mainLayout.SetBackgroundDrawable (d);

			relSingup.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);
			relLogin.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);


			linearLicencia.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearLicencia.Orientation = Orientation.Horizontal;
			linearLicencia.SetGravity (GravityFlags.Center);

			linearLogin.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearLogin.Orientation = Orientation.Horizontal;
			linearLogin.SetGravity (GravityFlags.Right);

			linearSingup.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearSingup.Orientation = Orientation.Vertical;
			linearSingup.SetGravity (GravityFlags.Center);


			linearLogo.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearLogo.Orientation = Orientation.Vertical;
			linearLogo.SetGravity (GravityFlags.Center);



			txtLicencia_a.Text = "TO REGISTER, ACCEPT THE";
			txtLicencia_a.SetTextSize(textFormat,Configuration.getHeight(25));
			txtLicencia_a.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			txtLicencia_a.SetTextColor (Color.ParseColor ("#ffffff"));
			txtLicencia_b.Text = " TERMS OF USE";
			txtLicencia_b.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtLicencia_b.SetTextSize(textFormat,Configuration.getHeight(25));
			txtLicencia_b.SetTextColor (Color.ParseColor ("#00c6ff"));
			chkLogin.Checked = false;

			chkLogin.SetBackgroundColor(Color.ParseColor("#ffffff"));

			imgLogo.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/logo.png"), Configuration.getWidth(360), Configuration.getHeight(230),true));
			btnLogin.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/login.png"), Configuration.getWidth(210), Configuration.getHeight(60),true));
			btnFacebook.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/signupface.png"), Configuration.getWidth(507), Configuration.getHeight(80),true));
			btnSingUp.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/signupnolisto.png"), Configuration.getWidth(507), Configuration.getHeight(80),true));

			Drawable dc = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/toregister.png"), Configuration.getWidth(47), Configuration.getHeight(47), true));
			chkLogin.SetBackgroundDrawable (dc);

			initButtonColor (btnLogin);
			initButtonColor (btnFacebook);
			initButtonColor (btnSingUp);
			linearLicencia.AddView (chkLogin);
			linearLicencia.AddView (txtLicencia_a);
			linearLicencia.AddView (txtLicencia_b);

			linearLogin.AddView (btnLogin);
			linearSingup.AddView (linearLicencia);
			linearSingup.AddView (btnSingUp);
			linearSingup.AddView (btnFacebook);

			linearLogo.AddView (imgLogo);

			linearLogo.SetX (0); linearLogo.SetY (Configuration.getHeight(373));
			linearContentText.SetX (0); linearContentText.SetY (Configuration.getHeight(557));
			linearLogin.SetX (0); linearLogin.SetY (Configuration.getHeight(30));
			linearSingup.SetX (0); linearSingup.SetY (Configuration.getHeight(785));

			relSingup.AddView (linearLogo);
			relSingup.AddView (linearContentText);
			relSingup.AddView (linearLogin);
			relSingup.AddView (linearSingup);
			mainLayout.AddView (relSingup);


			btnSingUp.Click+= delegate {

				if(chkLogin.Checked==true){
					var com = ((LoginViewModel)this.DataContext).SignUpCommand;
					com.Execute(null);
				}
			};


			btnLogin.Click+= BtnLogin_Click;

			btnFacebook.Click += async delegate {


				await Authenticate(MobileServiceAuthenticationProvider.Facebook);

				/*var com = ((LoginViewModel)this.DataContext).FacebookLoginCommand;
				com.Execute(null);
				*/
			};


			chkLogin.CheckedChange += delegate {
				if(chkLogin.Checked==true){
					btnSingUp.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/signuplisto.png"), Configuration.getWidth(507), Configuration.getHeight(80),true));
				}
				if(chkLogin.Checked==false){
					btnSingUp.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/signupnolisto.png"), Configuration.getWidth(507), Configuration.getHeight(80),true));
				}

			};
		}

		/*	void BtnSing_Touch (object sender, View.TouchEventArgs e)
		{
			if (e.Event.Action == MotionEventActions.Down) {
				btnSingUp.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/signuplisto.png"), Configuration.getWidth(507), Configuration.getHeight(78),true));
			}

			if (e.Event.Action == MotionEventActions.Up) {
				btnSingUp.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/signupnolisto.png"), Configuration.getWidth(507), Configuration.getHeight(78),true));
				var com = ((LoginViewModel)this.DataContext).SignUpCommand;
				com.Execute(null);
			}						

		}

*/
		void BtnLogin_Click (object sender, EventArgs e)
		{
			AlphaAnimation animation = new AlphaAnimation(1.0f,0.0f);
			animation.Duration = 350;
			animation.AnimationEnd+= Animation_AnimationEnd;               

			relSingup.StartAnimation(animation);     
		}

		void Animation_AnimationEnd (object sender, Animation.AnimationEndEventArgs e)
		{
			relSingup.Alpha = 0.0f;
			setItemLogin ();
			//setItemsContentRegister();

		}

		public void setItemLogin(){



			var txtFormat = Android.Util.ComplexUnitType.Px;

			linearButtonLogin = new LinearLayout (this);
			linearEditTextLogin = new LinearLayout (this);
			linearTextLogin = new LinearLayout (this);



			etxtUser = new EditText (this);
			etxtPassword = new EditText (this);
			btnLoginInto = new ImageButton (this);
			txtLogin_a = new TextView (this);
			txtLogin_b = new TextView (this);
			txtInicioSesion = new TextView (this);

			linearButtonLogin.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearEditTextLogin.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearTextLogin.LayoutParameters = new LinearLayout.LayoutParams (LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);

			etxtUser.LayoutParameters = new ViewGroup.LayoutParams (Configuration.getWidth (507), Configuration.getHeight (78));
			etxtPassword.LayoutParameters = new ViewGroup.LayoutParams (Configuration.getWidth (507), Configuration.getHeight (78));


			linearButtonLogin.Orientation = Orientation.Horizontal;
			linearButtonLogin.SetGravity (GravityFlags.Center);
			linearEditTextLogin.Orientation = Orientation.Vertical;
			linearEditTextLogin.SetGravity (GravityFlags.Center);
			linearTextLogin.Orientation = Orientation.Vertical;
			linearTextLogin.SetGravity (GravityFlags.Center);



			etxtUser.Hint = "  Usuario"; 
			etxtUser.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			etxtPassword.Hint = "  Contraseña";
			etxtPassword.InputType = Android.Text.InputTypes.TextVariationPassword | Android.Text.InputTypes.ClassText;
			etxtPassword.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			etxtPassword.InputType = InputTypes.TextVariationVisiblePassword;

			txtLogin_a.Text = "FORGOT PASSWORD?";
			txtLogin_a.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtLogin_b.Text = "            CHANGE";
			txtLogin_b.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtLogin_a.SetTextSize (txtFormat,Configuration.getHeight(30));
			txtLogin_b.SetTextSize (txtFormat, Configuration.getHeight (30));


			txtInicioSesion.Text = "Iniciar Sesión";
			txtInicioSesion.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			txtInicioSesion.SetTextColor (Color.ParseColor("#ffffff"));
			txtInicioSesion.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (36));

			btnLoginInto.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/otherlogin.png"),Configuration.getWidth (242), Configuration.getHeight (78),true));
			etxtUser.SetTextColor (Color.ParseColor ("#ffffff"));
			etxtPassword.SetTextColor (Color.ParseColor ("#ffffff"));



			btnLoginInto.Click += delegate {
				_dialog.Show();
				var com = ((LoginViewModel)this.DataContext).LoginCommand;
				com.Execute(null);
				//AlertDialog.Builder popupBuilder = new AlertDialog.Builder(this);




			};

			initButtonColor (btnLoginInto);

			etxtPassword.InputType = InputTypes.TextVariationVisiblePassword;
			etxtPassword.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;

			txtLogin_a.SetTextColor (Color.ParseColor ("#ffffff"));
			txtLogin_b.SetTextColor (Color.ParseColor ("#00c6ff"));


			Drawable drawableEditText = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/cajatexto.png"), Configuration.getWidth(507), Configuration.getHeight(80), true));
			etxtUser.SetBackgroundDrawable (drawableEditText);
			etxtPassword.SetBackgroundDrawable (drawableEditText);

			etxtUser.SetSingleLine (true);
			etxtPassword.SetSingleLine (true);

			LinearLayout space = new LinearLayout (this);
			space.LayoutParameters = new LinearLayout.LayoutParams (-1, 20);


			linearTextLogin.AddView (txtLogin_a);

			linearTextLogin.AddView (txtLogin_b);

			//linearButtonLogin.AddView (btnLoginInto);
			//linearButtonLogin.AddView (linearTextLogin);

			linearEditTextLogin.AddView (etxtUser);
			linearEditTextLogin.AddView (space);
			linearEditTextLogin.AddView (etxtPassword);


			txtInicioSesion.SetX (Configuration.getWidth(75)); txtInicioSesion.SetY (Configuration.getHeight(680));
			linearEditTextLogin.SetX (0); linearEditTextLogin.SetY (Configuration.getHeight(741));
			//linearButtonLogin.SetX (0); linearButtonLogin.SetY (Configuration.getHeight(978));


			btnLoginInto.SetX (Configuration.getWidth (45));btnLoginInto.SetY (Configuration.getHeight (980));
			linearTextLogin.SetX (Configuration.getWidth (345));linearTextLogin.SetY (Configuration.getHeight(995));

			relLogin.AddView (txtInicioSesion);
			relLogin.AddView (linearEditTextLogin);
			//relLogin.AddView (linearButtonLogin);
			relLogin.AddView(btnLoginInto);
			relLogin.AddView (linearTextLogin);

			((LoginViewModel)this.ViewModel).PropertyChanged += Login_propertyChanged;;



			var set = this.CreateBindingSet<LoginView, LoginViewModel>();
			set.Bind(etxtUser).To(vm => vm.Username);
			set.Bind(etxtPassword).To(vm => vm.Password);
			set.Apply(); 

			mainLayout.AddView (relLogin);
		}

		void Login_propertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{

			var vm = (LoginViewModel)this.ViewModel;
			if (e.PropertyName == "LoginOK")
			{
				if (!vm.LoginOK) {

					_dialog.Hide ();
					AlertDialog.Builder builder = new AlertDialog.Builder (this);
					AlertDialog alertDialog = builder.Create ();
					alertDialog.SetTitle ("");
					alertDialog.SetMessage ("Ingrese datos correctos");

					alertDialog.SetButton ("Ok", (s, ev) => {

					});
					alertDialog.Show ();

				} else {


				}


			}

			if (e.PropertyName == "ConnectionOK")
			{
				if (!vm.ConnectionOK)
				{
					_dialog.Hide ();
					AlertDialog.Builder builder = new AlertDialog.Builder (this);
					AlertDialog alertDialog = builder.Create ();
					alertDialog.SetTitle ("");
					alertDialog.SetMessage ("Verifique su conexión de Internet");

					alertDialog.SetButton ("Ok", (s, ev) => {
					});
					alertDialog.Show ();

				}
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

		public void initText(TextView txt, String texto, String colores){

			var txtf = Android.Util.ComplexUnitType.Px;

			txt.Text = texto;
			txt.SetTextColor(Color.ParseColor(colores));
			txt.SetTextSize(txtf,Configuration.getHeight(30));	
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			//_dialog.Hide ();
		}

		/*public override void OnBackPressed ()
		{
			base.OnBackPressed ();
			relLogin.Alpha = 0;
			relSingup.Alpha = 255;
		}
*/
		MobileServiceUser user ;

		private async Task Authenticate(MobileServiceAuthenticationProvider provider)
		{
			while (user == null)
			{ 
				try
				{
					WAMSRepositoryService service = Mvx.Resolve<IRepositoryService>() as WAMSRepositoryService;
					user = await service.MobileService.LoginAsync(this, provider); 
					System.Console.WriteLine ("Facebook : " + user.UserId + "  " + user.MobileServiceAuthenticationToken ) ;
				}
				catch (InvalidOperationException e)
				{} 
			} 
			var vm = ViewModel as LoginViewModel;
			vm.CreateUserCommand.Execute(user);
		}
	}
}