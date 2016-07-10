using Android.App;
using Android.OS;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Views;
using Core.Repositories;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
using MLearning.Core.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Widget;
using Android.Graphics;
using Android.Views;
using System.Collections.Generic;
using Android.Support.V4.View;
using DataSource;
using System.Collections.ObjectModel;
using Android.Content.PM;
using Android.Content;
using System.Threading;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Util;
using Tasky.Shared;
using Facebook;
using Android.Preferences;
using System.Collections;
using Android.Support.V7.Widget;
using Com.Telerik.Widget.List;
using Android.Runtime;

namespace MLearning.Droid.Views
{
	[Activity(Label = "View for LOViewModel", ScreenOrientation = ScreenOrientation.Portrait)]
	public class LOView : MvxActivity, VerticalScrollViewPager.ScrollViewListenerPager
	{

		public ArrayList source = new ArrayList();
		RadListView listView;
		SlideLayoutManager slideLayoutManager;
		int orientation = OrientationHelper.Horizontal;
		public RelativeLayout layoutList;
		TextView titulo_detalle;

		LOViewModel vm; 
		Bitmap bm_user;
		Bitmap bmLike;
		Drawable drBack;

		FacebookClient fb;
		string accessToken;
		bool isLoggedIn;

        IList<Tasky.Shared.TodoItem> listNotas;

        ProgressDialog _dialogDownload;
		//	LinearLayout layoutPanelScroll;
		RelativeLayout _mainLayout;
		RelativeLayout mainLayoutIndice;
		RelativeLayout mainLayoutPages;

		public Bitmap iconFavorito;
		private ImageView favorit;

		int widthInDp;
		int heightInDp;
		List<FrontContainerView> listFront = new List<FrontContainerView> ();


		List<FrontContainerViewPager> listFrontPager = new List<FrontContainerViewPager>();
		//	VerticalScrollView scrollVertical;
		bool ISLOADED= false;
		int IndiceSection=0;

		List<VerticalScrollViewPager> listaScroll = new List<VerticalScrollViewPager>();
		List<VerticalScrollViewPager> listaScrollIni = new List<VerticalScrollViewPager>();

		ViewPager viewPager;
		ViewPager viewPagerIni;

		public List<string> adsImagesPath = new List<string>();
		public LinearLayout selectLayout;
		public LinearLayout _publicidadLayout;
		public LinearLayout _adLayout;
		public bool adOpen = false;

		async protected  override  void OnCreate(Bundle bundle)
		{

            //Sacar la lista de Favoritos
            listNotas = TodoItemManager.GetTasks();
            //-----------------------------

            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
			base.OnCreate(bundle);
			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);
			vm = this.ViewModel as LOViewModel;

			int tam = Configuration.getWidth (80);

			drBack = new BitmapDrawable(Bitmap.CreateScaledBitmap (getBitmapFromAsset ("images/fondocondiagonalm.png"), 640, 1136, true));
			LinearLayout test = new LinearLayout (this);
			test.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);
			test.SetBackgroundColor(Color.Black);
			SetContentView (test);


			_dialogDownload = new ProgressDialog (this);
			_dialogDownload.SetCancelable (false);
			_dialogDownload.SetMessage ("Cargando");
			_dialogDownload.Show ();

			adsImagesPath = AddResources.Instance.addList;

			await ini();
			//LoadPagesDataSource ();

			SetContentView (_mainLayout);

