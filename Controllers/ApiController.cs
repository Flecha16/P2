using AuthSystem.Models;
using AuthSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AuthSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public ApiController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        // GET: api/APIStatistics
        [HttpGet]
        public ActionResult<IEnumerable<Statistic>> Get()
        {
            var statistics = _statisticsRepository.GetAllStatistics();
            return Ok(statistics);
        }

        // GET: api/APIStatistics/5
        [HttpGet("{id}")]
        public ActionResult<Statistic> Get(int id)
        {
            var statistic = _statisticsRepository.GetStatisticById(id);
            if (statistic == null)
            {
                return NotFound();
            }
            return Ok(statistic);
        }

        // POST: api/APIStatistics
        [HttpPost]
        public IActionResult Post([FromBody] Statistic statistic)
        {
            _statisticsRepository.CreateStatistic(statistic);
            return CreatedAtAction(nameof(Get), new { id = statistic.Id }, statistic);
        }

        // PUT: api/APIStatistics/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Statistic updatedStatistic)
        {
            var statistic = _statisticsRepository.GetStatisticById(id);
            if (statistic == null)
            {
                return NotFound();
            }
            updatedStatistic.Id = id;
            _statisticsRepository.UpdateStatistic(updatedStatistic);
            return NoContent();
        }

        // DELETE: api/APIStatistics/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var statistic = _statisticsRepository.GetStatisticById(id);
            if (statistic == null)
            {
                return NotFound();
            }
            _statisticsRepository.DeleteStatistic(id);
            return NoContent();
        }
    }
}
