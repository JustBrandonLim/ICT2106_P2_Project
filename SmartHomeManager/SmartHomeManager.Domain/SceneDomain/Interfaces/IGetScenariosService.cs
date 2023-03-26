using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface IGetScenariosService
	{
        // get all rules on startup and when director is informed
        Task<IEnumerable<Scenario>> GetAllScenarios();

        Task<Scenario?> GetScenarioById(Guid id);
    }
}

