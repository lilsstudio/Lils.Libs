using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using Lils.WpfLib.Tools;

namespace Lils.WpfLib.Controls
{
    /// <summary>
    /// A slider can fill with content
    /// </summary>
    [TemplatePart(Name = "PART_ContentGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_Pointer", Type = typeof(UIElement))]
    public class ContentSlider : ContentControl
    {
        #region private fields

        private const string ContentGridTemplateName = "PART_ContentGrid";
        private const string PointTemplateName = "PART_Pointer";

        private Grid contentGrid;
        private UIElement pointer;

        #endregion

        static ContentSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentSlider), new FrameworkPropertyMetadata(typeof(ContentSlider)));
        }

        public ContentSlider()
        {
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }



        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        [Category("Content")]
        public object Pointer
        {
            get { return (object)GetValue(PointerProperty); }
            set { SetValue(PointerProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(ContentSlider), new PropertyMetadata(0.0));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(ContentSlider), new PropertyMetadata(1.0));

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ContentSlider), new PropertyMetadata(Orientation.Vertical));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ContentSlider), new PropertyMetadata(0.0));

        public static readonly DependencyProperty PointerProperty =
            DependencyProperty.Register("Pointer", typeof(object), typeof(ContentSlider), new PropertyMetadata(null));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            contentGrid = GetTemplateChild(ContentGridTemplateName) as Grid;
            contentGrid.MouseDown += ContentGrid_MouseAction;
            contentGrid.MouseMove += ContentGrid_MouseAction;
            contentGrid.MouseUp += ContentGrid_MouseUp;

            pointer = GetTemplateChild(PointTemplateName) as UIElement;
        }

        private void ContentGrid_MouseAction(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
                return;

            var mousePos = e.GetPosition(contentGrid);
            var limit = new Point(contentGrid.ActualWidth, contentGrid.ActualHeight);
            mousePos = mousePos.Coerce(new Point(0, 0), limit);

            double ratio;
            if (Orientation == Orientation.Horizontal)
            {
                Canvas.SetLeft(pointer, mousePos.X);
                ratio = 1 - (mousePos.X / contentGrid.ActualWidth);
            }
            else
            {
                Canvas.SetTop(pointer, mousePos.Y);
                ratio = 1 - (mousePos.Y / contentGrid.ActualHeight);
            }

            Value = (Maximum - Minimum) * ratio + Minimum;
            Mouse.Capture(contentGrid);
        }

        private void ContentGrid_MouseUp(object sender, MouseEventArgs e)
        {
            Mouse.Capture(null);
        }
    }
}
