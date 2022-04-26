using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace TakuzuWithSolver.Logic
{
    public class PebbleButton : Button
    {

        public ImageSource ImageSource { get; set; }
        public State pebbleState { get; set; }


        //static PebbleButton()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(PebbleButton), new FrameworkPropertyMetadata(typeof(PebbleButton)));
        //}

        //public static readonly DependencyProperty ImageSourceProperty = 
        //    DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(PebbleButton), new PropertyMetadata(null));
        //public ImageSource ImageSource
        //{
        //    get { return (ImageSource)GetValue(ImageSourceProperty); }
        //    set { SetValue(ImageSourceProperty, value); }
        //}


        //public State pebbleState
        //{
        //    get { return (State)GetValue(pebbleStateProperty); }
        //    set { SetValue(pebbleStateProperty, value); }
        //}

        //public static readonly DependencyProperty pebbleStateProperty =
        //    DependencyProperty.Register("pebbleState", typeof(State), typeof(PebbleButton), new PropertyMetadata(null));


    }
}
