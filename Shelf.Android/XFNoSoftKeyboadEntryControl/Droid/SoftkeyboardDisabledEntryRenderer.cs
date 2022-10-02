// Decompiled with JetBrains decompiler
// Type: XFNoSoftKeyboadEntryControl.Droid.SoftkeyboardDisabledEntryRenderer
// Assembly: Shelf.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2ECED5B-D80F-4DDC-93D6-8A27414AADAF
// Assembly location: C:\Users\pc\Downloads\Shelf.Android.dll

using Android.Content;
using Android.Views.InputMethods;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace XFNoSoftKeyboadEntryControl.Droid
{
  public class SoftkeyboardDisabledEntryRenderer : EntryRenderer
  {
    public SoftkeyboardDisabledEntryRenderer(Context context)
      : base(context)
    {
    }

    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
    {
      base.OnElementChanged(e);
      if (e.NewElement != null)
        e.NewElement.PropertyChanging += new PropertyChangingEventHandler(this.OnPropertyChanging);
      if (e.OldElement != null)
        e.OldElement.PropertyChanging -= new PropertyChangingEventHandler(this.OnPropertyChanging);
      this.Control.ShowSoftInputOnFocus = false;
    }

    private void OnPropertyChanging(
      object sender,
      PropertyChangingEventArgs propertyChangingEventArgs)
    {
      if (!(propertyChangingEventArgs.PropertyName == VisualElement.IsFocusedProperty.PropertyName))
        return;
      ((InputMethodManager) this.Context.GetSystemService("input_method")).HideSoftInputFromWindow(this.Control.WindowToken, HideSoftInputFlags.None);
    }
  }
}
