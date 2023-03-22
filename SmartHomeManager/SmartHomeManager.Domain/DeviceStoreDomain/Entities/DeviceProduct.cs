using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Entities;

public class DeviceProduct :IDeviceProducts
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }

    [Required] public string ProductName { get; set; }

    [Required] public string ProductBrand { get; set; }

    [Required] public string ProductModel { get; set; }

    [Required] public string DeviceType { get; set; }

    [Required] public string ProductDescription { get; set; }

    [Required] public double ProductPrice { get; set; }

    [Required] public int ProductQuantity { get; set; }

    [Required] public string ProductImageUrl { get; set; }

}