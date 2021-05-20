using System;

namespace MP3Player.Logic.Ui
{
    public static class TimeConverter
    {
        public static string GetCurrentTrackTimeAsString()
        {
            TimeSpan t = TimeSpan.FromSeconds(SongPlayer.GetCurrentTrackTimeInSeconds());

            string result = string.Format("{0:D2} : {1:D2}",
                t.Minutes,
                t.Seconds);

            return result;
        }

        public static string GetAbsoluteTrackTimeAsString()
        {
            TimeSpan t = TimeSpan.FromSeconds(SongPlayer.GetTrackLengthInSeconds());

            string result = string.Format("{0:D2} : {1:D2}",
                t.Minutes,
                t.Seconds);

            return result;
        }
    }
}