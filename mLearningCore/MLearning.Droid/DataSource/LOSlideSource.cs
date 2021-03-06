﻿using System;
using System.ComponentModel;
using Android.Graphics;
using System.Collections.ObjectModel;
using MLearning.Droid;
using Android.Views;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Collections;
using Android.Util;
using Android.Text;

namespace DataSource
{
	public class LOSlideSource : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		Context context;
		public String title_page;
		public ImagenCamp imgCamp;
		public bool tieneFondoTxt = false;
		private String colorContent = "#616161";

		public LOSlideSource(Context context){
			this.context = context;
		}

		private int _type;

		public int Type
		{
			get { return _type; }
			set
			{
				_type = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Type"));
			}
		}

		/*
		private LOSlideStyle _style;

		public LOSlideStyle Style
		{
			get { return _style; }
			set { _style = value; }
		}*/

		private Color _color;
		public Color ColorTema
		{
			get { return _color; }
			set { _color = value; }
		}


		private string _colorS;
		public string ColorS{
			get{return _colorS; }
			set{_colorS = value; }
		}

		private string _title;

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Title"));
			}
		}
		// ---------------------------------------------------------------------------------------------------------------------------------

		// ---------------------------------------------------------------------------------------------------------------------------------
		private string _author;

		public string Author
		{
			get { return _author; }
			set
			{
				_author = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Author"));
			}
		}


		private string _paragraph;

		public string Paragraph
		{
			get { return _paragraph; }
			set
			{
				_paragraph = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Paragraph"));
			}
		}

		private Bitmap _image;

		public Bitmap Image
		{
			get { return _image; }
			set
			{
				_image = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Image"));
			}
		}


		private byte[] _imagebytes;

		public byte[] ImageBytes
		{
			get { return _imagebytes; }
			set
			{
				_imagebytes = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ImageBytes"));
			}
		}



		private string _imageurl;

		public string ImageUrl
		{
			get { return _imageurl; }
			set { _imageurl = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ImageUrl"));
			}
		}


		private string _videourl;

		public string VideoUrl
		{
			get { return _videourl; }
			set
			{
				_videourl = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("VideoUrl"));
			}
		}


		private ObservableCollection<LOItemSource> _itemize;

		public ObservableCollection<LOItemSource> Itemize
		{
			get { return _itemize; }
			set
			{
				_itemize = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Itemize"));
			}
		}

		public List<string> parseContent (string content)
		{
			List<string> elements = new List<string>();

			string[] words = content.Split (' ', '\t');

			if (words [0] == "@") {
				elements.Add (words[0]);//sign to parse
				elements.Add (words[1]);//color backgound
				elements.Add (words[2]);//color text
			//	elements.Add (words[3]);//content

				if (words.Length >= 3) {
					_title = "";
					for (int i = 3; i < words.Length; i++) {
						_title += words [i] + " ";
					}
				}

			}
			return elements;
		}


		public RelativeLayout getViewSlide(){

			if (_type == 1) { 				Template1 plantilla = new Template1 (context);  				plantilla.Author = _author;  				if (_title == null) { _title = ""; }  				if (!_title.Equals("")) 				{ 					List<string> elements = parseContent(_title); 					//Console.WriteLine (String.Format("Holaaaa {0}",elements.Count));   					if (elements.Count != 0 && elements[0] == "@") 					{//Console.WriteLine (elements.ToString());   						if (elements[2].Equals("#NONE")) 						{ 							plantilla.ColorTexto = elements[1];
							plantilla.ColorContent = colorContent; 						} 						else { 							plantilla.ColorTexto = _colorS;
							plantilla.ColorContent = colorContent; 						} 					} 				 				}else {
					plantilla.ColorContent = colorContent;
				}

				plantilla.Title = _title;  				plantilla.ImageUrl = _imageurl;//<----------HUILLCA 				plantilla.Contenido = eraseLastBR(_paragraph);;

				//Añdimos las imagenes a un array para dibujarlas luego
				imgCamp = new ImagenCamp(); 				imgCamp.Descripcion = plantilla.Contenido; 				imgCamp.image = plantilla.ImageUrl;  				//Console.WriteLine ("CREA PLANTILLAAAAAAAAA  111111"); 				return plantilla;  			}
			if (_type == 2) { 				Template2 plantilla = new Template2 (context);  				if (_title == null) 					_title = ""; 				List<string> elements = parseContent (_title); 				//Console.WriteLine (String.Format("Holaaaa {0}",elements.Count));   				if (elements.Count != 0 && elements [0] == "@") {//Console.WriteLine (elements.ToString()); 					  					if (!elements[2].Equals("#NONE")) 					{ 						plantilla.ColorDescription = elements[2]; 						plantilla.ColorTitle = elements[2]; 						plantilla.ColorBackgroundTemplate = elements[1]; 					} 					else {  						plantilla.ColorTitle = elements[1];
						plantilla.ColorDescription = colorContent; 					}  				}  else { 					plantilla.ColorTexto = _colorS;
					plantilla.ColorDescription = colorContent; 				}   				plantilla.Title = _title; 				plantilla.Contenido = eraseLastBR(_paragraph);

				//plantilla.ColorBackgroundTemplate = "#000000";  				/*Datos báicos*/ 				if(_title.Equals("Datos básicos ") || _title.Equals("Data ")){ 					Configuration.colorGlobal = elements[1];

					//plantilla.ColorBackgroundTemplate = elements[1];//Si el titulo esta vacio quitarlo en LoView
 					string pathImg = "mapas/" + replaceForImages (title_page) + ".png"; 					plantilla.Image = getBitmapFromAsset(pathImg);
					//Console.WriteLine (pathImg);
					//plantilla.contenLayout.LayoutParameters = new LinearLayout.LayoutParams(Configuration.getWidth(370), 200);
					//plantilla.contenLayout.SetBackgroundColor(Color.Aqua);

					//Para poder cambiar las dimensiones del contenLayout 					LinearLayout contenLayout = new LinearLayout(context); 					contenLayout.LayoutParameters = new LinearLayout.LayoutParams(Configuration.getWidth(390), -2); 					contenLayout.Orientation = Orientation.Vertical; 					contenLayout.SetMinimumHeight(Configuration.getHeight(238)); 					//contenLayout.SetBackgroundColor(Color.Aqua);  					TextView titleHeader = new TextView(context); 					TextView content = new TextView(context); 					titleHeader.SetTextSize(ComplexUnitType.Fraction, Configuration.getHeight(38)); 					content.SetTextSize(ComplexUnitType.Fraction, Configuration.getHeight(32));
					content.SetTextColor(Color.ParseColor(colorContent));  					plantilla.Title = Configuration.quitarErrorTildes(plantilla.Title); 					titleHeader.TextFormatted = Html.FromHtml(plantilla.Title);  					plantilla.Contenido = Configuration.quitarErrorTildes(plantilla.Contenido); 					content.TextFormatted = Html.FromHtml(plantilla.Contenido);

					titleHeader.SetTextColor(Color.ParseColor(plantilla.ColorTitle));
					//content.SetTextColor(Color.ParseColor(plantilla.ColorDescription));  					contenLayout.AddView(titleHeader); 					contenLayout.AddView(content);  					plantilla.contenLayout.RemoveAllViews(); 					plantilla.contenLayout.AddView(contenLayout);  				}

				if (_title.Equals(""))
				{
					plantilla.contenLayout.RemoveView(plantilla.titleHeader);
				}  				return plantilla;  			}
			if (_type == 3) {
				Template3 plantilla = new Template3 (context);
				plantilla.Title = _title;
				string [] lista = new string[_itemize.Count];
				for (int i = 0; i < _itemize.Count; i++) {
					lista[i]=_itemize[i].Text;
				}
				plantilla.ListItems = lista;
				return plantilla;
			}
			if (_type == 4) {
				Template4 plantilla = new Template4 (context);
				return plantilla;
				//Console.WriteLine ("CREA PLANTILLAAAAAAAAA  4444444444");
			}
			if (_type == 5) {
				PhraseView plantilla = new PhraseView (context);
				plantilla.Phrase = _paragraph;
				return plantilla;

			}
			if (_type == 6) {
				CustomerImageView plantilla = new CustomerImageView (context);
				plantilla.Title = _title;
				plantilla.Description = _paragraph;
				plantilla.Imagen = _imageurl;//BitmapFactory.DecodeByteArray (_imagebytes, 0, _imagebytes.Length);
				plantilla.ColorTexto = _colorS;
				return plantilla;
			}

			if (_type == 7) {
				CustomerVideoView plantilla = new CustomerVideoView (context);
				plantilla.Title = _title;
				plantilla.Imagen = _imageurl;
				plantilla.ImagenPlay = "images/playa.png";
				return plantilla;
			}
			return null;
		}

		public Bitmap getBitmapFromAsset( String filePath) {
			if (filePath.Contains("Playa_Colores.png"))
				filePath = "mapas/Playa_Colores_El_Nuro.png";
			
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}


		public string replaceForImages(string urlpathImage){
			urlpathImage = urlpathImage.Replace (' ', '_'); 
			urlpathImage = urlpathImage.Replace ('-', '_'); 

			var normalizedString = urlpathImage.Normalize(NormalizationForm.FormD);
			var stringBuilder = new StringBuilder();

			foreach (var c in normalizedString)
			{
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}

		public string eraseLastBR(string content){
			int n = content.Length;
			if(n>=8){
				string test="";
				for(int i=n-8;i<n;i++){
					test+= content[i];
				}Console.WriteLine (test);

				if (test.Equals ("<br></p>")) {
					content = content.Remove (n - 8,test.Length);
					Console.WriteLine ("SE ELIMINO BR P");
				} else {
					Console.WriteLine ("NO HAY BR P");
				}
				string  p = "<p>";
				content = content.Remove (0,p.Length);
			}else{
				Console.WriteLine ("MUY POCO CONTENIDO");
			}

			return content;
		}

	}
}

