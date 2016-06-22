
using System;
using System.Collections.Generic;
using Android.Content;
using Android.Util;
using Android.Widget;
using Android.Graphics;

using Android.Graphics.Drawables;
using Square.Picasso;
using MLearning.Droid.Views;
using MLearning.Core.ViewModels;
using Android.Text;
using Android.Text.Util;
using Android.App;
using Java.Util.Logging;
using System.Threading;
using MLearning.Core;
using MLearning.Core.Configuration;
using Tasky.Shared;

namespace MLearning.Droid
{
	public class WallView: RelativeLayout
	{
		//Tabla Favoritos Item
		FavoritosItem task = new FavoritosItem();
		string Titulo = "";
		string Descripcion = "";
		int Id_Unidad;
		int Id_section;


 	 
		IList<FavoritosItem> listFavorites;

		public NotasItemListAdapter taskList;
		public IList<TodoItem> tasks;
		Button addTaskButton;
		public ListView taskListView;


		MainViewModel vm;
		public VerticalScrollView _scrollSpace;
		public LinearLayout header;
		public List<UnidadItem> _listUnidades = new List<UnidadItem>();
		ListView  _listViewUnidades;
		public List<ImageIconMap> _listIconMap = new List<ImageIconMap> ();
		public List<LinearLayout> _listIconVerMap = new List<LinearLayout> ();

		public LinearLayout _spaceUnidades;
		public LinearLayout _mapSpace;
		public int lastSelected = -1;
		public List<LinearLayoutLO> _listLinearUnidades = new List<LinearLayoutLO> ();
		public LinearLayout _mainSpace;

		RelativeLayout _mainLayout;
		RelativeLayout _fondo2;
		public TextView _txtCursoN;
		public TextView _txtUnidadN;
		public Bitmap iconMap;
		public Bitmap iconPlay;
		public Bitmap iconInfo;
		public Bitmap iconFavorito;
		public Bitmap iconFavorito_BN;
		public bool isFavoritos;
		public bool isNotas = false;

		private ImageView favorit;

		LinearLayout linearGradiente;

		LinearLayout commentLayout;
		ListView commentList;
		List<CommentDataRow> _dataTemplateItem = new List<CommentDataRow> ();
		LinearLayout infoCursoUnidad;

		LinearLayout _workspace;

		public int currentLOImageIndex = 0;

		//section_1
		RelativeLayout _contentRLayout_S1;
		TextView _txtTitle_S1;
		TextView _txtAuthor_S1;
		ImageView _imAuthor_S1;
		TextView _txtChapter_S1;

		LinearLayout _itemsLayout_S1;
		List<ImageView> _imItem_S1;
		List<TextView> _txtItem_S1;

		public List<ImageLOView> _ListLOImages_S2;


		public List<string> adsImagesPath = new List<string>();
		public LinearLayout selectLayout;
		public LinearLayout _publicidadLayout;
		public LinearLayout _adLayout;
		public bool adOpen = false;

		public bool _isRemoved = false;

		public void setFooterBackground(Drawable background)
		{
			linearGradiente.SetBackgroundDrawable (background);
		}


		private CommentDataRow[] _listItems;
		public CommentDataRow[] ListItems{
			set{_listItems = value;
				for (int i = 0; i < _listItems.Length; i++) {
					_dataTemplateItem.Add (new CommentDataRow (){name = _listItems[i].name, im_profile = _listItems[i].im_profile, date = _listItems[i].date, comment = _listItems[i].comment });
					commentList.Adapter = new CommentAdapter (context, _dataTemplateItem);
					commentList.SetBackgroundColor (Color.White);
					commentList.DividerHeight = 0;
					commentList.Clickable = false;
					commentList.ChoiceMode = ChoiceMode.None;
				}

			}

		}


		string _title;
		public string Title{
			set{_title = value;
				_txtTitle_S1.Text = _title;}
		}

		string _author;
		public string Author{
			set{_author = value;
				_txtAuthor_S1.Text = _author;}
		}

		string _chapter;
		public string Chapter{
			set{_chapter = value;
				_txtChapter_S1.Text = _chapter;}
		}

		Bitmap _imageAuthor;
		public Bitmap ImageAuthor {
			set{_imageAuthor = value;
				_imAuthor_S1.SetImageBitmap (_imageAuthor);
				//_imageAuthor.Recycle ();
				_imageAuthor = null;
			}

		}

		string _info1;
		public string Info1{
			set{_info1 = value;
				_txtInfo1_S3.Text = _info1;}
		}

		string _info2;
		public string Info2{
			set{_info2 = value;
				_txtInfo2_S3.Text = _info2;}
		}
		string _info3;
		public string Info3{
			set{_info3 = value;
				_txtInfo3_S3.Text = _info3;}
		}


