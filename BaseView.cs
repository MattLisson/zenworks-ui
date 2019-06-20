using Xamarin.Forms;

namespace Zenworks.UI {

    public class BaseView<TViewModel> : ContentView where TViewModel : ViewModel {

        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create(nameof(ViewModel), typeof(TViewModel), typeof(BaseView<TViewModel>), propertyChanged: (bindable, oldValue, newValue) => {
                (bindable as BaseView<TViewModel>)?.OnViewModelChanged(oldValue as TViewModel, newValue as TViewModel);
            });

        public TViewModel ViewModel {
            get => (GetValue(ViewModelProperty) as TViewModel)!;
            set => SetValue(ViewModelProperty, value);
        }
        protected virtual void OnViewModelChanged(TViewModel? oldValue, TViewModel? newValue) {

        }
        public BaseView() { }
    }
}
