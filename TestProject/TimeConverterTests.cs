using Moq;
using MP3Player.Logic.Ui;
using Xunit;

namespace TestProject
{
    public class TimeConverterTests
    {
        [Fact]
        public void TestGetCurrentTrackTimeAsString()
        {
            var expectedTime = "02 : 15";

            var songPlayerMock = new Mock<ISongPlayer>();
            songPlayerMock.Setup(p => p.GetCurrentTrackTimeInSeconds()).Returns(135);

            Assert.Equal(expectedTime, TimeConverter.GetCurrentTrackTimeAsString(songPlayerMock.Object));
        }
    }
}