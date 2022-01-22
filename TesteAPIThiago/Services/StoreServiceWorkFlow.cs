using Xunit.Abstractions;
using Xunit;
using TesteAPIThiago.Helpers;
using TesteAPIThiago.Models;
using System.Text.Json;


namespace TesteAPIThiago.Services
{
    class StoreServiceWorkFlow
    {
        private readonly ITestOutputHelper LoggerOutput;

        public StoreServiceWorkFlow(ITestOutputHelper LoggerOutput)
        {
            this.LoggerOutput = LoggerOutput;
        }

        public void Validate_NewStoreOrder(object jsonInput)
        {
            Post_NewStoreOrder_Request requestObject = JsonSerializer.Deserialize<Post_NewStoreOrder_Request>(jsonInput.ToString());

            var response = new StoreAPIActions(LoggerOutput).Post_PetOrder(requestObject);
            Assert.True(response);
        }

        public void Validate_GetStoreOrderByOrderId(int orderId)
        {
            var response = new StoreAPIActions(LoggerOutput).Get_StoreOrderByOrderId(orderId);

            if(response != null)
            {
                Assert.Equal(orderId, response.id);
            }
            else
            {
                LoggerOutput.WriteLine("GET FALHOU!"); //Caso ocorra erro aqui é porque os ID's passados em StoreTest foram excluidos
                Assert.NotNull(response);
            }
        }

        public void Validate_DeleteStoreOrderByOrderId(int id)
        {
            var response = new StoreAPIActions(LoggerOutput).Delete_StoreOrderByOrderId(id);

            if (response == true)
            {
                Assert.True(response);
            }
            else
            {
                LoggerOutput.WriteLine("Erro, provavelmente o ID solicitado ja foi excluído anteriormente. Tente um ID existente.");
                Assert.True(response);
            }
        }

        public void Validate_GetAllStoreInventory()
        {
            var response = new StoreAPIActions(LoggerOutput).Get_AllStoreInventory();

            if(response != null)
            {
                Assert.Equal(int.MinValue.GetType(), response.totvs.GetType()); //Validando o teste pelo "tipo", já que ele muda o valor constantemente, mas o seu tipo permanece o mesmo
                Assert.Equal(int.MinValue.GetType(), response.string1.GetType());
                Assert.Equal(int.MinValue.GetType(), response.pending.GetType());
                Assert.Equal(int.MinValue.GetType(), response.available.GetType());
                Assert.Equal(int.MinValue.GetType(), response.notAvailable.GetType());
                Assert.Equal(int.MinValue.GetType(), response.dead.GetType());
                Assert.Equal(int.MinValue.GetType(), response.nonAvailable.GetType());
                Assert.Equal(int.MinValue.GetType(), response.dataSource.GetType());
                Assert.Equal(int.MinValue.GetType(), response.unavailable.GetType());
                Assert.Equal(int.MinValue.GetType(), response.soldOut.GetType());
                Assert.Equal(int.MinValue.GetType(), response.free.GetType());
                Assert.Equal(int.MinValue.GetType(), response.notForSale.GetType());
                Assert.Equal(int.MinValue.GetType(), response.sold.GetType());
                Assert.Equal(int.MinValue.GetType(), response.barraSold.GetType());
                Assert.Equal(int.MinValue.GetType(), response.dsda.GetType());
                Assert.Equal(int.MinValue.GetType(), response.forSale.GetType());
                Assert.Equal(int.MinValue.GetType(), response.nonavailable.GetType());
                Assert.Equal(int.MinValue.GetType(), response.avalible.GetType());
                Assert.Equal(int.MinValue.GetType(), response.openForSale.GetType());
                Assert.Equal(int.MinValue.GetType(), response.sweetest.GetType());
                Assert.Equal(int.MinValue.GetType(), response.brown.GetType());
                Assert.Equal(int.MinValue.GetType(), response.available1.GetType());
                Assert.Equal(int.MinValue.GetType(), response.connector.GetType());
                Assert.Equal(int.MinValue.GetType(), response.notForSale1.GetType());
                Assert.Equal(int.MinValue.GetType(), response.status.GetType());
            }
            else
            {
                LoggerOutput.WriteLine("GET FALHOU!");
                Assert.NotNull(response); //Forçando um erro para mostrar no teste que falhou
            }
        }
    }
}
