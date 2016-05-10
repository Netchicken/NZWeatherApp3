using System;
using System.Net;
using System.IO;
using System.Text;

namespace NZWeatherApp {
    class DataService {
        private string StrMetService;
        public string Temp;
        public string URL { get; set; }


        public void ConnectToNet() {
            //downloads the string and returns it    
            var webaddress = new Uri(URL);
            var webclient = new WebClient();


            webclient.DownloadStringAsync(webaddress);
            //download the website as a string. 
            //  StrMetService = webclient.DownloadString(webaddress);
            //https://developer.xamarin.com/recipes/ios/network/web_requests/download_a_file/
            webclient.DownloadStringCompleted += (s, e) =>
            {
                StrMetService = e.Result; // get the downloaded text as


            };
            ExtractTemperature();
            // string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            // string localFilename = "downloaded.txt";
            //  string localPath = Path.Combine(documentsPath, localFilename);
            // File.WriteAllText (localpath, text); // writes to local storage

            }
        public void ExtractTemperature() {
            //Return back the temperature from the string
            StrMetService = StrMetService.Replace(@"\", string.Empty);
            //<div class="summary"><h1>Christchurch</h1></div><div class="summary top"><div class="ul"><h2>17.5<span class="temp">&deg;C</span> 
            var intTempLeft = StrMetService.IndexOf("summary top><div class=ul><h2>") + 30;
            //add 30 to get to the end of this string and the beginning of the number
            // var intTempRight = StrMetService.IndexOf("<span class=temp>") - intTempLeft;
            //the text on the right of the number
            Temp = StrMetService.Substring(intTempLeft, 6);

            //return strTemp + " " + "c";
            }

        public string ExtractImage() {
            //Return back the path to the image only
            StrMetService = StrMetService.Replace(@"\", string.Empty);

            //</div><div class="mob-page" id="forecasts-block"><h2>10 Day Forecast</h2><div class="item"><img src="/sites/all/themes/mobile/images-new/wx-icons/showers_wht.gif" width="32" height="32" title="Showers" alt="Showers" />

            var intImageLeft = StrMetService.IndexOf("images-new/wx-icons/") + 20;
            //add 30 to get to the end of this string and the beginning of the number
            var intImageCount = StrMetService.IndexOf("width=32 height=32") - intImageLeft;
            //the text on the right of the number
            var strImage = StrMetService.Substring(intImageLeft, intImageCount);

            return "http://m.metservice.com/sites/all/themes/mobile/images-new/wx-icons/" + strImage;
            }
        }
    }