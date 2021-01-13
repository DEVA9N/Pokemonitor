using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Poke.App.Mvvm
{
    internal static class DependencyObjectExtension
    {
        internal static T GetVisualChild<T>(this DependencyObject dependencyObject) where T : FrameworkElement
        {
            if (dependencyObject == null)
            {
                return null;
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, i);

                var result = child as T ?? GetVisualChild<T>(child);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
