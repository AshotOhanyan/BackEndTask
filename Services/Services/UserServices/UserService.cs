using Azure.Core;
using Data.DbModels;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Mappings;
using Services.Models;
using Services.OtherServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        #region CRUD

        public async Task<UserModel> AddUser(UserModel newUser)
        {
            return (await _userRepository.AddDbObjectAsync(newUser.ToUser())).ToUserModel();
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            List<User> users = (await _userRepository.GetAllDbObjectsAsync()).ToList();

            return users.Select(x => x.ToUserModel()).ToList();
        }

        public async Task<UserModel> GetUserById(Guid id)
        {
            return (await _userRepository.GetDbObjectByIdAsync(id)).ToUserModel();
        }

        public List<UserModel> GetUsersByFilter(UserModel filter)
        {
            List<User> users = _userRepository.GetDbObjectsByFilter(filter.ToUser()).ToList();

            return users.Select(x => x.ToUserModel()).ToList();
        }

        public async Task RemoveUser(Guid id)
        {
            await _userRepository.RemoveDbObjectAsync(id);
        }

        public async Task<UserModel> UpdateUser(UserModel updatedUser)
        {
            return (await _userRepository.UpdateDbObjectAsync(updatedUser.ToUser())).ToUserModel();
        }

        #endregion


        public async Task<SignUpResponseModel> SignUp(SignUpRequestModel model)
        {
            string emailConfiramtionToken = TokenService.GenerateEightCharacterToken();

            var (passwordHash, salt) = PasswordHelper.GeneratePasswordHash(model.Password);

            UserModel userModel = new UserModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Email = model.Email,
                EmailConfirmationToken = emailConfiramtionToken,
                DateOfBirth = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                IsEmailConfirmed = false,
                PasswordHash = passwordHash,
                Salt = salt
            };


            await EmailService.SendEmail(model.Email, "Email Validation Code", emailConfiramtionToken);

            userModel.TokenExpirationDate = DateTime.UtcNow.AddMinutes(10);

            User user = await _userRepository.AddDbObjectAsync(userModel.ToUser());

            return new SignUpResponseModel { UserId = user.Id };
        }

        public async Task ConfirmUserEmail(EmailConfirmationRequestModel model)
        {
            User user = await _userRepository.GetDbObjectByIdAsync(model.UserId);

            if (user == null)
                throw new Exception("There is no user with this Id.");

            TimeSpan time = DateTime.UtcNow.Subtract(user.TokenExpirationDate.Value);


            if (time >= TimeSpan.Zero)
            {
                user.EmailConfirmationToken = null;
                user.IsEmailConfirmed = null;
                user.TokenExpirationDate = default(DateTime);

                await _userRepository.UpdateDbObjectAsync(user);

                throw new Exception("Token has expired! Try to SingUp again!");
            }


            user.IsEmailConfirmed = true;
            user.TokenExpirationDate = default(DateTime);
            user.EmailConfirmationToken = null;

            await _userRepository.UpdateDbObjectAsync(user);
        }


        public async Task<SignInResponseModel> SignIn(SignInRequestModel model)
        {
            User? user = await _userRepository.GetDbObjectsByFilter(new User { Email = model.Email }).FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("Wrong email or password!");

            if (user.IsEmailConfirmed == false)
                throw new Exception("Users email is not confirmed!");

            bool isPasswordValid = PasswordHelper.VerifyPassword(model.Password, user.PasswordHash);

            if (!isPasswordValid)
                throw new Exception("Wrong email or password!");

            string accessToken = TokenService.GenerateAccessToken(user.Id.ToString(), user.Email);
            string refreshToken = TokenService.GenerateRefreshToken();


            user.RefreshToken = refreshToken;

            await _userRepository.UpdateDbObjectAsync(user);

            return new SignInResponseModel { AccessToken = accessToken };
        }
    }
}
