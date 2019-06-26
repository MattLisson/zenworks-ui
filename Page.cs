using Xamarin.Forms;

namespace Zenworks.UI {
    public class BasePage : ContentPage {
        public void ForceAppearing() {
            OnAppearing();
        }
    }

    public class BasePage<TViewModel> : BasePage where TViewModel : ViewModel {
        private TViewModel viewModel;
        public TViewModel ViewModel {
            get => viewModel;
            set {
                if (viewModel != null) {
                    viewModel.CurrentPage = null;
                }
                viewModel = value;
                BindingContext = viewModel;
                if (viewModel != null) {
                    viewModel.CurrentPage = this;
                }
            }
        }

        public bool WarnOnBackButtonWithUnsavedChanges { get; set; } = false;

#pragma warning disable CS8618 // Non-nullable field is uninitialized.
        // TODO: Fix the null viewmodel for Design-Time view.
        public BasePage() { }

        public BasePage(TViewModel model) {
#pragma warning restore CS8618 // Non-nullable field is uninitialized.
            ViewModel = model;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
        protected override bool OnBackButtonPressed() {
            if (!WarnOnBackButtonWithUnsavedChanges || !ViewModel.HasChanged) {
                return base.OnBackButtonPressed();
            }
            // Begin an asyncronous task on the UI thread because we intend to ask the users permission.
            Device.BeginInvokeOnMainThread(async () => {
                if (await DisplayAlert("Exit page?", "You have unsaved changes, are you sure you want to exit?", "Discard Changes", "Stay here")) {
                    base.OnBackButtonPressed();
                    ViewModel.FinishTask();
                }
            });

            // Always return true because this method is not asynchronous.
            // We must handle the action ourselves: see above.
            return true;
        }
    }
}
