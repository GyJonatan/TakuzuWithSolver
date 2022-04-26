using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TakuzuWithSolver.Logic;

namespace TakuzuWithSolver.Renderer
{
    public class Display : FrameworkElement
    {
        IGameModel model;

        Size size;

        public void Resize(Size size) { this.size = size; }
        public void SetupModel(IGameModel model)
        {
            this.model = model;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (model != null && size.Width > 50 && size.Height > 50)
            {
                double rectWidth = size.Width / model.Map.GetLength(1);
                double rectHeight = size.Height / model.Map.GetLength(0);

                drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 0),
                    new Rect(0, 0, size.Width, size.Height));

                for (int i = 0; i < model.Map.GetLength(0); i++)
                {
                    for (int j = 0; j < model.Map.GetLength(1); j++)
                    {                        
                        PebbleButton pebble = new PebbleButton();
                        switch (model.Map[i, j])
                        {
                            case State.Empty:

                                pebble = new PebbleButton() 
                                { 
                                    ImageSource = new BitmapImage(
                                        new Uri(Path.Combine("Images", "TileEmpty.png"), UriKind.RelativeOrAbsolute)),
                                    pebbleState = State.Empty
                                };

                                break;
                            case State.Zero:

                                pebble = new PebbleButton()
                                {
                                    ImageSource = new BitmapImage(
                                        new Uri(Path.Combine("Images", "TileBlue.png"), UriKind.RelativeOrAbsolute)),
                                    pebbleState = State.Zero
                                };

                                break;
                            case State.One:

                                pebble = new PebbleButton()
                                {
                                    ImageSource = new BitmapImage(
                                        new Uri(Path.Combine("Images", "TileGreen.png"), UriKind.RelativeOrAbsolute)),
                                    pebbleState = State.One
                                };

                                break;
                        }

                    }
                }
            }
        }
    }
}
