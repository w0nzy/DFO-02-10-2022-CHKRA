// Decompiled with JetBrains decompiler
// Type: Shelf.Helpers.Settings
// Assembly: Shelf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 375995AA-8D0C-4500-B93C-F0EB5887EB9B
// Assembly location: C:\Users\pc\Downloads\Shelf.dll

using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Shelf.Helpers
{
  public static class Settings
  {
    private const string SettingsKey = "settings_key";
    private static readonly string SettingsDefault = string.Empty;

    private static ISettings AppSettings => CrossSettings.Current;

    public static string GeneralSettings
    {
      get => Shelf.Helpers.Settings.AppSettings.GetValueOrDefault("settings_key", Shelf.Helpers.Settings.SettingsDefault);
      set => Shelf.Helpers.Settings.AppSettings.AddOrUpdateValue("settings_key", value);
    }

    public static string UserName
    {
      get => Shelf.Helpers.Settings.AppSettings.GetValueOrDefault("username", "");
      set => Shelf.Helpers.Settings.AppSettings.AddOrUpdateValue("username", value);
    }

    public static string Password
    {
      get => Shelf.Helpers.Settings.AppSettings.GetValueOrDefault("password", "");
      set => Shelf.Helpers.Settings.AppSettings.AddOrUpdateValue("password", value);
    }

    public static string Server
    {
      get => Shelf.Helpers.Settings.AppSettings.GetValueOrDefault("server", "");
      set => Shelf.Helpers.Settings.AppSettings.AddOrUpdateValue("server", value);
    }
  }
}
