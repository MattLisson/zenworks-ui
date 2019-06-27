using System;
using Xamarin.Forms;

namespace Zenworks.UI {
    public class BasePage : ContentPage {
        public void ForceAppearing() {
            OnAppearing();
        }
    }

    public class BasePage<TViewModel> : BasePage where TViewModel : ViewModel {
        private readonly Lazy<TViewModel>? maybeLazyViewModel;

        private TViewModel? viewModel;
        public TViewModel? ViewModel {
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

        public TViewModel ForceViewModel => ViewModel ?? maybeLazyViewModel!.Value;

        public bool WarnOnBackButtonWithUnsavedChanges { get; set; } = false;

#pragma warning disable CS8618 // Non-nullable field is uninitialized.
        // TODO: Fix the null viewmodel for Design-Time view.
        public BasePage() { }

        public BasePage(Lazy<TViewModel> model) {
            maybeLazyViewModel = model;
        }

        public BasePage(TViewModel model) {
#pragma warning restore CS8618 // Non-nullable field is uninitialized.
            ViewModel = model;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            if (maybeLazyViewModel != null && ViewModel == null) {
                ViewModel = maybeLazyViewModel.Value;
            }
            ViewModel?.OnAppearing();
        }
        protected override bool OnBackButtonPressed() {
            if (ViewModel == null || !WarnOnBackButtonWithUnsavedChanges || !ViewModel.HasChanged) {
                return base.OnBackButtonPressed();
            }
            // Begin an asyncronous task on the UI thread because we intend to ask the users permission.
            Device.BeginInvokeOnMainThread(async () => {
                if (await DisplayAlert("Exit page?", "You have unsaved changes, are you sure you want to exit?", "Discard Changes", "Stay here")) {
                    base.OnBackButtonPressed();
                    ViewModel?.FinishTask();
                }
            });

            // Always return true because this method is not asynchronous.
            // We must handle the action ourselves: see above.
            return true;
        }
    }
}
