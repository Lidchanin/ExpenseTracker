using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDatePicker
    {
        #region Bindable properties

        #region DateFormat property

        public static readonly BindableProperty DateFormatProperty = BindableProperty.Create(
            nameof(DateFormat),
            typeof(string),
            typeof(MaterialDatePicker),
            defaultValue: "dddd, MMMM d, yyyy",
            defaultBindingMode: BindingMode.TwoWay);

        public string DateFormat
        {
            get => (string) GetValue(DateFormatProperty);
            set => SetValue(DateFormatProperty, value);
        }

        #endregion DateFormat property

        #region Date property

        public static readonly BindableProperty DateProperty = BindableProperty.Create(
            nameof(Date),
            typeof(DateTime?),
            typeof(MaterialDatePicker),
            defaultBindingMode: BindingMode.TwoWay);

        public DateTime? Date
        {
            get => (DateTime?) GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        #endregion Date property

        #region Placeholder property

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(MaterialDatePicker),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialDatePicker) bindable;
                matEntry.EntryField.Placeholder = (string) newVal;
                matEntry.HiddenLabel.Text = (string) newVal;
            });

        public string Placeholder
        {
            get => (string) GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        #endregion Placeholder property

        #region AccentColor property

        public static readonly BindableProperty AccentColorProperty = BindableProperty.Create(
            nameof(AccentColor),
            typeof(Color),
            typeof(MaterialDatePicker),
            defaultValue: Color.Accent);

        public Color AccentColor
        {
            get => (Color)GetValue(AccentColorProperty);
            set => SetValue(AccentColorProperty, value);
        }

        #endregion AccentColor property

        #region InvalidColor property

        public static readonly BindableProperty InvalidColorProperty = BindableProperty.Create(
            nameof(InvalidColor),
            typeof(Color),
            typeof(Entry),
            Color.Red,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialDatePicker) bindable;
                matEntry.UpdateValidation();
            });

        public Color InvalidColor
        {
            get => (Color)GetValue(InvalidColorProperty);
            set => SetValue(InvalidColorProperty, value);
        }

        #endregion InvalidColor property

        #region DefaultColor property

        public static readonly BindableProperty DefaultColorProperty = BindableProperty.Create(
            nameof(DefaultColor),
            typeof(Color), 
            typeof(Entry), 
            Color.Gray, 
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialDatePicker)bindable;
                matEntry.UpdateValidation();
            });

        public Color DefaultColor
        {
            get => (Color) GetValue(DefaultColorProperty);
            set => SetValue(DefaultColorProperty, value);
        }

        #endregion DefaultColor property

        #region IsValid property

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
            nameof(IsValid), 
            typeof(bool),
            typeof(Entry), 
            true, 
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialDatePicker) bindable;
                matEntry.UpdateValidation();
            });

        public bool IsValid
        {
            get => (bool) GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        #endregion IsValid property

        #endregion Bindable properties

        public MaterialDatePicker()
        {
            InitializeComponent();

            EntryField.BindingContext = this;

            BottomBorder.BackgroundColor = DefaultColor;

            EntryField.Focused += (s, a) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    EntryField.Unfocus();
                    Picker.Focus();
                });
            };

            Picker.Focused += async (s, a) =>
            {
                HiddenBottomBorder.BackgroundColor = AccentColor;
                HiddenLabel.TextColor = AccentColor;
                HiddenLabel.IsVisible = true;

                if (string.IsNullOrEmpty(EntryField.Text))
                {
                    await Task.WhenAll(
                        HiddenBottomBorder.LayoutTo(
                            new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height),
                            400),
                        HiddenLabel.FadeTo(1, 50),
                        HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y - EntryField.Height + 4, 400,
                            Easing.BounceIn)
                    );
                    EntryField.Placeholder = null;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(
                        new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 400);
                }
            };

            Picker.Unfocused += async (s, a) =>
            {
                HiddenLabel.TextColor = IsValid 
                    ? DefaultColor 
                    : InvalidColor;

                Picker_DateSelected(s, new DateChangedEventArgs(Picker.Date, Picker.Date));

                if (string.IsNullOrEmpty(EntryField.Text))
                {
                    await Task.WhenAll(
                        HiddenBottomBorder.LayoutTo(
                            new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 400),
                        HiddenLabel.FadeTo(0, 150),
                        HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y, 400, Easing.BounceIn)
                    );
                    EntryField.Placeholder = Placeholder;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(
                        new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 400);
                }
            };

            Picker.DateSelected += Picker_DateSelected;
        }

        private void Picker_DateSelected(object sender, DateChangedEventArgs e)
        {
            EntryField.Text = e.NewDate.ToString(DateFormat, CultureInfo.CurrentCulture);
            Date = e.NewDate;
        }

        private void UpdateValidation()
        {
            if (IsValid)
            {
                BottomBorder.BackgroundColor = DefaultColor;
                HiddenBottomBorder.BackgroundColor = AccentColor;

                HiddenLabel.TextColor = IsFocused 
                    ? AccentColor 
                    : DefaultColor;
            }
            else
            {
                BottomBorder.BackgroundColor = InvalidColor;
                HiddenBottomBorder.BackgroundColor = InvalidColor;
                HiddenLabel.TextColor = InvalidColor;
            }
        }
    }
}