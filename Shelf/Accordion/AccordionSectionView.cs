// Decompiled with JetBrains decompiler
// Type: Accordion.AccordionSectionView
// Assembly: Shelf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 375995AA-8D0C-4500-B93C-F0EB5887EB9B
// Assembly location: C:\Users\pc\Downloads\Shelf.dll

using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace Accordion
{
  public class AccordionSectionView : StackLayout
  {
    private bool _isExpanded;
    private StackLayout _content;
    private Color _headerColor;
    private ImageSource _arrowRight;
    private ImageSource _arrowDown;
    private AbsoluteLayout _header;
    private Image _headerIcon;
    private Label _headerTitle;
    private DataTemplate _template;
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof (ItemsSource), typeof (IList), typeof (AccordionSectionView), propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(AccordionSectionView.PopulateList));
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof (Title), typeof (string), typeof (AccordionSectionView), propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(AccordionSectionView.ChangeTitle));

    public IList ItemsSource
    {
      get => (IList) this.GetValue(AccordionSectionView.ItemsSourceProperty);
      set => this.SetValue(AccordionSectionView.ItemsSourceProperty, (object) value);
    }

    public string Title
    {
      get => (string) this.GetValue(AccordionSectionView.TitleProperty);
      set => this.SetValue(AccordionSectionView.TitleProperty, (object) value);
    }

    public AccordionSectionView(DataTemplate itemTemplate, ScrollView parent)
    {
      StackLayout stackLayout = new StackLayout();
      stackLayout.HeightRequest = 0.0;
      this._content = stackLayout;
      this._headerColor = Color.FromRgb(83, 186, 157);
      this._arrowRight = ImageSource.FromFile("ic_keyboard_arrow_right_white_24dp.png");
      this._arrowDown = ImageSource.FromFile("ic_keyboard_arrow_down_white_24dp.png");
      this._header = new AbsoluteLayout();
      Image image = new Image();
      image.VerticalOptions = LayoutOptions.Center;
      this._headerIcon = image;
      Label label = new Label();
      label.TextColor = Color.White;
      label.FontAttributes = FontAttributes.Bold;
      label.VerticalTextAlignment = TextAlignment.Center;
      label.FontSize = 20.0;
      label.HeightRequest = 70.0;
      this._headerTitle = label;
      // ISSUE: explicit constructor call
      base.\u002Ector();
      AccordionSectionView accordionSectionView = this;
      this._template = itemTemplate;
      this._headerTitle.BackgroundColor = this._headerColor;
      this._headerIcon.Source = this._arrowRight;
      this._header.BackgroundColor = this._headerColor;
      this._header.Children.Add((View) this._headerIcon, new Rectangle(0.0, 1.0, 0.1, 1.0), AbsoluteLayoutFlags.All);
      this._header.Children.Add((View) this._headerTitle, new Rectangle(1.0, 1.0, 0.9, 1.0), AbsoluteLayoutFlags.All);
      this.Spacing = 0.0;
      this.Children.Add((View) this._header);
      this.Children.Add((View) this._content);
      this._header.GestureRecognizers.Add((IGestureRecognizer) new TapGestureRecognizer()
      {
        Command = (ICommand) new Command((Action) (() =>
        {
          if (accordionSectionView._isExpanded)
          {
            accordionSectionView._headerIcon.Source = accordionSectionView._arrowRight;
            accordionSectionView._content.HeightRequest = 0.0;
            accordionSectionView._content.IsVisible = false;
            accordionSectionView._isExpanded = false;
          }
          else
          {
            accordionSectionView._headerIcon.Source = accordionSectionView._arrowDown;
            accordionSectionView._content.HeightRequest = (double) (accordionSectionView._content.Children.Count * 100);
            accordionSectionView._content.IsVisible = true;
            accordionSectionView._isExpanded = true;
            if (!(parent.Parent is VisualElement))
              return;
            await parent.ScrollToAsync(0.0, accordionSectionView.Y, true);
          }
        }))
      });
    }

    private void ChangeTitle()
    {
      string title = this.Title;
      this._headerTitle.Text = this.Title;
    }

    private void PopulateList()
    {
      this._content.Children.Clear();
      foreach (object obj in (IEnumerable) this.ItemsSource)
      {
        View content = (View) this._template.CreateContent();
        content.BindingContext = obj;
        this._content.Children.Add(content);
      }
    }

    private static void ChangeTitle(BindableObject bindable, object oldValue, object newValue)
    {
      if (oldValue == newValue)
        return;
      ((AccordionSectionView) bindable).ChangeTitle();
    }

    private static void PopulateList(BindableObject bindable, object oldValue, object newValue)
    {
      if (oldValue == newValue)
        return;
      ((AccordionSectionView) bindable).PopulateList();
    }
  }
}
