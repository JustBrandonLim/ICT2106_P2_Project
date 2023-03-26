using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
    public interface IRuleServicesMock
    {
        public Task<IEnumerable<Rule?>> GetAllRulesByScenarioId(Guid ScenarioId);
    }
}