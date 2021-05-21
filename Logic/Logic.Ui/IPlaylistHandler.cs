namespace MP3Player.Logic.Ui
{
    public interface IPlaylistHandler
    {
        TrulyObservableCollection<SingleTrack> returnWholePlaylist();
        void addSongToPlaylist(SingleTrack track);
        SingleTrack returnFirstTrack();
        int getNumberOfTracks();
        SingleTrack GetNextTrack();
        SingleTrack GetLastTrack();
    }
}