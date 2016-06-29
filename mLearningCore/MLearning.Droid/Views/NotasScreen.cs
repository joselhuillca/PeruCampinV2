using System;
using Android.App;
using Android.Widget;
using System.Collections.Generic;
using Android.OS;
using Tasky.Shared;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace MLearning.Droid
{
	/// <summary>
	/// Main ListView screen displays a list of tasks, plus an [Add] button
	/// </summary>
	[Activity (Label = "Tasky2")/*,  
		Icon="@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		ScreenOrientation = ScreenOrientation.Portrait)*/]
	public class NotasScreen : Activity 
	{
		NotasItemListAdapter taskList;
		IList<TodoItem> tasks;
		Button addTaskButton;
		ListView taskListView;
		LinearLayout mainLayout;
		LinearLayout header;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// set our layout to be the home screen
			SetContentView(Resource.Layout.NotasScreen);

			//Find our controls
			mainLayout = FindViewById<LinearLayout>(Resource.Id.LayoutAllNotas);
			taskListView = new ListView(this);
			taskListView.LayoutParameters = new LinearLayout.LayoutParams (-1,-1);
			//addTaskButton = FindViewById<Button> (Resource.Id.AddButton);

			// wire up add task button handler
			/*if(addTaskButton != null) {
				addTaskButton.Click += (sender, e) => {
					StartActivity(typeof(NotasItemScreen));
				};
			}*/

			// wire up task click handler
			if(taskListView != null) {
				taskListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
					var taskDetails = new Intent (this, typeof (NotasItemScreen));
					taskDetails.PutExtra ("TaskID", tasks[e.Position].ID);
					StartActivity (taskDetails);
					Finish();
				};
			}

			Drawable dr = new BitmapDrawable (getBitmapFromAsset("images/header5.png"));
			header = new LinearLayout(this);
			header.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(125));
			header.Orientation = Orientation.Vertical;

			header.SetBackgroundDrawable (dr);


			mainLayout.AddView(header);
			mainLayout.AddView (taskListView);
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			tasks = TodoItemManager.GetTasks();

			// create our adapter
			taskList = new NotasItemListAdapter(this, tasks);

			//Hook up our adapter to our ListView
			taskListView.Adapter = taskList;
		}

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = this.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}
	}
}

