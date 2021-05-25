using System;

namespace MP3Player.Logic.Ui
{
    public static class TimeConverter
    {
        public static string GetCurrentTrackTimeAsString(ISongPlayer songPlayer)
        {
            TimeSpan t = TimeSpan.FromSeconds(songPlayer.GetCurrentTrackTimeInSeconds());

            string result = string.Format("{0:D2} : {1:D2}",
                t.Minutes,
                t.Seconds);

            return result;
        }

        public static string GetAbsoluteTrackTimeAsString(ISongPlayer songPlayer)
        {
            TimeSpan t = TimeSpan.FromSeconds(songPlayer.GetTrackLengthInSeconds());

            string result = string.Format("{0:D2} : {1:D2}",
                t.Minutes,
                t.Seconds);

            return result;
        }
    }
}