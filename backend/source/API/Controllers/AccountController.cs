﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OriolOr.Maneko.API.Domain.IdentityManagement;
using OriolOr.Maneko.API.Service.Interfaces;


namespace OriolOr.Maneko.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        public IAccountService AccountService;
        public IUserCredentialsService UserCredentialsService;

        public AccountController(IAccountService accountService, IUserCredentialsService userCredentialsService)
        {
            this.AccountService = accountService;
            this.UserCredentialsService = userCredentialsService;
        }

 
        [HttpGet("GetCurrentBalance")]
        public IActionResult GetCurrentBalance(string token)
        {

            if (this.UserCredentialsService.ValidateToken(token)) return Ok(JsonConvert.SerializeObject(this.AccountService.GetCurrentBalanceFromDb()));

            else return StatusCode(StatusCodes.Status401Unauthorized);
        }
   

        [HttpGet("GetYearData")]
        public IActionResult GetYearData(string token)
        {

            if (this.UserCredentialsService.ValidateToken(token)) return Ok(JsonConvert.SerializeObject(this.AccountService.GetYearBalanceFromDb().FirstOrDefault()));

            else return StatusCode(StatusCodes.Status401Unauthorized);
        }
    }
}