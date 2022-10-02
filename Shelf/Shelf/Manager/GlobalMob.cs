// Decompiled with JetBrains decompiler
// Type: Shelf.Manager.GlobalMob
// Assembly: Shelf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 375995AA-8D0C-4500-B93C-F0EB5887EB9B
// Assembly location: C:\Users\pc\Downloads\Shelf.dll

using Plugin.Connectivity;
using Plugin.SimpleAudioPlayer;
using Shelf.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shelf.Manager
{
  public static class GlobalMob
  {
    public static Color ButtonColor { get; set; }

    public static Color TextColor { get; set; }

    public static bool isLogin { get; set; }

    public static ztIOShelfUser User { get; set; }

    public static string ServerName { get; set; }

    public static void Exit() => DependencyService.Get<INativeHelper>()?.CloseApp();

    public static string PostJson(string url)
    {
      string serverName = GlobalMob.ServerName;
      url = !string.IsNullOrEmpty(serverName) ? "http://" + serverName + "/ShelfWebApi/" + url : "http://" + "iontegration.com" + "/ShelfWebApi/" + url;
      using (HttpClient httpClient = new HttpClient())
      {
        byte[] bytes = Encoding.UTF8.GetBytes("a:a");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
        HttpResponseMessage result = httpClient.GetAsync(url).Result;
        if (result.IsSuccessStatusCode)
          return result.Content.ReadAsStringAsync().Result;
      }
      return "";
    }

    public static async Task<string> PostJson(string url, Dictionary<string, string> paramList)
    {
      string serverName = GlobalMob.ServerName;
      url = !string.IsNullOrEmpty(serverName) ? "http://" + serverName + "/ShelfWebApi/" + url : "http://" + "iontegration.com" + "/ShelfWebApi/" + url;
      using (HttpClient client = new HttpClient())
      {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("a:a")));
        HttpResponseMessage httpResponseMessage = await client.PostAsync(url, (HttpContent) new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>) paramList)).ConfigureAwait(false);
        if (httpResponseMessage.IsSuccessStatusCode)
          return httpResponseMessage.Content.ReadAsStringAsync().Result;
      }
      return "";
    }

    public static bool IsInternet() => CrossConnectivity.Current.IsConnected;

    public static void PlaySave()
    {
      ISimpleAudioPlayer current = CrossSimpleAudioPlayer.Current;
      current.Load("Save.wav");
      current.Play();
    }

    public static void PlayError()
    {
      ISimpleAudioPlayer current = CrossSimpleAudioPlayer.Current;
      current.Load("Error.wav");
      current.Play();
    }
  }
}
