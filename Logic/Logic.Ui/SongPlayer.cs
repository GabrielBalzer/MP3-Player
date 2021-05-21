﻿using System;
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
        private PlaylistHandler playlistHandler;

        public SongPlayer(PlaylistHandler playlistHandler)
        {
            this.waveOut.PlaybackStopped += OnPlayBackStopped;
            this.PlayBackPaused = true;
            this.playlistHandler = playlistHandler;
        }


        public void SetVolume(double volume)
        {
            if (volume >= 100)
            {
                waveOut.Volume = 1;
            }
            else if (volume <= 0)
            {
                waveOut.Volume = 0;
            }
            else
            {
                waveOut.Volume = (float)(volume / 100);
            }
            Console.WriteLine($"Player value is {waveOut.Volume}");
        }

        public float GetCurrentVolume()
        {
            return waveOut.Volume;
        }

        

        public bool getPauseStatus()
        {
            return PlayBackPaused;
        }

        public void PlaySong()
        {
            if ((audioFileReader == null) || waveOut == null)
            {
                var firstTrack = playlistHandler.returnFirstTrack();
                if (firstTrack == null)
                {
                    Console.WriteLine("No Track in Playlist");
                }
                else
                {
                    audioFileReader = new AudioFileReader(playlistHandler.returnFirstTrack().FilePath);
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
                        audioFileReader = new AudioFileReader(playlistHandler.GetNextTrack().FilePath);
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
                audioFileReader = new AudioFileReader(playlistHandler.GetNextTrack().FilePath);
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
                audioFileReader = new AudioFileReader(playlistHandler.GetLastTrack().FilePath);
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
