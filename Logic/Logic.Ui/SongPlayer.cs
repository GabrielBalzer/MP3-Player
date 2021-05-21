using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using NAudio.Wave;

namespace MP3Player.Logic.Ui
{
    public class SongPlayer
    {
        private WaveOutEvent waveOut = new WaveOutEvent();
        private  AudioFileReader audioFileReader;
        private bool PlayBackPaused;

        public SongPlayer()
        {
            waveOut.PlaybackStopped += OnPlayBackStopped;
            PlayBackPaused = true;
        }


        public void SetVolume(double volume)
        {
            waveOut.Volume = (float) (volume / 100);
            Console.WriteLine($"Player value is {waveOut.Volume}");
        }

        

        public bool getPauseStatus()
        {
            return PlayBackPaused;
        }

        public void PlaySong()
        {
            if ((audioFileReader == null) || waveOut == null)
            {
                var firstTrack = PlaylistHandler.returnFirstTrack();
                if (firstTrack == null)
                {
                    Console.WriteLine("Not possible");
                }
                else
                {
                    audioFileReader = new AudioFileReader(PlaylistHandler.returnFirstTrack().FilePath);
                    waveOut.Init(audioFileReader);

                    PlayBackPaused = false;
                    waveOut.Play();
                }
                
            }
            else
            {
                if (PlayBackPaused)
                {
                    try
                    {
                        waveOut.Play();
                        PlayBackPaused = false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else
                {
                        audioFileReader = new AudioFileReader(PlaylistHandler.GetNextTrack().FilePath);
                        waveOut.Stop();
                        waveOut.Init(audioFileReader);
                        waveOut.Play();
                        PlayBackPaused = false;
                }
            }
        }

        public void OnPlayBackStopped(object sender, StoppedEventArgs e)
        {
            Console.WriteLine("PlaybackstoppedEvent");
            PlaySong();
        }

        public void PauseSong()
        {
            waveOut.Pause();
            PlayBackPaused = true;
        }

        public void StopSong()
        {
            waveOut.Stop();
        }

        public void PlayNextSong()
        {
            if (!PlayBackPaused)
            {
                waveOut.PlaybackStopped -= OnPlayBackStopped;
                audioFileReader = new AudioFileReader(PlaylistHandler.GetNextTrack().FilePath);
                waveOut.Stop();
                waveOut.Init(audioFileReader);
                waveOut.Play();
                waveOut.PlaybackStopped += OnPlayBackStopped;
            }
        }

        public void PlayLastSong()
        {
            if (!PlayBackPaused)
            {
                waveOut.PlaybackStopped -= OnPlayBackStopped;
                audioFileReader = new AudioFileReader(PlaylistHandler.GetLastTrack().FilePath);
                waveOut.Stop();
                waveOut.Init(audioFileReader);
                waveOut.Play();
                waveOut.PlaybackStopped += OnPlayBackStopped;
            }
        }

        public double GetTrackLengthInSeconds()
        {
            if (audioFileReader != null)
            {
                return audioFileReader.TotalTime.TotalSeconds;

            }
            else
            {
                return 0;
            }
        }

        public double GetCurrentTrackTimeInSeconds()
        {
            if (audioFileReader != null)
            {
                var currentTime = audioFileReader.CurrentTime.TotalSeconds;

                return Math.Floor(currentTime);
            }
            else
            {
                return 0;
            }
        }

        public void ClearSongReader()
        {
            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
            }

            if (waveOut != null)
            {
                waveOut.Dispose();
            }
            
        }

        public int GetCurrentTrackProgress()
        {
            if (audioFileReader != null)
            {
                try
                {
                    var currentTime = Math.Floor(audioFileReader.CurrentTime.TotalSeconds);
                    var totalTime = Math.Floor(audioFileReader.TotalTime.TotalSeconds);

                    //Console.WriteLine("Progress " + (currentTime / totalTime));
                    return (int)((currentTime / totalTime) * 1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return 0;
                }


            }
            else
            {
                return 0;
            }
        }

        public void SetPosition(int value)
        {
            if (audioFileReader != null)
            {
                var totalTimeSteps = (Math.Floor(audioFileReader.TotalTime.TotalSeconds) / 1000);
                var currentTime = totalTimeSteps * value;

                audioFileReader.CurrentTime = TimeSpan.FromSeconds(currentTime);
            }
        }
    }
}
