using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace HomeAutomation.Wpf.Controls
{
    public class BindableSlider : Slider
    {
        public double OldValue
        {
            get => (double)GetValue(OldValueProperty);
            set => SetValue(OldValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for OldValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OldValueProperty =
            DependencyProperty.Register("OldValue", typeof(double), typeof(BindableSlider), new PropertyMetadata(default(double)));

        public IInputElement CommandTarget
        {
            get => (IInputElement)GetValue(CommandTargetProperty);
            set => SetValue(CommandTargetProperty, value);
        }

        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register(
                "CommandTarget",
                typeof(IInputElement),
                typeof(BindableSlider),
                new UIPropertyMetadata(null)
            );

        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }


        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(BindableSlider),
                new UIPropertyMetadata(null)
            );

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(BindableSlider),
                typeMetadata: new PropertyMetadata(null,CommandChanged
                ));

        // Command dependency property change callback.
        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cs = (BindableSlider)d;
            cs.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            // If oldCommand is not null, then we need to remove the handlers.
            if (oldCommand != null)
            {
                RemoveCommand(oldCommand, newCommand);
            }
            AddCommand(oldCommand, newCommand);
        }

        // Remove an old command from the Command Property.
        private void RemoveCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = CanExecuteChanged;
            oldCommand.CanExecuteChanged -= handler;
        }

        // Add the command.
        private void AddCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = CanExecuteChanged;
            if (newCommand != null)
            {
                newCommand.CanExecuteChanged += handler;
            }
        }

        private void CanExecuteChanged(object sender, EventArgs e)
        {
            if (this.Command == null) return;
            var command = Command as RoutedCommand;

            // If a RoutedCommand.
            IsEnabled = command?.CanExecute(CommandParameter, CommandTarget) ?? Command.CanExecute(CommandParameter);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);

            if (Command == null)
                return;

            var command = Command as RoutedCommand;

            if (command != null)
            {
                command.Execute(CommandParameter, CommandTarget);
            }
            else
            {
                Command.Execute(CommandParameter);
            }
        }

        protected override void OnThumbDragStarted(DragStartedEventArgs e)
        {
            OldValue = Value;
            OnOldValueChanged?.Invoke(this, e);
            base.OnThumbDragStarted(e);
        }

        public delegate void OldValueChanged(object sender, DragStartedEventArgs e);

        public event OldValueChanged OnOldValueChanged;
    }
}
