using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.UserServices
{
    public interface IUserService
    {
        public Task<List<UserModel>> GetAllUsers();
        public Task<UserModel> GetUserById(Guid id);
        public List<UserModel> GetUsersByFilter(UserModel filter);
        public Task<UserModel> AddUser(UserModel model);
        public Task<UserModel> UpdateUser(UserModel model);
        public Task RemoveUser(Guid id);

        public Task<SignUpResponseModel> SignUp(SignUpRequestModel model);

        public Task ConfirmUserEmail(EmailConfirmationRequestModel model);

        public Task<SignInResponseModel> SignIn(SignInRequestModel model);

    }
}
