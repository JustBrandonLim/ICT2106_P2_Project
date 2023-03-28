using SmartHomeManager.Domain.DeviceStoreDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Decorators;

public class WinterDiscountDecorator : ProductDiscountDecorator
{
    public WinterDiscountDecorator(IDeviceProduct product) : base(product)
    {
    }

    protected override void ApplyDiscount()
    {
        _discountedPrice = Product.ProductPrice * 0.9; // 10% discount
    }
}