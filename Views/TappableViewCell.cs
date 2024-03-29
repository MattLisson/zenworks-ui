﻿using System.Runtime.Versioning;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Zenworks.UI {
    [SupportedOSPlatform("ios")]
    public class TappableViewCell : ViewCell {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(TappableViewCell));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object), 
            typeof(TappableViewCell));
        public ICommand Command {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        public object CommandParameter {
            get => (ICommand)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        protected override void OnTapped() {
            Command?.Execute(CommandParameter);
        }
    }
}
