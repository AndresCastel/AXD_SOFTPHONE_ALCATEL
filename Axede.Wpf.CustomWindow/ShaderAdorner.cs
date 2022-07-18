using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;

namespace Axede.WPF.CustomWindow
{
    
    public class ShaderAdorner : Adorner
    {
        #region Constructors

        public ShaderAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            this.Background = new SolidColorBrush(Colors.Red);
            this.Background.Opacity = 0.5d;
            this.StrokeBorder = null;
        }

        public ShaderAdorner(UIElement adornedElement, SolidColorBrush background, Pen strokeBorder)
            : this(adornedElement)
        {
            //caller needs to have set opacity on background brush
            this.Background = background;
            this.StrokeBorder = strokeBorder;
        }

        #endregion Constructors

        #region Properties

        SolidColorBrush Background
        {
            get;
            set;
        }

        Pen StrokeBorder
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        protected override void OnRender(DrawingContext drawingContext)
        {
            FrameworkElement elem = (FrameworkElement)this.AdornedElement;
            Rect adornedElementRect = new Rect(0, 0, elem.ActualWidth, elem.ActualHeight);
            drawingContext.DrawRectangle(Background, StrokeBorder, adornedElementRect);
        }

        #endregion Methods
    }
}
