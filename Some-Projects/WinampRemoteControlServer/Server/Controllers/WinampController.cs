using System;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Winamp.Models;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace Server.Controllers
{    
    public class WinampController : BaseController
    {
        public static WinAmpSDK.Winamp wa;

        [HttpGet]
        public HttpResponseMessage GetPlayList()
        {
            var response = this.PerformOperation(() => {
                    ValidateWinamp();
                    var songs = wa.GetSongsFromPlaylist();
                    return songs;              
            });
            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetCurrentInfo()
        {
            var response = this.PerformOperation(() =>
            {
                ValidateWinamp();
                var curSong = wa.CurrentSongInfo();
                return curSong;
            });
            return response;
        }

        [HttpPost]
        public HttpResponseMessage PlaySong(Command comand)
        {
            return this.PerformOperation(() => {
                ValidateWinamp();
                wa.Play((uint)comand.Param);
            });           
        }

        [HttpGet]
        public HttpResponseMessage Play()
        {
            return this.PerformOperation(() => {
                ValidateWinamp();
                wa.Play();
            });      
        }

        [HttpGet]
        public HttpResponseMessage Pause()
        {
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                wa.Pause();
            });
        }

        [HttpGet]
        public HttpResponseMessage Stop()
        {
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                wa.Stop();
            });
        }

        [HttpGet]
        public HttpResponseMessage Next()
        {
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                wa.NextSong();
            });
        }

        [HttpGet]
        public HttpResponseMessage Prev()
        {
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                wa.PrevSong();
            });
        }

        [HttpPost]
        public HttpResponseMessage SetVolume(Command command)
        {
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                wa.Volume = command.Param;
            });
        }

        [HttpGet]
        public HttpResponseMessage GetVolume()
        {
            return this.PerformOperation(() => {
                ValidateWinamp();
                return wa.Volume;
            });
        }

        [HttpGet]
        public HttpResponseMessage VolumeUp()
        {
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                wa.Volume += 15;
            });
        }

        [HttpGet]
        public HttpResponseMessage VolumeDown()
        {
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                wa.Volume -= 15;
            });
        }

        [HttpPost]
        public HttpResponseMessage SetRepeat(Command command)
        {
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                wa.Repeat = command.Param;
            });
        }

        [HttpGet]
        public HttpResponseMessage GetRepeat()
        {
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                return wa.Repeat;
            });
        }

        [HttpPost]
        public HttpResponseMessage SetShuffle(Command command)
        {
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                wa.Shuffle = command.Param;
            });
        }

        [HttpGet]
        public HttpResponseMessage GetShuffle()
        {
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                return wa.Shuffle;
            });
        }

        [HttpGet]
        public HttpResponseMessage Close()
        {
            // todo
            return this.PerformOperation(() =>
            {
                ValidateWinamp();
                wa.Close();
            });
        }

        [HttpGet]
        public HttpResponseMessage Start() 
        {

            var response = this.PerformOperation(() => {
                InstanceWinamp();
            });
            return response;
        }

        private void InstanceWinamp() 
        {
            wa = new WinAmpSDK.Winamp(true);
            Thread.Sleep(1000);
            wa = new WinAmpSDK.Winamp(true);
          
        }

        private void ValidateWinamp() 
        {
            if (wa == null)
            {
                throw new ServerErrorException("Winamp not started", "WIN_NO_RUN");
            }
            else if (wa.Handle.ToInt32() == 0)
            {
                throw new ServerErrorException("Winamp not started or you are not connected.", "WIN_NO_RUN");
            }
        }
    }
}
