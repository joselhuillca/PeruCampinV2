
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
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Util;
using Facebook;
using Android.Preferences;

namespace MLearning.Droid
{

	[Activity (Label = "FacebookLogin")]			
	public class FacebookLogin : Activity
	{
		public RelativeLayout mainLayout;
		ImageView loginFace;
		CheckBox checkbox;
		Button loginFree;



		/// <summary>
		/// Extended permissions is a comma separated list of permissions to ask the user.
		/// </summary>


		FacebookClient fb;
		string accessToken;
		bool isLoggedIn;
		string lastMessageId;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			this.Window.AddFlags(WindowManagerFlags.Fullscreen);
			base.OnCreate (savedInstanceState);

			mainLayout = new RelativeLayout (this);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);
			SetContentView (mainLayout);

			Drawable dr = new BitmapDrawable (getBitmapFromAsset ("images/fondoLogin.png"));
			mainLayout.SetBackgroundDrawable (dr);

		
			var btnFace = Bitmap.CreateScaledBitmap(getBitmapFromAsset("icons/loginFace.png"), Configuration.getWidth(505), Configuration.getWidth(76), true);
			loginFace = new ImageView(this);
			loginFace.SetImageBitmap(btnFace);
			loginFace.SetX(Configuration.getWidth(74));
			loginFace.SetY(Configuration.getHeight(784));
			loginFace.Click += delegate {
				if(checkbox.Checked){
					//funcFavoritos(favorit_);
					var webAuth = new Intent (this, typeof (FBWebViewAuthActivity));
					webAuth.PutExtra ("AppId", Configuration.AppId);
					webAuth.PutExtra ("ExtendedPermissions", Configuration.ExtendedPermissions);
					StartActivityForResult (webAuth, 0);
				}else{
					Toast.MakeText (this, "Acepta los Terminos!!", ToastLength.Short).Show();
				}

			};

			loginFree = new Button(this);
			loginFree.Text = "Ingresar sin registrarse";
			//loginFree.Typeface = Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			loginFree.SetTextSize(ComplexUnitType.Fraction, Configuration.getWidth(22));
			loginFree.SetTextColor(Color.White);
			loginFree.SetBackgroundColor(Color.ParseColor("#2979FF"));
			loginFree.LayoutParameters = new ViewGroup.LayoutParams(Configuration.getWidth(505), Configuration.getWidth(70));
			loginFree.SetX(Configuration.getWidth(74));
			loginFree.SetY(Configuration.getHeight(878));
			loginFree.Click += delegate
			{
				ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
				ISharedPreferencesEditor editor = prefs.Edit();
				editor.PutBoolean("inicioSesion", true);
				editor.Apply();
				Finish();
			};


			var registercheckfondo = Bitmap.CreateScaledBitmap(getBitmapFromAsset("icons/toregister.png"), Configuration.getWidth(30), Configuration.getWidth(30), true);
			var regist = new ImageView(this);
			regist.SetImageBitmap(registercheckfondo);
			regist.SetX(Configuration.getWidth(82));
			regist.SetY(Configuration.getHeight(713));

			checkbox = new CheckBox (this);
			checkbox.SetX (Configuration.getWidth(82));
			checkbox.SetY (Configuration.getHeight(713));
			LinearLayout linearL = new LinearLayout (this);
			linearL.LayoutParameters = new LinearLayout.LayoutParams (-2, -2);
			linearL.Orientation = Orientation.Horizontal;
			linearL.SetX (Configuration.getWidth(146));
			linearL.SetY (Configuration.getHeight(722));
			TextView txt1 = new TextView (this);
			txt1.Text  = "Registrar, acepto los ";
			//txt1.Typeface = Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			txt1.SetTextColor (Color.ParseColor("#ffffff"));
			txt1.SetTextSize (ComplexUnitType.Fraction,Configuration.getWidth (22));
			TextView txt2 = new TextView (this);
			txt2.Text = "terminos de uso ";
			txt2.SetTextColor (Color.ParseColor("#00BCD4"));
			//txt2.Typeface = Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			txt2.SetTextSize (ComplexUnitType.Fraction,Configuration.getWidth (22));
			linearL.AddView (txt1);
			linearL.AddView (txt2);

			mainLayout.AddView (loginFace);
			//mainLayout.AddView (regist);
			mainLayout.AddView(loginFree);
			mainLayout.AddView (checkbox);
			mainLayout.AddView (linearL);
		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = this.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			switch (resultCode) {
			case Result.Ok:

				accessToken = data.GetStringExtra ("AccessToken");
				string userId = data.GetStringExtra ("UserId");
				string error = data.GetStringExtra ("Exception");

				fb = new FacebookClient (accessToken);

				//ImageView imgUser = FindViewById<ImageView> (Resource.Id.imgUser);
				//TextView txtvUserName = FindViewById<TextView> (Resource.Id.txtvUserName);

				fb.GetTaskAsync ("me").ContinueWith( t => {
					if (!t.IsFaulted) {

						var result = (IDictionary<string, object>)t.Result;

						// available picture types: square (50x50), small (50xvariable height), large (about 200x variable height) (all size in pixels)
						// for more info visit http://developers.facebook.com/docs/reference/api
						string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", userId, "square", accessToken);
						var bm = BitmapFactory.DecodeStream (new Java.Net.URL(profilePictureUrl).OpenStream());
						string profileName = (string)result["name"];

						RunOnUiThread (()=> {
							//imgUser.SetImageBitmap (bm);
							//txtvUserName.Text = profileName;
							mainLayout.RemoveAllViews();
							//Toast.MakeText (this, "Presiona Back!!", ToastLength.Short).Show();
							Finish();
						});

						isLoggedIn = true;

						ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences (this);
						ISharedPreferencesEditor editor = prefs.Edit ();
						editor.PutBoolean ("inicioSesion", true);
						editor.PutString("AccessToken",accessToken);
						editor.Apply(); 



						/*StartActivity(typeof (MainView));
						Finish ();*/

					} else {
						Alert ("Error al Iniciar Sesion", "Motivo: " + error, false, (res) => {} );
					}
				});

				break;
			case Result.Canceled:
				Alert ("Error al Iniciar Sesion", "El usuario a cancelado", false, (res) => {} );
				break;
			default:
				break;
			}
		}

		public void Alert (string title, string message, bool CancelButton , Action<Result> callback)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.SetTitle(title);
			builder.SetIcon(Resource.Drawable.Icon);
			builder.SetMessage(message);

			builder.SetPositiveButton("Ok", (sender, e) => {
				callback(Result.Ok);
			});

			if (CancelButton) {
				builder.SetNegativeButton("Cancel", (sender, e) => {
					callback(Result.Canceled);
				});
			}

			builder.Show();
		}
	}
}

