using System;
using System.IO;
using Moq;
using MP3Player.Logic.Ui;
using Xunit;

namespace TestProject
{
    public class SongPlayerTests
    {
        [Fact]
        public void PlayerStartsInPauseState()
        {
            bool expectedPauseStatus = true;

            var playListHandlerMock = new Mock<IPlaylistHandler>();
            SongPlayer songPlayer = new SongPlayer(playListHandlerMock.Object);

            Assert.Equal(expectedPauseStatus, songPlayer.GetPauseStatus());
        }

        [Fact]
        public void PlayerStaysInPauseStatusOnBackwardButton()
        {
            bool expectedPauseStatus = true;

            var playListHandlerMock = new Mock<IPlaylistHandler>();
            SongPlayer songPlayer = new SongPlayer(playListHandlerMock.Object);
            songPlayer.PlayLastSong();

            Assert.Equal(expectedPauseStatus, songPlayer.GetPauseStatus());
        }

        [Fact]
        public void PlayerStaysInPauseStatusOnForwardButton()
        {
            bool expectedPauseStatus = true;

            var playListHandlerMock = new Mock<IPlaylistHandler>();
            SongPlayer songPlayer = new SongPlayer(playListHandlerMock.Object);
            songPlayer.PlayNextSong();

            Assert.Equal(expectedPauseStatus, songPlayer.GetPauseStatus());
        }

        [Fact]
        public void CheckIfSongStartsPlayingIfNoTrackInPlaylist()
        {

            string expectedResult = "No Track in Playlist";

            var playListHandlerMock = new Mock<IPlaylistHandler>();
            playListHandlerMock.Setup(p => p.returnFirstTrack()).Returns((SingleTrack)null);

            SongPlayer songPlayer = new SongPlayer(playListHandlerMock.Object);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                songPlayer.PlaySong();

                var result = sw.ToString().Trim();
                Assert.Equal(expectedResult, result);
            }
        }
    }
}
