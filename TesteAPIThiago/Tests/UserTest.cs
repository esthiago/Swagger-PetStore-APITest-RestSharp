using TesteAPIThiago.Services;
using Xunit;
using Xunit.Abstractions;

namespace TesteAPIThiago.Tests
{
    public class UserTest
    {
        private readonly ITestOutputHelper output;

        public UserTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(DisplayName = "Post list of users with array")]
        [Trait("category", "user")]
        public void Post_UserListWithArray()
        {
            new UserServiceWorkFlow(output).Validate_Post_UserWithArray(CustomConfigurationProvider.GetSection("post_responseBody"));
        }

        //OBS: O ENDPOINT POST USER CREATE WITH LIST É EXATAMENTE IGUAL AO LIST OF ARRAY

        [Fact(DisplayName = "Get Username")]
        [Trait("category", "user")]
        public void Get_Username()
        {
            new UserServiceWorkFlow(output).Validate_GetUsername(CustomConfigurationProvider.GetSection("username"));
        }

        [Fact(DisplayName = "Put Modify user by Username")]
        [Trait("category", "user")]
        public void put_ModifyUserByUserName()
        {
            new UserServiceWorkFlow(output).Validate_PutUserByUserName(CustomConfigurationProvider.GetSection("put_responseBody"), CustomConfigurationProvider.GetSection("put_username"));
        }

        [Fact(DisplayName = "Delete User By User Name")]
        [Trait("category", "user")]
        public void Delete_UserByUserName()
        {
            new UserServiceWorkFlow(output).Validate_DeleteUserByUserName("string");
        }

        [Theory(DisplayName = "Get Login")]
        [InlineData("string", "string")] //Passando os dados aqui cao invés do Json, mas tem a mesma funcionalidade
        [InlineData("thiago", "string")]
        [Trait("category", "user")]
        public void Get_Login(string username, string password)
        {
            new UserServiceWorkFlow(output).Validate_GetUserAndPassword(username, password);
        }

        [Fact(DisplayName = "Get Logout")]
        [Trait("category", "user")]
        public void Get_Logout()
        {
            new UserServiceWorkFlow(output).Validate_GetLogout();
        }

        //PENDENCIA: POST USER
        [Fact(DisplayName = "Post New User")]
        [Trait("category", "user")]
        public void Post_NewUser()
        {
            new UserServiceWorkFlow(output).Validate_PostNewUser(CustomConfigurationProvider.GetSection("post_responseBody1"));
        }
    }
}
