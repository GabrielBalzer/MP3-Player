using MP3Player.Logic.Ui;
using Xunit;

namespace TestProject
{
    public class PlaylistHandlerTests
    {
        [Fact]
        public void TestIfFirstElementIsReturnedCorrectly()
        {
            PlaylistHandler playlistHandler = new PlaylistHandler();
            var expectedSingleTrack = new SingleTrack("test1", "test1", "1", true);

            playlistHandler.addSongToPlaylist(new SingleTrack("test1", "test1", "1", true));
            playlistHandler.addSongToPlaylist(new SingleTrack("test2", "test2", "2", false));

            var resultSingleTrack = playlistHandler.returnFirstTrack();

            Assert.Equal(expectedSingleTrack.FilePath, resultSingleTrack.FilePath);
            Assert.Equal(expectedSingleTrack.TrackName, resultSingleTrack.TrackName);
            Assert.Equal(expectedSingleTrack.TrackNumber, resultSingleTrack.TrackNumber);
            Assert.Equal(expectedSingleTrack.IsPlaying, resultSingleTrack.IsPlaying);
        }

        [Fact]
        public void TestIfNextElementIsReturnedAndIsPlayingStateIsChangedCorrectly()
        {
            PlaylistHandler playlistHandler = new PlaylistHandler();
            var expectedSingleTrack = new SingleTrack("testy", "testy", "2", true);

            playlistHandler.addSongToPlaylist(new SingleTrack("testx", "testx", "1", true));
            playlistHandler.addSongToPlaylist(new SingleTrack("testy", "testy", "2", false));

            var resultSingleTrack = playlistHandler.GetNextTrack();

            Assert.Equal(expectedSingleTrack.FilePath, resultSingleTrack.FilePath);
            Assert.Equal(expectedSingleTrack.TrackName, resultSingleTrack.TrackName);
            Assert.Equal(expectedSingleTrack.TrackNumber, resultSingleTrack.TrackNumber);
            Assert.Equal(expectedSingleTrack.IsPlaying, resultSingleTrack.IsPlaying);
        }
    }
}