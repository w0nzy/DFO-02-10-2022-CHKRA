// Decompiled with JetBrains decompiler
// Type: Accordion.AccordionView
// Assembly: Shelf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 375995AA-8D0C-4500-B93C-F0EB5887EB9B
// Assembly location: C:\Users\pc\Downloads\Shelf.dll

using System;
using System.Collections;
using Xamarin.Forms;

namespace Accordion
{
  public class AccordionView : ScrollView
  {
    private StackLayout _layout = new StackLayout()
    {
      Spacing = 1.0
    };
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof (ItemsSource), typeof (IList), typeof (AccordionSectionView), propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(AccordionView.PopulateList));

    public DataTemplate Template { get; set; }

    public DataTemplate SubTemplate { get; set; }

    public IList ItemsSource
    {
      get => (IList) this.GetValue(AccordionView.ItemsSourceProperty);
      set => this.SetValue(AccordionView.ItemsSourceProperty, (object) value);
    }

    public AccordionView(DataTemplate itemTemplate)
    {
      AccordionView parent = this;
      this.SubTemplate = itemTemplate;
      this.Template = new DataTemplate((Func<object>) (() => (object) new AccordionSectionView(itemTemplate, (ScrollView) parent)));
      this.Content = (View) this._layout;
    }

    private void PopulateList()
    {
      this._layout.Children.Clear();
      foreach (object obj in (IEnumerable) this.ItemsSource)
      {
        View content = (View) this.Template.CreateContent();
        content.BindingContext = obj;
        this._layout.Children.Add(content);
      }
    }

    private static void PopulateList(BindableObject bindable, object oldValue, object newValue)
    {
      if (oldValue == newValue)
        return;
      ((AccordionView) bindable).PopulateList();
    }
  }
}
