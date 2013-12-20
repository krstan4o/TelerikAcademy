using System;
using System.Diagnostics;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using Winamp.Models;

namespace WinAmpSDK
{
    public class Winamp
    {
        public IntPtr Handle;
        public string WA_Path;
        public Process WinAmpProcess;
        public bool Exited;

        public static IntPtr SendCommandToWinamp(IntPtr hWnd, WM_COMMAND_MSGS Command, uint lParam)
        {
            return Win32.SendMessage(hWnd, Win32.WM_COMMAND, (int)Command, lParam);
        }
        private static int ToInt32(bool b)
        {
            return (b) == true ? 1 : 0; //Shorthand for if statement
        }
        private static bool ToBool(int i)
        {
            return (i) == 1 ? true : false;
        }

        /// <summary>
        /// Creates a Winamp object and binds it to a running instance of winamp. 
        /// Throws a "Winamp instance not found" Exception if no winamp is running.
        /// </summary>
        public Winamp()
        {
            StartWinAmp();
        }
        private void StartWinAmp()
        {
            //IntPtr hWnd = Win32.FindWindow("BaseWindow_RootWnd", null);
            IntPtr hWnd = Win32.FindWindow("Winamp v1.x", null);

            if (hWnd.ToInt32() == 0)
            {
                throw new Exception("Winamp instance not found.");
            }
            else
                WinAmpProcess = Process.GetProcessesByName("Winamp")[0];

            WinAmpProcess.EnableRaisingEvents = true;
            WinAmpProcess.Exited += new EventHandler(WinAmpProcess_Exited);

            this.Handle = hWnd;
            Exited = false;
            WA_Path = ((string)Registry.GetValue("HKEY_CLASSES_ROOT\\Winamp.File\\shell\\open\\command", null, null)).Split(new char[] { '"' })[1];
        }
        public Winamp(bool bAutoStart)
        {
            if (bAutoStart)
            {
                try
                {
                    StartWinAmp();
                }
                catch
                {
                    // 'HCR\Applications\Winamp.exe\shell\command' - Path to WinAmp Command
                    string path = Microsoft.Win32.Registry.GetValue(@"HKEY_CLASSES_ROOT\Winamp.File\shell\open\command",null, null).ToString();
                    WA_Path = path.Split(new char[] { '"' })[1];
                    if (WA_Path != null)
                    {
                      
                        WinAmpProcess = Process.Start(WA_Path);
                    }
                    else
                    {
                        throw new Exception("No installation of winamp detected.");
                    }
                    new Winamp(bAutoStart);               
                }
            }
        }
        void WinAmpProcess_Exited(object sender, EventArgs e)
        {
            Handle = (IntPtr)0;
            WinAmpProcess = null;
            Exited = true;
        }

