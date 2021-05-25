using System;

namespace MP3Player.Logic.Ui
{
    public static class TimeConverter
    {
        public static string GetCurrentTrackTimeAsString(ISongPlayer songPlayer)
        {
            return ConvertTimeFromSecondsToFormattedString(songPlayer.GetCurrentTrackTimeInSeconds());
        }

        public static string GetAbsoluteTrackTimeAsString(ISongPlayer songPlayer)
        {
            return ConvertTimeFromSecondsToFormattedString(songPlayer.GetTrackLengthInSeconds());
        }

        private static string ConvertTimeFromSecondsToFormattedString(double seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            string result = $"{t.Minutes:D2} : {t.Seconds:D2}";
            return result;
        }
    }
}