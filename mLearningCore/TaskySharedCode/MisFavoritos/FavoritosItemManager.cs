using System;
using System.Collections.Generic;

namespace Tasky.Shared
{
	/// <summary>
	/// Manager classes are an abstraction on the data access layers
	/// </summary>
	public class FavoritosItemManager
	{
		
		public FavoritosItemManager ()
		{
		}

		public static FavoritosItem GetTask(int id)
		{
			return FavoritosItemRepositoryADO.GetTask(id);
		}

		public static IList<FavoritosItem> GetTasks ()
		{
			return new List<FavoritosItem>(FavoritosItemRepositoryADO.GetTasks());
		}

		public static int SaveTask (FavoritosItem item)
		{
			return FavoritosItemRepositoryADO.SaveTask(item);
		}

		public static int DeleteTask(int id)
		{
			return FavoritosItemRepositoryADO.DeleteTask(id);
		}
	}
}

