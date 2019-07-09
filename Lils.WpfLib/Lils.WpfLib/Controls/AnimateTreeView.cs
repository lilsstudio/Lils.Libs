using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace Lils.WpfLib.Controls
{
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(AnimateTreeViewItem))]
    public class AnimateTreeView : TreeView
    {
        static AnimateTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimateTreeView), new FrameworkPropertyMetadata(typeof(AnimateTreeView)));
        }

        public static readonly DependencyProperty IsBranchSelectionEnableProperty =
            DependencyProperty.Register("IsBranchSelectionEnable", typeof(bool), typeof(AnimateTreeView), new PropertyMetadata(true));

        public static readonly DependencyProperty AnimateTimeProperty =
            DependencyProperty.Register("AnimateTime", typeof(Duration), typeof(AnimateTreeView), new PropertyMetadata(default(Duration)));

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

        protected override DependencyObject GetContainerForItemOverride() => new AnimateTreeViewItem();

        protected override bool IsItemItsOwnContainerOverride(object item) => item is AnimateTreeViewItem;
    }


    [TemplatePart(Name = "PART_AnimateTarget", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Expander", Type = typeof(ToggleButton))]
    [TemplatePart(Name = "PART_ItemsHost", Type = typeof(ItemsPresenter))]
    [TemplatePart(Name = "PART_HeaderBorder", Type = typeof(FrameworkElement))]
    public class AnimateTreeViewItem : TreeViewItem
    {
        public static readonly DependencyProperty AnimateTimeProperty =
            DependencyProperty.Register("AnimateTime", typeof(Duration), typeof(AnimateTreeViewItem), new PropertyMetadata(default(Duration), AnimateTimePropertyChanged));

        public static readonly DependencyProperty IsBranchSelectionEnableProperty =
            DependencyProperty.Register("IsBranchSelectionEnable", typeof(bool), typeof(AnimateTreeViewItem), new PropertyMetadata(true));

        static AnimateTreeViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimateTreeViewItem), new FrameworkPropertyMetadata(typeof(AnimateTreeViewItem)));
        }

        private static QuadraticEase quadraticEaseInOut = new QuadraticEase { EasingMode = EasingMode.EaseInOut };

        private DoubleAnimation ExpandAnimation { get; } = new DoubleAnimation { To = 1, EasingFunction = quadraticEaseInOut };

        private DoubleAnimation CollapseAnimation { get; } = new DoubleAnimation { To = 1, EasingFunction = quadraticEaseInOut };

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

        protected override DependencyObject GetContainerForItemOverride() => new AnimateTreeViewItem();

        protected override bool IsItemItsOwnContainerOverride(object item) => item is AnimateTreeViewItem;

        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            if (!IsBranchSelectionEnable && HasItems)
            {
                IsSelected = false;
            }
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
            var animateTarget = GetTemplateChild("PART_AnimateTarget") as FrameworkElement;

            if (GetTemplateChild("PART_Expander") is ToggleButton expander)
            {
                expander.Checked += (s, e) =>
                {
                    ExpandAnimation.Duration = AnimateTime;
                    animateTarget?.BeginAnimation(HeightProperty, ExpandAnimation);
                };

                expander.Unchecked += (s, e) => animateTarget?.BeginAnimation(HeightProperty, CollapseAnimation);
            }

            if (GetTemplateChild("PART_ItemsHost") is FrameworkElement itemsHost)
            {
                itemsHost.SizeChanged += (s, e) =>
                {
                    ExpandAnimation.To = e.NewSize.Height;
                    if (IsExpanded)
                    {
                        ExpandAnimation.Duration = TimeSpan.FromSeconds(0);
                        animateTarget?.BeginAnimation(HeightProperty, ExpandAnimation);
                    }
                };
            }

            if (GetTemplateChild("PART_HeaderBorder") is FrameworkElement headerBorder)
            {
                headerBorder.MouseEnter += (s, e) => { VisualStateManager.GoToState(this, "HeaderMouseOver", false); };
                headerBorder.MouseLeave += (s, e) => { VisualStateManager.GoToState(this, "HeaderNormal", false); };
            }
        }
    }
}
