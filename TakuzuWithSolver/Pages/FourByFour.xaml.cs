﻿using System;
using System.Collections.Generic;
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
using TakuzuWithSolver.Logic;
using System.IO;

namespace TakuzuWithSolver.Pages
{
    /// <summary>
    /// Interaction logic for FourByFour.xaml
    /// </summary>
    public partial class FourByFour : Page
    {
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var ClickedPebble = e.OriginalSource as PebbleButton;

            switch (ClickedPebble.pebbleState)
            {
                case State.Empty:
                    ClickedPebble.ImageSource =
                        new BitmapImage(new Uri(System.IO.Path.Combine("Images", "TileBlue.png"),
                        UriKind.RelativeOrAbsolute));

                    ClickedPebble.pebbleState = State.Zero;
                    break;

                case State.Zero:
                    ClickedPebble.ImageSource =
                        new BitmapImage(new Uri(System.IO.Path.Combine("Images", "TileGreen.png"),
                        UriKind.RelativeOrAbsolute));

                    ClickedPebble.pebbleState = State.One;
                    break;

                case State.One:
                    ClickedPebble.ImageSource =
                        new BitmapImage(new Uri(System.IO.Path.Combine("Images", "TileEmpty.png"),
                        UriKind.RelativeOrAbsolute));

                    ClickedPebble.pebbleState = State.Empty;
                    break;
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
           // controller.KeyPressed(e.Key);
            display.InvalidateVisual();
        }

        public FourByFour()
        {

            InitializeComponent();
            //LabyLogic logic = new LabyLogic();
            //display.SetupModel(logic);
            //controller = new GameController(logic);
        }

        
    }
}
