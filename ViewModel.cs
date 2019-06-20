using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Zenworks.UI {
    public class ViewModel : INotifyPropertyChanged {
        //The interface only includes this evennt
        public event PropertyChangedEventHandler PropertyChanged;

        public Page? CurrentPage { get; set; }

        public event Action<ViewModel> TaskFinished;

        protected static string[] Deps(params string[] args) {
            return args;
        }

        //Common implementations of SetProperty
        protected bool SetProperty<T>(ref T field, T value, string[]? dependantProperties = null, [CallerMemberName]string? name = null) {
            bool propertyChanged = false;

            //If we have a different value, do stuff
            if (!EqualityComparer<T>.Default.Equals(field, value)) {
                field = value;

                // Compiler sets name from the annotation.
                OnPropertyChanged(name!);
                if (dependantProperties != null) {
                    foreach (string depName in dependantProperties) {
                        OnPropertyChanged(depName);
                    }
                }
                propertyChanged = true;
            }

            return propertyChanged;
        }

        //The C#6 version of the common implementation
        protected void OnPropertyChanged([CallerMemberName]string? name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
        }

        public void FinishTask() {
            TaskFinished?.Invoke(this);
        }

        public virtual void OnAppearing() { }
    }
}
