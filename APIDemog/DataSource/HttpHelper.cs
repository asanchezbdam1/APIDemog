using Newtonsoft.Json.Linq;
using APIDemog.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                string result = await resp.Content.ReadAsStringAsync();
                result.ToString();
            }
            return lista;
        }
    }
}
