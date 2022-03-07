using APIDemog.DataSource;
using APIDemog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APIDemog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Viviendas : ContentPage
    {
        public Viviendas()
        {
            InitializeComponent();
            Actualizar();
        }

        private async void Actualizar()
        {
            lvViv.ItemsSource = await HttpHelper.GetDatosViviendas();
            rvViviendas.IsRefreshing = false;
        }

        private void rvViviendas_Refreshing(object sender, EventArgs e)
        {
            Actualizar();
        }

        private async void lvViv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var it = (Vivienda)lvViv.SelectedItem;
            Navigation.PushAsync(new DetalleVivienda(it));
        }
    }
}