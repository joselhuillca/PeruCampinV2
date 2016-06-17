using System;
using System.Collections.Generic;

namespace MLearning.Droid
{
	public class AddResources
	{

		public  List<String> addList;

		public  List<String> bannerList;


		private AddResources (){
			addList = new List<string>();
			bannerList = new List<string>();

			addList.Add ("images/ad1.jpg");
			addList.Add ("images/ad2.jpg"); 
			addList.Add ("images/ad3.jpg"); 
			addList.Add ("images/ad4.jpg"); 
			addList.Add ("images/ad5.jpg");
			addList.Add ("images/ad6.jpg"); 
			//addList.Add ("images/ad7.jpg"); 
			//addList.Add ("images/ad8.jpg"); 

			bannerList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/banner-01.png");
			bannerList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/banner-02.png");
			bannerList.Add ("https://dl.dropboxusercontent.com/u/8925441/banners/banner-03.png");
		}


		private static AddResources instance;
		public static AddResources Instance
		{
			get 
			{
				if (instance == null)
				{
					instance = new AddResources();
				}
				return instance;
			}
		}

	}
}

