using System;
using Xamarin.Forms;

namespace Zenworks.UI {
    public class LazyPage<T> : Page where T : BasePage {

        public event Action<BasePage> ReplaceMe;
        private readonly Lazy<T> page;
        public LazyPage(string title, Lazy<T> page) {
            Title = title;
            this.page = page;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            ReplaceMe(page.Value);
        }
    }
}
