using System;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class RuleServicesMock : IRuleServicesMock
	{
        private readonly IRuleRepository<Rule> _ruleRepository;

        public RuleServicesMock(IRuleRepository<Rule> ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }

        public async Task<IEnumerable<Rule?>> GetAllRulesByScenarioId(Guid ScenarioId)
        {
            return await _ruleRepository.GetByScenarioId(ScenarioId);
        }
    }
}

