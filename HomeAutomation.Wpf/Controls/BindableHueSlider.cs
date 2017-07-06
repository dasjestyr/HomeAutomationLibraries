using System.Windows;
using System.Windows.Media;

namespace HomeAutomation.Wpf.Controls
{
    public class BindableHueSlider : BindableSlider
    {
        public Color Hue
        {
            get => (Color)GetValue(HueProperty);
            set => SetValue(HueProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HueProperty = DependencyProperty.Register("Hue", typeof(Color), typeof(BindableHueSlider), new PropertyMetadata(Color.FromRgb(255, 0, 0)));

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            Hue = new HslColor(newValue / 273.06, 240, 120);
        }
    }
}