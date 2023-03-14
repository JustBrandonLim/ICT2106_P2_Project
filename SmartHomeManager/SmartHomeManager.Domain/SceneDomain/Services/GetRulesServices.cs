using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class GetRulesServices: IGetRulesService
	{
        private readonly IRuleRepository<Rule> _ruleRepository;
        public GetRulesServices(IRuleRepository<Rule> ruleRepository)
		{
            _ruleRepository = ruleRepository;
		}
        
        public async Task<IEnumerable<Rule>> GetAllRules()
        {
            return await _ruleRepository.GetAllAsync();
        }

        public async Task<Rule?> GetRuleById(Guid id)
        {
            return await _ruleRepository.GetByIdAsync(id);
        }
    }
}

