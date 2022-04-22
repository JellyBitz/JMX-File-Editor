using System;
using System.Windows;
using System.Windows.Controls;

namespace JMXFileEditor
{
    /// <summary>
    /// Enable binding functionality to the item selected on Treeview's
    /// </summary>
    public class TreeviewSelectedItemEnabledProperty : BaseAttachedProperty<TreeviewSelectedItemEnabledProperty, bool>
    {
        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Make sure is a treeview attachment
            if (!(sender is TreeView element))
                return;

            // handler
            void OnSelectedItemChanged(object _s, EventArgs _e)
            {
                TreeviewSelectedItemProperty.SetValue(sender, element.SelectedItem);
            };
            // Set event tracking
            element.SelectedItemChanged -= OnSelectedItemChanged;
            if ((bool)value)
                element.SelectedItemChanged += OnSelectedItemChanged;
        }
    }
    /// <summary>
    /// Contains the selected item from treeview
    /// </summary>
    public class TreeviewSelectedItemProperty : BaseAttachedProperty<TreeviewSelectedItemProperty, object>
    {

    }
}