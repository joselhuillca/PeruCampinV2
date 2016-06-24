using System;
using Tasky.Shared;
using Android.Widget;
using Android.App;
using Android.OS;

namespace MLearning.Droid
{
	/// <summary>
	/// View/edit a Task
	/// </summary>
	[Activity (Label = "Tasky")]			
	public class NotasItemScreen : Activity 
	{
		TodoItem task = new TodoItem();
		Button cancelDeleteButton;
		EditText notesTextEdit;
		EditText nameTextEdit;
		Button saveButton;
		CheckBox doneCheckbox;
		int Id_page = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			int taskID = Intent.GetIntExtra("TaskID", 0);
			if(taskID > 0) {

				task = TodoItemManager.GetTask(taskID);
				 
			}
			else {

				Id_page = Intent.GetIntExtra("PageID", 0);

				if (Id_page > 0)
				{
					var ret = TodoItemManager.GetTasks();
					foreach (var item in ret)
					{
						if (item.Id_Page == Id_page)
							task = item;
					}
				}
			}

			// set our layout to be the home screen
			SetContentView(Resource.Layout.TaskDetails);
			nameTextEdit = FindViewById<EditText>(Resource.Id.NameText);
			notesTextEdit = FindViewById<EditText>(Resource.Id.NotesText);
			saveButton = FindViewById<Button>(Resource.Id.SaveButton);

			// TODO: find the Checkbox control and set the value
			doneCheckbox = FindViewById<CheckBox>(Resource.Id.chkDone);
			doneCheckbox.Checked = task.Done;

			// find all our controls
			cancelDeleteButton = FindViewById<Button>(Resource.Id.CancelDeleteButton);

			// set the cancel delete based on whether or not it's an existing task
			cancelDeleteButton.Text = (task.ID == 0 ? "Cancel" : "Delete");

			nameTextEdit.Text = task.Name; 
			notesTextEdit.Text = task.Notes;

			// button clicks 
			cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
			saveButton.Click += (sender, e) => { Save(); };
		}

		void Save()
		{
			task.Name = nameTextEdit.Text;
			task.Notes = notesTextEdit.Text;
			//TODO: 
			task.Done = doneCheckbox.Checked;
			task.Id_Page = Id_page;
			TodoItemManager.SaveTask(task);
			Finish();
		}

		void CancelDelete()
		{
			if (task.ID != 0) {
				TodoItemManager.DeleteTask(task.ID);
			}
			Finish();
		}
	}
}

