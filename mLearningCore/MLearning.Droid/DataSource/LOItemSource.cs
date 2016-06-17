using System;
using System.ComponentModel;
using Android.Graphics;

namespace MLearning.Droid
{
	public class LOItemSource : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string _text;

		public string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Text"));
			}
		}


		private string _imageurl;

		public string ImageUrl
		{
			get { return _imageurl; }
			set
			{
				_imageurl = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ImageUrl"));
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



	}
}

