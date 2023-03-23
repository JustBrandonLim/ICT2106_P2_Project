using SmartHomeManager.Domain.DeviceStoreDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Decorators;

public class SummerDiscountDecorator : ProductDiscountDecorator
{
    public SummerDiscountDecorator(IDeviceProduct product) : base(product)
    {
    }

    protected override void ApplyDiscount()
    {
        _discountedPrice = Product.ProductPrice * 0.85; // 15% discount
    }
}