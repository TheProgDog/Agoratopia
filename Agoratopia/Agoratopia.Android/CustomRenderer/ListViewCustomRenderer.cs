using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Agoratopia.CustomRenderer;
using Agoratopia.CustomRenderer.Effects;
using Agoratopia.Droid;

[assembly: ExportRenderer(typeof(CustomListView), typeof(ListViewCustomRenderer))]
namespace Agoratopia.Droid
{
    public class ListViewCustomRenderer : ListViewRenderer
    {
        public ListViewCustomRenderer(Context areYouHappyNow) : base(areYouHappyNow)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetOutlineAmbientShadowColor(Android.Graphics.Color.Black);

                var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);

                if (effect != null)
                {
                    float radius = effect.Radius;

                    Control.Elevation = radius;
                    Control.TranslationZ = (effect.DistanceX + effect.DistanceY) / 2;
                }

                
            }
        }
    }
}