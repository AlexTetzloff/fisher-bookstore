using System;
using Fisher.Bookstore.Models;
using Xunit;
namespace tests
{
    public class BookTest
    {
        [Fact]
        public void ChangePublicationDate()
        {
            //Arrange
            var book = new Book()
            {
                Id = 1,
                Title = "Domain Driven Design",
                Author = new Author()
                {
                    Id = 65,
                    Name = "Eric Evans"
                },
                PublishDate = DateTime.Now.AddMonths(-6),
                Publisher = "McGraw-Hill"
            };
            //Act
            var newPublicationDate = DateTime.Now.AddMonths(2);
            book.ChangePublicationDate(newPublicationDate);

            //Assert
            var expectedPublicationDate = newPublicationDate.ToShortDateString();
            var actualPublicationDate = book.PublishDate.ToShortDateString();

            Assert.Equal(expectedPublicationDate, actualPublicationDate);
        }
        [Fact]
        public void ChangeAuthor()
        {
            //Arrange
            var book = new Book()
            {
                Id = 1,
                Title = "Domain Driven Design",
                Author = new Author()
                {
                    Id = 65,
                    Name = "Eric Evans"
                },
                PublishDate = DateTime.Now.AddMonths(2),
                Publisher = "McGraw-Hill"   
            };

            //Act
            var newAuthor = new Author(){Id = 64, Name = "J.D. Salinger"};
            book.ChangeAuthor(newAuthor);

            //Assert
            var expectedAuthor = newAuthor;
            var actualAuthor = book.Author;

            Assert.Equal(expectedAuthor, actualAuthor);
        }

        [Fact]
        public void ChangeTitle()
        {
            //Arrange
            var book = new Book()
            {
                Id = 1,
                Title = "Domain Driven Design",
                Author = new Author()
                {
                    Id = 65,
                    Name = "Eric Evans"
                },
                PublishDate = DateTime.Now.AddMonths(2),
                Publisher = "McGraw-Hill"   
            };

            //Act
            var newTitle = "A War to Be Won";
            book.ChangeTitle(newTitle);

            //Assert
            var expectedTitle = newTitle.ToString();
            var actualTitle = book.Title.ToString();

            Assert.Equal(expectedTitle, actualTitle);

        }
    }
}