        #region Winamp Commands
        public int PlayListLength { get { return (int)Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_USER, 1, (uint)WA_IPC.IPC_GETLISTLENGTH); } }
        public int Shuffle 
        {
            get { return (int)Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_USER, 0, (uint)WA_IPC.IPC_GET_SHUFFLE); } 
            set
            {
                Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_USER, (int)value, (uint)WA_IPC.IPC_SET_SHUFFLE);               
            }        
        }
        public int Repeat
        {
            get { return (int)Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_USER, 0, (uint)WA_IPC.IPC_GET_REPEAT); }
            set
            {
                Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_USER, (int)value, (uint)WA_IPC.IPC_SET_REPEAT);
            }
        }
        public int Volume 
        {
            get { return (int)Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_USER, -666, (uint)WA_IPC.IPC_SETVOLUME); }
            set
            {
                if (value > 255)
                {
                    value = 255;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_USER, value, (uint)WA_IPC.IPC_SETVOLUME);
            }
        }
        public void Close() 
        {
            Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_COMMAND, 40001, 0);
        }
        /// <summary>
        /// Gets the playback status
        /// </summary>
        /// <returns>1-if playng;3-if paused; other- stoped</returns>
        public int GetPlayBackStatus() 
        {
            return (int)Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_USER, 0, (uint)104);
        }
        public void VolumeUp() 
        {
            Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_COMMAND, (int)WM_COMMAND_MSGS.WINAMP_VOLUMEUP, 30);
        }
        public void VolumeDown()
        {
            Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_COMMAND, (int)WM_COMMAND_MSGS.WINAMP_VOLUMEDOWN, 30);
        }
        public PlayListSong[] GetSongsFromPlaylist()
        {
            int length = this.PlayListLength;
            PlayListSong[] songs = new PlayListSong[length];          

            for (int i = 0; i < length; i++)
            {
                var buffer = new string('0', 2048);
                string filePath = Marshal.PtrToStringUni(Win32.GetPlaylistFileByPosition((uint)i));
                string songLen = Marshal.PtrToStringUni(Win32.GetMetaDataFromFile(filePath, "Length", buffer, 2048));
                int songLength = -1;
                bool isParsed = int.TryParse(songLen, out songLength);
                string title = Marshal.PtrToStringUni(Win32.GetPlaylistTitleByPosition(i));
                
                string songLengthToSeconds = songLength / 60000 + ":" + ((songLength % 60000) / 1000);
                if (songLengthToSeconds.Length <= 3)
                {
                    songLengthToSeconds += "0";
                }
                songs[i] = new PlayListSong { Position = i, Length = songLengthToSeconds, Title = title};
            }
            return songs;
        }
        public void Play() 
        {
            Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_COMMAND, (int)WM_COMMAND_MSGS.WINAMP_PlayBtn, 0);
        }
        public void Play(uint position)
        {
            Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_USER, (int)position, (uint)WM_USER_MSGS.WA_PLAYLIST_JUMP);
            this.Play();
        }
        public void Pause()
        {
            Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_COMMAND, (int)WM_COMMAND_MSGS.WINAMP_PauseBtn, 0);
        }
        public void Stop()
        {
            Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_COMMAND, (int)WM_COMMAND_MSGS.WINAMP_StopBtn, 0);
        }
        public void PrevSong()
        {
            Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_COMMAND, (int)WM_COMMAND_MSGS.WINAMP_PrevSongBtn, 0);
        }
        public void NextSong()
        {
            Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_COMMAND, (int)WM_COMMAND_MSGS.WINAMP_NextSongBtn, 0);
        }
        public CurrentSongInfo CurrentSongInfo() 
        {
            var song = new CurrentSongInfo();
            var status = this.GetPlayBackStatus();
            if (status == 1)
	        {
                song.WinampStatus = "Playing";
	        }
            else if (status == 3)
            {
                song.WinampStatus = "Paused";
            }
            else
            {
                song.WinampStatus = "Stoped";
            }

            if (status == 1 || status == 3)
            {
                 var buffer = new string('0', 2048);
                 string curFilePath = Marshal.PtrToStringUni(Win32.GetCurrentPlaylistFile());
                 song.PathToFile = curFilePath;

                 string songLenStr = Marshal.PtrToStringUni(Win32.GetMetaDataFromFile(curFilePath, "Length", buffer, 2048));
                 //songLenStr = songLenStr ?? "";
                 int songLengthInMiliSeconds = -1;
                 bool wtf = int.TryParse(songLenStr, out songLengthInMiliSeconds);
                 int songLengthToSeconds = songLengthInMiliSeconds / 1000;

                 string title = Marshal.PtrToStringUni(Win32.GetCurrentPlaylistTitle())?? "";
                 title = title ?? "";
                 string artist = Marshal.PtrToStringUni(Win32.GetMetaDataFromFile(curFilePath, "Artist", buffer, 2048));
                 artist = artist ?? "";
                 string album = Marshal.PtrToStringUni(Win32.GetMetaDataFromFile(curFilePath, "Album", buffer, 2048));
                 album = album ?? ""; 
                 string year = Marshal.PtrToStringUni(Win32.GetMetaDataFromFile(curFilePath, "Year", buffer, 2048));
                 year = year ?? "";
                 string bitRate = Marshal.PtrToStringUni(Win32.GetMetaDataFromFile(curFilePath, "Bitrate", buffer, 2048));
                 bitRate = bitRate ?? "";

                 song.Title = title;
                 song.Artist = artist;
                 song.Album = album;
                 song.Year = year;
                 song.Bitrate = bitRate;
                 if (songLengthToSeconds % 60 < 10)
                 {
                     song.Length = songLengthToSeconds / 60 + ":0" + songLengthToSeconds % 60;
                 }
                 else
                 {
                     song.Length = songLengthToSeconds / 60 + ":" + songLengthToSeconds % 60;
                 }
                 

                 int elapsedInMiliSeconds = (int)Win32.SendMessage(this.Handle, (int)WA_MsgTypes.WM_USER, 0, 105);
                 int elapsedToSeconds = elapsedInMiliSeconds / 1000;
                 if (elapsedToSeconds % 60 < 10)
                 {
                     song.Elapsed = string.Format("{0}:0{1}", elapsedToSeconds / 60, elapsedToSeconds % 60);
                 }
                 else
                 {
                     song.Elapsed = string.Format("{0}:{1}", elapsedToSeconds / 60, elapsedToSeconds % 60);
                 }

            }
            else
            {
                song.Title = "";
                song.Length = "";
                song.PathToFile = "";
                song.Year = "";
                song.Positon = -1;
                song.Elapsed = "";
                song.Artist = "";
                song.Album = "";
                song.Bitrate = "";
            }
            return song;
        }

        #endregion
    }
}
