using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD50.Audio
{
    public class AudioPlaybackEngine
    {
        private readonly IWavePlayer _outputDevice;
        private readonly MixingSampleProvider _mixer;
        private float _volume;

        /// <summary>
        /// Initialize a new AudioPlaybackEngine
        /// </summary>
        public AudioPlaybackEngine(int sampleRate = 44100, int channelCount = 2)
        {
            // Link outputdevice
            _outputDevice = new WaveOutEvent();

            // Create a mixer to which we can write audiostreams
            _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount))
            {
                // Keep the mixer open at all times
                ReadFully = true
            };

            _volume = 1.0f;

            // Set the outputdevice to play the steam from the mixer
            _outputDevice.Init(_mixer);
            _outputDevice.Play();

            // Create eventhandler
            _mixer.MixerInputEnded += HandlePlaybackEnd;
        }

        /// <summary>
        /// Sets the volume of sound effects
        /// </summary>
        /// <param name="volume">Volume in range [0.0 - 1.0]</param>
        public void SetVolume(float volume)
        {
            _volume = volume;

            foreach (var sfx in AudioLibrary.sounds)
            {
                sfx.SetVolume(volume);
            }
        }

        /// <summary>
        /// Play a sound effect from file
        /// </summary>
        /// <param name="fileName">Path to the sound file</param>
        public void PlaySound(string fileName)
        {
            var input = new AudioFileReader(fileName)
            {
                Volume = _volume
            };
            AddMixerInput(new AutoDisposeFileReader(input));
        }

        /// <summary>
        /// Play sound from cache
        /// </summary>
        /// <param name="sound">CachedSound object</param>
        public void PlaySound(CachedSound sound)
        {
            AddMixerInput(new CachedSoundSampleProvider(sound));
        }

        /// <summary>
        /// Convert the input sound to 2 channels
        /// </summary>
        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == _mixer.WaveFormat.Channels)
            {
                return input;
            }
            if (input.WaveFormat.Channels == 1 && _mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }
            throw new NotImplementedException("Not implemented this channel count");
        }

        /// <summary>
        /// Add a sound to the mixer
        /// </summary>
        private void AddMixerInput(ISampleProvider input)
        {
            _mixer.AddMixerInput(ConvertToRightChannelCount(input));
        }

        /// <summary>
        /// Disposes the audiodevice
        /// </summary>
        public void Dispose()
        {
            _outputDevice.Dispose();
        }

        /// <summary>
        /// Eventhandler for when a sound finishes playing
        /// </summary>
        private void HandlePlaybackEnd(object sender, SampleProviderEventArgs e)
        {
            
        }
    }

    public class CachedSound
    {
        public float[] AudioData { get; private set; }
        public WaveFormat WaveFormat { get; private set; }

        private float _volume;
        private string _audioFileName;

        public CachedSound(string audioFileName, float volume)
        {
            _volume = volume;
            _audioFileName = audioFileName;
            LoadAudioData();
        }

        /// <summary>
        /// Set the volume for the cached sound
        /// </summary>
        /// <param name="volume">Volume in range [0.0 - 1.0]</param>
        public void SetVolume(float volume)
        {
            _volume = volume;
            LoadAudioData();
        }

        /// <summary>
        /// Cache the audiodata
        /// </summary>
        private void LoadAudioData()
        {
            // Read the audiofile
            using var audioFileReader = new AudioFileReader(_audioFileName);

            // Set the volume
            audioFileReader.Volume = _volume;

            // Get the format
            WaveFormat = audioFileReader.WaveFormat;

            // Load the file to memory
            var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
            var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];

            int samplesRead;
            while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
            {
                wholeFile.AddRange(readBuffer.Take(samplesRead));
            }
            AudioData = wholeFile.ToArray();
        }
    }

    public class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound _cachedSound;
        private long _position;

        public CachedSoundSampleProvider(CachedSound cachedSound)
        {
            _cachedSound = cachedSound;
        }

        /// <summary>
        /// Read the audiodata
        /// </summary>
        public int Read(float[] buffer, int offset, int count)
        {
            var availableSamples = _cachedSound.AudioData.Length - _position;
            var samplesToCopy = Math.Min(availableSamples, count);

            Array.Copy(_cachedSound.AudioData, _position, buffer, offset, samplesToCopy);
            _position += samplesToCopy;
            return (int)samplesToCopy;
        }

        public WaveFormat WaveFormat { get { return _cachedSound.WaveFormat; } }
    }

    public class AutoDisposeFileReader : ISampleProvider
    {
        private readonly AudioFileReader _reader;
        private bool _isDisposed;
        public WaveFormat WaveFormat { get; private set; }

        public AutoDisposeFileReader(AudioFileReader reader)
        {
            _reader = reader;
            WaveFormat = reader.WaveFormat;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (_isDisposed) return 0;

            int read = _reader.Read(buffer, offset, count);
            if (read == 0)
            {
                _reader.Dispose();
                _isDisposed = true;
            }
            return read;
        }
    }
}
