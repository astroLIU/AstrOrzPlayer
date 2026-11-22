using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AstrOrzPlayer
{
    public class PlayLists
    {
        public PlayLists()
        {
        }
        public int ID
        {
            get; set;
        }
        public string PlayListName
        {
            get; set;
        }
        public string Artist
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string Album
        {
            get; set;
        }
        public double Duration
        {
            get; set;
        }
        public string DurationString
        {
            get; set;
        }
        public string FileType
        {
            get; set;
        }
        public string FilePath
        {
            get; set;
        }
        public string MusicType
        {
            get; set;
        }
        public int PlayCount
        {
            get; set;
        }
        public int? Rate
        {
            get; set;
        }
        public bool FavSong
        {
            get; set;
        }
        public string StartTime
        {
            get; set;
        }
        public string EndTime
        {
            get; set;
        }
        public string FromFilePath
        {
            get; set;
        }
        public string FileLastModifyDate
        {
            get; set;
        }
        public DateTime CreateDate
        {
            get; set;
        }
        public DateTime? UpdateDate
        {
            get; set;
        }
    }
}