			_dialogDownload.Dismiss ();
		} 

		void showAd(int idAd)
		{
			adOpen = true;
			_adLayout = new LinearLayout (this);
			_adLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (255));
			Drawable dr = new BitmapDrawable (getBitmapFromAsset (adsImagesPath[idAd]));
			_adLayout.SetBackgroundDrawable (dr);
			_adLayout.SetY (Configuration.getHeight(1136-85-255));
			_mainLayout.AddView (_adLayout);

			_adLayout.Click += delegate {
				//se cuelga
				//this.StartActivity(Configuration.getOpenFacebookIntent(this,"fb://page/114091405281757","http://www.hi-tec.com/pe/"));
			};
		}

		void hideAd()
		{
			adOpen = false;
			_mainLayout.RemoveView (_adLayout);
		}


		async Task  ini(){

			_mainLayout = new RelativeLayout (this);

			_mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);	
			_mainLayout.SetBackgroundColor(Color.ParseColor("#ffffff"));

			mainLayoutIndice = new RelativeLayout (this);
			mainLayoutIndice.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);	

			mainLayoutPages = new RelativeLayout (this);
			mainLayoutPages.LayoutParameters = new RelativeLayout.LayoutParams (-1,Configuration.getWidth(1136-85));	
			mainLayoutPages.SetBackgroundColor(Color.ParseColor("#ffffff"));
			viewPager = new ViewPager (this);
			viewPagerIni = new ViewPager (this);

			mainLayoutIndice.SetX (0); mainLayoutIndice.SetY (0);
			_mainLayout.AddView (mainLayoutIndice);


			await vm.InitLoad();

			LoadPagesDataSource();

			viewPagerIni.SetOnPageChangeListener (new MyPageChangeListener (this,listFront));
			viewPager.SetOnPageChangeListener (new MyPageChangeListenerPager (this, listFrontPager));
			vm.PropertyChanged += Vm_PropertyChanged;




		}
		//Al hacer clic en el iconFavoritos
		public void funcFavoritos(ImageView favorit_)
		{
			AlertDialog.Builder popupBuilder = new AlertDialog.Builder(this);
			popupBuilder.SetTitle("Mis Favoritos");
			popupBuilder.SetCancelable(false);
			popupBuilder.SetMessage("Ir a mis favoritos");
			popupBuilder.SetNeutralButton("Volver", delegate {  });
			//popupBuilder.Show();

			//linearContenido.RemoveView (favorit);
			Dialog dial = popupBuilder.Create ();
			dial.Show ();
			new Thread(() =>
				{
					Thread.Sleep(1000);
					dial.Dismiss();

				}).Start();
			
		}

		void Vm_PropertyChanged (object sender, PropertyChangedEventArgs e)
		{

			//var vm = this.ViewModel as LOViewModel;
			if (e.PropertyName == "IsWaiting") {
			}

			if (e.PropertyName == "LOsInCircle")
			if (vm.LOsInCircle != null) {
			}
			//vm.LOsInCircle.CollectionChanged+= Vm_LOsInCircle_CollectionChanged;
		}

		void Vm_LOsInCircle_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{

			//loadLOsInCircle (e.NewStartingIndex);		
			if (e.NewItems != null) {
				int i = 0;
				foreach (LOViewModel.lo_by_circle_wrapper lobycircle in e.NewItems) {
					VerticalScrollViewPager scrollPager = new VerticalScrollViewPager (this);
					scrollPager.setOnScrollViewListener (this); 
					LinearLayout linearScroll = new LinearLayout (this);
					linearScroll.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
					linearScroll.Orientation = Orientation.Vertical;

					//	if(Configuration.IndiceActual==i){
					FrontContainerView front = new FrontContainerView (this);
					front.Tag = "indice";
					front.Author = lobycircle.lo.name + " " + vm.LOsInCircle [i].lo.lastname;
					front.Chapter = lobycircle.lo.description;
					front.NameLO = lobycircle.lo.title;
					front.Like = "10";
					front.ImageChapter = lobycircle.lo.url_background;

					listFront.Add (front);
					listFront [i].setBack (drBack,bmLike);

					lobycircle.PropertyChanged += (s1, e1) =>
					{
						if (e1.PropertyName == "background_bytes")
						{
							front.ImageChapter = lobycircle.lo.url_background;

						}
					};								

					linearScroll.AddView (front);
					if (lobycircle.stack.IsLoaded) {				
						var s_list = lobycircle.stack.StacksList;
						int indice = 0;
						for (int j = 0; j < s_list.Count; j++) {				


							for (int k = 0; k < s_list [j].PagesList.Count; k++) {

								ChapterContainerView section = new ChapterContainerView (this);								
								section.Author = lobycircle.lo.name + " " + lobycircle.lo.lastname;					
								section.Title = s_list [j].PagesList [k].page.title;
								section.Container = s_list [j].PagesList [k].page.description;							
								section.ColorText = Configuration.ListaColores [indice % 6];
								section.setDefaultProfileUserBitmap (bm_user);


								section.Image = s_list [j].PagesList [k].page.url_img;
								section.Indice = indice;								
								section.Click += delegate {
									IndiceSection = section.Indice; 										
									mainLayoutIndice.Visibility = ViewStates.Invisible;		
									if (ISLOADED == false) {		
										LoadPagesDataSource();
									} else {
										viewPager.CurrentItem= IndiceSection;
										mainLayoutPages.Visibility = ViewStates.Visible;
										mainLayoutIndice.Visibility = ViewStates.Invisible;
									}

								};
								linearScroll.AddView (section);
								indice++;
							}

						}
					}
					scrollPager.VerticalScrollBarEnabled = false;
					scrollPager.AddView (linearScroll);
					listaScrollIni.Add (scrollPager);




					i++;
				}

				mainLayoutIndice.RemoveAllViews ();
				//_progresD.Hide ();
				mainLayoutIndice.AddView (viewPagerIni);
				mainLayoutIndice.SetX (0);
				mainLayoutIndice.SetY (0);
				//mainLayout.AddView (mainLayoutIndice);
				LOViewAdapter adapter = new LOViewAdapter (this, listaScrollIni);
				viewPagerIni.Adapter = adapter;



			}




		}

		void loadLOsInCircle(int index){


			//var vm = this.ViewModel as LOViewModel;
			if (vm.LOsInCircle != null) {		


				for (int i = 0; i < vm.LOsInCircle.Count; i++) {
					VerticalScrollViewPager scrollPager = new VerticalScrollViewPager (this);
					scrollPager.setOnScrollViewListener (this); 
					LinearLayout linearScroll = new LinearLayout (this);
					linearScroll.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
					linearScroll.Orientation = Orientation.Vertical;

					//	if(Configuration.IndiceActual==i){
					FrontContainerView front = new FrontContainerView (this);
					front.Tag = "indice";
					front.Author = vm.LOsInCircle [i].lo.name + " " + vm.LOsInCircle [i].lo.lastname;
					front.Chapter = vm.LOsInCircle [i].lo.description;
					front.NameLO = vm.LOsInCircle [i].lo.title;
					front.Like = "10";
					front.ImageChapter = vm.LOsInCircle [i].lo.url_background;

					listFront.Add (front);
					listFront [i].setBack (drBack,bmLike);


					/*
					if (vm.LOsInCircle [i].background_bytes != null) {
						Bitmap bm = BitmapFactory.DecodeByteArray (vm.LOsInCircle [i].background_bytes, 0, vm.LOsInCircle [i].background_bytes.Length);

						front.ImageChapterBitmap = bm;
						bm = null;
					}
					*/

					vm.LOsInCircle[i].PropertyChanged += (s1, e1) =>
					{
						if (e1.PropertyName == "background_bytes")
						{
							/*
							Bitmap bm = BitmapFactory.DecodeByteArray (vm.LOsInCircle [i].background_bytes, 0, vm.LOsInCircle [i].background_bytes.Length);
							front.ImageChapterBitmap = bm;
							bm = null;
							*/
							front.ImageChapter = vm.LOsInCircle [i].lo.url_background;

						}
					};								

					linearScroll.AddView (front);

					if (vm.LOsInCircle [i].stack.IsLoaded) {				
						var s_list = vm.LOsInCircle [i].stack.StacksList;
						int indice = 0;
						for (int j = 0; j < s_list.Count; j++) {				


							for (int k = 0; k < s_list [j].PagesList.Count; k++) {

								ChapterContainerView section = new ChapterContainerView (this);								
								section.Author = vm.LOsInCircle [i].lo.name + " " + vm.LOsInCircle [i].lo.lastname;					
								section.Title = s_list [j].PagesList [k].page.title;
								section.Container = s_list [j].PagesList [k].page.description;							
								section.ColorText = Configuration.ListaColores [indice % 6];
								section.setDefaultProfileUserBitmap (bm_user);


								section.Image = s_list [j].PagesList [k].page.url_img;
								/*
								if (s_list [j].PagesList [k].cover_bytes != null) {
									Bitmap bm = BitmapFactory.DecodeByteArray (s_list [j].PagesList [k].cover_bytes, 0, s_list [j].PagesList[k].cover_bytes.Length);
									section.ImageBitmap = bm;
									bm = null;
								}
								*/

								section.Indice = indice;								
								section.Click += delegate {


									IndiceSection = section.Indice; 										



									mainLayoutIndice.Visibility = ViewStates.Invisible;		



									if (ISLOADED == false) {		
										LoadPagesDataSource();
									} else {

										viewPager.CurrentItem= IndiceSection;
										mainLayoutPages.Visibility = ViewStates.Visible;

										mainLayoutIndice.Visibility = ViewStates.Invisible;



									}

								};
								linearScroll.AddView (section);
								indice++;
							}

						}
					} else {
						vm.LOsInCircle [i].stack.PropertyChanged+= (s3, e3) => {
							var s_list = vm.LOsInCircle [i].stack.StacksList;
							int indice = 0;
							for (int j = 0; j < s_list.Count; j++) {
								for (int k = 0; k < s_list [j].PagesList.Count; k++) {
									ChapterContainerView section = new ChapterContainerView (this);								
									section.Author = vm.LOsInCircle [i].lo.name + " " + vm.LOsInCircle [i].lo.lastname;					
									section.Title = s_list [j].PagesList [k].page.title;
									section.Container = s_list [j].PagesList [k].page.description;							
									section.ColorText = Configuration.ListaColores [indice % 6];
									section.setDefaultProfileUserBitmap (bm_user);
									section.Image = s_list [j].PagesList [k].page.url_img;
									section.Indice = indice;								
									section.Click += delegate {
										IndiceSection = section.Indice; 										
										mainLayoutIndice.Visibility = ViewStates.Invisible;		
										if (ISLOADED == false) {	
											LoadPagesDataSource();
										} else {
											viewPager.CurrentItem= IndiceSection;
											mainLayoutPages.Visibility = ViewStates.Visible;
											mainLayoutIndice.Visibility = ViewStates.Invisible;
										}
									};
									linearScroll.AddView (section);
									indice++;
								}

							}
						};

					}

					scrollPager.VerticalScrollBarEnabled = false;
					scrollPager.AddView (linearScroll);

					listaScrollIni.Add (scrollPager);

				}
				mainLayoutIndice.RemoveAllViews ();
				//_progresD.Hide ();
				mainLayoutIndice.AddView (viewPagerIni);
				mainLayoutIndice.SetX (0);
				mainLayoutIndice.SetY (0);
				//mainLayout.AddView (mainLayoutIndice);
				LOViewAdapter adapter = new LOViewAdapter (this, listaScrollIni);
				viewPagerIni.Adapter = adapter;
				//viewPager.CurrentItem = IndiceSection;
			}

		}

        //Verificar si esta en la lista de favoritos
        public int isListNotas(String Titulo)
        {
            //Sacar la lista de Favoritos
            listNotas.Clear();
            listNotas = TodoItemManager.GetTasks();
            //-----------------------------
            int tam = listNotas.Count;
            for (int i = 0; i < tam; i++)
            {
                if (listNotas[i].Name.Equals(Titulo))
                {
                    return listNotas[i].ID; ;
                }
            }
            return -1;
        }

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			switch (resultCode)
			{
				case Result.Ok:

					accessToken = data.GetStringExtra("AccessToken");
					string userId = data.GetStringExtra("UserId");
					string error = data.GetStringExtra("Exception");

					fb = new FacebookClient(accessToken);

					//ImageView imgUser = FindViewById<ImageView> (Resource.Id.imgUser);
					//TextView txtvUserName = FindViewById<TextView> (Resource.Id.txtvUserName);

					fb.GetTaskAsync("me").ContinueWith(t =>
					{
						if (!t.IsFaulted)
						{

							var result = (IDictionary<string, object>)t.Result;

							// available picture types: square (50x50), small (50xvariable height), large (about 200x variable height) (all size in pixels)
							// for more info visit http://developers.facebook.com/docs/reference/api
							string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", userId, "square", accessToken);
							var bm = BitmapFactory.DecodeStream(new Java.Net.URL(profilePictureUrl).OpenStream());
							string profileName = (string)result["name"];

							RunOnUiThread(() =>
							{
								//imgUser.SetImageBitmap (bm);
								//txtvUserName.Text = profileName;
								//Toast.MakeText (this, "Presiona Back!!", ToastLength.Short).Show();
								//Finish();
							});

							isLoggedIn = true;

							ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
							ISharedPreferencesEditor editor = prefs.Edit();
							editor.PutBoolean("inicioSesion", true);
							editor.PutString("AccessToken", accessToken);
							editor.Apply();



							/*StartActivity(typeof (MainView));
							Finish ();*/

						}
						else {
							Alert("Error al Iniciar Sesion", "Motivo: " + error, false, (res) => { });
						}
					});

					break;
				case Result.Canceled:
					Alert("Error al Iniciar Sesion", "El usuario a cancelado", false, (res) => { });
					break;
				default:
					break;
			}
		}

		public void Alert(string title, string message, bool CancelButton, Action<Result> callback)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.SetTitle(title);
			builder.SetIcon(Resource.Drawable.Icon);
			builder.SetMessage(message);

			builder.SetPositiveButton("Ok", (sender, e) =>
			{
				callback(Result.Ok);
			});

			if (CancelButton)
			{
				builder.SetNegativeButton("Cancel", (sender, e) =>
				{
					callback(Result.Canceled);
				});
			}

			builder.Show();
		}

		public void HandlePostHiToWall (String titulo)
		{
			
			ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences (this);
			bool mBool = prefs.GetBoolean ("inicioSesion",false);
			if (mBool) {
				Toast.MakeText (this, "Loading...", ToastLength.Short).Show();
				String accessToken = prefs.GetString ("AccessToken",null);
				if (accessToken == null) { 
					Toast.MakeText(this, "Primero inicia sesiòn!!", ToastLength.Short).Show();
					var webAuth = new Intent(this, typeof(FBWebViewAuthActivity));
					webAuth.PutExtra("AppId", Configuration.AppId);
					webAuth.PutExtra("ExtendedPermissions", Configuration.ExtendedPermissions);
					StartActivityForResult(webAuth, 0);
					return;
				}
				fb = new FacebookClient (accessToken);
				fb.PostTaskAsync ("me/feed", new { message = "Vas a visitar " + titulo }).ContinueWith (t => {
					if (!t.IsFaulted) {

						var result = (IDictionary<string, object>)t.Result;
						//lastMessageId = (string)result["id"];

						RunOnUiThread ( ()=> {
							//Alert ("Success", "You have posted \"Hi\" to your wall.", false, (res) => { });
							Toast.MakeText (this, "Has hecho un post en tu muro de Facebook!!", ToastLength.Short).Show();
						});
					}else{
						Toast.MakeText (this, "Error al hacer el post!!", ToastLength.Short).Show();
					}
				});
			} else {
				//Alert ("Not Logged In", "Please Log In First", false, (res) => { });
				Toast.MakeText (this, "Primero inicia sesiòn!!", ToastLength.Short).Show();
				var webAuth = new Intent(this, typeof(FBWebViewAuthActivity));
				webAuth.PutExtra("AppId", Configuration.AppId);
				webAuth.PutExtra("ExtendedPermissions", Configuration.ExtendedPermissions);
				StartActivityForResult(webAuth, 0);
				return;
			}
		}

        void LoadPagesDataSource()
		{



			bool is_main = true;
			int space = Configuration.getWidth (30);
		
			var s_listp = vm.LOsInCircle[vm._currentUnidad].stack.StacksList;
				int indice = 0;
		 
					
				if (s_listp != null) {


				int j = vm._currentSection;
				//	for (int j = 0; j < s_listp.Count; j++) {						

						for (int k = 0; k < s_listp [j].PagesList.Count; k++) {

				//		if (j == vm._currentSection) {

							VerticalScrollViewPager scrollPager = new VerticalScrollViewPager (this);
							scrollPager.setOnScrollViewListener (this); 
							LinearLayout linearScroll = new LinearLayout (this);
							linearScroll.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
							linearScroll.Orientation = Orientation.Vertical;

							var content = s_listp [j].PagesList [k].content;
							FrontContainerViewPager front = new FrontContainerViewPager (this);
							front.Tag = "pager";


							front.ImageChapter = s_listp [j].PagesList [k].page.url_img;


							front.Title = s_listp [j].PagesList [k].page.title;
							front.Description = s_listp [j].PagesList [k].page.description;


							var slides = s_listp [j].PagesList [k].content.lopage.loslide;
							front.setBack (drBack);


							linearScroll.AddView (front);

							LinearLayout descriptionLayout = new LinearLayout (this);
							descriptionLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
							descriptionLayout.SetPadding (space, 0, space, space);
							descriptionLayout.Orientation = Orientation.Vertical;

							titulo_detalle = new TextView (this);
							titulo_detalle.Text = "Descripción";
							titulo_detalle.Typeface = Typeface.CreateFromAsset (this.Assets, "fonts/ArcherMediumPro.otf");
							titulo_detalle.SetTextSize (ComplexUnitType.Fraction, Configuration.getHeight (38));
					        titulo_detalle.SetTextColor (Color.ParseColor (Configuration.colorGlobal));
							titulo_detalle.SetPadding (0, 0, 0, space);
							descriptionLayout.AddView (titulo_detalle);

							TextView detalle = new TextView (this);
							detalle.TextFormatted = Html.FromHtml (slides [0].loparagraph);
							detalle.Typeface = Typeface.CreateFromAsset (this.Assets, "fonts/ArcherMediumPro.otf");
							detalle.SetTextSize (ComplexUnitType.Fraction, Configuration.getHeight (32));
							descriptionLayout.AddView (detalle);

							ViewTreeObserver vto = detalle.ViewTreeObserver;
							int H = 0;
							vto.GlobalLayout += (sender, args) =>
							{     
								H = detalle.Height;
								Console.WriteLine ("TAM:::1:" + H );
								detalle.LayoutParameters.Height = H-Configuration.getHeight(72);

							};  



							LinearLayout separationLinear = new LinearLayout (this);
							separationLinear.LayoutParameters = new LinearLayout.LayoutParams (-1, 5);
							separationLinear.SetBackgroundColor (Color.ParseColor ("#D8D8D8"));
							separationLinear.Orientation = Orientation.Horizontal;
							//separationLinear.SetPadding (0,0,0,50);

					//Añadir mis-Favoritos-----------------------------------------------------------
					iconFavorito = Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/notas.png"), Configuration.getWidth (52), Configuration.getWidth (42), true);
					//Colocando icono de Favoritos
					ImageView favorit_ = new ImageView (this);
					favorit_.Tag = j;
					favorit_.SetImageBitmap (iconFavorito);
					favorit_.SetX (Configuration.getWidth(5));
					favorit_.SetY (Configuration.getHeight (55));
					//favorit_.Click += delegate{funcFavoritos(favorit_);};

                    var iconFace = Bitmap.CreateScaledBitmap(getBitmapFromAsset("icons/face_icon.jpg"), Configuration.getWidth(30), Configuration.getWidth(30), true);
                    //Colocando icono de Favoritos
                    ImageView faceicon = new ImageView(this);
                    faceicon.Tag = j;
                    faceicon.SetImageBitmap(iconFace);
                    faceicon.SetX(Configuration.getWidth(15));
                    faceicon.SetY(Configuration.getHeight(0));
                    //favorit_.Click += delegate { funcFavoritos(favorit_); };

                    TextView shared_face = new TextView(this);
                    shared_face.Text = "Comparte tu experiencia!";
					//shared_face.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (580), LinearLayout.LayoutParams.WrapContent);
                    shared_face.SetTextColor(Color.ParseColor("#1A237E"));
                    shared_face.SetTextSize(ComplexUnitType.Fraction, Configuration.getHeight(30));
                    shared_face.SetX(Configuration.getWidth(48));
					//shared_face.Gravity = GravityFlags.Right;
                    shared_face.SetY(Configuration.getHeight(-11));
					shared_face.Click += delegate
					{
						HandlePostHiToWall( front.Title);
					};


                    TextView tomar_notas = new TextView(this);
                    tomar_notas.Text = "Toma notas";
                    tomar_notas.SetTextColor(Color.ParseColor("#E65100"));
                    tomar_notas.SetTextSize(ComplexUnitType.Fraction, Configuration.getHeight(30));
                    tomar_notas.SetX(Configuration.getWidth(48));
                    tomar_notas.SetY(Configuration.getHeight(55));
                    tomar_notas.Click += delegate
                    {
                        Bundle bundle = new Bundle();
                        bundle.PutString("Titulo", front.Title);
                        bundle.PutInt("TaskID", isListNotas(front.Title));

				                        //Intent nos permite enlazar dos actividades
				                        Intent intent = new Intent(this, typeof(NotasItemScreen));
				                   //añadir parametros
				                   intent.PutExtras( bundle );
				                   //ejuta intent
				                   StartActivity(intent );
				    };

                        RelativeLayout misFavoritos = new RelativeLayout (this);
					misFavoritos.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (122));
					misFavoritos.SetX (Configuration.getWidth(0));
					misFavoritos.SetY (Configuration.getHeight (0));
                    misFavoritos.AddView(tomar_notas);
                    misFavoritos.AddView (shared_face);
                    misFavoritos.AddView(favorit_);
                    misFavoritos.AddView(faceicon);
                    linearScroll.AddView (misFavoritos);
					//FIN FAVoritos-----------------------------------------------------------
							linearScroll.AddView (descriptionLayout);
							linearScroll.AddView (separationLinear);

							listFrontPager.Add (front);

							var currentpage = s_listp [j].PagesList [k];

				
					bool is50Campamentos = false;

					for (int m = 1; m < slides.Count; m++)
					{
						LOSlideSource slidesource = new LOSlideSource(this);

						var _id_ = vm.LOsInCircle[vm._currentUnidad].lo.color_id;
						is_main = !is_main;


						slidesource.ColorS = Configuration.ListaColores[indice % 6];

						slidesource.Type = slides[m].lotype;
						if (slides[m].lotitle != null)
							slidesource.Title = slides[m].lotitle;
						if (slides[m].loparagraph != null)
							slidesource.Paragraph = slides[m].loparagraph;
						if (slides[m].loimage != null)
							slidesource.ImageUrl = slides[m].loimage;
						if (slides[m].lotext != null)
							slidesource.Paragraph = slides[m].lotext;
						if (slides[m].loauthor != null)
							slidesource.Author = slides[m].loauthor;
						if (slides[m].lovideo != null)
							slidesource.VideoUrl = slides[m].lovideo;

						var c_slide = slides[m];
						if (c_slide.loitemize != null)
						{
							slidesource.Itemize = new ObservableCollection<LOItemSource>();
							var items = c_slide.loitemize.loitem;

							for (int n = 0; n < items.Count; n++)
							{
								LOItemSource item = new LOItemSource();
								if (items[n].loimage != null)
									item.ImageUrl = items[n].loimage;
								if (items[n].lotext != null)
									item.Text = items[n].lotext;


								var c_item_ize = items[n];

								slidesource.Itemize.Add(item);
							}
						}



						slidesource.title_page = front.Title;




						if (slidesource.Title != null)
						{
							if (slidesource.Title.Equals("@ #E98300 #NONE Datos básicos") || slidesource.Title.Equals("@ #97233F #NONE Datos básicos") || slidesource.Title.Equals("@ #5B8F22 #NONE Datos básicos"))
							{
								is50Campamentos = true;

							}

						}

						var vista = slidesource.getViewSlide();
						titulo_detalle.SetTextColor(Color.ParseColor(Configuration.colorGlobal));
						if (slidesource.Type != 5 )
						{
								if (slidesource.imgCamp != null && is50Campamentos)
								{
									source.Add(slidesource.imgCamp);
								}
								else {
									linearScroll.AddView(vista);//Toda la info menos la descripcion
								}
						}
					}

					//Solo los 50 campamentos pueden tener titulo de Descripcion
					if (!is50Campamentos)
					{
						descriptionLayout.RemoveView(titulo_detalle);
					}

					//Añadimos las imagenes del array source
					if (source.Count > 0)
					{
						layoutList = new RelativeLayout(this);
						layoutList.LayoutParameters = new LinearLayout.LayoutParams(-2, -2);
						//layoutList.SetGravity(GravityFlags.CenterHorizontal);
						//layoutList.Orientation = Android.Widget.Orientation.Vertical;
						//layoutList.SetBackgroundColor(Color.ParseColor("#FFC107"));
						//listView = (RadListView)FindViewById(Resource.Id.listView).JavaCast<RadListView>();
						listView = new RadListView(this).JavaCast<RadListView>();
						listView.LayoutParameters = new LinearLayout.LayoutParams(-1, Configuration.getHeight(600));
						//listView.SetBackgroundColor(Color.ParseColor("#FFC107"));

						//Añadimos botones previus and next
						/*var btnImg= Bitmap.CreateScaledBitmap(getBitmapFromAsset("icons/atras.png"), Configuration.getWidth(25), Configuration.getWidth(25), true);
						Drawable dr = new BitmapDrawable(btnImg);
						Button previousBtn = new Button(this);
						//previousBtn.Text = "<";
						previousBtn.SetBackgroundDrawable(dr);
						previousBtn.SetX(Configuration.getWidth(0));
						previousBtn.SetY(Configuration.getHeight(290));
						previousBtn.Click += (object sender, EventArgs e) =>
						{
							slideLayoutManager.ScrollToPrevious();
						};

						var btnImg2 = Bitmap.CreateScaledBitmap(getBitmapFromAsset("icons/adelante.png"), Configuration.getWidth(25), Configuration.getWidth(25), true);
						Drawable dr2 = new BitmapDrawable(btnImg2);
						Button nextBtn = new Button(this);
						//nextBtn.Text = ">";
						nextBtn.SetBackgroundDrawable(dr2);
						nextBtn.SetX(Configuration.getWidth(550));
						nextBtn.SetY(Configuration.getHeight(290));
						nextBtn.Click += (object sender, EventArgs e) =>
						{
							slideLayoutManager.ScrollToNext();
						};*/


						layoutList.AddView(listView);
						//layoutList.AddView(previousBtn);
						//layoutList.AddView(nextBtn);


						LinearLayout linearCirculos = new LinearLayout(this);
						linearCirculos.LayoutParameters = new LinearLayout.LayoutParams(-2, -2);
						linearCirculos.Orientation = Orientation.Horizontal;
						linearCirculos.SetBackgroundColor(Color.ParseColor("#40000000"));

						List<ImageView> listCirculos = new List<ImageView>();
						int tamCirc = 25;
						int tamS = source.Count;
						linearCirculos.SetY(Configuration.getHeight(402));
						linearCirculos.SetX(Configuration.getWidth(640 / 2) );

						for (int i = 0; i < tamS; i++)
						{

							var circulo_ = Bitmap.CreateScaledBitmap(getBitmapFromAsset("icons/circulo.png"), Configuration.getWidth(tamCirc), Configuration.getWidth(tamCirc), true);
							Drawable dr = new BitmapDrawable(circulo_);
							ImageView imgTemp = new ImageView(this);
							imgTemp.SetBackgroundDrawable(dr);
							//imgTemp.SetPadding(Configuration.getWidth(5), 0, Configuration.getWidth(5), 0);
							//imgTemp.SetY(Configuration.getHeight(495));
							//imgTemp.SetX(Configuration.getWidth(640 / 2 - (tamS * tamCirc) / 2) + i * tamCirc + 5);

							listCirculos.Add(imgTemp);
							//layoutList.AddView(imgTemp);
							linearCirculos.AddView(imgTemp);
						}
						layoutList.AddView(linearCirculos);

						ImageAdapterTelerik adapterT = new ImageAdapterTelerik(source);
						adapterT.ctx = this;
						listView.SetAdapter(adapterT);

						slideLayoutManager = new SlideLayoutManager(this);
						listView.SetLayoutManager(slideLayoutManager);

						//slideLayoutManager.TransitionMode = SlideLayoutManager.Transition.SlideOver;

						linearScroll.AddView(layoutList);
					}



					if(!is50Campamentos){
						linearScroll.RemoveView (misFavoritos);
						//Añadimos un espacio en blanco al final-para los epertos era necesario
						LinearLayout linearTmp = new LinearLayout(this);
						linearTmp.Orientation = Orientation.Vertical;
						linearTmp.LayoutParameters = new LinearLayout.LayoutParams(-1, Configuration.getHeight(100));
						linearScroll.AddView(linearTmp);
					}


							scrollPager.VerticalScrollBarEnabled = false;
							if (k == 0) {
								scrollPager.AddView (linearScroll);
								listaScroll.Add (scrollPager);
								indice++;
							}


					//	}
						}

					//}

				} else {
					Console.WriteLine ("ERROR");
				}


			mainLayoutPages.RemoveAllViews ();
			mainLayoutPages.AddView (viewPager);
			mainLayoutPages.SetX (0);
			mainLayoutPages.SetY (0);
			_mainLayout.AddView (mainLayoutPages);

			_publicidadLayout = new LinearLayout (this);
			_publicidadLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (85));
			Drawable drp = new BitmapDrawable (getBitmapFromAsset ("images/footerad.jpg"));
			_publicidadLayout.SetBackgroundDrawable (drp);
			_publicidadLayout.SetY (Configuration.getHeight(1136-85));
			_mainLayout.AddView (_publicidadLayout);
			_publicidadLayout.Click += delegate {
				if (adOpen) {
					hideAd ();
				} else {
					Random rnd = new Random();
					showAd (rnd.Next(adsImagesPath.Count));
				}
			};


			LOViewAdapter adapter = new LOViewAdapter (this, listaScroll);
			viewPager.Adapter = adapter;
			//viewPager.CurrentItem = IndiceSection;
			//viewPager.SetCurrentItem (vm._currentSection, true);
		}
		/*
		public void  OnScrollChanged(VerticalScrollView scrollView, int l, int t, int oldl, int oldt) {

			listFront[0].Imagen.SetY (scrollView.ScrollY / 2);
		//	listFront[0].Imagen.ScaleX =scrollView.ScrollY / 10;
		//	listFront[0].Imagen.ScaleY =scrollView.ScrollY / 10;

		}*/

		public void OnScrollChangedPager(VerticalScrollViewPager scrollView, int l, int t, int oldl, int oldt) {
			var view=(LinearLayout)scrollView.GetChildAt (0);

			if (view.GetChildAt (0).Tag.Equals("indice")) {
				var pagerrr =  (FrontContainerView)view.GetChildAt (0);
				pagerrr.Imagen.SetY (scrollView.ScrollY / 2);	
			}if (view.GetChildAt (0).Tag.Equals("pager")) {
				var pagerrr =  (FrontContainerViewPager)view.GetChildAt (0);
				pagerrr.Imagen.SetY (scrollView.ScrollY / 2);	
			}


			//Console.WriteLine("SCROLLEOOO LOS PAGERRRRRRRRRRRRR "+ scrollView.ScrollY);

		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =this.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;

		}


		public class MyPageChangeListener : Java.Lang.Object, ViewPager.IOnPageChangeListener
		{
			Context _context;
			List<FrontContainerView> listFront;
			public MyPageChangeListener (Context context, List<FrontContainerView> listFront)
			{
				_context = context;	
				this.listFront = listFront;

			}

			#region IOnPageChangeListener implementation
			public void OnPageScrollStateChanged (int p0)
			{
				//Console.WriteLine (p0);
			}

			public void OnPageScrolled (int p0, float p1, int p2)
			{
				//Console.WriteLine ("p0 = " + p0 + " p1 = " + p1 + " p2 = " + p2);
				listFront [p0].Imagen.SetX (p2 / 2);		
			}

			public void OnPageSelected (int position)
			{
				//	Toast.MakeText (_context, "Changed to page " + position, ToastLength.Short).Show ();
			}
			#endregion
		}




		public class MyPageChangeListenerPager : Java.Lang.Object, ViewPager.IOnPageChangeListener
		{
			Context _context;
			List<FrontContainerViewPager> listFront;
			//ScrollViewHorizontal scroll;
			public MyPageChangeListenerPager (Context context, List<FrontContainerViewPager> listFront)
			{
				_context = context;	
				this.listFront = listFront;

			}

			#region IOnPageChangeListener implementation
			public void OnPageScrollStateChanged (int p0)
			{
				//Console.WriteLine (p0);
			}

			public void OnPageScrolled (int p0, float p1, int p2)
			{

				//Console.WriteLine ("p0 = " + p0 + " p1 = " + p1 + " p2 = " + p2);
				listFront [p0].Imagen.SetX (p2 / 2);		
				//if(p0+1<listFront.Count){
				//	listFront [p0 + 1].Imagen.SetX (p2/2);
				//}

			}

			public void OnPageSelected (int position)
			{
				//	Toast.MakeText (_context, "Changed to page " + position, ToastLength.Short).Show ();
			}
			#endregion
		}


		public override void OnBackPressed ()
		{
			if (mainLayoutIndice.Visibility == ViewStates.Visible) {
				base.OnBackPressed ();
			}
			ISLOADED = true;
			mainLayoutIndice.Visibility = ViewStates.Visible;
			mainLayoutPages.Visibility = ViewStates.Invisible;


		}

	}
}