		public List<ImageLOView> ListImages{
			set{ _ListLOImages_S2 = value;

				selectLayout = new LinearLayout (context);
				selectLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);
				selectLayout.SetBackgroundColor (Color.ParseColor ("#20000000"));

				_images_S2.RemoveAllViews ();	

				lastSelected = -1;
				for (int i = 0; i < _ListLOImages_S2.Count; i++) {

					_images_S2.AddView (_ListLOImages_S2[i]);
					_ListLOImages_S2 [i].Click += imLoClick;


				}



				if (_ListLOImages_S2.Count == 1) {
					//_mainSpace.RemoveView (_contentScrollView_S2);
					_contentScrollView_S2.LayoutParameters.Height=0;
					_isRemoved = true;
				} else {
					if (_isRemoved) {
						//	_mainSpace.AddView (_contentScrollView_S2);
						_contentScrollView_S2.LayoutParameters.Height=Configuration.getHeight(160);
						_isRemoved = false;
					}
				}



			}

		}

		public LinearLayout getMapSpaceLayout{
			get{ return _mapSpace;}
		}

		public LinearLayout getWorkSpaceLayout{
			get{ return _workspace;}
		}

		public ImageView OpenUnits{
			get{return _imItems_S4 [0]; }
		}

		public ImageView OpenComments{
			get{return _imItems_S4 [1]; }
		}

		public ImageView OpenLO{
			get{return _imItems_S4 [2]; }
		}

		public ImageView OpenChat{
			get{return _imItems_S4 [3]; }
		}

		public ImageView OpenTasks{
			get{return _imItems_S4 [4]; }
		}
		//section_2


		public HorizontalScrollView _contentScrollView_S2;
		LinearLayout _images_S2;


		public void imLoClick(object sender, EventArgs eventArgs)
		{

			var imView = sender as ImageLOView;
			currentLOImageIndex = imView.index;

			if (lastSelected != -1) {
				_ListLOImages_S2 [lastSelected].RemoveView (selectLayout);
			}
			imView.AddView(selectLayout);
			lastSelected = currentLOImageIndex;
			var textFormat = Android.Util.ComplexUnitType.Px;





			var test = new ImageView (context);
			test.DrawingCacheEnabled = true;
			test.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);

			Picasso.With (context).Load (imView.sBackgoundUrl).Resize(Configuration.getWidth(640),Configuration.getWidth(640)).CenterCrop().Into (test);
			_fondo2.SetVerticalGravity (Android.Views.GravityFlags.Start);
			_fondo2.RemoveAllViews();

			infoCursoUnidad.RemoveAllViews ();
			infoCursoUnidad.AddView (_txtCursoN);
			infoCursoUnidad.AddView (_txtUnidadN);

			_fondo2.AddView(test);
			//_txtCursoN.Text = "PROBANDO";
			//_txtUnidadN.Text = "PROBANDO";
			_txtCursoN.SetTextSize (ComplexUnitType.Fraction, Configuration.getHeight(55));
			_txtUnidadN.SetTextSize (ComplexUnitType.Fraction, Configuration.getHeight(50));

			_txtCursoN.SetTextColor (Color.ParseColor("#ffffff"));
			_txtCursoN.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");

			_txtUnidadN.SetTextColor (Color.ParseColor("#ffffff"));
			_txtUnidadN.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");
			_txtCursoN.Gravity = Android.Views.GravityFlags.Right;
			_txtUnidadN.Gravity = Android.Views.GravityFlags.Right;
			//_txtCursoN.TextAlignment = Android.Views.TextAlignment.Gravity;


			_fondo2.AddView (infoCursoUnidad);
			//infoCursoUnidad.SetX (Configuration.getWidth (300));
			infoCursoUnidad.SetY (Configuration.getWidth (420));

