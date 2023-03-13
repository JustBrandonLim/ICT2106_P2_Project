using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.DirectorDomain.Services;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System.Data;
using Rule = SmartHomeManager.Domain.SceneDomain.Entities.Rule;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class RuleServices
	{
		private readonly IRuleRepository<Rule> _ruleRepository;
		private readonly IInformDirectorServices _informDirectorServices;

		//Initialise the service by passing the repo
		public RuleServices(IRuleRepository<Rule> ruleRepository, IInformDirectorServices informDirectorServices)
		{
			_ruleRepository = ruleRepository;
			_informDirectorServices = informDirectorServices;
        }

		//Get all
		public async Task<IEnumerable<Rule>> GetAllRulesAsync()
		{
			return await _ruleRepository.GetAllAsync();
		}

		//Get using id
		public async Task<Rule?> GetRuleByIdAsync(Guid id)
		{
			return await _ruleRepository.GetByIdAsync(id);
		}

		//Create
		public async Task<bool> CreateRuleAsync(Rule rule)
		{
			if(await _ruleRepository.AddAsync(rule))
			{
				_informDirectorServices.InformRuleChangesAsync(rule.RuleId, 'c');
				return true;
			}

			return false;
        }

		//Update
		public async Task<bool> EditRuleAsync(Rule rule)
		{
            if (await _ruleRepository.UpdateAsync(rule))
            {
                _informDirectorServices.InformRuleChangesAsync(rule.RuleId, 'u');
                return true;
            }

            return false;
        }

		//Delete using Id
		public async Task<bool> DeleteRuleByIdAsync(Guid id)
		{
			if (await _ruleRepository.DeleteByIdAsync(id))
			{
                _informDirectorServices.InformRuleChangesAsync(id, 'd');
				return true;

            }
            
			return false;
        }

        //TODO not sure what's the right way
        public async Task<IEnumerable<Rule?>> GetAllRulesByScenarioId(Guid ScenarioId)
        {
            return await _ruleRepository.GetByScenarioId(ScenarioId);
        }
    }
}

