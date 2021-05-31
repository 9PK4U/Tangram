using System;
using Xamarin.Forms;
using Tangram_V1.Droid;
using Android.Media;
using Tangram;

[assembly: Dependency(typeof(AudioService))]

namespace Tangram_V1.Droid
{
	public class AudioService : IAudio
	{
		public AudioService() { }

		private MediaPlayer _mediaPlayer;

		public bool PlayBackMusic()
		{
			_mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.backmusic);
			_mediaPlayer.Start();

			return true;
		}

	}
}