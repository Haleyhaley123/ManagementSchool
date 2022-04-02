using DoMain;
using DoMainModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConnectAPI_1
{
    public class UserConnectAPI
    {
        public static UserName GetUserName(string username)
        {//gọi API hàm get
            UserName result = new UserName();
            ResponseModel<UserName> resultAPI = new ResponseModel<UserName>();
            string URL = "https://localhost:44368/api/Users/GetUser";
            string urlParameters = $"?username={username}";
           
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsStreamAsync();
                    Stream dataStreamResponse = dataObjects.Result;
                    StreamReader tReader = new StreamReader(dataStreamResponse);
                    string sResponseFromServer = tReader.ReadToEnd();
                    resultAPI = JsonConvert.DeserializeObject<ResponseModel<UserName>>(sResponseFromServer);
                }
                if (resultAPI.Data != null && resultAPI.Success)
                {
                    result = resultAPI.Data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ServerError:{ex.Message}");
                return result;
            }
            return result;
        }

    }
}
