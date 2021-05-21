using System;
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

            SongPlayer songPlayer = new SongPlayer();

            Assert.Equal(expectedPauseStatus, songPlayer.getPauseStatus());
        }

        [Fact]
        public void PlayerStaysInPauseStatusOnBackwardButton()
        {
            bool expectedPauseStatus = true;

            SongPlayer songPlayer = new SongPlayer();
            songPlayer.PlayLastSong();

            Assert.Equal(expectedPauseStatus, songPlayer.getPauseStatus());
        }


    }
}
