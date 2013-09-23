using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Countries.Attachements
{
    public static class Behaviours
    {
        public static ICommand GetMouseClick(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseClickProperty);
        }

        public static void SetMouseClick(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseClickProperty, value);
        }

        public static readonly DependencyProperty MouseClickProperty =
            DependencyProperty.RegisterAttached("MouseClick", typeof(ICommand), typeof(Behaviours), new PropertyMetadata(null, SubscribeToPreviewMouseDownEvent));

        public static object GetMouseClickParameter(DependencyObject obj)
        {
            return obj.GetValue(MouseClickParameterProperty);
        }

        public static void SetMouseClickParameter(DependencyObject obj, object value)
        {
            obj.SetValue(MouseClickParameterProperty, value);
        }

        public static readonly DependencyProperty MouseClickParameterProperty =
            DependencyProperty.RegisterAttached("MouseClickParameter", typeof(object), typeof(Behaviours), new PropertyMetadata(null));

        private static void SubscribeToPreviewMouseDownEvent(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var sender = d as UIElement;
            if (sender != null)
            {
                sender.PreviewMouseDown += ExecuteCommand;
            }
        }

        private static void ExecuteCommand(object sender, object e)
        {
            var element = sender as UIElement;
            var command = element.GetValue(MouseClickProperty) as ICommand;
            if (command != null)
            {
                var commandParameter = element.GetValue(MouseClickParameterProperty);
                if (command.CanExecute(commandParameter))
                {
                    command.Execute(commandParameter);
                }
            }
        }
    }
}
