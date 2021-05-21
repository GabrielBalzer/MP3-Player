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
            var expecteSingleTrack = new SingleTrack("test1", "test1", "1", true);

            playlistHandler.addSongToPlaylist(new SingleTrack("test1", "test1", "1", true));
            playlistHandler.addSongToPlaylist(new SingleTrack("test2", "test2", "2", false));

            var resultSingleTrack = playlistHandler.returnFirstTrack();

            Assert.Equal(expecteSingleTrack.FilePath, resultSingleTrack.FilePath);
            Assert.Equal(expecteSingleTrack.TrackName, resultSingleTrack.TrackName);
            Assert.Equal(expecteSingleTrack.TrackNumber, resultSingleTrack.TrackNumber);
            Assert.Equal(expecteSingleTrack.IsPlaying, resultSingleTrack.IsPlaying);
        }
    }
}