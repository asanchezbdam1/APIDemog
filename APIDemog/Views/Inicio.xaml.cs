using APIDemog.DataSource;
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
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            Actualizar();
            HttpHelper.GetDatosViviendas();
        }

        private async Task Actualizar()
        {
            lvZonas.ItemsSource = await HttpHelper.GetDatosPoblacion();
        }
    }
}