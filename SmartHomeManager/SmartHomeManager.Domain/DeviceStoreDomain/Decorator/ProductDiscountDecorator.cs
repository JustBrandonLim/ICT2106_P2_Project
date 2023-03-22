using SmartHomeManager.Domain.DeviceStoreDomain.Interfaces;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Decorator;
public abstract class ProductDiscountDecorator : IDeviceProducts
{
    protected readonly IDeviceProducts _product;
    protected double? _discountedPrice;

    public ProductDiscountDecorator(IDeviceProducts product)
    {
        _product = product;
        ApplyDiscount();
    }
    public DeviceProduct UndecoratedProduct => _product as DeviceProduct; // Add this property

    public int ProductId => _product.ProductId;
    public string ProductName => _product.ProductName;
    public string ProductBrand => _product.ProductBrand;
    public string ProductModel => _product.ProductModel;
    public string DeviceType => _product.DeviceType;
    public string ProductDescription => _product.ProductDescription;
    public double ProductPrice => _discountedPrice ?? _product.ProductPrice;
    public int ProductQuantity
    {
        get => _product.ProductQuantity;
        set => _product.ProductQuantity = value; // Implement the setter here
    }
    public string ProductImageUrl => _product.ProductImageUrl;

    protected abstract void ApplyDiscount();

}
