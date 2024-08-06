using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OriolOr.Maneko.API.Domain;
using OriolOr.Maneko.API.Service.Interfaces;
using System.Collections.ObjectModel;



namespace OriolOr.Maneko.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountMockController : ControllerBase
    {
        public IBalanceService AccountService;

        public AccountMockController(IBalanceService accountService)
        {
            this.AccountService = accountService;
        }

        [HttpGet("GetCurrentBalance")]
        public IActionResult  GetCurrentBalance()
        {
            return Ok(9017);
        }

        [HttpGet("GetCurrentYearData")]
        public IActionResult GetCurrentYearData()
        {
            //get data from database
            //save data in file or database.
            var balance = new Collection<MonthBalance>();
            balance.Add(new MonthBalance(){Month = MonthEnum.January.ToString(), InitialBalance = 200, FinalBalance = 90});
            balance.Add(new MonthBalance() {Month = MonthEnum.February.ToString(), InitialBalance = 1298, FinalBalance = 545});
            balance.Add(new MonthBalance() {Month = MonthEnum.March.ToString(), InitialBalance = 4959, FinalBalance = 8049});
            balance.Add(new MonthBalance() { Month = MonthEnum.April.ToString(), InitialBalance = 12034, FinalBalance = 11590 });
            balance.Add(new MonthBalance() { Month = MonthEnum.May.ToString(), InitialBalance = 12998, FinalBalance = 15455 });
            balance.Add(new MonthBalance() { Month = MonthEnum.June.ToString(), InitialBalance = 14959, FinalBalance = 13049 });

            var yearBalance = new YearBalance()
            {
                Year = 2024,
                MonthBalances = balance
            };

            return Ok(JsonConvert.SerializeObject(yearBalance));
        }

        [HttpPost("AddMonthData")]
        public IActionResult AddMonthData([FromBody] MonthBalance monthBalance)
        {
            //monthBalance.Month.
            //check if month is valid()
            //check if month and year already exists ()
            if (this.AccountService.ValidateMonth(monthBalance.Month))
            {
                this.AccountService.AddMonthBalanceToDb(monthBalance);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("UpdateCurrentYearData")]
        public IActionResult UpdateCurrentYearData([FromBody] MonthBalance data)
        {
            return Ok();
        }
    }
}