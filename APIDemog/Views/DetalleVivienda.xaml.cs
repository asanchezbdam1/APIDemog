using APIDemog.DataSource;
using APIDemog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APIDemog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleVivienda : ContentPage
    {
        public DetalleVivienda()
        {
            InitializeComponent();
        }
        public DetalleVivienda(Vivienda viv)
        {
            InitializeComponent();
            BindingContext = viv;
        }

        private async void btnAbrirUbicacion_Clicked(object sender, EventArgs e)
        {
            var it = (Vivienda)BindingContext;
            var conf = await DisplayAlert("Confirmación", "¿Desea abrir la ubicación?", "Sí", "No");
            if (conf)
            {
                MapHelper.OpenLoc(it.Coord_X, it.Coord_Y);
            }
        }
    }
}