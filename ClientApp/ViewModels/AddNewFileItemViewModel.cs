using ClientApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;
using System.Net.Mail;

namespace ClientApp.ViewModels
{
    public class AddNewFileItemViewModel : INotifyPropertyChanged
    {
        private FileItem newFileItem;
        public FileItem NewFileItem
        {
            get { return newFileItem; }
            set
            {
                newFileItem = value;
                OnPropertyChanged("NewFileItem");
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand( async obj =>
                  {
                      FileItem fileItem = new FileItem
                      {
                          Name = NewFileItem.Name,
                          Path = NewFileItem.Path,
                          IsSaveByChange = NewFileItem.IsSaveByChange,
                          MaxNumCopy = NewFileItem.MaxNumCopy,
                          Period = NewFileItem.Period
                      };

                      using (var client = new HttpClient())
                      {
                          var json = System.Text.Json.JsonSerializer.Serialize(fileItem);
                          StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                          using var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:7018/api/items/create/");
                          request.Content = content;
                          using var response = await client.SendAsync(request);
                          
                          response.EnsureSuccessStatusCode();
                      
                          if (response.Content.ReadAsStringAsync().Result == "Ok")
                          {
                              NewFileItem = new FileItem { Name = "", Path = "", IsSaveByChange = false, MaxNumCopy = 0, Period = TimeSpan.Zero };
                              Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                              window.Close();
                          }
                          var s = response.Content.ReadAsStringAsync().Result;
                          if (response.Content.ReadAsStringAsync().Result == "This file already added to system.")
                          {
                              string messageBoxText = "This file already added to system.";
                              string caption = "";
                              MessageBoxButton button = MessageBoxButton.OK;
                              MessageBoxImage icon = MessageBoxImage.Warning;
                              MessageBoxResult result;

                              result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                          }
                      }
                  }));
            }
        }

        

        public AddNewFileItemViewModel()
        {
            NewFileItem = new FileItem { Name = "name", Path = "path", IsSaveByChange = false, MaxNumCopy = 0, Period = TimeSpan.Zero };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
