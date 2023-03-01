using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities.DTOs;
using SmartHomeManager.Domain.SceneDomain.Services;
using SmartHomeManager.Domain.Common;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHomeManager.API.Controllers.ScenariosAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenariosController : ControllerBase
    {
        private readonly ScenarioServices _scenarioServices;
        //private readonly GetScenarioService _getScenarioService;

        public ScenariosController(IScenarioRepository<Scenario> scenarioRepository)
        {
            _scenarioServices = new(scenarioRepository);
            //_getScenarioService = new(scenarioRepository);
        }

        // GET: api/Scenarios/GetAllScenarios
        [HttpGet("GetAllScenarios")]
        public async Task<IEnumerable<ScenarioRequest>> GetAllScenarios()
        {
            var scenarios = await _scenarioServices.GetAllScenariosAsync();
            //var scenarios = await _getScenarioService.GetAllScenarios();
            var resp = scenarios.Select(scenario => new ScenarioRequest
            {
                ScenarioId = scenario.ScenarioId,
                ScenarioName = scenario.ScenarioName,
                ProfileId = scenario.ProfileId,                
            }).ToList();
            return resp;
        }

        // GET api/Scenarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Scenario>> GetScenario([FromBody] Guid id)
        {
            return null;
        }

        // POST api/Scenarios
        [HttpPost("CreateScenario")]
        public async Task<ActionResult<Scenario>> CreateScenario([FromBody] Scenario scenario)
        {
            return null;
        }

        // PUT api/Scenarios/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/Scenarios/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScenario([FromBody] Guid id)
        {
            return null;
        }
    }

}

