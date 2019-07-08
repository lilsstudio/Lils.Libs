using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

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

        GeometryModel3D layerGeometryModel;

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

        public double YOffset
        {
            get { return (double)GetValue(YOffsetProperty); }
            set { SetValue(YOffsetProperty, value); }
        }

        public object LayerContent
        {
            get { return (object)GetValue(LayerContentProperty); }
            set { SetValue(LayerContentProperty, value); }
        }

        public static readonly DependencyProperty XYContentProperty =
            DependencyProperty.Register("XYContent", typeof(object), typeof(ContentCube), new PropertyMetadata(null));

        public static readonly DependencyProperty ZYContentProperty =
            DependencyProperty.Register("ZYContent", typeof(object), typeof(ContentCube), new PropertyMetadata(null));

        public static readonly DependencyProperty XZContentProperty =
            DependencyProperty.Register("XZContent", typeof(object), typeof(ContentCube), new PropertyMetadata(null));

        public static readonly DependencyProperty LayerContentProperty =
            DependencyProperty.Register("LayerContent", typeof(object), typeof(ContentCube), new PropertyMetadata(null));

        public static readonly DependencyProperty YOffsetProperty =
            DependencyProperty.Register("YOffset", typeof(double), typeof(ContentCube), new PropertyMetadata(0.0, YOffsetPropertyChangedCallback, CoerceYOffsetPropertyCallback));

        private static void YOffsetPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContentCube cube)
            {
                if (cube.layerGeometryModel.Transform is TranslateTransform3D t3d)
                {
                    // TODO: replace this with binding
                    t3d.OffsetY = (double)e.NewValue * 10;
                }
            }
        }

        private static object CoerceYOffsetPropertyCallback(DependencyObject d, object baseValue)
        {
            if ((double)baseValue < 0)
            {
                return 0.0;
            }
            else if ((double)baseValue > 1)
            {
                return 1.0;
            }
            else
            {
                return baseValue;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            layerGeometryModel = GetTemplateChild("PART_LayerModel3D") as GeometryModel3D;
        }
    }
}
