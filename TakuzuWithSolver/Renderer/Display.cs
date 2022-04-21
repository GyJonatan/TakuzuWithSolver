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
                        ImageBrush brush = new ImageBrush();
                        switch (model.Map[i, j])
                        {
                            case Logic.State.Empty:
                                brush = new ImageBrush(
                                    new BitmapImage(
                                        new Uri(Path.Combine("Images", "TileEmpty.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Logic.State.Zero:
                                brush = new ImageBrush(
                                   new BitmapImage(
                                       new Uri(Path.Combine("Images", "TileBlue.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Logic.State.One:
                                brush = new ImageBrush(
                                   new BitmapImage(
                                       new Uri(Path.Combine("Images", "TileGreen.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            default:
                                break;
                        }

                        drawingContext.DrawRectangle(brush,
                                    new Pen(Brushes.Black, 0),
                                    new Rect(j * rectWidth, i * rectHeight, rectWidth, rectHeight));
                    }
                }
            }
        }
    }
}
