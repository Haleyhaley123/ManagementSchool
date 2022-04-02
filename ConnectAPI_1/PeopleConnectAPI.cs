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
    public class PeopleConnectAPI
    { //Xem danh sách

        // loại 1
        public static  List<People> GetPeople()
        {
            List<People> result = new List<People>();
            ResponseModel<List<People>> resultAPI = new ResponseModel<List<People>>();
            string URL = "https://localhost:44368/api/People/GetAllPeople";
            //string urlParameters = $"";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response =  client.GetAsync(URL).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsStreamAsync();
                    Stream dataStreamResponse = dataObjects.Result;
                    StreamReader tReader = new StreamReader(dataStreamResponse);
                    string sResponseFromServer = tReader.ReadToEnd();
                    resultAPI = JsonConvert.DeserializeObject<ResponseModel<List<People>>>(sResponseFromServer);
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


        // đoạn thêm trong winfgorms đâu

        // loại 2
        public static bool InsertPeople(People people)
        {
            bool result = false;
            ResponseModel<bool> resultAPI = new ResponseModel<bool>();
            try
            {
                Uri urlapi = new Uri("https://localhost:44368/api/People/InsertData");
                using (var wc = new HttpClient())
                {
                    //wc.DefaultRequestHeaders.Add("Authorization", $"Bearer {VariableSharing._token}");

                    // data truyền sang là kiểu json

                    // đoạn này ép giá trị đối tượng về kiểu string
                    var modelString = JsonConvert.SerializeObject(people);
                    // đoạn này ép giá trị đối tượng về kiểu application/json
                    var content = new StringContent(modelString, Encoding.UTF8, "application/json");
                    // tất cả cái sau làm tương tự chỉ cần thay đổi cái PostAsync = PutAsync hoặc deleteAsync
                    var jsonResult = wc.PostAsync($@"{urlapi}", content).Result.Content.ReadAsStringAsync().Result;
                    resultAPI = JsonConvert.DeserializeObject<ResponseModel<bool>>(jsonResult);
                }
                if (resultAPI.Success)
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
        public static bool EditPeople(People edit)
        {
            bool result = false;
            ResponseModel<bool> resultAPI = new ResponseModel<bool>();
            try
            {
                Uri urlapi = new Uri("https://localhost:44368/api/People/EditData");
                using (var wc = new HttpClient())
                {
                    var modelString = JsonConvert.SerializeObject(edit);
                    var content = new StringContent(modelString, Encoding.UTF8, "application/json");
                    var jsonResult = wc.PutAsync($@"{urlapi}", content).Result.Content.ReadAsStringAsync().Result;
                    resultAPI = JsonConvert.DeserializeObject<ResponseModel<bool>>(jsonResult);
                }
                if (resultAPI.Success)
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
        public static bool DeletePeople(int pk_id)
        {
            bool result = false;
            ResponseModel<bool> resultAPI = new ResponseModel<bool>();
            try
            {
                Uri urlapi = new Uri("https://localhost:44368/api/People/DeleteData");
                string urlParameters = $"?id={pk_id}"; // ý là bây giơ chỉ gửi id sang đúng ko, vấn đề là như này, lúc xóa thì nó chỉ có mỗi theo ID này, nhưng xóa api là pk_id(int)
                using (var wc = new HttpClient())
                {
                    // tất cả cái sau làm tương tự chỉ cần thay đổi cái PostAsync = PutAsync hoặc deleteAsync
                    var jsonResult = wc.DeleteAsync($@"{urlapi + urlParameters}").Result.Content.ReadAsStringAsync().Result;
                    resultAPI = JsonConvert.DeserializeObject<ResponseModel<bool>>(jsonResult);
                }
                if (resultAPI.Success)
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

        // có 2 cách để dùng, nếu chỉ dùng để gọi và ko truyền giá trị, hoặc truyền giá trị kiểu đơn giản thì dùng loại 1
    }   // loại gọi api mà phải truyền 1 đối tượng sang hoặc 1 danh sách nói chung param phức tạp thì dùng loại 2
}
