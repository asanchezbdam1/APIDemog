using Newtonsoft.Json.Linq;
using APIDemog.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace APIDemog.DataSource
{
    public class HttpHelper
    {
        public static async Task<List<ZonaBasica>> GetDatosPoblacion()
        {
            var lista = new List<ZonaBasica>();
            using (var client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync("https://datosabiertos.navarra.es/api/3/action/datastore_search?resource_id=12fbefdb-fe4c-42e3-a731-036025c0f2e7");
                resp.EnsureSuccessStatusCode();
                string result = await resp.Content.ReadAsStringAsync();
                JContainer json = (JContainer)JObject.Parse(result);
                foreach (var zb in json["result"]["records"])
                {
                    try
                    {
                        lista.Add(new ZonaBasica()
                        {
                            NombreZona = (string)zb["Zona Básica"],
                            HabMayores = Convert.ToInt32((string)zb["Habitantes adultos mayores de 14 años"]),
                            HabTotales = Convert.ToInt32((string)zb["TOTAL POBLACIÓN"])
                        });
                    }
                    catch (Exception ex)
                    { }
                }
            }
            return lista;
        }
        public static async Task<List<Vivienda>> GetDatosViviendas()
        {
            var lista = new List<Vivienda>();
            using (var client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync("https://datosabiertos.navarra.es/dataset/b5ed4729-1d09-4729-8b71-26ef6e08124f/resource/7bf44c36-6a34-4c12-859e-78b6fbc4e8db/download/solicitudesunidadfamiliar.xml");
                resp.EnsureSuccessStatusCode();
                var result = await resp.Content.ReadAsStreamAsync();
                var data = XDocument.Load(result);
                var viviendas = from viv in data.Descendants("PROMOCION")
                                select viv;
                foreach (var vivienda in viviendas)
                {
                    try
                    {
                        var viv = new Vivienda()
                        {
                            Localidad = vivienda.Descendants("LOCALIDAD").First().Value,
                            Ofertadas = Int32.Parse(vivienda.Descendants("VIVIENDASOFERTADAS").First().Value),
                            Promotora = vivienda.Descendants("PROMOTORA").First()
                                .Descendants("NOMBRE").First().Value,
                            TelfPromo = vivienda.Descendants("PROMOTORA").First()
                                .Descendants("TELEFONO").First().Value,
                            MailPromo = vivienda.Descendants("PROMOTORA").First()
                                .Descendants("CORREOELECTRONICO").First().Value,
                            PagPromo = vivienda.Descendants("PROMOTORA").First()
                                .Descendants("PAGINAWEB").First().Value
                        };
                        if (!String.IsNullOrWhiteSpace(vivienda.Descendants("COORD_UTM_X").First().Value) &&
                            !String.IsNullOrWhiteSpace(vivienda.Descendants("COORD_UTM_Y").First().Value))
                        {
                            viv.Coord_X = Int32.Parse(vivienda.Descendants("COORD_UTM_X").First().Value);
                            viv.Coord_Y = Int32.Parse(vivienda.Descendants("COORD_UTM_Y").First().Value);
                            viv.HasCoords = true;
                        }
                        lista.Add(viv);
                    }
                    catch (Exception ex)
                    { }
                }
            }
            return lista;
        }
    }
}
