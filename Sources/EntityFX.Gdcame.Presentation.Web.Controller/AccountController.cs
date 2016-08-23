﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using EntityFX.Gdcame.Manager.Contract.AdminManager;
using EntityFX.Gdcame.Manager.Contract.UserManager;
using EntityFX.Gdcame.Presentation.Web.Api.Models;
using EntityFX.Gdcame.Presentation.Web.Model;

namespace EntityFX.Gdcame.Presentation.Web.Controller
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/accounts")]
    public class AccountController : ApiController, IAccountController
    {
        private readonly ISimpleUserManager _simpleUserManager;

        public AccountController(ISimpleUserManager simpleUserManager)
        {
            _simpleUserManager = simpleUserManager;
        }

        [HttpDelete]
        [Route("{id:length(32)}")]
        public void Delete(string id)
        {
            _simpleUserManager.Delete(id);
        }

        [HttpGet]
        [Route("{id:length(32)}")]
        public AccountInfoModel GetById(string id)
        {
            var userData = _simpleUserManager.FindById(id);
            if (userData != null)
            {
                return new AccountInfoModel()
                {
                    UserId = userData.Id,
                    Login = userData.Login
                };
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("filter/{filter?}")]
        [Route("")]
        public IEnumerable<AccountInfoModel> Get([FromUri]string filter = null)
        {
            return _simpleUserManager.FindByFilter(filter)
                .Select(u => new AccountInfoModel
                {
                    UserId = u.Id,
                    Login = u.Login
                });

        }
    }
}