﻿using Microsoft.AspNetCore.Mvc;
using OriolOr.Maneko.API.Domain.IdentityManagement;
using OriolOr.Maneko.API.Service.Interfaces;

namespace OriolOr.Maneko.API.Controllers
{
    public class UserDataController : Controller
    {
        public IUserCredentialsService UserCredentialsService;

        public UserDataController() { }

        [HttpGet("GetLoginCredentials")]
        public IActionResult GetLoginCredentials() {

            var userCredentials = new UserCredentials();

            UserCredentialsService.Authenticate(userCredentials);
            return Ok(); 
        }
    }
}
