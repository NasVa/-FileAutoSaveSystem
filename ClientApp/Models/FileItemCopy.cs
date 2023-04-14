using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ClientApp.Models
{
    public class FileItemCopy
    {
        private string hash;
        private string path;
        private FileItem parentItem;
        private FileItemCopy prevCopy;
        private DateTime creationTime;

        public string Hash
        {
            get { return hash; }
            set
            {
                hash = value;
                OnPropertyChanged("Hash");
            }
        }
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }
        public FileItem ParentItem
        {
            get { return parentItem; }
            set
            {
                parentItem = value;
                OnPropertyChanged("ParentItem");
            }
        }

        public FileItemCopy PrevCopy
        {
            get { return prevCopy; }
            set
            {
                prevCopy = value;
                OnPropertyChanged("PrevCopy");
            }
        }

        public DateTime CreationTime
        {
            get { return creationTime; }
            set
            {
                creationTime = value;
                OnPropertyChanged("CreationTime");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