			//actualizar titulo, nombreAuthor, capitulo, imAuthor
		}

		//section_3

		LinearLayout _contentLLayout_S3;
		TextView _txtInfo1_S3;
		TextView _txtInfo2_S3;
		TextView _txtInfo3_S3;

		//section_4

		LinearLayout _contentLLayout_S4;
		List<ImageView> _imItems_S4;


		int widthInDp;
		int heightInDp;


		Context context;

		public WallView (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}

		void Initialize ()
		{
			//Sacar la lista de Favoritos
			listFavorites = FavoritosItemManager.GetTasks();
			//-----------------------------
			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);

			adsImagesPath = AddResources.Instance.addList;

			//iconMap = Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/iconmap.png"), Configuration.getWidth (60), Configuration.getWidth (80), true);
			iconPlay = Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/playc.png"), Configuration.getWidth (60), Configuration.getWidth (60), true);
			iconInfo = Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/info.png"), Configuration.getWidth (60), Configuration.getWidth (60), true);
			iconFavorito_BN = Bitmap.CreateScaledBitmap(getBitmapFromAsset("icons/favoritos_BN.png"), Configuration.getWidth(60), Configuration.getWidth(60), true);
			iconFavorito = Bitmap.CreateScaledBitmap(getBitmapFromAsset("icons/favoritos.png"), Configuration.getWidth(60), Configuration.getWidth(60), true);
			ini ();


			this.AddView (_mainLayout);
		}




		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		void showAd(int idAd)
		{
			adOpen = true;
			_adLayout = new LinearLayout (context);
			_adLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (255));
			Drawable dr = new BitmapDrawable (getBitmapFromAsset (adsImagesPath[idAd]));
			_adLayout.SetBackgroundDrawable (dr);
			_adLayout.SetY (Configuration.getHeight(1136-85-255));
			_mainLayout.AddView (_adLayout);

			_adLayout.Click += delegate {
				context.StartActivity(Configuration.getOpenFacebookIntent(context,"fb://page/114091405281757","http://www.hi-tec.com/pe/"));
			};
		}

		void hideAd()
		{
			adOpen = false;
			_mainLayout.RemoveView (_adLayout);
		}


		public void ini(){

			_txtCursoN = new TextView (context);
			_txtCursoN.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_txtUnidadN = new TextView (context);
			_mainLayout = new RelativeLayout (context);

			linearGradiente = new LinearLayout (context);
			linearGradiente.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (310));
			linearGradiente.SetBackgroundResource (Resource.Drawable.gradiente);


			var textFormat = Android.Util.ComplexUnitType.Px;

			_mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);

			_scrollSpace = new VerticalScrollView (context);
			_scrollSpace.LayoutParameters = new VerticalScrollView.LayoutParams (-1, Configuration.getHeight(1015-85));
			_scrollSpace.SetY (Configuration.getHeight (125));
			//_scrollSpace.SetBackgroundColor (Color.ParseColor ("#FF0000"));
			_mainLayout.AddView (_scrollSpace);


			_publicidadLayout = new LinearLayout (context);
			_publicidadLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (85));
			Drawable dr = new BitmapDrawable (getBitmapFromAsset ("images/footerad.jpg"));
			_publicidadLayout.SetBackgroundDrawable (dr);
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




			_mapSpace = new LinearLayout (context);

			_mapSpace.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (1015));
			_mapSpace.SetY(Configuration.getHeight (125));
			_mainLayout.AddView (_mapSpace);


			_mainSpace = new LinearLayout (context);
			_mainSpace.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_mainSpace.Orientation = Orientation.Vertical;

			_scrollSpace.AddView (_mainSpace);

			_fondo2 = new RelativeLayout (context);
			_fondo2.LayoutParameters = new RelativeLayout.LayoutParams (-1, Configuration.getWidth (640));
			_fondo2.SetY (Configuration.getHeight (0));

			Drawable dr1 = new BitmapDrawable (getBitmapFromAsset("icons/fondoselec.png"));
			_fondo2.SetBackgroundDrawable (dr1);
			dr1 = null;
			_mainSpace.AddView (_fondo2);
			

			infoCursoUnidad = new LinearLayout (context);
			infoCursoUnidad.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(250));
			infoCursoUnidad.Orientation = Orientation.Vertical;
			infoCursoUnidad.SetGravity(Android.Views.GravityFlags.Right);
			infoCursoUnidad.SetPadding (Configuration.getWidth(30), Configuration.getWidth (25), Configuration.getWidth(30), Configuration.getWidth (25));
			infoCursoUnidad.SetBackgroundColor (Color.ParseColor ("#40000000"));

			TextView _txtCurso = new TextView (context);
			_txtCurso.Text = "LAS RUTAS";
			_txtCurso.SetY (-100);

			//_mainSpace.AddView (_txtCurso);


			//section1-----------------------------------------------
			_contentRLayout_S1 = new RelativeLayout(context);
			_txtTitle_S1 = new TextView (context);
			_txtAuthor_S1 = new TextView (context);
			_imAuthor_S1 = new ImageView (context);
			_txtChapter_S1 = new TextView (context);

			_itemsLayout_S1 = new LinearLayout (context);//not used
			_imItem_S1 = new List<ImageView> ();
			_txtItem_S1 = new List<TextView>();





			//_mainLayout.AddView (_txtTitle_S1);
			//_mainLayout.AddView (_txtAuthor_S1);
			//_mainLayout.AddView (_imAuthor_S1);

			//_mainLayout.AddView (_txtChapter_S1);

			//_mainSpace.AddView (_txtChapter_S1);






			_contentRLayout_S1.LayoutParameters = new RelativeLayout.LayoutParams (-1, Configuration.getHeight (480));


			LinearLayout _linearTitle = new LinearLayout (context);
			_linearTitle.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_linearTitle.SetGravity (Android.Views.GravityFlags.Center);
			_linearTitle.AddView (_txtTitle_S1);
			_linearTitle.SetY (Configuration.getHeight (60));

			linearGradiente.SetX (0); linearGradiente.SetY (Configuration.getHeight(860));
			//_mainLayout.AddView (linearGradiente);
			//_mainLayout.AddView (_linearTitle);

			//_txtTitle_S1.SetX (Configuration.getWidth (245));_txtTitle_S1.SetY (Configuration.getHeight (60));

			//Bitmap newbm = Configuration.getRoundedShape(Bitmap.CreateScaledBitmap( getBitmapFromAsset("icons/imgautor.png"), Configuration.getWidth(170), Configuration.getWidth(170), true),Configuration.getWidth(170),Configuration.getHeight(170));

			//_imAuthor_S1.SetImageBitmap (newbm);
			//	newbm.Recycle ();
			//newbm = null;

			//_imAuthor_S1.SetX (Configuration.getWidth (240));_imAuthor_S1.SetY (Configuration.getHeight (189));

			LinearLayout _linearAuthor = new LinearLayout (context);
			_linearAuthor.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_linearAuthor.SetGravity (Android.Views.GravityFlags.Center);
			_linearAuthor.AddView (_txtAuthor_S1);
			_linearAuthor.SetY (Configuration.getHeight (378));
			//_mainLayout.AddView (_linearAuthor);

			//_txtAuthor_S1.SetX (Configuration.getWidth (228));_txtAuthor_S1.SetY (Configuration.getHeight (378));

			LinearLayout _linearChapter = new LinearLayout (context);
			_linearChapter.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_linearChapter.SetGravity (Android.Views.GravityFlags.Center);
			//_linearChapter.AddView (_txtChapter_S1);
			_linearChapter.SetY (Configuration.getHeight (502));
			//_mainLayout.AddView (_linearChapter);


			//_txtChapter_S1.SetX (Configuration.getWidth (191));_txtChapter_S1.SetY (Configuration.getHeight (502));






			_txtTitle_S1.Text = "Camino Inca";
			_txtTitle_S1.SetTextColor (Color.White);
			_txtTitle_S1.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			_txtTitle_S1.SetTextSize (textFormat,Configuration.getHeight(30));

			_txtAuthor_S1.Text = "David Spencer";
			_txtAuthor_S1.SetTextColor (Color.White);
			_txtAuthor_S1.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			_txtAuthor_S1.SetTextSize (textFormat,Configuration.getHeight(30));
			//_txtAuthor_S1.SetBackgroundColor (Color.ParseColor ("#60000000"));
			_txtAuthor_S1.SetShadowLayer (50.8f, 0.0f, 0.0f, Color.ParseColor ("#000000"));



			_txtChapter_S1.Text = "FLORA Y FAUNA";
			_txtChapter_S1.SetTextColor (Color.White);
			_txtChapter_S1.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			_txtChapter_S1.SetTextSize (textFormat,Configuration.getHeight(35));


			List<string> item_path = new List<string> ();
			item_path.Add ("icons/icona.png");
			item_path.Add ("icons/iconb.png");
			item_path.Add ("icons/iconc.png");
			item_path.Add ("icons/icond.png");
			item_path.Add ("icons/icone.png");
			item_path.Add ("icons/iconf.png");
			item_path.Add ("icons/icong.png");


			int inixItemIM = Configuration.getWidth (33);
			int crecIM = Configuration.getWidth (90);

			int inixItemTXT = Configuration.getWidth (42);
			int crecTXT = Configuration.getWidth (90);
			int inixLinea = Configuration.getWidth (93);

			for (int i = 0; i < item_path.Count; i++) {

				_imItem_S1.Add(new ImageView(context));
				//_imItem_S1[i].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(item_path[i]), Configuration.getWidth (30), Configuration.getWidth (30), true));
				//_mainLayout.AddView (_imItem_S1 [i]);
				_imItem_S1 [i].SetX (inixItemIM+(i*crecIM));_imItem_S1 [i].SetY (Configuration.getHeight(602));


				if (i != item_path.Count - 1) {
					ImageView linea = new ImageView (context);
					//linea.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/lineatareas.png"), 1, Configuration.getHeight (68), true));
					//_mainLayout.AddView (linea);
					linea.SetX (inixLinea + (i * crecIM));
					linea.SetY (Configuration.getHeight (605));
					linea = null;
				}



				_txtItem_S1.Add (new TextView (context));
				_txtItem_S1 [i].Text = "0";
				_txtItem_S1 [i].SetTextColor (Color.ParseColor ("#2E9AFE"));
				_txtItem_S1[i].Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
				_txtItem_S1[i].SetTextSize (textFormat,Configuration.getHeight(30));
				//_mainLayout.AddView (_txtItem_S1 [i]);
				_txtItem_S1 [i].SetX (inixItemTXT+(i*crecTXT));_txtItem_S1 [i].SetY (Configuration.getHeight(640));
				_imItem_S1 [i] = null;

			}
			_imItem_S1 = null;


			//-------------------------------------------------------


			//section2------------------------------------------------

			_contentScrollView_S2 = new HorizontalScrollView (context);
			_contentScrollView_S2.LayoutParameters = new HorizontalScrollView.LayoutParams (-1, Configuration.getWidth(160));
			_contentScrollView_S2.HorizontalScrollBarEnabled = false;

			_images_S2 = new LinearLayout (context);
			_images_S2.Orientation = Orientation.Horizontal;
			_images_S2.LayoutParameters = new LinearLayout.LayoutParams(-2,-1);

			_contentScrollView_S2.SetX (0);


			_contentScrollView_S2.AddView (_images_S2);

			//----------------------------------------------------------

			//section3------------------------------------------------

			_contentLLayout_S3 = new LinearLayout (context);
			_contentLLayout_S3.Orientation = Orientation.Vertical;
			_contentLLayout_S3.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (160));
			_contentLLayout_S3.SetX (0);_contentLLayout_S3.SetY (Configuration.getHeight(875));
			_contentLLayout_S3.SetGravity (Android.Views.GravityFlags.Center);



			_txtInfo1_S3 = new TextView (context);
			_txtInfo2_S3 = new TextView (context);
			_txtInfo3_S3 = new TextView (context);

			_txtInfo1_S3.Text = "Duración: 05 dias / 04 noches ";
			_txtInfo2_S3.Text = "Distancia: 65km";
			_txtInfo3_S3.Text = "Punto mas elevado: 4,6386 msnm (Salkantay)";

			_txtInfo1_S3.Gravity = Android.Views.GravityFlags.CenterHorizontal;
			_txtInfo2_S3.Gravity = Android.Views.GravityFlags.CenterHorizontal;
			_txtInfo3_S3.Gravity = Android.Views.GravityFlags.CenterHorizontal;


			_txtInfo1_S3.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			_txtInfo2_S3.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			_txtInfo3_S3.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");

			_txtInfo1_S3.SetTextSize (textFormat,Configuration.getHeight(30));
			_txtInfo2_S3.SetTextSize (textFormat,Configuration.getHeight(30));
			_txtInfo3_S3.SetTextSize (textFormat,Configuration.getHeight(30));

			_txtInfo1_S3.SetTextColor (Color.White);
			_txtInfo2_S3.SetTextColor (Color.White);
			_txtInfo3_S3.SetTextColor (Color.White);


			_contentLLayout_S3.AddView (_txtInfo1_S3);
			_contentLLayout_S3.AddView (_txtInfo2_S3);
			_contentLLayout_S3.AddView (_txtInfo3_S3);

			//Drawable dr3 = new BitmapDrawable (getBitmapFromAsset("icons/fondonotif.png"));
			//_contentLLayout_S3.SetBackgroundDrawable(dr3);
			//_contentLLayout_S3.SetBackgroundColor(Color.ParseColor("#80000000"));
			//_mainLayout.AddView (_contentLLayout_S3);

			//_mainLayout.AddView (_contentScrollView_S2);
			_mainSpace.AddView (_contentScrollView_S2);
			//----------------------------------------------------------
			/*
			_listUnidades.Add(new UnidadItem{ Title = "Dia 1", Description = "Piscacucho-Wayllabamba" });
			_listUnidades.Add(new UnidadItem{ Title = "Dia 2", Description = "Wayllabamba-Pacaymayo" });
			_listUnidades.Add(new UnidadItem{ Title = "Dia 3", Description = "Pacaymayo-Wiñay Wayna" });
			_listUnidades.Add(new UnidadItem{ Title = "Dia 4", Description = "WIñay Wayna-Machu PIcchu"});
		*/

			/*
			_listViewUnidades = new ListView(context);
			_listViewUnidades.Adapter = new UnidadAdapter (context, _listUnidades);

			_mainSpace.AddView (_listViewUnidades);
			*/

			_spaceUnidades = new LinearLayout (context);
			_spaceUnidades.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_spaceUnidades.Orientation = Orientation.Vertical;
			_spaceUnidades.SetBackgroundColor (Color.White);
			_mainSpace.AddView (_spaceUnidades);

			//section4------------------------------------------------
			_imItems_S4 = new List<ImageView>();


			List<string> botton_icon_path = new List<string> ();
			botton_icon_path.Add ("icons/btnhome.png");
			botton_icon_path.Add ("icons/btncomentariosazul.png");
			botton_icon_path.Add ("icons/btncontenido.png");
			botton_icon_path.Add ("icons/btnchatazul.png");
			botton_icon_path.Add ("icons/btnmap.png");

			_imItems_S4.Add (new ImageView (context));
			//_imItems_S4[0].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(botton_icon_path[0]), Configuration.getWidth (40), Configuration.getWidth (40), true));
			//_mainLayout.AddView (_imItems_S4[0]);
			_imItems_S4[0].SetX (Configuration.getWidth(58));_imItems_S4[0].SetY (Configuration.getHeight(1069));
			_imItems_S4 [0].Visibility = Android.Views.ViewStates.Invisible;

			_imItems_S4.Add (new ImageView (context));
			//_imItems_S4[1].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(botton_icon_path[1]), Configuration.getWidth (78), Configuration.getWidth (55), true));
			//_mainLayout.AddView (_imItems_S4[1]);
			_imItems_S4[1].SetX (Configuration.getWidth(169));_imItems_S4[1].SetY (Configuration.getHeight(1069));
			_imItems_S4 [1].Visibility = Android.Views.ViewStates.Invisible;




			_imItems_S4.Add (new ImageView (context));
			//_imItems_S4[2].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(botton_icon_path[2]), Configuration.getWidth (80), Configuration.getWidth (80), true));
			//_mainLayout.AddView (_imItems_S4 [2]);
			_imItems_S4[2].SetX (Configuration.getWidth(297));_imItems_S4[2].SetY (Configuration.getHeight(1050));
			_imItems_S4 [2].Visibility = Android.Views.ViewStates.Invisible;



			_imItems_S4.Add (new ImageView (context));
			//_imItems_S4[3].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(botton_icon_path[3]), Configuration.getWidth (30), Configuration.getWidth (51), true));
			//_mainLayout.AddView (_imItems_S4[3]);
			_imItems_S4[3].SetX (Configuration.getWidth(421));_imItems_S4[3].SetY (Configuration.getHeight(1069));
			_imItems_S4 [3].Visibility = Android.Views.ViewStates.Invisible;


			_imItems_S4.Add (new ImageView (context));
			//_imItems_S4[4].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(botton_icon_path[4]), Configuration.getWidth (30), Configuration.getWidth (40), true));
			//_mainLayout.AddView (_imItems_S4 [4]);
			_imItems_S4[4].SetX (Configuration.getWidth(540));_imItems_S4[4].SetY (Configuration.getHeight(1069));
			_imItems_S4 [4].Visibility = Android.Views.ViewStates.Invisible;

			//----------------------------------------------------------

			//Drawable dr = new BitmapDrawable (getBitmapFromAsset("images/header1.png"));
			header = new LinearLayout(context);
			header.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(125));
			header.Orientation = Orientation.Vertical;

			//header.SetBackgroundDrawable (dr);


			//_mainLayout.SetBackgroundDrawable (dr);
			_mainLayout.AddView(header);

			//dr = null;






			_workspace = new LinearLayout (context);
			_workspace.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);
			//_workspace.SetBackgroundColor (Color.ParseColor ("#ffffff"));
			//_workspace.SetY (Configuration.getHeight (110));

			_mainLayout.AddView (_workspace);
			//_mainLayout.SetBackgroundColor (Color.ParseColor ("#ffffff"));
			//_workspace.AddView (_foro);
			//_workspace.Visibility = Android.Views.ViewStates.Invisible;


		}


		public void initUnidades(int indexCurso, int indexUnidad)
		{
			var textFormat = Android.Util.ComplexUnitType.Px;
			_spaceUnidades.RemoveAllViews ();
			_listLinearUnidades.Clear ();
			_listIconMap.Clear ();
			_listIconVerMap.Clear ();
			int numUnidades = _listUnidades.Count;
			_mainSpace.SetY (Configuration.getHeight (0));

			if (isNotas)
			{
				_mainSpace.RemoveAllViews();
				taskListView = new ListView(context);
				taskListView.LayoutParameters = new LinearLayout.LayoutParams(-1, Configuration.getHeight(850));

				addTaskButton = new Button(context);
				addTaskButton.Text = "Añadir Nota";
				addTaskButton.LayoutParameters = new LinearLayout.LayoutParams(-1, -2);

				_mainSpace.SetY (Configuration.getHeight (20));

				//addTaskButton.SetY (Configuration.getHeight (130));
				//_mainLayout.AddView(addTaskButton);
				_mainSpace.AddView(addTaskButton);
				_mainSpace.AddView(taskListView);

				if (addTaskButton != null)
				{
					addTaskButton.Click += (sender, e) =>
					{
						//layoutSave.Visibility = Android.Views.ViewStates.Visible;
						context.StartActivity(typeof(NotasItemScreen));
					};
				}

				// wire up task click handler
				if (taskListView != null)
				{
					taskListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
					{
						var taskDetails = new Intent(context, typeof(NotasItemScreen));
						taskDetails.PutExtra("TaskID", tasks[e.Position].ID);
						context.StartActivity(taskDetails);
					};
				}
				return;
			}

			for (int i = 0; i < numUnidades; i++) 
			{
				LinearLayoutLO linearUnidad = new LinearLayoutLO (context);
				linearUnidad.content = _listUnidades[i];
				linearUnidad.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
				linearUnidad.Orientation = Orientation.Vertical;
				linearUnidad.SetGravity (Android.Views.GravityFlags.CenterVertical);
				//linearUnidad.SetBackgroundColor (Color.Blue);
				linearUnidad.index = i;
				linearUnidad.SetPadding (Configuration.getWidth(100), Configuration.getWidth (25),0, Configuration.getWidth (25));
				//linearUnidad.SetX (100);



				TextView titleUnidad = new TextView(context);
				titleUnidad.SetTextSize (textFormat,Configuration.getHeight(42));
				titleUnidad.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (440), -2);


				/*if (indexCurso == 2) {
					if (indexUnidad == 3) {
						linearUnidad.Orientation = Orientation.Horizontal;
						ImageIconMap icon = new ImageIconMap (context);
						icon.index = i;
						icon.SetImageBitmap(iconPlay);
						icon.SetX (Configuration.getWidth (60));
						linearUnidad.AddView (icon);
						_listIconMap.Add (icon);
					}
				} */

				if (indexCurso == 3) {
					titleUnidad.SetTextSize (textFormat,Configuration.getHeight(55));
				}

				RelativeLayout linearContenido = new RelativeLayout (context);
				linearContenido.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
				linearContenido.SetGravity (Android.Views.GravityFlags.Center);



				//TextView titleUnidad = new TextView(context);
				//titleUnidad.Text = _listUnidades [i].Title;
				titleUnidad.TextFormatted = Html.FromHtml (_listUnidades [i].Title);
				titleUnidad.SetTextColor(Color.ParseColor (Configuration.ListaColores [i % 6]));
				titleUnidad.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");


				TextView descriptionUnidad = new TextView(context);
				descriptionUnidad.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (440), -2);
				//descriptionUnidad.Text = _listUnidades [i].Description;

				descriptionUnidad.TextFormatted = Html.FromHtml (_listUnidades [i].Description);
				//descriptionUnidad.Text = _listUnidades [i].Description;
				descriptionUnidad.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");
				descriptionUnidad.SetTextSize (textFormat,Configuration.getHeight(28));
				Linkify.AddLinks (descriptionUnidad, MatchOptions.All);
				//descriptionUnidad.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
				//descriptionUnidad.LinksClickable = true;

				//descriptionUnidad.SetTextIsSelectable (true);

				LinearLayout linearContenidoIn = new LinearLayout (context);
				linearContenidoIn.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
				linearContenidoIn.Orientation = Orientation.Vertical;
				//linearContenidoIn.SetGravity (Android.Views.GravityFlags.Center);



				linearContenidoIn.AddView (titleUnidad);
				linearContenidoIn.AddView (descriptionUnidad);

				linearContenido.AddView (linearContenidoIn);

				/*if (indexCurso == 2) {
					linearContenidoIn.RemoveView (descriptionUnidad);
					ImageView imgUnidad = new ImageView (context);
					Picasso.With (context).Load (_listUnidades[i].ImageUrl).Resize(Configuration.getWidth(440),Configuration.getHeight(440)).Placeholder(context.Resources.GetDrawable (Resource.Drawable.progress_animation)).CenterInside().Into (imgUnidad);
					linearContenidoIn.AddView (imgUnidad);
					linearContenidoIn.SetGravity (Android.Views.GravityFlags.Center);
					linearUnidad.SetPadding (0, Configuration.getWidth (25),0, Configuration.getWidth (25));

				} */

				linearUnidad.AddView (linearContenido);

				if (indexCurso == 3) {
					if (indexUnidad != 3) {

						ImageView info = new ImageView (context);
						info.Tag = i;
						info.SetImageBitmap (iconInfo);
						info.SetX (Configuration.getWidth(450));
						info.SetY (Configuration.getHeight (10));

						ImageView favorit_ = new ImageView(context);
						favorit_.Tag = i;
						favorit_.SetX(Configuration.getWidth(450));
						favorit_.SetY(Configuration.getHeight(150));
						favorit_.Click += delegate {
							funcFavoritos(favorit_);
						};
						//Colocando icono de Favoritos
						if(!isFavoritos){
							
							int id_auto = 0;
							if (isListFavorites(_listUnidades[i].Id)!=-1)
							{
								favorit_.SetImageBitmap(iconFavorito);
							}
							else
							{
								favorit_.SetImageBitmap(iconFavorito_BN);
							}


							linearContenido.AddView (favorit_);
						}
						bool removido = true;
						if(isFavoritos){
							/*_mainSpace.RemoveView(_contentScrollView_S2);
							_mainSpace.RemoveView (_fondo2);*/
							_mainSpace.RemoveAllViews ();
							favorit_.SetImageBitmap(iconFavorito);
							linearContenido.AddView (favorit_);
							_mainSpace.AddView (_spaceUnidades);
							removido = false;
						}
						if(removido){
							try{
								_mainSpace.RemoveAllViews ();
								_mainSpace.AddView (_fondo2);
								_mainSpace.AddView (_contentScrollView_S2);
								_mainSpace.AddView (_spaceUnidades);
							}catch(Exception e){
								//ya existe esos hijos
							}
						}


						linearContenido.AddView (info);

					} else {
						titleUnidad.SetTextSize (textFormat,Configuration.getHeight(55));
					}

				}

				if (indexCurso == 1 && indexUnidad==7) {
					linearContenidoIn.RemoveView (titleUnidad);
					linearContenidoIn.RemoveView (descriptionUnidad);
					//linearContenidoIn.LayoutParameters = new LinearLayout.LayoutParams (-2, -2);
					linearContenidoIn.SetX(Configuration.getWidth (0));
					ImageView imgUnidad = new ImageView (context);
					Picasso.With (context).Load (_listUnidades[i].ImageUrl).Resize(Configuration.getWidth(640),Configuration.getHeight(2362)).Placeholder(context.Resources.GetDrawable (Resource.Drawable.progress_animation)).CenterInside().Into (imgUnidad);
					linearContenidoIn.AddView (imgUnidad);
					linearUnidad.SetPadding (0, 0, 0, 0);
					linearUnidad.SetX(Configuration.getWidth (0));
				}



				_listLinearUnidades.Add (linearUnidad);
				LinearLayout separationLinear = new LinearLayout (context);
				separationLinear.LayoutParameters = new LinearLayout.LayoutParams (-1, 5);
				separationLinear.SetBackgroundColor (Color.ParseColor ("#D8D8D8"));
				separationLinear.Orientation = Orientation.Horizontal;


				//linearUnidad.AddView (separationLinear);
				_spaceUnidades.AddView (linearUnidad);
				_spaceUnidades.AddView (separationLinear);
			}
		}

		public void funcFavoritos(ImageView fav)
		{
			AlertDialog.Builder popupBuilder = new AlertDialog.Builder(context);
			popupBuilder.SetTitle("Mis Favoritos");
			popupBuilder.SetCancelable(false);
			popupBuilder.SetMessage("Se añadio a mis favoritos!");
			popupBuilder.SetNeutralButton("Volver", delegate {  });
			//popupBuilder.Show();


			int id = (int)fav.Tag;
			/*Marcadores prefs = Constants.GetMarkers(Constants.MarkersFileName);
			prefs.PutString(Constants.MarkerTitleKey, _listUnidades[id].Title);
			prefs.PutString(Constants.MarkerInfoKey,_listUnidades[id].Description);
			prefs.PutString(Constants.MarkerIdKey, String.Format("{0}",_listUnidades[id].Id));

			prefs.Commit();*/
			//------------------------Utilizando TodList para la Tabla Favoritos ------------------------------------------------------------------
			//------------------------Utilizando TodList para la Tabla Favoritos ------------------------------------------------------------------
			//Sacar la lista de Favoritos
			listFavorites.Clear();
			listFavorites = FavoritosItemManager.GetTasks();
			//-----------------------------
			int id_auto = isListFavorites(_listUnidades[id].Id);
			if (id_auto==-1)
			{
				Titulo = _listUnidades[id].Title;
				Descripcion = _listUnidades[id].Description;
				Id_Unidad = _listUnidades[id].Id;

				fav.SetImageBitmap(iconFavorito);
				SaveFav();
			}
			else
			{
				FavoritosItemManager.DeleteTask(id_auto);
				popupBuilder.SetMessage("Se elimino de Favoritos!");
				fav.SetImageBitmap(iconFavorito_BN);
			}
			//Dialog------------------------------------------
			Dialog dial = popupBuilder.Create ();
			dial.Show ();
			new Thread(() =>
				{
					Thread.Sleep(1000);
					dial.Dismiss();

				}).Start();




			//linearContenido.RemoveView (favorit);


		}

		//Trabajar con la BD - Tabla Favoritos
		void SaveFav()
		{
			task.Titulo = Titulo;
			task.Descripcion = Descripcion;
			task.Id_unidad = Id_Unidad;

			task.Unit_Index = Id_Unidad;
			task.Section_Index = Id_section;

			FavoritosItemManager.SaveTask(task);
		}

		//Verificar si esta en la lista de favoritos
		public int isListFavorites(int _id)
		{
			int tam = listFavorites.Count;
			for (int i = 0; i < tam; i++)
			{
				if (listFavorites[i].Id_unidad == _id)
				{
					return listFavorites [i].ID;;
				}
			}
			return -1;
		}

	}
}

