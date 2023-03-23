using SmartHomeManager.Domain.DeviceStoreDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Decorators;

public abstract class ProductDiscountDecorator : IDeviceProduct
{
    protected readonly IDeviceProduct Product;
    protected double? _discountedPrice;

    public ProductDiscountDecorator(IDeviceProduct product)
    {
        Product = product;
        ApplyDiscount();
    }

    public DeviceProduct UndecoratedProduct => Product as DeviceProduct; // Add this property
    public int ProductId => Product.ProductId;
    public string ProductName => Product.ProductName;
    public string ProductBrand => Product.ProductBrand;
    public string ProductModel => Product.ProductModel;
    public string DeviceType => Product.DeviceType;
    public string ProductDescription => Product.ProductDescription;
    public double ProductPrice => _discountedPrice ?? Product.ProductPrice;

    public int ProductQuantity
    {
        get => Product.ProductQuantity;
        set => Product.ProductQuantity = value; // Implement the setter here
    }

    public string ProductImageUrl => Product.ProductImageUrl;

    protected abstract void ApplyDiscount();
}