using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace LD50.Audio
{
    public class BackgroundMusicManager
    {
        private static WaveOut music = null;
        private static string filePath;

        private static SampleChannel ch;

        /// <summary>
        /// Play music file from path
        /// </summary>
        /// <param name="file">Path to music file</param>
        public static void PlayMusic(string file)
        {
            if (file == filePath) return;
            filePath = file;
            WaveFileReader reader = new WaveFileReader(filePath);
            LoopStream m = new LoopStream(reader);
            ch = new SampleChannel(m)
            {
                Volume = 0.1f
            };

            if (music != null)
            {
                music.Stop();
                music.Dispose();
            }

            music = new WaveOut();
            music.Init(ch);
            music.Play();
        }

        public static void SetVolume(float volume)
        {
            ch.Volume = volume;
        }
    }

    internal class LoopStream : WaveStream
    {
        WaveStream sourceStream;

        public LoopStream(WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
            EnableLooping = true;
        }

        public bool EnableLooping { get; set; }

        public override WaveFormat WaveFormat { get { return sourceStream.WaveFormat; } }
        public override long Length { get { return sourceStream.Length; } }
        public override long Position
        {
            get { return sourceStream.Position; }
            set { sourceStream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;
            while(totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (sourceStream.Position == 0 || !EnableLooping)
                    {
                        break;
                    }
                    sourceStream.Position = 0;
                }

                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }
    }
}
