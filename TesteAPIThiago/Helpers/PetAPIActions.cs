using System;
using Xunit.Abstractions;
using RestSharp;
using TesteAPIThiago.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace TesteAPIThiago.Helpers
{
    public class PetAPIActions
    {
        private ITestOutputHelper LoggerOutput;

        public ITestOutputHelper Logger
        {
            get
            {
                return LoggerOutput;
            }
            set
            {
                LoggerOutput = value;
            }
        }

        public PetAPIActions(ITestOutputHelper output)
        {
            this.LoggerOutput = output;
        }

        //GROUP: PET
        public bool Post_UpImagePet(long id)
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest(Method.POST);
            IRestResponse restResponse;

            restClient.BaseUrl = new Uri(APIMethods.PetId + $"{id}/uploadImage"); //Link do end-point

            string filePatch = $"{Directory.GetCurrentDirectory()}/Data/imagem.jpg"; //link do diretorio da imagem que estou passando

            //Aqui está a configuração da requisição para realizar upoload de imagens
            restRequest.AddHeader("Content-Type", "multipart/form-data");
            restRequest.AddFile("file", filePatch);
            restRequest.AlwaysMultipartFormData = true;
            restResponse = restClient.Execute(restRequest);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Post_NewPet(CreatNewPet_Request requestBody)
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest(Method.POST);
            IRestResponse restResponse;

            restClient.BaseUrl = new Uri(APIMethods.NewPet);
            restRequest.AddJsonBody(JsonSerializer.Serialize(requestBody)); //Essa linha é a diferença no endpoint POST, adicionando um corpo Json na requisição

            restResponse = restClient.Execute(restRequest);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //PUT UPDATE PET
        public bool Put_ModifyPet(CreatNewPet_Request requestBody)
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest(Method.PUT);
            IRestResponse restResponse;

            restClient.BaseUrl = new Uri(APIMethods.NewPet);
            restRequest.AddJsonBody(JsonSerializer.Serialize(requestBody));

            restResponse = restClient.Execute(restRequest);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Get_pet_Response> Get_AllPetFindByStatus(string status)
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse;

            restClient.BaseUrl = new Uri(APIMethods.PetFindByStatus +"?status="+status); //Link do endpoint
            restResponse = restClient.Execute(restRequest);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<List<Get_pet_Response>>(restResponse.Content);
            }
            else
            {
                LoggerOutput.WriteLine("Esperando o code:" + System.Net.HttpStatusCode.OK +"mas, foi retornado: " +restResponse.StatusCode);
                return null;
            }
        }

        public Get_pet_Response Get_petByPetId(int id)
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse;

            restClient.BaseUrl = new Uri(APIMethods.PetId +id); //Link do endpoint
            restResponse = restClient.Execute(restRequest);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<Get_pet_Response>(restResponse.Content);
            }
            else
            {
                LoggerOutput.WriteLine("Esperando o code:" + System.Net.HttpStatusCode.OK + "mas, foi retornado: " + restResponse.StatusCode);
                return null;
            }
        }

        public Generic_Response Get_APIError(int invalidId)
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse;

            restClient.BaseUrl = new Uri(APIMethods.PetId + invalidId);
            restResponse = restClient.Execute(restRequest);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return JsonSerializer.Deserialize<Generic_Response>(restResponse.Content);
            }
            else
            {
                Console.WriteLine("Erro, esperava encontrar code: " + System.Net.HttpStatusCode.NotFound + ", mas foi retornado: " + restResponse.StatusCode); ;
                return null;
            }
        }
    }
}
