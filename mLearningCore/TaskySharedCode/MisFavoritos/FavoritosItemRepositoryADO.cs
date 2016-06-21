using System;
using System.IO;
using System.Collections.Generic;

namespace Tasky.Shared
{
	public class FavoritosItemRepositoryADO
	{
		TodoDatabase db = null;
		protected static string dbLocation;		
		protected static FavoritosItemRepositoryADO me;		

		static FavoritosItemRepositoryADO ()
		{
			me = new FavoritosItemRepositoryADO();
		}

		protected FavoritosItemRepositoryADO ()
		{
			// set the db location
			dbLocation = DatabaseFilePath;

			// instantiate the database	
			db = new TodoDatabase(dbLocation);
		}

		public static string DatabaseFilePath 
		{
			get 
			{ 
				var sqliteFilename = "cache.db";
				#if NETFX_CORE
				var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
				#else

				#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
				var path = sqliteFilename;
				#else

				#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
				#else
				// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
				// (they don't want non-user-generated data in Documents)
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				#endif
				var path = Path.Combine (libraryPath, sqliteFilename);
				#endif

				#endif
				return path;	
			}
		}

		public static FavoritosItem GetTask(int id)
		{
			return me.db.GetItemFav(id);
		}

		public static IEnumerable<FavoritosItem> GetTasks ()
		{
			return me.db.GetItemsFav();
		}

		public static int SaveTask (FavoritosItem item)
		{
			return me.db.SaveItemFav(item);
		}

		public static int DeleteTask(int id)
		{
			return me.db.DeleteItemFav(id);
		}
	}
}

