using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class FileItem
    {
        private int id;
        private string name;
        private string path;
        private bool isSaveByChange;
        private TimeSpan period;
        private int maxNumCopy;

        [JsonProperty("id")]
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        [JsonProperty("path")]
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        [JsonProperty("isSaveByChange")]
        public bool IsSaveByChange
        {
            get { return isSaveByChange; }
            set
            {
                isSaveByChange = value;
                OnPropertyChanged("IsSaveByChange");
            }
        }

        [JsonProperty("period")]
        public TimeSpan Period
        {
            get { return period; }
            set
            {
                period = value;
                OnPropertyChanged("Period");
            }
        }

        [JsonProperty("maxNumCopy")]
        public int MaxNumCopy
        {
            get { return maxNumCopy; }
            set
            {
                maxNumCopy = value;
                OnPropertyChanged("MaxNumCopy");
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
