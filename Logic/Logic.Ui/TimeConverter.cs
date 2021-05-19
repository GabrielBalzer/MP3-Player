using System;

namespace MP3Player.Logic.Ui
{
    public static class TimeConverter
    {
        public static string getCurrentTrackTimeAsString()
        {
            TimeSpan t = TimeSpan.FromSeconds(SongPlayer.GetCurrentTrackTimeInSeconds());

            string result = string.Format("{0:D2} : {1:D2}",
                t.Minutes,
                t.Seconds);

            return result;
        }
    }
}