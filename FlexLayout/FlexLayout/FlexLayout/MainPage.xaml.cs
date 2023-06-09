﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlexLayout
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadImages();
        }

        [DataContract]

        class Images
        {
            [DataMember(Name = "photos")]
            public List<string> Photos;
        }

        async void LoadImages()
        {
            using (var wc = new WebClient())
            {
                var data = await wc.DownloadDataTaskAsync("https://raw.githubusercontent.com/xamarin/docs-archive/master/Images/stock/small/stock.json");

                using (var stream = new MemoryStream(data))
                {
                    var jSer = new DataContractJsonSerializer(typeof(Images));
                    var images = (Images)jSer.ReadObject(stream);

                    foreach (string fileUrl in images.Photos.Take(24))
                        flFlex.Children.Add(new Image() { 
                        Source = ImageSource.FromUri(new Uri(fileUrl + "?width=1024&height=1024"))
                        });
                }
            }
        }
    }
}
