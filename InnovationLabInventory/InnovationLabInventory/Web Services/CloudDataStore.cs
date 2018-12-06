using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InnovationLabInventory.Models;
using Newtonsoft.Json;
using System;
using InnovationLabInventory.Views;
using System.Net.Http.Headers;

namespace InnovationLabInventory.WebServices
{
    public class CloudDataStore
    {
        HttpClient client;
        string matched;

        public CloudDataStore()
        {
            client = new HttpClient();
        }
        //Login API
        public async Task<LoginToken> CheckLogin(Dictionary<string, string> dict)
        {
            if (dict == null)
                return null;

            var serializedItem = JsonConvert.SerializeObject(dict);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var request = await client.PostAsync("http://ec2-54-89-116-114.compute-1.amazonaws.com:8000/login", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            matched = await Task.Run(() => request.Content.ReadAsStringAsync());
            var result = await Task.Run(() => JsonConvert.DeserializeObject<LoginToken>(matched));
            Constants.token = result.token;
            if (result != null)
            {
                return result;
            }

            return null;
        }
        //Create Asset API
        public async Task<bool> AddCreateAsset(Dictionary<string, string> dict)
        {
            if (dict == null)//!CrossConnectivity.Current.IsConnected
                return false;
            var serializedItem = JsonConvert.SerializeObject(dict);
            client.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = await client.PostAsync(Constants.uri + "/AssetCreateTransaction", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode ? true : false;
        }
        //Scan Barcode API
        public async Task<ScanResponce> GetBarcode(string assetId)
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var response = await client.GetStringAsync("http://ec2-54-89-116-114.compute-1.amazonaws.com:3000/api/LabAsset/" + assetId);
                var result = await Task.Run(() => JsonConvert.DeserializeObject<ScanResponce>(response));
                if (result != null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //Update Asset API
        public async Task<bool> UpdateAsset(Dictionary<string, string> dict)
        {
            if (dict == null)//!CrossConnectivity.Current.IsConnected
                return false;
            var serializedItem = JsonConvert.SerializeObject(dict);
            client.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = await client.PostAsync(Constants.uri + "/AssetMoveTransaction", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> EditDescription(Dictionary<string, string> dict)
        {
            if (dict == null)//!CrossConnectivity.Current.IsConnected
                return false;
            HttpClient clients = new HttpClient();
            var serializedItem = JsonConvert.SerializeObject(dict);
            clients.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            clients.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = await clients.PostAsync(Constants.uri + "/AssetEditDescriptionTransaction", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> EditComment(Dictionary<string, string> dict)
        {
            if (dict == null)//!CrossConnectivity.Current.IsConnected
                return false;
            HttpClient clients = new HttpClient();
            var serializedItem = JsonConvert.SerializeObject(dict);
            clients.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            clients.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = await clients.PostAsync(Constants.uri + "/AssetEditCommentTransaction", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode ? true : false;
        }


        //Assign Asset API
        public async Task<bool> AssignAsset(Dictionary<string, string> dict)
        {
            if (dict == null)
                return false;
            var serializedItem = JsonConvert.SerializeObject(dict);
            //client.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = await client.PostAsync(Constants.uri + "/AssetAssignTransaction", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode ? true : false;
        }
        //Delete Asset API
        public async Task<bool> DeleteAsset(Dictionary<string, string> dict)
        {
            if (dict == null)
                return false;
            HttpClient clients = new HttpClient();
            var serializedItem = JsonConvert.SerializeObject(dict);
            clients.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            clients.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = await clients.PostAsync(Constants.uri + "/AssetRemoveTransaction", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode ? true : false;
        }
        //Return Asset API
        public async Task<bool> ReturnAsset(Dictionary<string, string> dict)
        {
            if (dict == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(dict);
            client.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = await client.PostAsync(Constants.uri + "/AssetReturnTransaction", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode ? true : false;
        }
        public async Task<bool> AddAsset(Dictionary<string, string> dict)
        {
            if (dict == null)
                return false;
            HttpClient clients = new HttpClient();
            var serializedItem = JsonConvert.SerializeObject(dict);
            clients.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            clients.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = await clients.PostAsync(Constants.uri + "/AssetAdditionTransaction", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode ? true : false;
        }
        public async Task<bool> SubtractAsset(Dictionary<string, string> dict)
        {
            if (dict == null)
                return false;
            HttpClient clients = new HttpClient();
            var serializedItem = JsonConvert.SerializeObject(dict);
            clients.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            clients.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = await clients.PostAsync(Constants.uri + "/AssetSubtractionTransaction", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<bool> ScanAsset(Dictionary<string, string> dict)
        {
            if (dict == null)
                return false;
            var serializedItem = JsonConvert.SerializeObject(dict);
            client.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var response = await client.PostAsync(Constants.uri + "/AssetScanTransaction", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode ? true : false;
        }




        public async Task<List<ViewAsset>> GetAssets()
        {
            client.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var json = await client.GetStringAsync(Constants.uri + "/LabAsset");
            List<ViewAsset> response = await Task.Run(() =>
                                                      JsonConvert.DeserializeObject<List<ViewAsset>>(json));
            return response;
        }

        public async Task<List<Users>> GetUsers()
        {
            client.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var json = await client.GetStringAsync(Constants.uri + "/InventoryUser");
            List<Users> response = await Task.Run(() =>
                                                  JsonConvert.DeserializeObject<List<Users>>(json));
            return response;
        }

        public async Task<List<Users>> GetManagers()
        {
            // client.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            // client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            var json = await client.GetStringAsync(Constants.uri + "/InventoryManager");
            List<Users> response = await Task.Run(() =>
                                                JsonConvert.DeserializeObject<List<Users>>(json));
            return response;
        }

        public async Task<ScanResponce> GetScanUser(string assetId)
        {
            //client.DefaultRequestHeaders.Add("X-Access-Token", Constants.token);
            //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            string url = Constants.uri + "/LabAsset/" + assetId;
            var json = await client.GetStringAsync(url);
            ScanResponce response = JsonConvert.DeserializeObject<ScanResponce>(json.ToString());
            // ScanResponce response = await Task.Run(() =>
            //JsonConvert.DeserializeObject<ScanResponce>(json));
            return response;
        }

        public async Task<string> GenarateAssetID()
        {
            var json = await client.GetStringAsync("http://ec2-54-89-116-114.compute-1.amazonaws.com:8000/genid");
            //var response = await Task.Run(() => JsonConvert.DeserializeObject<string>(json));
            return json;
        }

    }
}

