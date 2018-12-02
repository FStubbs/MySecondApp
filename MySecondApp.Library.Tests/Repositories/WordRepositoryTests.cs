using System.Linq;
using Microsoft.Extensions.Options;
using Moq;
using MySecondApp.Library.Models;
using MySecondApp.Library.Repositories;
using Xunit;

namespace MySecondApp.Library.Tests.Repositories
{
    public class WordRepositoryTests
    {
        [Fact]
        public void CanReverseWord()
        {
            //Arrange
            var settings = new Mock<IOptions<ApplicationSettings>>();

            var repository = new WordRepository(settings.Object);

            const string word = "MyWordToTest";

            const string expectedResult = "tseToTdroWyM";

            //Act
            var result = repository.ReverseWord(word);

            //Assert
            Assert.Equal(expectedResult, result);
        }

         [Fact]
        public void CanSayHello()
        {
            //Arrange
            var settings = new Mock<IOptions<ApplicationSettings>>();

            settings
                .SetupGet(s => s.Value)
                .Returns(new ApplicationSettings
                {
                    Greeter = "MyGreeter",
                    GreetingFormat = "Hello {0} from {1}"
                });

            var repository = new WordRepository(settings.Object);

            const string recipient = "User";

            const int iterations = 2;

            const string expectedResult = "Hello User from MyGreeter";

            //Act
            var result = repository.SayHello(recipient, iterations);

            //Assert
            var list = result.ToList();
            Assert.Equal(2, list.Count);
            Assert.True(list.All(l => l == expectedResult));
        }
    }
}