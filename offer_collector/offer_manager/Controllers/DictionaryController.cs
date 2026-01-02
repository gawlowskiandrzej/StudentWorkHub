using Microsoft.AspNetCore.Mvc;
using Offer_collector.Models.DatabaseService;
using offer_manager.Models.Dictionaries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace offer_manager.Controllers
{
    [Route("api/dictionary")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly DBService _dbService;

        public DictionaryController(DBService dbService)
        {
            _dbService = dbService;
        }
        // GET: api/<DictionaryController>
        [HttpGet("searchview-dictionaries")]
        public async Task<IActionResult> SearchDictionaries()
        {
            try
            { 
                return Ok(await _dbService.GetDictionaries(new List<string> { "employment_types", "employment_schedules", "salary_periods" }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
