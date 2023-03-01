using SmartHomeManager.Domain.AccountDomain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.SceneDomain.Entities
{
    public class Scenario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ScenarioId { get; set; }

        [Required]
        public string ScenarioName { get; set; }

        [Required]
        public Guid ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }

        [Required]
        public Boolean IsActive { get; set; }

        [Required]
        public Boolean IsShareable { get; set; }
    }
}
