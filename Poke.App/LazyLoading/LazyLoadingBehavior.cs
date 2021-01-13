using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Poke.App.Mvvm;

namespace Poke.App.LazyLoading
{
    public sealed class LazyLoadingBehavior : Behavior<ItemsControl>
    {
        private ScrollViewer _scrollViewer;

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

            _scrollViewer = AssociatedObject.GetVisualChild<ScrollViewer>();

            if (_scrollViewer != null)
            {
                _scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;

                (_scrollViewer.DataContext as ILazyLoadAsync)?.InitializeAsync(PageSize);
            }
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (_scrollViewer.VerticalOffset >= _scrollViewer.ScrollableHeight)
            {
                (_scrollViewer.DataContext as ILazyLoadAsync)?.LoadNextAsync(PageSize);
            }
        }
    }
}
