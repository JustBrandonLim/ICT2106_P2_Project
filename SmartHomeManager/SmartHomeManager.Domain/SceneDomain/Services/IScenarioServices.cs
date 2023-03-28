namespace SmartHomeManager.Domain.SceneDomain.Services
{
    public interface IScenarioServices
    {
        public Task<bool> UpdateScenarioByProfileId(Guid id);
    }
}