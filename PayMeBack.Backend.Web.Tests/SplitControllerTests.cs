using PayMeBack.Backend.Web.Controllers;
using PayMeBack.Backend.Web.Models;
using System;
using Xunit;

namespace PayMeBack.Backend.Web.Tests
{
    public class SplitControllerTests
    {
        private SplitsController _controller = new SplitsController();

        [Fact]
        public void GetSplits_ReturnsSplits()
        {
            var controller = new SplitsController();

            var splits = controller.Get();

            Assert.NotEmpty(splits);
        }

        [Fact]
        public void GetSplit_GetOneById_ReturnsSplit()
        {
            var controller = new SplitsController();

            var split = controller.Get(1);

            Assert.Equal(1, split.Id);
        }

        [Fact]
        public void CreateSplit_ReturnsTheCreatedSplit()
        {
            var splitCreationDto = CreateSplit();

            Assert.InRange(splitCreationDto.Id, 2, 1000);

            Assert.Equal(splitCreationDto.Name, splitCreationDto.Name);
        }

        [Fact]
        public void CreateSplit_AddsTheSplitInCollection()
        {
            var splitCreationDto = CreateSplit();

            var splitDto = _controller.Get(splitCreationDto.Id);

            Assert.Equal(splitCreationDto.Name, splitDto.Name);
        }

        private SplitDto CreateSplit()
        {
            var splitCreationDto = new SplitCreationDto { Name = "Tomorrow", Created = new DateTime(2016, 12, 05, 12, 30, 58) };

            return _controller.Create(splitCreationDto);
        }
    }
}
