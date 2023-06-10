using ApiRest_CI_CD.src.Models.Request;
using ApiRest_CI_CD.src.Models.Response;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using static System.Net.Mime.MediaTypeNames;

namespace ApiRest_CI_CD
{
    [Binding]
    public class TestApiRestStepDefinitions
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse response;
        private string productId;
        private List<CategoryResponse> allCategories;
        private string token;
        private CategoryResponse categoryResponse;
        private CategoryRequest categoryRequest;

        [Given(@"the Demo Store API is available")]
        public void GivenTheDemoStoreAPIIsAvailable()
        {
            client = new RestClient("http://demostore.gatling.io/api");
        }

        [Given(@"I am authenticated as admin")]
        public void GivenIAmAuthenticatedAsAdmin()
        {
            var userRequest = new UserRequest { username = "admin", password = "admin" };
            var authRequest = new RestRequest("/authenticate", Method.Post);
            authRequest.AddJsonBody(userRequest);

            var authResponse = client.Execute<UserResponse>(authRequest);
            Assert.That(authResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            token = authResponse.Data.token;
        }


        [Given(@"Tengo acceso a la api rest")]
        public void GivenTengoAccesoALaApiRest()
        {
            request = new RestRequest("/category", Method.Get);

            response = client.Execute(request);
        }

        [When(@"Cuando hago una peticion de tipo get")]
        public void WhenCuandoHagoUnaPeticionDeTipoGet()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"Devuelve todas las categorias")]
        public void ThenDevuelveTodasLasCategorias()
        {
            var categoryResponseList = JsonConvert.DeserializeObject<List<CategoryResponse>>(response.Content);
            Assert.That(categoryResponseList, Is.Not.Null);
            Assert.That(categoryResponseList != null && categoryResponseList.Count > 0);
        }

        [Given(@"Tengo el token de autenticacion")]
        public void GivenTengoElTokenDeAutenticacion()
        {
            var userRequest = new UserRequest { username = "admin", password = "admin" };
            var authRequest = new RestRequest("/authenticate", Method.Post);
            authRequest.AddJsonBody(userRequest);

            var authResponse = client.Execute<UserResponse>(authRequest);
            Assert.That(authResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            token = authResponse.Data.token;
        }

        [When(@"Cuando hago una peticion de tipo post")]
        public void WhenCuandoHagoUnaPeticionDeTipoPost()
        {
            categoryRequest = new CategoryRequest
            {
                name="Prueba"
            };

            request = new RestRequest("/category", Method.Post);
            request.AddJsonBody(categoryRequest);
            request.AddHeader("Authorization", $"Bearer {token}");

            response = client.Execute(request);
        }

        [Then(@"Se agrega la categoria nueva")]
        public void ThenSeAgregaLaCategoriaNueva()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [When(@"Cuando hago una peticion de tipo get con el id")]
        public void WhenCuandoHagoUnaPeticionDeTipoGetConElId()
        {
            request = new RestRequest($"/category/{6}", Method.Get);
            response = client.Execute<CategoryResponse>(request);
        }

        [Then(@"Obtengo la categoria en especifico")]
        public void ThenObtengoLaCategoriaEnEspecifico()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [When(@"Cuando actulizo el nombre")]
        public void WhenCuandoActulizoElNombre()
        {
            categoryRequest = new CategoryRequest
            {
                name = "Prueba test"
            };

            request = new RestRequest($"/category/{5}", Method.Put);
            request.AddJsonBody(categoryRequest);
            request.AddHeader("Authorization", $"Bearer {token}");

            response = client.Execute(request);
        }

        [Then(@"Se actulzia la lista de categorias")]
        public void ThenSeActulziaLaListaDeCategorias()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }



    }
}
