using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lils.WpfLib.Controls
{
    /// <summary>
    /// A slider can fill with content
    /// </summary>
    public class ContentCube : Control
    {
        static ContentCube()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentCube), new FrameworkPropertyMetadata(typeof(ContentCube)));
        }

        public ContentCube()
        {
        }

        public object XYContent
        {
            get { return (object)GetValue(XYContentProperty); }
            set { SetValue(XYContentProperty, value); }
        }

        public object ZYContent
        {
            get { return (object)GetValue(ZYContentProperty); }
            set { SetValue(ZYContentProperty, value); }
        }

        public object XZContent
        {
            get { return (object)GetValue(XZContentProperty); }
            set { SetValue(XZContentProperty, value); }
        }

        public static readonly DependencyProperty XYContentProperty =
            DependencyProperty.Register("XYContent", typeof(object), typeof(ContentCube), new PropertyMetadata(null));

        public static readonly DependencyProperty ZYContentProperty =
            DependencyProperty.Register("ZYContent", typeof(object), typeof(ContentCube), new PropertyMetadata(null));

        public static readonly DependencyProperty XZContentProperty =
            DependencyProperty.Register("XZContent", typeof(object), typeof(ContentCube), new PropertyMetadata(null));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
