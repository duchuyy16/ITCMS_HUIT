﻿using ITCMS_HUIT.Client.Models;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace ITCMS_HUIT.Client.Common
{
    public class Utilities
    {
        public static ApiResponse<T> SendDataRequest<T>(string APIUrl, object? input = null)
        {          
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:44352");
            client.DefaultRequestHeaders.Accept.Clear();

            var sessionToken = AppContext.Current!.Session.Get<string>("Token");

            if (sessionToken != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         
            var jsonContent = JsonConvert.SerializeObject(input);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.SendAsync(new HttpRequestMessage(HttpMethod.Post, APIUrl) { Content = content }).Result;
            //T result = default!;

            if (response.IsSuccessStatusCode)
            {
                //if(typeof(T) == typeof(MemoryStream))
                //{
                //    var file = response.Content.ReadAsStreamAsync().Result;
                //    var fileName = response.Content.Headers.ContentDisposition!.FileName;
                //    return (T)(object)file;
                //}    

                var jsonString = response.Content.ReadAsStringAsync().Result;
                var responseData = JsonConvert.DeserializeObject<ApiResponse<T>>(jsonString);
                return responseData!;
                
            }
            else
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var responseData = JsonConvert.DeserializeObject<ApiResponse<T>>(jsonString);
                return responseData!;
                //var statusCode = response.StatusCode;
                //var reasonPhrase = response.ReasonPhrase;
                //var errorContent = response.Content.ReadAsStringAsync().Result;
            }
            //return result;
        }

        public static ValueTuple<MemoryStream,string> SendDataRequestExcel(string APIUrl, object? input = null)
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:44352");
            client.DefaultRequestHeaders.Accept.Clear();

            var sessionToken = AppContext.Current!.Session.Get<string>("Token");

            if (sessionToken != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionToken);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonContent = JsonConvert.SerializeObject(input);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.SendAsync(new HttpRequestMessage(HttpMethod.Post, APIUrl) { Content = content }).Result;

            if (response.IsSuccessStatusCode)
            {
                    var file = response.Content.ReadAsStreamAsync().Result;
                    var fileName = response.Content.Headers.ContentDisposition!.FileName;
                    return new ValueTuple<MemoryStream,string>((file as MemoryStream)!,fileName!);                
            }

            return new ValueTuple<MemoryStream, string>();
        }

        //static MultipartFormDataContent ConvertObjectToFormData(object data)
        //{
        //    var formData = new MultipartFormDataContent();

        //    var properties = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    foreach (var property in properties)
        //    {
        //        var value = property.GetValue(data);

        //        if (value is byte[] fileData)
        //        {
        //            var fileContent = new ByteArrayContent(fileData);
        //            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
        //            formData.Add(fileContent, property.Name, "file.txt");
        //        }
        //        else
        //        {
        //            var stringValue = value != null ? value.ToString() : string.Empty;
        //            var stringContent = new StringContent(stringValue!, Encoding.UTF8);
        //            formData.Add(stringContent, property.Name);
        //        }
        //    }

        //    return formData;
        //}

        //public static T FromData<T>(string APIUrl, object? input = null, IFormFile? imageFile = null)
        //{
        //    HttpClient client = new();
        //    client.BaseAddress = new System.Uri("https://localhost:44350");
        //    client.DefaultRequestHeaders.Accept.Clear();

        //    MultipartFormDataContent formData = ConvertObjectToFormData(input!);

        //    if (imageFile != null)
        //    {
        //        byte[] fileBytes;
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            imageFile.CopyTo(memoryStream);
        //            fileBytes = memoryStream.ToArray();
        //        }

        //        ByteArrayContent fileContent = new ByteArrayContent(fileBytes);
        //        formData.Add(fileContent, "imageFile", imageFile.FileName);
        //    }

        //    HttpResponseMessage response = client.PostAsync(APIUrl, formData).Result;

        //    T result = default!;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonString = response.Content.ReadAsStringAsync().Result;
        //        var returnData = JsonConvert.DeserializeObject<T>(jsonString);
        //        if (returnData != null)
        //        {
        //            return returnData;
        //        }
        //    }
        //    else
        //    {
        //        var statusCode = response.StatusCode;
        //        var reasonPhrase = response.ReasonPhrase;
        //        var content = response.Content.ReadAsStringAsync().Result;
        //    }
        //    return result;
        //}

        //public static DataTable GetTable<TEntity>(List<TEntity> table, string name) where TEntity : class
        //{
        //    var offset = 78;
        //    DataTable result = new DataTable(name);
        //    PropertyInfo[] infos = typeof(TEntity).GetProperties();
        //    foreach (PropertyInfo info in infos)
        //    {
        //        if (info.PropertyType.IsGenericType
        //        && info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        //        {
        //            result.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType)!));
        //        }
        //        else
        //        {
        //            result.Columns.Add(new DataColumn(info.Name, info.PropertyType));
        //        }
        //    }
        //    foreach (var el in table)
        //    {
        //        DataRow row = result.NewRow();
        //        foreach (PropertyInfo info in infos)
        //        {
        //            if (info.PropertyType.IsGenericType &&
        //                info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        //            {
        //                object t = info.GetValue(el)!;
        //                if (t == null)
        //                {
        //                    t = Activator.CreateInstance(Nullable.GetUnderlyingType(info.PropertyType)!)!;
        //                }

        //                row[info.Name] = t;
        //            }
        //            else
        //            {
        //                if (info.PropertyType == typeof(byte[]))
        //                {
        //                    //Fix for Image issue.
        //                    var imageData = (byte[])info.GetValue(el)!;
        //                    var bytes = new byte[imageData.Length - offset];
        //                    Array.Copy(imageData, offset, bytes, 0, bytes.Length);
        //                    row[info.Name] = bytes;
        //                }
        //                else
        //                {
        //                    row[info.Name] = info.GetValue(el);
        //                }
        //            }
        //        }
        //        result.Rows.Add(row);
        //    }

        //    return result;
        //}
    }
}
