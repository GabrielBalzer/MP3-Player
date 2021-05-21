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
        public void VolumeChangeWorksAsExpected()
        {
            float expectedVolume = (float) 0.25;

            SongPlayer songPlayer = new SongPlayer();
            songPlayer.SetVolume(25);

            Assert.Equal(expectedVolume, songPlayer.GetCurrentVolume(), 2);

        }

        [Fact]
        public void VolumeChangeWithWrongValues()
        {
            float expectedLowValue = (float) 0.00;
            float expectedHighValue = (float) 1.00;

            SongPlayer songPlayer = new SongPlayer();
            songPlayer.SetVolume(-5);

            Assert.Equal(expectedLowValue, songPlayer.GetCurrentVolume());

            songPlayer = new SongPlayer();
            songPlayer.SetVolume(150);


            Assert.Equal(expectedHighValue, songPlayer.GetCurrentVolume(), 2);
        }
    }
}
