
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Axede.WPF.Softphone.Applications.UtilControls.PopUp
{
    /// <summary>
    /// A class for creating an irregular popup window for information
    /// </summary>
    public class PopupWinHelper
    {
        /// <summary>
        ///  Show a fade in & fade out pop up window at the right lower corner of the primary screen
        /// </summary>
        /// <param name="winHeight">Pop up window's height</param>
        /// <param name="winWidth">Pop up window's width</param>
        /// <param name="bgImg">The background image of the pop up window</param>
        /// <param name="msgText">Message shown on the pop up window</param>
        /// <param name="msgPadding">The message's padding value to the edge of the window, it depends on the image's border</param>
        public static void ShowPopUp(Window popUp, double winHeight, double winWidth, string msgText, Thickness msgPadding)
        {
            popUp.Name = "PopUp";      
            //Create a inner Grid
            Grid g = new Grid();

         
            //Create the fade in & fade out animation
            DoubleAnimation winFadeAni = new DoubleAnimation();
            winFadeAni.From = 0;
            winFadeAni.To = 1;
            winFadeAni.Duration = new Duration(TimeSpan.FromSeconds(1.5));		//Duration for 1.5 seconds
            winFadeAni.AutoReverse = true;
            winFadeAni.Completed += delegate(object sender, EventArgs e)			//Close the window when this animation is completed
            {
                popUp.Close();
            };

         
            popUp.Show();
        }


    }
}
