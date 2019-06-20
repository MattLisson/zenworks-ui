using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zenworks.UI {

    public class ErrorViewModel : ViewModel {
        private bool isVisible;
        public bool IsVisible {
            get => isVisible;
            set => SetProperty(ref isVisible, value);
        }

        private string message;
        public string Message {
            get => message;
            set => SetProperty(ref message, value);
        }

        private string firstButtonText = "Dismiss";
        public string FirstButtonText {
            get => firstButtonText;
            set => SetProperty(ref firstButtonText, value);
        }

        private string? secondButtonText = null;
        public string? SecondButtonText {
            get => secondButtonText;
            set => SetProperty(ref secondButtonText, value);
        }

        public bool IsSecondButtonVisible => SecondButtonText != null;

        public Command OnFirstButtonClicked { get; }
        public Command OnSecondButtonClicked { get; }

        public event Action FirstAction;
        public event Action SecondAction;


        public ErrorViewModel(string message, bool dismissOnClicks = true,
            string firstButtonText = "Dismiss", Action? firstButtonClicked = null,
            string? secondButtonText = null, Action? secondButtonClicked = null) {
            IsVisible = true;
            this.message = message;
            this.firstButtonText = firstButtonText;
            if (firstButtonClicked != null) {
                FirstAction += firstButtonClicked;
            }
            if (secondButtonText != null) {
                this.secondButtonText = secondButtonText;
            }
            if (secondButtonClicked != null) {
                SecondAction += secondButtonClicked;
            }
            if (dismissOnClicks) {
                FirstAction += Dismiss;
                SecondAction += Dismiss;
            }
            OnFirstButtonClicked = new Command(() => FirstAction?.Invoke());
            OnSecondButtonClicked = new Command(() => SecondAction?.Invoke());
        }

        public void Dismiss() {
            IsVisible = false;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErrorView {
        public ErrorView() {
            InitializeComponent();
        }

        protected override void OnViewModelChanged(ErrorViewModel? oldValue, ErrorViewModel? newValue) {
            base.OnViewModelChanged(oldValue, newValue);
            if (oldValue != null) {
                oldValue.PropertyChanged -= ViewModelPropertyChanged;
            }
            if (newValue != null) {
                newValue.PropertyChanged += ViewModelPropertyChanged;
                IsVisible = newValue.IsVisible;
            } else {
                IsVisible = false;
            }
        }

        private void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(ErrorViewModel.IsVisible)) {
                IsVisible = ViewModel.IsVisible;
            }
        }
    }
}
