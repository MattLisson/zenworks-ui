﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zenworks.UI {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepperEntry : ContentView {

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            nameof(Value),
            typeof(double),
            typeof(StepperEntry),
            0.0,
            defaultBindingMode: BindingMode.TwoWay);

#pragma warning disable CA1721 // This property should match the Xamarin Forms Stepper.
        public double Value {
#pragma warning restore CA1721 // This property should match the Xamarin Forms Stepper.
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty IncrementProperty = BindableProperty.Create(
            nameof(Increment),
            typeof(double),
            typeof(StepperEntry),
            0.0,
            defaultBindingMode: BindingMode.OneWay);

        public double Increment {
            get => (double)GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, value);
        }
        public StepperEntry() {
            InitializeComponent();
            Content.BindingContext = this;
            Entry.TextChanged += Entry_TextChanged;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e) {
            if (double.TryParse(e.NewTextValue, out double newValue)) {
                Value = newValue;
            }
        }
    }
}
