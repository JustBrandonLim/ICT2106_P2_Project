using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHomeManager.API.Controllers.RulesAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesControllerMock : ControllerBase
    {
        private readonly RuleServicesMock _ruleServicesMock;

        public RulesControllerMock(RuleServicesMock ruleServicesMock)
        {
            _ruleServicesMock = ruleServicesMock;
        }

        // GET api/Rules/GetByScenariosId/AC38AF14-9A57-4DF3-89F3-78F9CE9F4983
        [HttpGet("GetByScenariosId/{scenarioId}")]
        public async Task<ActionResult<Rule>> GetRulesByScenarioId(Guid scenarioId)
        {
            //TODO NOT SURE IF CORRECT
            var rules = await _ruleServicesMock.GetAllRulesByScenarioId(scenarioId);
            if (rules != null)
            {
                return Ok(rules);
            }
            return StatusCode(404, "rule not exist");
        }
    }
}

