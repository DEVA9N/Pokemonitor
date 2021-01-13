using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Poke.App.LazyLoading
{
    public sealed class LazyLoadingBehavior : Behavior<ItemsControl>
    {
        private ScrollViewer _scrollViewer;

        public ILazyLoadAsync Loader
        {
            get => (ILazyLoadAsync)GetValue(LoaderProperty);
            set => SetValue(LoaderProperty, value);
        }

        public static readonly DependencyProperty LoaderProperty = DependencyProperty.Register("Loader", typeof(ILazyLoadAsync), typeof(LazyLoadingBehavior), new PropertyMetadata(null));

        public int PageSize
        {
            get => (int)GetValue(PageSizeProperty);
            set => SetValue(PageSizeProperty, value);
        }

        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(LazyLoadingBehavior), new PropertyMetadata(50));

        protected override void OnAttached()
        {
            base.OnAttached();
        
            TypeDescriptor.GetProperties(AssociatedObject)["ItemsSource"].AddValueChanged(AssociatedObject, ItemsSourceChanged);
        }

        private void ItemsSourceChanged(object sender, EventArgs e)
        {
            if (_scrollViewer != null)
            {
                _scrollViewer.ScrollChanged -= ScrollViewer_ScrollChanged;
            }
            
            _scrollViewer = FindChildControl<ScrollViewer>(AssociatedObject);
            
            if (_scrollViewer != null)
            {
                _scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
            
                Loader?.InitializeAsync(PageSize);
            }
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (_scrollViewer.VerticalOffset >= _scrollViewer.ScrollableHeight)
            {
                Loader?.LoadNextAsync(PageSize);
            }
        }

        private T FindChildControl<T>(DependencyObject control) where T : FrameworkElement
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                if (child != null && child is T)
                    return child as T;
                else
                    return FindChildControl<T>(child);
            }
            return default(T);
        }
    }
}
