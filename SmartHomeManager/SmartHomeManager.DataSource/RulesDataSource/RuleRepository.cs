using System;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.DataSource.RulesDataSource
{
    public class RuleRepository : IRuleRepository<Rule>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected DbSet<Rule> _dbSet;
        public RuleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            this._dbSet = _applicationDbContext.Set<Rule>();
        }

        // Add rule
        public async Task<bool> AddAsync(Rule rule)
        {
            try
            {
                //await RuleSeedData.Seed(_applicationDbContext);
                await _applicationDbContext.AddRangeAsync(rule);
                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(Rule entity)
        {
            throw new NotImplementedException();
        }

        // Delete by Id
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                Rule? rule = await _applicationDbContext.Rules.FindAsync(id);
                if(rule != null)
                {
                    _applicationDbContext.Rules.Remove(rule);
                    return await SaveAsync();
                }
                return false;
            }
            catch
            {
                return false;
            }           
        }

        // Get all
        public async Task<IEnumerable<Rule>> GetAllAsync()
        {
            //await RuleSeedData.Seed(_applicationDbContext);
            return await _applicationDbContext.Rules.Include(d => d.Device).Include(s => s.Scenario).ToListAsync();
        }


        //Get by Id
        public async Task<Rule?> GetByIdAsync(Guid id)
        {
            try
            {
                var rule = await _applicationDbContext.Rules.Include(d => d.Device).Include(s => s.Scenario).FirstOrDefaultAsync(r => r.RuleId == id);
                return rule;
            }
            catch
            {
               return  null;
            }
        }

        //Save
        public async Task<bool> SaveAsync()
        {
            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false; 
            }
        }

        //Update
        public async Task<bool> UpdateAsync(Rule rule)
        {
            try
            {
                _applicationDbContext.Update(rule);
                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }
        //Get By ScenarioID
        public async Task<IEnumerable<Rule>> GetByScenarioId(Guid id)
        {
            //NEED TO DO THIS PART NOT SURE IF CORRECT
            return await _applicationDbContext.Rules.Where(s => s.ScenarioId == id).ToListAsync();
        }
    }
}
