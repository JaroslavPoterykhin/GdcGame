﻿using System;
using System.Collections.Generic;
using System.Linq;
using EntityFX.Gdcame.DataAccess.Contract.User;
using EntityFX.Gdcame.Infrastructure.Common;
using EntityFX.Gdcame.Manager.Contract.SessionManager;
using EntityFX.Gdcame.Manager.Contract.UserManager;

namespace EntityFX.Gdcame.Manager
{
    public class SimpleUserManager : ISimpleUserManager
    {
        private readonly IHashHelper _hashHelper;
        private readonly ILogger _logger;
        private readonly IUserDataAccessService _userDataAccess;

        public SimpleUserManager(ILogger logger, IUserDataAccessService userDataAccess, IHashHelper hashHelper)
        {
            _logger = logger;
            _userDataAccess = userDataAccess;
            _hashHelper = hashHelper;
        }

        public bool Exists(string login)
        {
            var user = _userDataAccess.FindByName(login);
            return user != null;
        }

        public UserData FindById(string id)
        {
            var user = _userDataAccess.FindById(id);

            return user != null
                ? new UserData
                {
                    Id = user.Id, Login = user.Email, PasswordHash = user.PasswordHash,
                    UserRoles = GetUserRoles(user)
                }
                : null;
        }

        public UserData Find(string login)
        {
            var user = _userDataAccess.FindByName(login);
            return user != null
                ? new UserData {Id = user.Id, Login = user.Email, PasswordHash = user.PasswordHash,
                    UserRoles = GetUserRoles(user)
                }
                : null;
        }

        public UserData[] FindByFilter(string searchString)
        {
            return _userDataAccess.FindByFilter(searchString)
                .Select(user =>
                    new UserData {Id = user.Id, Login = user.Email, PasswordHash = user.PasswordHash,
                        UserRoles = GetUserRoles(user)
                    })
                .ToArray();
        }

        public void Create(UserData login)
        {
            try
            {
                _userDataAccess.Create(new User
                {
                    Id = _hashHelper.GetHashedString(login.Login),
                    Email = login.Login,
                    IsAdmin = login.UserRoles != null && login.UserRoles.Contains(UserRole.Admin),
                    PasswordHash = login.PasswordHash
                });
            }
            catch (Exception exp)
            {
                _logger.Error(exp);
                throw;
            }
        }

        public void Delete(string id)
        {
            _userDataAccess.Delete(id);
        }

        private UserRole[] GetUserRoles(User user)
        {
            var roles = new List<UserRole> { UserRole.GenericUser };
            if (user.IsAdmin)
            {
                roles.Add(UserRole.Admin);
            }
            return roles.ToArray();
        }
    }
}