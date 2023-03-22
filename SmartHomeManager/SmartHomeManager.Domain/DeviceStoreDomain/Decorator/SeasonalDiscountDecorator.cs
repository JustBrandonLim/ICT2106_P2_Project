using SmartHomeManager.Domain.DeviceStoreDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Decorator;

public class WinterDiscountDecorator : ProductDiscountDecorator
{
    public WinterDiscountDecorator(IDeviceProducts product) : base(product) { }

    protected override void ApplyDiscount()
    {
        _discountedPrice = _product.ProductPrice * 0.9; // 10% discount
    }
}


public class SummerDiscountDecorator : ProductDiscountDecorator
{
    public SummerDiscountDecorator(IDeviceProducts product) : base(product) { }

    protected override void ApplyDiscount()
    {
        _discountedPrice = _product.ProductPrice * 0.85; // 15% discount
    }
}
