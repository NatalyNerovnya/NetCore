using Microsoft.AspNetCore.Mvc;
using Moq;
using NetCoreMentoring.Controllers;
using NetCoreProject.Common;
using NetCoreProject.Domain.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreMentoring.Tests
{
    [TestFixture]
    class ProductControllerTests
    {
        private ProductController _controller;
        private Mock<IProductService> _productServicceMock;
        private Mock<ICategoryService> _categoryServiceMock;
        private Mock<ISupplierService> _supplierServiceMock;

        [SetUp]
        public void Setup()
        {
            _productServicceMock = new Mock<IProductService>();
            _categoryServiceMock = new Mock<ICategoryService>();
            _supplierServiceMock = new Mock<ISupplierService>();

            _controller = new ProductController(_productServicceMock.Object, _categoryServiceMock.Object, _supplierServiceMock.Object);
        }

        [Test]
        public async Task Index_ProductsAreReturned()
        {
            _productServicceMock.Setup(x => x.GetProductsAsync()).ReturnsAsync(new List<Product>());

            await _controller.Index();

            _productServicceMock.Verify(x => x.GetProductsAsync(), Times.Once);
        }

        [Test]
        public async Task Add_ValidModel_ProductIsAdded()
        {
            var product = new Product();
            await _controller.Add(product);

            _productServicceMock.Verify(x => x.AddProductAsync(product), Times.Once);
        }

        [Test]
        public async Task Add_InvalidModel_ProductIsNotAdded()
        {
            _controller.ModelState.AddModelError("SessionName", "Required");

            await _controller.Add(new Product());

            _productServicceMock.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

        [Test]
        public async Task Add_InvalidModel_RedirectedToAdd()
        {
            _controller.ModelState.AddModelError("SessionName", "Required");

            var result = await _controller.Add(new Product()) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Add", result.ActionName);
        }

        [Test]
        public async Task Edit_ValidModel_ProductIsEdited()
        {
            var product = new Product();
            await _controller.Edit(product);

            _productServicceMock.Verify(x => x.EditProductAsync(product), Times.Once);
        }

        [Test]
        public async Task Edit_InvalidModel_RedirectedToEdit()
        {
            _controller.ModelState.AddModelError("SessionName", "Required");

            var result = await _controller.Edit(new Product() { Id = 1}) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ActionName);
        }

        [Test]
        public async Task Edit_InvalidModel_ProductIsNotEdited()
        {
            _controller.ModelState.AddModelError("SessionName", "Required");

            await _controller.Edit(new Product());

            _productServicceMock.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
        }

    }
}
