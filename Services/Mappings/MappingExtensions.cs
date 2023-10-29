using Data.DbModels;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappings
{
    public static class MappingExtensions
    {

        #region UserMappings

        public static UserModel ToUserModel(this User user)
        {
            List<ClassModel> classModels;

            if (user.Classes != null && user.Classes.Any())
            {
                classModels = user.Classes.Select(x => x.ToClassModel()).ToList();
            }
            else
            {
                classModels = new List<ClassModel>();
            }

            UserModel userModel = new UserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                Email = user.Email,
                EmailConfirmationToken = user.EmailConfirmationToken,
                FullName = user.FullName,
                IsEmailConfirmed = user.IsEmailConfirmed,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                RefreshToken = user.RefreshToken,
                Salt = user.Salt,
                TokenExpirationDate = user.TokenExpirationDate,
                Classes = classModels
            };

            return userModel;
        }


        public static User ToUser(this UserModel model)
        {
            List<Class> classes = new List<Class>();

            if (model.Classes != null && model.Classes.Any())
            {

                classes = model.Classes.Select(x => x.ToClass()).ToList();
            }

            User user = new User()
            {
                Id = model.Id ?? Guid.Empty,
                FirstName = model.FirstName ?? string.Empty,
                LastName = model.LastName ?? string.Empty,
                Address = model.Address ?? string.Empty,
                DateOfBirth = model.DateOfBirth != null ? DateTime.ParseExact(model.DateOfBirth, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : default(DateTime),
                Email = model.Email ?? string.Empty,
                EmailConfirmationToken = model.EmailConfirmationToken,
                IsEmailConfirmed = model.IsEmailConfirmed,
                TokenExpirationDate = model.TokenExpirationDate,
                PasswordHash = model.PasswordHash ?? string.Empty,
                PhoneNumber = model.PhoneNumber ?? string.Empty,
                Salt = model.Salt ?? string.Empty,
                RefreshToken = model.RefreshToken,
                Classes = classes
            };


            return user;
        }

        #endregion


        #region ClassMappings

        public static ClassModel ToClassModel(this Class @class)
        {
            List<UserModel> userModels = new List<UserModel>();

            if (@class.Users != null && @class.Users.Any())
            {
                userModels = @class.Users.Select(x => x.ToUserModel()).ToList();
            }

            ClassModel classModel = new ClassModel()
            {
                Id = @class.Id,
                Name = @class.Name,
                Users = userModels
            };

            return classModel;
        }


        public static Class ToClass(this ClassModel model)
        {
            List<User> users = new List<User>();
            if (model.Users != null && model.Users.Any())
            {
                users = model.Users.Select(x => x.ToUser()).ToList();
            }


            Class Class = new Class()
            {
                Id = model.Id ?? Guid.Empty,
                Name = model.Name ?? string.Empty,
                Users = users
            };


            return Class;

        }
        #endregion
    }
}
