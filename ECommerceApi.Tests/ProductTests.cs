using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Exceptions;
using Xunit;

namespace ECommerceApi.Tests;

public class ProductTests
{
    [Fact]
    public void DecreaseStock_ReducesQuantity_WhenEnoughStock()
    {
        // Arrange: اعمل منتج فيه 10 قطع في المخزون
        var product = new Product("Laptop", 15000m, stockQuantity: 10, categoryId: 1);

        // Act: انقص 3 قطع
        product.DecreaseStock(3);

        // Assert: المفروض يبقى فاضل 7 بس
        Assert.Equal(7, product.StockQuantity);
    }

    [Fact]
    public void DecreaseStock_ThrowsException_WhenNotEnoughStock()
    {
        // Arrange: منتج فيه قطعتين بس
        var product = new Product("Mouse", 200m, stockQuantity: 2, categoryId: 1);

        // Act & Assert: حاول تنقص 5 وهي مش موجودة أصلاً
        Assert.Throws<InsufficientStockException>(() => product.DecreaseStock(5));
    }
}