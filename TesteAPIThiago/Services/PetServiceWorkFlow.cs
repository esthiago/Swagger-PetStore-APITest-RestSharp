using System;
using System.Collections.Generic;
using TesteAPIThiago.Helpers;
using TesteAPIThiago.Models;
using Xunit;
using Xunit.Abstractions;
using System.Text.Json;

namespace TesteAPIThiago.Services
{
    public class PetServiceWorkFlow
    {
        private readonly ITestOutputHelper LoggerOutput;

        public PetServiceWorkFlow(ITestOutputHelper LoggerOutput)
        {
            this.LoggerOutput = LoggerOutput;
        }

        //Executando para o id desejado, nesse caso (1) "Ver na classe PetTest"
        public void Validate_PostupImgWithId(long id)
        {
            var response = new PetAPIActions(LoggerOutput).Post_UpImagePet(id);
            Assert.True(response);
        }

        public void Validate_PostupImgFixed()
        {
            //Utilizando o getPetStatus para pegar uma lista de objetos do tipo available
            var response = new PetAPIActions(LoggerOutput).Get_AllPetFindByStatus("available");

            //Pegando o primeiro id da lista de objetos contida no getpetStatus e executando o mesmo teste que o Validate_postupImgWithId
            var responsePost = new PetAPIActions(LoggerOutput).Post_UpImagePet(response[0].id);

            Assert.True(responsePost);
        }

        //PET POST
        public void Validate_PostNewPet(object jsonInput)
        {
            //Arrange
            CreatNewPet_Request requestObject = JsonSerializer.Deserialize<CreatNewPet_Request>(jsonInput.ToString()); //Pegando os valores do Json e convertendo para a classe CreatNetPet_Request

            var response = new PetAPIActions(LoggerOutput).Post_NewPet(requestObject);
            Assert.True(response);
        }

        //PET PUT
        public void Validate_PutModifyPet(object jsonInput)
        {
            CreatNewPet_Request requestObject = JsonSerializer.Deserialize<CreatNewPet_Request>(jsonInput.ToString());

            var response = new PetAPIActions(LoggerOutput).Put_ModifyPet(requestObject);
            Assert.True(response);
        }

        public void Validate_GetAllPetFindByStatusRandom(List<string> status)
        {
            Random rdm = new Random();
            int position = rdm.Next(status.Count);

            var responseList = new PetAPIActions(LoggerOutput).Get_AllPetFindByStatus(status[position]);
            Assert.NotNull(responseList);

            for (int i=0; i<responseList.Count; i++)
            {
                Assert.True(status[position] == responseList[i].status);
            }
        }

        public void Validate_GetAllPetFindByStatus(string status)
        {
            var responseList = new PetAPIActions(LoggerOutput).Get_AllPetFindByStatus(status);
            Assert.NotNull(responseList);

            for (int i = 0; i < responseList.Count; i++)
            {
                Assert.True(status == responseList[i].status);
            }
        }

        public void Validate_GetPetById(int id)
        {
            var response = new PetAPIActions(LoggerOutput).Get_petByPetId(id);
            Assert.NotNull(response);
            Assert.True(id == response.id, "ID" + id + "não é igual ao " + response.id);
        }

        public void Validade_Error_GetPetById(int invalidId)
        {
            var response = new PetAPIActions(LoggerOutput).Get_APIError(invalidId);
            Assert.NotNull(response);
            Assert.True(1 == response.code, "O code não corresponde, 1" +" e " +response.code);
        }
    }
}
