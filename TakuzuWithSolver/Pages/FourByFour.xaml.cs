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
        GameMechanic logic;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            //display.InvalidateVisual();

            

            //for (int i = 0; i < logic.Pebbles.GetLength(0); i++)
            //{
            //    grid.RowDefinitions.Add(new RowDefinition());
            //    grid.ColumnDefinitions.Add(new ColumnDefinition());
            //}

            //for (int i = 0; i < logic.Pebbles.GetLength(0); i++)
            //{
            //    for (int j = 0; j < logic.Pebbles.GetLength(1); j++)
            //    {
            //        PebbleButton pebble = logic.Pebbles[i, j];
            //        Grid.SetRow(pebble, i);
            //        Grid.SetColumn(pebble, j);
            //        pebble.Click += new RoutedEventHandler(this.Grid_Click);
            //        grid.Children.Add(pebble);
            //    }
            //}
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            //display.InvalidateVisual();

            
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var ClickedPebble = e.OriginalSource as PebbleButton;
            //képnézegetőshöz hasonló pebble nézegető, amik így már külön-külön érzékelik a klikkeket
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

            ClickedPebble.Background = new ImageBrush() { ImageSource = ClickedPebble.ImageSource };
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
           // controller.KeyPressed(e.Key);
            //display.InvalidateVisual();
        }

        public FourByFour()
        {

            InitializeComponent();
            Takuzu takuzu = new Takuzu(4);
            logic = new GameMechanic(takuzu);


            for (int i = 0; i < logic.Pebbles.GetLength(0); i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            //PebbleButton btn = new PebbleButton()
            //{
            //    ImageSource = new BitmapImage(
            //                        new Uri(System.IO.Path.Combine("Images", "TileEmpty.png"), UriKind.RelativeOrAbsolute)),
            //    pebbleState = State.Empty
            //};
            //Button btn1 = new Button();
            //btn.Name = "btn1";
            ////btn.Content = "AASDSLADASÉLD?AÉS";
            //btn.BorderThickness = new Thickness(1);
            ////btn.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            //btn.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine("Images", "TileEmpty.png"), UriKind.RelativeOrAbsolute)) };
            //btn.Click += this.Grid_Click;
            //Grid.SetRow(btn, 0);
            //Grid.SetColumn(btn, 0);
            //grid.Children.Add(btn);



            for (int i = 0; i < logic.Pebbles.GetLength(0); i++)
            {
                for (int j = 0; j < logic.Pebbles.GetLength(1); j++)
                {
                    PebbleButton pebble = logic.Pebbles[i, j];
                    pebble.BorderThickness = new Thickness(1);
                    pebble.Background = new ImageBrush() { ImageSource = pebble.ImageSource};
                    Grid.SetRow(pebble, i);
                    Grid.SetColumn(pebble, j);
                    pebble.Click += this.Grid_Click;
                    grid.Children.Add(pebble);
                }
            }

            //logic = new Takuzu(4);
            //display.SetupModel(logic);
            //controller = new GameController(logic);
        }

    }
}
