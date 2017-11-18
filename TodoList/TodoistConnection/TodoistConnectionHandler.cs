using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TodoList.TodoistConnection
{
    class TodoistConnectionHandler
    {
        private string _token;

        //ajkhsdhfsg


        public TodoistConnectionHandler()
        {
            _token = "620f2d9c1d81acc12ae14c9a527697fe94b5abee";
        }

        public async void GetAllTasks()
        {
            var httpClient= new HttpClient();

            string s = await AsyncGetTask(httpClient);
            MessageBox.Show(s);

        }


        private async Task<string> AsyncGetTask(HttpClient httpClient)
        {
            var requestContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("token", _token),
                new KeyValuePair<string, string>("sync_token", "*"), 
                new KeyValuePair<string, string>("resource_types", "[\"items\"]")
            });

            HttpResponseMessage response = await httpClient.PostAsync(
                "https://todoist.com/api/v7/sync",
                requestContent);

            string s;
            HttpContent responseContent = response.Content;
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                 s = await reader.ReadToEndAsync();
                
            }
            return s;
        }


    }
}
