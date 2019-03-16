using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
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
    public class CategoryControllerTests
    {
        private CategoryController _controller;
        private Mock<ICategoryService> _categoryServiceMock;

        [SetUp]
        public void SetUp()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _controller = new CategoryController(_categoryServiceMock.Object);
        }

        [Test]
        public async Task Index_CategoriesAreSetted()
        {
            _categoryServiceMock.Setup(x => x.GetCategoriesAsync()).ReturnsAsync(new List<Category>());

            await _controller.Index();

            _categoryServiceMock.Verify(x => x.GetCategoriesAsync(), Times.Once);
        }

        [Test]
        public async Task Index_ViewIsReturned()
        {
            _categoryServiceMock.Setup(x => x.GetCategoriesAsync()).ReturnsAsync(new List<Category>());

            var result = await _controller.Index();

            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public async Task GetImage_One_ImageIsTaken()
        {
            var categoryId = 1;

            await _controller.GetImage(categoryId);

            _categoryServiceMock.Verify(x => x.GetImageByCategoryIdAsync(categoryId), Times.Once);
        }

        [Test]
        public async Task GetImage_ExistingCategoryId_File()
        {
            _categoryServiceMock.Setup(x => x.GetImageByCategoryIdAsync(It.IsAny<int>())).ReturnsAsync(new byte[] { });

            var result = await _controller.GetImage(1);

            Assert.IsInstanceOf(typeof(FileStreamResult), result);
        }

        [Test]
        public async Task GetImage_NotExistingCategoryId_NotFound()
        {
            _categoryServiceMock.Setup(x => x.GetImageByCategoryIdAsync(It.IsAny<int>())).ReturnsAsync((byte[])null);

            var result = await _controller.GetImage(1);

            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public async Task UpdateImage_One_UploadImageView()
        {
            var result = await _controller.UpdateImage(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("UploadImageView", result.ViewName);
        }

        [Test]
        public async Task UpdateImage_IFormFileAndOne_UpdateImageAsync()
        {
            var formFileMock = new Mock<IFormFile>();

            await _controller.UpdateImage(formFileMock.Object, 1);

            _categoryServiceMock.Verify(x => x.UpdateImageAsync(It.IsAny<byte[]>(), 1), Times.Once);
        }

        [Test]
        public async Task UpdateImage_IFormFileAndOne_RedirectToIndex()
        {
            var formFileMock = new Mock<IFormFile>();

            var result = await _controller.UpdateImage(formFileMock.Object, 1) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }
    }
}
