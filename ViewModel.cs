using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Zenworks.UI {
    public class ViewModel : INotifyPropertyChanged {

        public delegate void VMEventHandler<TVm, T1, T2>(TVm viewModel, T1 value, T2 value2)
            where TVm : ViewModel;
        public delegate void VMEventHandler<TVm, T1>(TVm viewModel, T1 value)
            where TVm : ViewModel;
        public delegate void VMEventHandler<TVm>(TVm viewModel)
            where TVm : ViewModel;

        //The interface only includes this evennt
        public event PropertyChangedEventHandler PropertyChanged;

        public Page? CurrentPage { get; set; }

        public event VMEventHandler<ViewModel> TaskFinished;

        private bool hasChanged = false;
        public bool HasChanged {
            get => hasChanged;
            set {
                hasChanged = value;
                if (value) {
                    AnythingChanged?.Invoke(this);
                }
            }
        }
        public event VMEventHandler<ViewModel> AnythingChanged;

        protected static string[] Deps(params string[] args) {
            return args;
        }

        //Common implementations of SetProperty
        protected bool SetProperty<T>(ref T field, T value, string[]? dependantProperties = null, [CallerMemberName]string? name = null) {
            bool propertyChanged = false;

            //If we have a different value, do stuff
            if (!EqualityComparer<T>.Default.Equals(field, value)) {
                StopObservingChild(field);
                field = value;
                ObserveChild(field);

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

        #region Child Observation
        private void ChildViewModelChanged(ViewModel vm) {
            HasChanged = true;
        }

        private void ChildPropertyChanged(object sender, PropertyChangedEventArgs args) {
            HasChanged = true;
        }

        private void ChildCollectionChanged(object sender, NotifyCollectionChangedEventArgs args) {
            HasChanged = true;
        }

        protected void StopObservingChild(object? child) {
            if (child is ViewModel vm) {
                vm.AnythingChanged -= ChildViewModelChanged;
            } else if (child is INotifyPropertyChanged changable) {
                changable.PropertyChanged -= ChildPropertyChanged;
            } else if (child is INotifyCollectionChanged collection) {
                collection.CollectionChanged -= ChildCollectionChanged;
            }
        }

        protected void ObserveChild(object? child) {
            // If we can listen to this property, listen for changes for our HasChanged.
            if (child is ViewModel newVm) {
                newVm.AnythingChanged += ChildViewModelChanged;
            } else if (child is INotifyPropertyChanged newChangable) {
                newChangable.PropertyChanged += ChildPropertyChanged;
            } else if (child is INotifyCollectionChanged newCollection) {
                newCollection.CollectionChanged += ChildCollectionChanged;
            }
        }
        #endregion

        protected void OnPropertyChanged([CallerMemberName]string? name = null) {
            HasChanged = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name!));
        }

        public void FinishTask() {
            TaskFinished?.Invoke(this);
        }

        public virtual void OnAppearing() { }
    }
}
