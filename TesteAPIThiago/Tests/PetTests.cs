using System.Collections.Generic;
using TesteAPIThiago.Services;
using Xunit;
using Xunit.Abstractions;

namespace TesteAPIThiago.Tests
{
    public class PetTests
    {
        private readonly ITestOutputHelper output;

        public PetTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "Post upload image by ID")] //Upload de uma imagem utilizando um ID
        [Trait(" ategory", "pet")]
        public void Post_uploadImgById()
        {
            new PetServiceWorkFlow(output).Validate_PostupImgWithId(1);
        }

        [Fact(DisplayName = "Post upload image fixed Id")] //Upload de uma imagem utilizando o primeiro elemento de uma lista
        [Trait("category", "pet")]
        public void Post_uploadImgFixedId()
        {
            new PetServiceWorkFlow(output).Validate_PostupImgFixed();
        }

        [Fact(DisplayName = "Post new Pet")]
        [Trait("category", "pet")]
        public void Post_newPet()
        {
            new PetServiceWorkFlow(output).Validate_PostNewPet(CustomConfigurationProvider.GetSection("addPet"));
        }

        [Fact(DisplayName = "Put Modify a Existing Pet")]
        [Trait("category", "pet")]
        public void Put_ModifyPet()
        {
            new PetServiceWorkFlow(output).Validate_PutModifyPet(CustomConfigurationProvider.GetSection("ModifyPet"));
        }

        //Metodo 1, pegando aleatoriamente os possíveis status

        [Fact(DisplayName = "Get All Pet Find by Status")]
        [Trait("categoty", "pet")]
        public void Get_PetFindByStatus()
        {
            List<string> status = new List<string>();
            status.Add(CustomConfigurationProvider.GetSection("status.val1"));
            status.Add(CustomConfigurationProvider.GetSection("status.val2"));
            status.Add(CustomConfigurationProvider.GetSection("status.val3"));
            new PetServiceWorkFlow(output).Validate_GetAllPetFindByStatusRandom(status);
        }

        //Metodo 2, pegando especificamente os tipos de status

        [Fact(DisplayName = "Get All Pet Find by Sold")]
        [Trait("categoty", "pet")]
        public void Get_PetFindByStatusBySold()
        {

            new PetServiceWorkFlow(output).Validate_GetAllPetFindByStatus(CustomConfigurationProvider.GetSection("status.val1"));
        }

        [Fact(DisplayName = "Get All Pet Find by Avaliable")]
        [Trait("categoty", "pet")]
        public void Get_PetFindByStatusByAvaliable()
        {
            new PetServiceWorkFlow(output).Validate_GetAllPetFindByStatus(CustomConfigurationProvider.GetSection("status.val2"));
        }

        [Fact(DisplayName = "Get All Pet Find by Pending")]
        [Trait("categoty", "pet")]
        public void Get_PetFindByStatusByPending()
        {
            new PetServiceWorkFlow(output).Validate_GetAllPetFindByStatus(CustomConfigurationProvider.GetSection("status.val3"));
        }

        //PET FIND BY ID
        [Fact(DisplayName = "Get Pet by Id")]
        [Trait("category", "pet")]
        public void Get_PetById()
        {
            new PetServiceWorkFlow(output).Validate_GetPetById(int.Parse(CustomConfigurationProvider.GetSection("id")));
        }

        //Pegando um id inválido e recebendo um erro
        [Fact(DisplayName = "Get Bad Request by Id")]
        [Trait("category", "pet")]
        public void Get_BadRequestById()
        {
            new PetServiceWorkFlow(output).Validade_Error_GetPetById(int.Parse(CustomConfigurationProvider.GetSection("invalidId")));
        }

        //PENDÊNCIAS
        //POST PET BY ID
        //DELETE ÉT BY ID
    }
}
