using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ClientApp.Models;
using System.Windows.Documents;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private FileItem selectedPhone;
        private FileItem newFileItem;

        public RelayCommand loadCommand { get; set; }
        public RelayCommand LoadCommand
        {
            get
            {
                return loadCommand ??
                    (loadCommand = new RelayCommand(async obj =>
                    {
                        using var httpClient = new HttpClient();
                        httpClient.BaseAddress = new Uri("http://localhost:7018/");
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var response = await httpClient.GetAsync("api/items");
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var items = JsonConvert.DeserializeObject<List<FileItem>>(json);
                            FileItems.Clear();
                            foreach (var item in items)
                            {
                                FileItems.Add(item); // Add each item to the collection
                            }
                            //FileItems = new ObservableCollection<FileItem>(items);
                        }
                    }));
            }
        }

        private RelayCommand selectionChangedCommand;
        public RelayCommand SelectionChangedCommand
        {
            get
            {
                return selectionChangedCommand ??
                    (selectionChangedCommand = new RelayCommand(async obj =>
                    {
                        using var httpClient = new HttpClient();
                        httpClient.BaseAddress = new Uri("http://localhost:7018/");
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var response = await httpClient.GetAsync("api/itemCopyes");
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var items = JsonConvert.DeserializeObject<List<FileItemCopy>>(json);
                            FileItemCopies.Clear();
                            foreach (var item in items)
                            {
                                FileItemCopies.Add(item); // Add each item to the collection
                            }
                            //FileItems = new ObservableCollection<FileItem>(items);
                        }
                    }));
            }
        }

        private RelayCommand openAddFileItemWindowCommand;
        public RelayCommand OpenAddFileItemWindowCommand
        {
            get
            {
                return openAddFileItemWindowCommand ??
                    (openAddFileItemWindowCommand = new RelayCommand(obj =>
                    {
                        AddNewFileItem addFileItemWindow = new AddNewFileItem();
                        addFileItemWindow.ShowDialog();
                    }));
            }
        }

        public ObservableCollection<FileItem> FileItems { get; set; }

        public ObservableCollection<FileItemCopy> FileItemCopies { get; set; }
        public FileItem SelectedFileItem
        {
            get { 

                return selectedPhone; }
            set
            {

                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public FileItem NewFileItem
        {
            get { return newFileItem; }
            set
            {
                newFileItem = value;
                OnPropertyChanged("NewFileItem");
            }
        }

        public ApplicationViewModel()
        {
            //LoadedCommand = new RelayCommand(Loaded);
            /*myWindow.Loaded += async (sender, args) =>
            {Window myWindow
                await InitializeAsync();
            };*/

            NewFileItem = new FileItem { Name = "", Path = "", IsSaveByChange = false, MaxNumCopy = 0, Period = TimeSpan.Zero };
            FileItems = new ObservableCollection<FileItem>();

            FileItemCopies = new ObservableCollection<FileItemCopy>
            {
                new FileItemCopy {Hash="acwcvvhmklhkmjl89y", Path="D:\\backups", ParentItem=null , PrevCopy = null, CreationTime = DateTime.Now },
                new FileItemCopy {Hash="acwcvvhmklhkmjl89y", Path="D:\\backups", ParentItem=null , PrevCopy = null, CreationTime = DateTime.Now }
                
                //new FileItem {Name="Elite x3", Path="HP", IsSaveByChange=true },
                //new FileItem {Name="Mi5S", Path="Xiaomi", IsSaveByChange=false }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public async Task InitializeAsync()
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:7018/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync("api/items");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<FileItem>>(json);
                FileItems = new ObservableCollection<FileItem>(items);
            }
        }

    }


}
