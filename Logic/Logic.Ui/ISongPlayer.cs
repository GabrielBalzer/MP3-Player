using NAudio.Wave;

namespace MP3Player.Logic.Ui
{
    public interface ISongPlayer
    {
        void SetVolume(double volume);
        float GetCurrentVolume();
        bool GetPauseStatus();
        void PlaySong();
        void OnPlayBackStopped(object sender, StoppedEventArgs e);
        void PauseSong();
        void StopSong();
        void PlayNextSong();
        void PlayLastSong();
        double GetTrackLengthInSeconds();
        double GetCurrentTrackTimeInSeconds();
        void DisposeAudioFileReaderAndWaveOut();
        int GetCurrentTrackProgress();
        void SetPositionInTrackToGivenSeconds(int value);

    }
}