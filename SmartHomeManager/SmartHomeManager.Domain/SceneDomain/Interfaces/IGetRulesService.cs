using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface IGetRulesService
	{
        // get all rules on startup
        Task<IEnumerable<Rule>> GetAllRules();

        Task<Rule?> GetRuleById(Guid id);
    }
}

