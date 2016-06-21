using System;
using Android.App;
using Android.Content.PM;
using System.Collections.Generic;
using Android.Widget;
using Tasky.Shared;
using Android.OS;

namespace MLearning.Droid
{
	[Activity (Label = "Tasky",  
		Icon="@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		ScreenOrientation = ScreenOrientation.Portrait)]
	public class FavoritosScreen: Activity 
	{
		FavoritosItemListAdapter taskList;
		IList<FavoritosItem> tasks;
		ListView taskListView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// set our layout to be the home screen
			SetContentView(Resource.Layout.FavoritoScreen);

			//Find our controls
			taskListView = FindViewById<ListView> (Resource.Id.FavoritosList);


			// wire up task click handler
			/*if(taskListView != null) {
				taskListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
					var taskDetails = new Intent (this, typeof (NotasItemScreen));
					taskDetails.PutExtra ("TaskID", tasks[e.Position].ID);
					StartActivity (taskDetails);
				};
			}*/
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			tasks = FavoritosItemManager.GetTasks();

			// create our adapter
			taskList = new FavoritosItemListAdapter(this, tasks);

			//Hook up our adapter to our ListView
			taskListView.Adapter = taskList;
			taskListView.ItemClick += listView_ItemClick;

		}

		void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			//Get our item from the list adapter
			var item = this.taskList[ e.Position ];

			//Make a toast with the item name just to show it was clicked
			Toast.MakeText(this, item.Titulo + " Clicked!", ToastLength.Short).Show();


		}
	}
}

