using System;
using System.ComponentModel;
using System.Runtime;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Xml;

namespace Axede.WPF.Softphone.Applications.GUI
{
    public class DataGridContentControlColumn : DataGridBoundColumn
    {
        private static Style _defaultElementStyle;
        private static Style _defaultEditingElementStyle;

        static DataGridContentControlColumn()
        {
            ElementStyleProperty.OverrideMetadata(typeof(DataGridContentControlColumn), new FrameworkPropertyMetadata(DefaultElementStyle));
            EditingElementStyleProperty.OverrideMetadata(typeof(DataGridContentControlColumn), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
        }

        public static Style DefaultElementStyle
        {
            get
            {
                if (_defaultElementStyle == null)
                {
                    Style style = new Style(typeof(ContentControl));

                    // When not in edit mode, the end-user should not be able to toggle the state
                    style.Setters.Add(new Setter(UIElement.IsHitTestVisibleProperty, false));
                    style.Setters.Add(new Setter(UIElement.FocusableProperty, false));
                    style.Setters.Add(new Setter(ContentControl.HorizontalAlignmentProperty, HorizontalAlignment.Center));
                    style.Setters.Add(new Setter(ContentControl.VerticalAlignmentProperty, VerticalAlignment.Top));

                    style.Seal();
                    _defaultElementStyle = style;
                }

                return _defaultElementStyle;
            }
        }

        public static Style DefaultEditingElementStyle
        {
            get
            {
                if (_defaultEditingElementStyle == null)
                {
                    Style style = new Style(typeof(ContentControl));

                    style.Setters.Add(new Setter(ContentControl.HorizontalAlignmentProperty, HorizontalAlignment.Center));
                    style.Setters.Add(new Setter(ContentControl.VerticalAlignmentProperty, VerticalAlignment.Top));

                    style.Seal();
                    _defaultEditingElementStyle = style;
                }

                return _defaultEditingElementStyle;
            }
        }

        protected override void CancelCellEdit(FrameworkElement editingElement, object uneditedValue)
        {


        }

        protected override bool CommitCellEdit(FrameworkElement editingElement)
        {
            return true;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            ContentControl content = new ContentControl();
            Binding b = new Binding();
            Binding bb = this.Binding as Binding;
            b.Path = bb.Path;
            b.Source = ContentControl.ContentProperty;

            content.SetBinding(ContentControl.ContentProperty, this.Binding);
            return content;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            ContentControl content = new ContentControl();
            content.BeginInit();
            Binding b = new Binding();
            Binding bb = this.Binding as Binding;
            b.Path = bb.Path;
            b.Source = cell.DataContext;

            content.SetBinding(ContentControl.ContentProperty, this.Binding);
            content.EndInit();
            return content;
        }

        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            ContentControl content = new ContentControl();
            Binding b = new Binding();
            Binding bb = this.Binding as Binding;
            b.Path = bb.Path;


            content.SetBinding(ContentControl.ContentProperty, this.Binding);
            return content;

        }
    }
}
