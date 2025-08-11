using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceService.Data;

namespace FinanceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CurrencyController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetCurrencies()
        {
            var currencies = _db.Currency.ToList();
            return Ok(currencies);
        }
    }
}
