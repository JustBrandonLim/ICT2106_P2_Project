using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class ScenarioServices
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

        public Boolean ShareScenarios(Guid id)
        {
            IEnumerable<Scenario?> scenarios = _scenarioRepository.GetByProfileId(id);
            foreach(Scenario? s in scenarios)
            {
                if (s != null)
                {
                    s.IsShareable = true;
                    _scenarioRepository.UpdateAsync(s);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<Scenario?> GetScenarioByIdAsync(Guid id)
        {
            return await _scenarioRepository.GetByIdAsync(id);
        }

    }
}

