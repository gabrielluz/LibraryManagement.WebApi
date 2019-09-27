using AutoMapper;
using LibraryManager.Api.Models.Dto;
using LibraryManager.Api.Models.Entities;
using LibraryManager.Api.Utils;
using NUnit.Framework;
using System;

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
        public void Map_User_Input()
        {
            //Arrange
            var sut = _mapperConfiguration.CreateMapper();
            var userInputDto = new UserInputDto
            {
                Description = "Test desc",
                Email = "Test e-mail",
                FirstName = "Gabriel",
                LastName = "Luz"
            };

            //Act
            var user = sut.Map<User>(userInputDto);

            //Assert
            Assert.That(user.Description, Is.EqualTo(userInputDto.Description));
            Assert.That(user.FirstName, Is.EqualTo(userInputDto.FirstName));
            Assert.That(user.LastName, Is.EqualTo(userInputDto.LastName));
            Assert.That(user.Email, Is.EqualTo(userInputDto.Email));
            Assert.That(user.Id, Is.EqualTo(0));
        }

        [Test]
        public void Map_User_Output()
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
        public void Map_Rental_Input()
        {
            //Arrange
            var sut = _mapperConfiguration.CreateMapper();
            var rentalInputDto = new AddRentalInputDto
            {
                UserId = 1,
                BookId = 2,
                Issued = DateTime.Now
            };

            //Act
            var rental = sut.Map<Rental>(rentalInputDto);

            //Assert
            Assert.That(rental.Book.Id, Is.EqualTo(rentalInputDto.BookId));
            Assert.That(rental.User.Id, Is.EqualTo(rentalInputDto.UserId));
            Assert.That(rental.Issued, Is.EqualTo(rentalInputDto.Issued));
            Assert.That(rental.Id, Is.EqualTo(0));
            Assert.That(rental.Returned, Is.Null);
        }

        [Test]
        public void Map_Rental_Output()
        {
            //Arrange
            var sut = _mapperConfiguration.CreateMapper();
            var rental = new Rental
            {
                Id = 123,
                Issued = DateTime.Now,
                Returned = DateTime.Now.AddDays(2),
                Book = new Book
                {
                    Id = 4
                },
                User = new User
                {
                    Id = 5
                }
            };

            //Act
            var rentalOutputDto = sut.Map<RentalOutputDto>(rental);

            //Assert
            Assert.That(rentalOutputDto.Id, Is.EqualTo(rental.Id));
            Assert.That(rentalOutputDto.Issued, Is.EqualTo(rental.Issued));
            Assert.That(rentalOutputDto.Returned, Is.EqualTo(rental.Returned));
            Assert.That(rentalOutputDto.BookId, Is.EqualTo(rental.Book.Id));
            Assert.That(rentalOutputDto.UserId, Is.EqualTo(rental.User.Id));
        }

        [Test]
        public void Map_Review_Input()
        {
            //Arrange
            var sut = _mapperConfiguration.CreateMapper();
            var reviewInputDto = new ReviewInputDto
            {
                UserId = 2,
                Comment = "Nice!",
                Rate = 10
            };

            //Act
            var review = sut.Map<Review>(reviewInputDto);

            //Assert
            Assert.That(review.User.Id, Is.EqualTo(reviewInputDto.UserId));
            Assert.That(review.Comment, Is.EqualTo(reviewInputDto.Comment));
            Assert.That(review.Rate, Is.EqualTo(reviewInputDto.Rate));
        }

        [Test]
        public void Map_Review_Output()
        {
            //Arrange
            var sut = _mapperConfiguration.CreateMapper();
            var review = new Review
            {
                User = new User { Id = 2 },
                Book = new Book { Id = 3 },
                Comment = "Nice!",
                Rate = 10,
                Id = 11
            };

            //Act
            var reviewOutputDto = sut.Map<ReviewOutputDto>(review);

            //Assert
            Assert.That(reviewOutputDto.Id, Is.EqualTo(review.Id));
            Assert.That(reviewOutputDto.UserId, Is.EqualTo(review.User.Id));
            Assert.That(reviewOutputDto.Comment, Is.EqualTo(review.Comment));
            Assert.That(reviewOutputDto.Rate, Is.EqualTo(review.Rate));
        }

        [Test]
        public void Map_Book_Input()
        {
            //Arrange
            var sut = _mapperConfiguration.CreateMapper();
            var bookInputDto = new BookInputDto
            {
                Title = "Harry Potter",
                Description = "He is a wizard!",
                Author = "J.K. Rowling"
            };

            //Act
            var book = sut.Map<Book>(bookInputDto);

            //Assert
            Assert.That(book.Id, Is.EqualTo(0));
            Assert.That(book.Title, Is.EqualTo(bookInputDto.Title));
            Assert.That(book.Description, Is.EqualTo(bookInputDto.Description));
            Assert.That(book.Author, Is.EqualTo(bookInputDto.Author));
        }

        [Test]
        public void Map_Book_Output()
        {
            //Arrange
            var sut = _mapperConfiguration.CreateMapper();
            var book = new Book
            {
                Id = 5,
                Author = "J.K. Rowling",
                Description = "Wizards!!",
                Title = "Harry Potter"
            };

            //Act
            var bookOutputDto = sut.Map<BookOutputDto>(book);

            //Assert
            Assert.That(bookOutputDto.Id, Is.EqualTo(book.Id));
            Assert.That(bookOutputDto.Author, Is.EqualTo(book.Author));
            Assert.That(bookOutputDto.Description, Is.EqualTo(book.Description));
            Assert.That(bookOutputDto.Title, Is.EqualTo(book.Title));
        }

        [Test]
        public void Configuration_Is_Valid()
        {
            _mapperConfiguration.AssertConfigurationIsValid<MappingProfile>();
        }
    }
}
