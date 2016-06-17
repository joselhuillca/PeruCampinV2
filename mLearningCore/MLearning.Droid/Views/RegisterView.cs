
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
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Text;
using Cirrious.MvvmCross.Binding.BindingContext;
using MLearning.Core;

namespace MLearning.Droid
{
	[Activity(Label = "View for FirstViewModel")]	
	public class RegisterView : MvxActivity
	{


		RelativeLayout mainLayout;
		TextView txtRegister;
		EditText etxtUser;
		EditText etxtEmail;
		EditText etxtPassword;
		ImageButton btnCreateAccount;


		LinearLayout linearRegister;
		LinearLayout linearButtonRegister;

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
			txtRegister = new TextView (this);
			etxtEmail = new EditText (this);
			etxtUser = new EditText (this);
			etxtPassword = new EditText (this);
			btnCreateAccount = new ImageButton (this);
			linearButtonRegister = new LinearLayout (this);
			linearRegister = new LinearLayout (this);


			linearButtonRegister.LayoutParameters = new LinearLayout.LayoutParams (-1,LinearLayout.LayoutParams.WrapContent);
			linearRegister.LayoutParameters = new LinearLayout.LayoutParams (-1,LinearLayout.LayoutParams.WrapContent);
			linearButtonRegister.Orientation = Orientation.Horizontal;
			linearRegister.Orientation = Orientation.Vertical;
			linearButtonRegister.SetGravity (GravityFlags.Center);
			linearRegister.SetGravity (GravityFlags.Center);

			etxtUser.LayoutParameters = new ViewGroup.LayoutParams (Configuration.getWidth (507), Configuration.getHeight (78));
			etxtPassword.LayoutParameters = new ViewGroup.LayoutParams (Configuration.getWidth (507), Configuration.getHeight (78));
			etxtEmail.LayoutParameters = new ViewGroup.LayoutParams (Configuration.getWidth (507), Configuration.getHeight (78));
				
	


			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);
			Drawable drawableBackground = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/afondo.png"), 768, 1024, true));
			mainLayout.SetBackgroundDrawable (drawableBackground);


			txtRegister.Text = "Registro";
			txtRegister.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");


			etxtUser.Hint ="  Nombre de usuario";
			etxtUser.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");


			etxtEmail.Hint = "  Dirección de correo";
			etxtEmail.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");


			etxtPassword.Hint ="  Contraseña";
			etxtPassword.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");


			Drawable drawableEditText = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/cajatexto.png"), Configuration.getWidth (507), Configuration.getHeight (80), true));

			etxtUser.SetBackgroundDrawable (drawableEditText);
			etxtPassword.SetBackgroundDrawable (drawableEditText);
			etxtEmail.SetBackgroundDrawable (drawableEditText);

			etxtUser.SetTextColor (Color.ParseColor ("#ffffff"));
			etxtUser.SetHintTextColor(Color.ParseColor ("#ffffff"));
			etxtUser.SetSingleLine (true);
			etxtPassword.SetTextColor (Color.ParseColor ("#ffffff"));
			etxtPassword.SetHintTextColor(Color.ParseColor ("#ffffff"));
			etxtPassword.SetSingleLine (true);
			etxtEmail.SetTextColor (Color.ParseColor ("#ffffff"));
			etxtEmail.SetHintTextColor(Color.ParseColor ("#ffffff"));
			etxtEmail.SetSingleLine (true);

			txtRegister.SetTextColor (Color.ParseColor("#ffffff"));
			txtRegister.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (40));

			etxtPassword.InputType = InputTypes.TextVariationVisiblePassword;
			etxtPassword.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;

			btnCreateAccount.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/crearcuenta.png"), Configuration.getWidth (507), Configuration.getHeight (80), true));
			btnCreateAccount.Alpha = 255;
			//btn.SetAlpha(255);
			btnCreateAccount.SetBackgroundColor(Color.Transparent);


			LinearLayout space = new LinearLayout (this);
			space.LayoutParameters = new LinearLayout.LayoutParams (-1, 20);
			LinearLayout space2 = new LinearLayout (this);
			space2.LayoutParameters = new LinearLayout.LayoutParams (-1, 20);

			linearRegister.AddView (etxtUser);
			linearRegister.AddView (space);
			linearRegister.AddView (etxtEmail);
			linearRegister.AddView (space2);
			linearRegister.AddView (etxtPassword);

			linearButtonRegister.AddView (btnCreateAccount);

			txtRegister.SetX (Configuration.getWidth(72)); txtRegister.SetY (Configuration.getHeight(535));
			linearRegister.SetX (0); linearRegister.SetY (Configuration.getHeight(592));
			linearButtonRegister.SetX (0); linearButtonRegister.SetY (Configuration.getHeight(975));
			mainLayout.AddView (txtRegister);
			mainLayout.AddView (linearRegister);
			mainLayout.AddView (linearButtonRegister);

			//string ndef = "None" ;

			//string foto = "http://www.clinicatorielli.com/img/icons/no-user.png";

			EditText lastName = new EditText (this);
			lastName.Text = "None";

			EditText url = new EditText (this);
			url.Text = "http://www.clinicatorielli.com/img/icons/no-user.png";

			var set = this.CreateBindingSet<RegisterView, RegisterViewModel>();
			set.Bind(etxtUser).To(vm=>vm.RegUsername);
			set.Bind(etxtEmail).To(vm=>vm.Email);
			set.Bind(etxtPassword).To(vm=>vm.RegPassword);
			set.Bind(etxtUser).To(vm=>vm.Name);


			set.Apply ();

			btnCreateAccount.Click += delegate {
				var com = ((RegisterViewModel)this.DataContext).RegisterCommand;
				com.Execute (null);
			};


		}

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =this.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}
	}
}

