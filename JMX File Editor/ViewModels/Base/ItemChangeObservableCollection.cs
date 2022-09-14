using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace JMXFileEditor
{
    public class ItemChangeObservableCollection<T> : ObservableCollection<T> 
        where T : BaseViewModel
    {
        #region Private Helpers
        private void RegisterPropertyChanged(IList items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                if (item != null)
                    item.PropertyChanged += new PropertyChangedEventHandler(OnItemPropertyChanged);
            }
        }
        private void UnRegisterPropertyChanged(IList items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                if (item != null)
                    item.PropertyChanged -= new PropertyChangedEventHandler(OnItemPropertyChanged);
            }
        }
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e) => base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender));
        #endregion

        #region Events
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                RegisterPropertyChanged(e.NewItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                UnRegisterPropertyChanged(e.OldItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                UnRegisterPropertyChanged(e.OldItems);
                RegisterPropertyChanged(e.NewItems);
            }

            base.OnCollectionChanged(e);
        }
        #endregion


        #region Public Methods
        protected override void ClearItems()
        {
            UnRegisterPropertyChanged(this);
            base.ClearItems();
        }
        #endregion
    }
}
