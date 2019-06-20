using System.Collections.Generic;
using Xamarin.Forms;
using Zenworks.Utils;

namespace Zenworks.UI {
    [ContentProperty(nameof(OptionTexts))]
    public class SegmentedControl : ContentView {

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
            nameof(SelectedIndex),
            typeof(int),
            typeof(SegmentedControl),
            0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanging: OnSelectedIndexChanging);

        public int SelectedIndex {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly BindableProperty OptionTextsProperty = BindableProperty.Create(
            nameof(OptionTexts),
            typeof(string),
            typeof(SegmentedControl),
            string.Empty,
            propertyChanging: OnTextChange);

        public string OptionTexts {
            get => (string)GetValue(OptionTextsProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        private readonly StackLayout stack;

        public const string ButtonStyleClass = "SegmentButton";
        public const string SelectedButtonStyleClass = "SelectedSegmentButton";
        public SegmentedControl() {
            stack = new StackLayout {
                Orientation = StackOrientation.Horizontal,
            };
            Content = stack;
        }

        private void UpdateTexts(string texts) {
            stack.Children.Clear();
            int i = 0;
            int selectedIndex = SelectedIndex;
            foreach (string text in texts.Split(',')) {
                int index = i++;
                List<string> styleClasses = selectedIndex == index
                    ? new List<string> { ButtonStyleClass, SelectedButtonStyleClass }
                    : new List<string> { ButtonStyleClass };
                stack.Children.Add(new Button {
                    Text = text.Trim(),
                    StyleClass = styleClasses,
                    Command = new Command(() => { SelectedIndex = index; })
                });
            }
        }

        private static void OnTextChange(BindableObject bindable, object oldValue, object newValue) {
            if (bindable is SegmentedControl control && newValue is string texts) {
                control.UpdateTexts(texts);
            }
        }

        private static void OnSelectedIndexChanging(BindableObject bindable, object oldValue, object newValue) {
            if (bindable is SegmentedControl control) {
                View oldView = control.stack.Children[(int)oldValue];
                View newView = control.stack.Children[(int)newValue];
                oldView.StyleClass = oldView.StyleClass.Filter(@class => @class != SelectedButtonStyleClass).ToList();
                newView.StyleClass = newView.StyleClass.Append(SelectedButtonStyleClass).ToList();
            }
        }
    }
}
