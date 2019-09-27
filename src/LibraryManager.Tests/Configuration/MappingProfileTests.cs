using AutoMapper;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.Tests.Configuration
{
    public class MappingProfileTests
    {
        private MapperConfiguration _mapperConfiguration;

        [SetUp]
        public void SetUp()
        {
            _mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
        }

        [Test]
        public void Map()
        {
            //Arrange
            var sut = _mapperConfiguration.CreateMapper();
            var user = new User
            {
                Id = 1,
                Description = "Test Description",
                Email = "Test",
                FirstName = "Gabriel",
                LastName = "Luz"
            };
            
            //Act
            var dto = sut.Map<User, UserOutputDto>(user);

            //Assert
            Assert.That(dto.Id, Is.EqualTo(1));
            Assert.That(dto.FirstName, Is.EqualTo("Gabriel"));
            Assert.That(dto.LastName, Is.EqualTo("Luz"));
            Assert.That(dto.Email, Is.EqualTo("Test"));
            Assert.That(dto.Description, Is.EqualTo("Test Description"));
        }

        [Test]
        public void Configuration_Is_Valid()
        {
            _mapperConfiguration.AssertConfigurationIsValid<MappingProfile>();
        }
    }
}
