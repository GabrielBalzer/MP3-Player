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

        [Fact]
        public void PlayerStaysInPauseStatusOnForwardButton()
        {
            bool expectedPauseStatus = true;

            SongPlayer songPlayer = new SongPlayer();
            songPlayer.PlayNextSong();

            Assert.Equal(expectedPauseStatus, songPlayer.getPauseStatus());
        }

        [Fact]
        public void CheckIfVolumeChangeWorksAsExpected()
        {
            float expectedVolume = (float) 0.25;

            SongPlayer songPlayer = new SongPlayer();
            songPlayer.SetVolume(25);

            Assert.Equal(expectedVolume, songPlayer.GetCurrentVolume(), 2);

        }
    }
}
