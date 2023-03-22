namespace SmartHomeManager.Domain.DeviceStoreDomain.Entities;

public interface IDeviceProducts
{
    int ProductId { get; }
    string ProductName { get; }
    string ProductBrand { get; }
    string ProductModel { get; }
    string DeviceType { get; }
    string ProductDescription { get; }
    double ProductPrice { get; }
    int ProductQuantity { get; set; }
    string ProductImageUrl { get; }
    
}
