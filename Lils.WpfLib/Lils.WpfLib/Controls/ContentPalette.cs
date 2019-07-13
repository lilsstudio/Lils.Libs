using Lils.WpfLib.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lils.WpfLib.Controls
{
    [TemplatePart(Name = "PART_Pointer", Type = typeof(UIElement))]
    public class ContentPalette : ContentControl
    {
        #region private fields

        private const string ContentGridTemplateName = "PART_ContentGrid";
        private const string PointTemplateName = "PART_Pointer";

        private Grid contentGrid;
        private UIElement pointer;

        #endregion

        static ContentPalette()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentPalette), new FrameworkPropertyMetadata(typeof(ContentPalette)));
        }

        public ContentPalette()
        {
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        public Point Maximum
        {
            get { return (Point)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public Point Minimum
        {
            get { return (Point)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public Point Value
        {
            get { return (Point)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        [Category("Content")]
        public object Pointer
        {
            get { return (object)GetValue(PointerProperty); }
            set { SetValue(PointerProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(Point), typeof(ContentPalette), new PropertyMetadata(new Point(0, 0)));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(Point), typeof(ContentPalette), new PropertyMetadata(new Point(1, 1)));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Point), typeof(ContentPalette), new PropertyMetadata(new Point(0, 0)));

        public static readonly DependencyProperty PointerProperty =
            DependencyProperty.Register("Pointer", typeof(object), typeof(ContentPalette), new PropertyMetadata(null));

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

            Canvas.SetLeft(pointer, mousePos.X);
            Canvas.SetTop(pointer, mousePos.Y);

            var ratioX = 1 - (mousePos.X / contentGrid.ActualWidth);
            var ratioY = 1 - (mousePos.Y / contentGrid.ActualHeight);

            var offset = Maximum - Minimum;
            var scaledOffset = new Vector(offset.X * ratioX, offset.Y * ratioY);

            Value = scaledOffset + Minimum;
            Mouse.Capture(contentGrid);
        }

        private void ContentGrid_MouseUp(object sender, MouseEventArgs e)
        {
            Mouse.Capture(null);
        }
    }
}
