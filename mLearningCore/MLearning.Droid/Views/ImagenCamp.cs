using System;
using System.Collections;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Util;
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

			vh.txtDescription.TextFormatted = Html.FromHtml(item.Descripcion);
			vh.txtDescription.SetTextColor(Color.ParseColor("#616161"));
			//vh.txtDescription.Typeface = Typeface.CreateFromAsset(ctx.Assets, "fonts/ArcherMediumPro.otf");
			vh.txtDescription.SetTextSize(ComplexUnitType.Fraction, Configuration.getHeight(32));
			//vh.txtDescription.SetBackgroundColor(Color.ParseColor("#E6E6E6"));

			// load image as Drawable
			//Drawable d = new BitmapDrawable(getBitmapFromAssets(item.image));
			// set image to ImageView
			//vh.Imagen.SetImageDrawable(d);

			vh.Imagen.SetMinimumWidth(Configuration.getWidth(640));
			vh.Imagen.SetMinimumHeight(Configuration.getHeight(427));
			Picasso.With(ctx).Load(item.image).Placeholder(ctx.Resources.GetDrawable(Resource.Drawable.progress_animation)).Resize(Configuration.getWidth(640), Configuration.getHeight(427)).CenterCrop().Into(vh.Imagen);



		}

	}
}

