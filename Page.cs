﻿using Xamarin.Forms;

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
    }
}