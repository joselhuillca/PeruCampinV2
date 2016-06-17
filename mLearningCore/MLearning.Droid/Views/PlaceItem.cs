using System;

namespace MLearning.Droid
{
	public class PlaceItem
	{
			public string titulo { get; set; }
			public string detalle { get; set; }
			public int index{ get; set; }
			public string pathIcon{ get; set;}
		public int tipoIndex;
		public Tuple<int,int> position;
	}
}

