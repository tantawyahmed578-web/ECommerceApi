using Moq;
using Xunit;
using FluentAssertions;
using ECommerceApi.Application.Services;
using ECommerceApi.Domain.Interfaces;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Application.DTOs.ProductDto;
using AutoMapper;

namespace ECommerceApi.Tests;

public class ProductServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        // 1. تهيئة الكائنات الوهمية للمستودع والمابّر
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();

        // 2. حقن الكائنات الوهمية داخل الخدمة الحقيقية
        _productService = new ProductService(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProductDto_WhenProductExists()
    {
        // Arrange
        var productId = 1;
        var fakeProduct = new Product("Laptop", 15000m, 10, 1);

        // تجهيز الـ DTO الوهمي (تأكد أن الخصائص تطابق الموجودة في ProductDto عندك)
        var fakeProductDto = new ProductDto { Id = productId, Name = "Laptop", Price = 15000m };

        // برمجة واجهة المستودع لترجع المنتج المزيف
        _mockUnitOfWork.Setup(u => u.Products.GetByIdAsync(productId))
                       .ReturnsAsync(fakeProduct);

        // برمجة المابّر ليرجع الـ DTO عند محاولة تحويل المنتج
        _mockMapper.Setup(m => m.Map<ProductDto>(fakeProduct))
                   .Returns(fakeProductDto);

        // Act
        var result = await _productService.GetByIdAsync(productId);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Laptop");

        // التأكد من استدعاء الدوال الصحيحة
        _mockUnitOfWork.Verify(u => u.Products.GetByIdAsync(productId), Times.Once);
        _mockMapper.Verify(m => m.Map<ProductDto>(fakeProduct), Times.Once);
    }
}