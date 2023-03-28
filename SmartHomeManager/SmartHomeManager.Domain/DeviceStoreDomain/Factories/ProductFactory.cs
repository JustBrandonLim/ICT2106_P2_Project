using SmartHomeManager.Domain.DeviceStoreDomain.Decorators;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;

namespace SmartHomeManager.Domain.DeviceStoreDomain.Factories;

public class ProductFactory
{
    public static IDeviceProduct CreateProductWithSummerDiscountDecorator(IDeviceProduct product)
    {
        return new SummerDiscountDecorator(product);
    }

    public static IDeviceProduct CreateProductWithWinterDiscountDecorator(IDeviceProduct product)
    {
        return new WinterDiscountDecorator(product);
    }
}