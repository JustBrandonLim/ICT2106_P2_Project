using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class ScenarioServices: IScenarioServices
	{
		private readonly IScenarioRepository<Scenario> _scenarioRepository;
		public ScenarioServices(IScenarioRepository<Scenario> scenarioRepository)
		{
			_scenarioRepository = scenarioRepository;
		}

        public async Task<IEnumerable<Scenario>> GetAllScenariosAsync()
        {
            return await _scenarioRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Scenario?>> GetScenarioByIdAsync(Guid id)
        {
            return await _scenarioRepository.GetByProfileId(id);
        }

        public async Task<IEnumerable<Scenario>> GetScenariosByShareable()
        {
            return await _scenarioRepository.GetByShareable();
        }

        //Get the scenario from profileId

        public async Task<Scenario?> GetScenarioByProfileId(Guid id)
        {
            return await _scenarioRepository.GetScenarioByProfileId(id);
        }

        //Update the profile to shareable
        public async Task<bool> UpdateScenarioByProfileId(Guid id)
        {
            Scenario? scenario = await GetScenarioByProfileId(id);
            scenario.IsShareable = true;

            if (scenario == null)
            {
                return false;
            }
            bool updateReponse = await _scenarioRepository.UpdateAsync(scenario);
            if (updateReponse)
            {
                return true;
            }

            return false;
        }
    }
}

