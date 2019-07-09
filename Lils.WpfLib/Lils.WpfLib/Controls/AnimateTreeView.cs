using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lils.WpfLib.Controls
{
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof (AnimateTreeViewItem))]
    public class AnimateTreeView : TreeView
    {
        static AnimateTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimateTreeView), new FrameworkPropertyMetadata(typeof(AnimateTreeView)));
        }

        public static readonly DependencyProperty IsBranchSelectionEnableProperty =
    DependencyProperty.Register("IsBranchSelectionEnable", typeof(bool), typeof(AnimateTreeView),
        new PropertyMetadata(true));

        public static readonly DependencyProperty AnimateTimeProperty =
            DependencyProperty.Register("AnimateTime", typeof(Duration), typeof(AnimateTreeView),
                new PropertyMetadata(default(Duration)));

        public bool IsBranchSelectionEnable
        {
            get { return (bool)GetValue(IsBranchSelectionEnableProperty); }
            set { SetValue(IsBranchSelectionEnableProperty, value); }
        }

        public Duration AnimateTime
        {
            get { return (Duration)GetValue(AnimateTimeProperty); }
            set { SetValue(AnimateTimeProperty, value); }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new AnimateTreeViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is AnimateTreeViewItem;
        }
    }

    [TemplatePart(Name = "PART_AnimateTarget", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Expander", Type = typeof(ToggleButton))]
    [TemplatePart(Name = "PART_ItemsHost", Type = typeof(ItemsPresenter))]
    [TemplatePart(Name = "PART_HeaderBorder", Type = typeof(FrameworkElement))]
    public class AnimateTreeViewItem : TreeViewItem
    {
        public static readonly DependencyProperty AnimateTimeProperty =
            DependencyProperty.Register("AnimateTime", typeof(Duration), typeof(AnimateTreeViewItem),
                new PropertyMetadata(default(Duration), AnimateTimePropertyChanged));

        public static readonly DependencyProperty IsBranchSelectionEnableProperty =
            DependencyProperty.Register("IsBranchSelectionEnable", typeof(bool), typeof(AnimateTreeViewItem),
                new PropertyMetadata(true));

        private FrameworkElement _animateTarget;
        private DoubleAnimation _collapseAnimation;
        private DoubleAnimation _expandAnimation;

        static AnimateTreeViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimateTreeViewItem),
                new FrameworkPropertyMetadata(typeof(AnimateTreeViewItem)));
        }

        private DoubleAnimation ExpandAnimation => _expandAnimation ?? (_expandAnimation = new DoubleAnimation
        {
            To = 1,
            Duration = AnimateTime,
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
        });

        private DoubleAnimation CollapseAnimation => _collapseAnimation ?? (_collapseAnimation = new DoubleAnimation
        {
            To = 1,
            Duration = AnimateTime,
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
        });

        public bool IsBranchSelectionEnable
        {
            get { return (bool)GetValue(IsBranchSelectionEnableProperty); }
            set { SetValue(IsBranchSelectionEnableProperty, value); }
        }

        public Duration AnimateTime
        {
            get { return (Duration)GetValue(AnimateTimeProperty); }
            set { SetValue(AnimateTimeProperty, value); }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new AnimateTreeViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is AnimateTreeViewItem;
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            if (!IsBranchSelectionEnable)
            {
                if (HasItems)
                {
                    IsSelected = false;
                }
            }
        }

        protected void Expand(Duration duration)
        {
            ExpandAnimation.Duration = duration;
            _animateTarget?.BeginAnimation(HeightProperty, ExpandAnimation);
        }

        protected void Collapse()
        {
            _animateTarget?.BeginAnimation(HeightProperty, CollapseAnimation);
        }

        private static void AnimateTimePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AnimateTreeViewItem)?.ResetAnimateTime();
        }

        private void ResetAnimateTime()
        {
            ExpandAnimation.Duration = AnimateTime;
            CollapseAnimation.Duration = AnimateTime;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _animateTarget = GetTemplateChild("PART_AnimateTarget") as FrameworkElement;

            var expander = GetTemplateChild("PART_Expander") as ToggleButton;
            if (expander != null)
            {
                expander.Checked += (s, e) => { Expand(AnimateTime); };
                expander.Unchecked += (s, e) => { Collapse(); };
            }

            var itemsHost = GetTemplateChild("PART_ItemsHost") as FrameworkElement;
            if (itemsHost != null)
            {
                itemsHost.SizeChanged += ItemsHostOnSizeChanged;
            }

            var headerBorder = GetTemplateChild("PART_HeaderBorder") as FrameworkElement;
            if (headerBorder != null)
            {
                headerBorder.MouseEnter += (s, e) => { VisualStateManager.GoToState(this, "HeaderMouseOver", false); };
                headerBorder.MouseLeave += (s, e) => { VisualStateManager.GoToState(this, "HeaderNormal", false); };
            }
        }

        private void ItemsHostOnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            ExpandAnimation.To = sizeChangedEventArgs.NewSize.Height;
            if (IsExpanded)
            {
                Expand(TimeSpan.FromSeconds(0));
            }
        }
    }
}
