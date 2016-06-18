using System;

namespace Tasky.Shared
{
	public class FavoritosItem
	{
		public FavoritosItem ()
		{
		}
		public int ID { get; set; }
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public int Id_unidad { get; set; }	// TODO: add this field to the user-interface
	}
}

