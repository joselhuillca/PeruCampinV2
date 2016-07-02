using System;
using System.Collections;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Views;
using Android.Widget;
using Com.Telerik.Widget.List;
using Square.Picasso;

namespace MLearning.Droid
{
	public class ImagenCamp : Java.Lang.Object {
			public String Descripcion;
			public String image;
	}	


	public class ImageViewHolder : ListViewHolder
	{

		public TextView txtDescription;
		public ImageView Imagen;

		public ImageViewHolder(View itemView) : base(itemView)
		{

			this.txtDescription = (TextView)itemView.FindViewById(Resource.Id.DescriptionTelerik);
			this.Imagen = (ImageView)itemView.FindViewById(Resource.Id.ImagenTelerik);
		}
	}

	public class ImageAdapterTelerik : ListViewAdapter
	{
		public Context ctx;
		public ImageAdapterTelerik(IList items) : base(items)
		{
		}

		public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			LayoutInflater inflater = LayoutInflater.From(parent.Context);
			View itemView = inflater.Inflate(Resource.Layout.example_list_view_deck_item_layout, parent, false);
			return new ImageViewHolder(itemView);
		}

		public override void OnBindListViewHolder(ListViewHolder holder, int position)
		{
			ImageViewHolder vh = (ImageViewHolder)holder;
			ImagenCamp item = (ImagenCamp)GetItem(position);

			var textFormat = Android.Util.ComplexUnitType.Px;
			vh.txtDescription.TextFormatted = Html.FromHtml(item.Descripcion);
			vh.txtDescription.SetTextColor(Color.White);
			vh.txtDescription.Typeface = Typeface.CreateFromAsset(ctx.Assets, "fonts/HelveticaNeue.ttf");
			vh.txtDescription.SetTextSize(textFormat, Configuration.getHeight(24));
			vh.txtDescription.SetBackgroundColor(Color.ParseColor("#40000000"));

			// load image as Drawable
			//Drawable d = new BitmapDrawable(getBitmapFromAssets(item.image));
			// set image to ImageView
			//vh.Imagen.SetImageDrawable(d);

			//vh.Imagen.SetMinimumWidth(Configuration.getWidth(637));
			Picasso.With(ctx).Load(item.image).Placeholder(ctx.Resources.GetDrawable(Resource.Drawable.progress_animation)).Resize(Configuration.getWidth(640), Configuration.getHeight(427)).CenterCrop().Into(vh.Imagen);



		}

	}
}

