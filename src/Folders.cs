using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AstrOrzPlayer
{
    public class Folders
    {
        public Folders()
        {
        }
        public int ID
        {
            get; set;
        }
        public string FolderName
        {
            get; set;
        }
        public string FolderFilePath
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
