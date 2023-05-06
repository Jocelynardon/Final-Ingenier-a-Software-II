using Newtonsoft.Json;
using System.Text;

namespace FinalSoftwareII.Functions
{
    public class APIService
    {
        private static readonly int timeout = 600;
        private static readonly string baseurl = "https://localhost:7121/";

        // ============================================================================================= Autenticacion
        public static async Task<FinalSoftwareII.Models.Token> AutenticacionGetToken(FinalSoftwareII.Models.Usuario usuario)
        {
            var json_ = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.PostAsync(baseurl + "Autenticacion/GetToken", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<FinalSoftwareII.Models.Token>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        /*========================================================================= Usuario Verificacion*/

        public static async Task<bool> UsuarioVerificacion(FinalSoftwareII.Models.Usuario usuario)
        {
            var json_ = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.PostAsync(baseurl + "Usuarios/Check", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // ============================================================================== Usuario CRUD
        public static async Task<IEnumerable<FinalSoftwareII.Models.Usuario>> UsuarioGetList(string token)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await httpClient.PostAsync(baseurl + "Usuarios/GetList", null);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<FinalSoftwareII.Models.Usuario>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<bool> UsuarioSet(FinalSoftwareII.Models.Usuario usuario)
        {
            var json_ = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.PostAsync(baseurl + "Usuarios/Set", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<bool> UsuarioUpdate(FinalSoftwareII.Models.Usuario usuario, string token)
        {
            var json_ = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.PostAsync(baseurl + "Usuarios/Update", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async System.Threading.Tasks.Task<bool> UsuarioDelete(string usuario, string token)
        {
            var json_ = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await httpClient.PostAsync(baseurl + "Usuarios/Delete", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //return JsonConvert.DeserializeObject<IEnumerable<ClasificacionPeliculasModel.Movie>>(await response.Content.ReadAsStringAsync());
                return true;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}
