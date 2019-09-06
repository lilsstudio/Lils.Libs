using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Lils.WpfLib.Controls
{
    public class StringListEditor : Control
    {
        public static RoutedCommand RemoveItemCommand = new RoutedUICommand("Remove", "Remove", typeof(StringListEditor));

        static StringListEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StringListEditor), new FrameworkPropertyMetadata(typeof(StringListEditor)));

            RegisterCommands();
        }

        public StringListEditor()
        {
        }

        public event EventHandler<ItemIsRemovedEventArgs> ItemIsRemoved;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(StringListEditor), new PropertyMetadata(null));

        public bool DoDelete
        {
            get { return (bool)GetValue(DoDeleteProperty); }
            set { SetValue(DoDeleteProperty, value); }
        }

        public static readonly DependencyProperty DoDeleteProperty =
            DependencyProperty.Register("DoDelete", typeof(bool), typeof(StringListEditor), new PropertyMetadata(false));

        #region Commands

        private static void RegisterCommands()
        {
            CommandManager.RegisterClassCommandBinding(typeof(FrameworkElement), new CommandBinding(RemoveItemCommand, RemoveItemClassHandler, RemoveItemCanExecuteClassHandler));
        }

        private static void RemoveItemClassHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var targetListViewItem = e.Parameter as ListViewItem;
            var listView = ItemsControl.ItemsControlFromItemContainer(targetListViewItem) as ListView;

            DependencyObject obj = listView;
            do
            {
                obj = VisualTreeHelper.GetParent(obj);
                if (obj is StringListEditor editor)
                {
                    var index = listView.ItemContainerGenerator.IndexFromContainer(targetListViewItem);

                    if (editor.DoDelete)
                    {
                        if (editor.ItemIsRemoved != null)
                        {
                            var message = $"You should not handle {nameof(ItemIsRemoved)} event if Property {nameof(DoDelete)} is set to true.";
                            throw new InvalidOperationException(message);
                        }
                        if (editor.ItemsSource is IList list)
                        {
                            list.RemoveAt(index);
                        }
                    }
                    else
                    {
                        editor.ItemIsRemoved?.Invoke(editor, new ItemIsRemovedEventArgs(index));
                    }

                    break;
                }
            } while (obj != null);
        }

        private static void RemoveItemCanExecuteClassHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion
    }

    public class ItemIsRemovedEventArgs
    {
        public ItemIsRemovedEventArgs(int index)
        {
            Index = index;
        }

        public int Index { get; set; }
    }
}
