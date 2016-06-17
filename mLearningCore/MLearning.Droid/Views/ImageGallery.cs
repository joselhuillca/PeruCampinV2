using System;
using System.Collections.Generic;

namespace MLearning.Droid
{
	public class ImageGallery
	{
		public List<string> imageItem{ get; set;}
		
		public ImageGallery ()
		{
			imageItem = new List<string>();
		}



		public void new_item(string name_image)
		{
			imageItem.Add(name_image);
		}


	}
}

