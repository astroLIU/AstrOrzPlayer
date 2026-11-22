using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace AstrOrzPlayer
{
    public class PlayList2: INotifyPropertyChanged//排序后没用
    {
        public PlayList2()
        {
        }
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set 
            {
                _id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
        }
        private string _artist;
        public string Artist
        {
            get
            {
                return _artist;
            }
            set
            {
                _artist = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Artist"));
            }
        }
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Title"));
            }
        }
        private string _album;
        public string Album
        {
            get
            {
                return _album;
            }
            set
            {
                _album = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Album"));
            }
        }
        private double _duration;
        public double Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Duration"));
            }
        }
        private string _durationString;
        public string DurationString
        {
            get
            {
                return _durationString;
            }
            set
            {
                _durationString = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DurationString"));
            }
        }
        private string _fileType;
        public string FileType
        {
            get
            {
                return _fileType;
            }
            set
            {
                _fileType = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FileType"));
            }
        }
        private string _filePath;
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FilePath"));
            }
        }
        private string _musicType;
        public string MusicType
        {
            get
            {
                return _musicType;
            }
            set
            {
                _musicType = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MusicType"));
            }
        }
        private int _playCount;
        public int PlayCount
        {
            get
            {
                return _playCount;
            }
            set
            {
                _playCount = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PlayCount"));
            }
        }
        private int? _rate;
        public int? Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                _rate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Rate"));
            }
        }
        private bool _favSong;
        public bool FavSong
        {
            get
            {
                return _favSong;
            }
            set
            {
                _favSong = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FavSong"));
            }
        }
        private string _startTime;
        public string StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("StartTime"));
            }
        }
        private string _endTime;
        public string EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EndTime"));
            }
        }
        private DateTime _createDate;
        public DateTime CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CreateDate"));
            }
        }
        private DateTime? _updateDate;
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                _updateDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UpdateDate"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
