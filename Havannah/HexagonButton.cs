using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Havannah
{
    class HexagonButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            // Create an array of points.
            Point[] myArray =
                     {
                new Point(Convert.ToInt32((33.3/Convert.ToDouble(100)*Convert.ToDouble(ClientSize.Width))),  0),
                new Point(0,ClientSize.Height/2),
                new Point( Convert.ToInt32((33.3/Convert.ToDouble(100)*Convert.ToDouble(ClientSize.Width))),ClientSize.Height),
                new Point(Convert.ToInt32((66.6/Convert.ToDouble(100)*Convert.ToDouble(ClientSize.Width))), ClientSize.Height),  // Half
                new Point(ClientSize.Width, ClientSize.Height/2),
                new Point(Convert.ToInt32((66.6/Convert.ToDouble(100)*Convert.ToDouble(ClientSize.Width))),0),
                new Point(Convert.ToInt32((33.3/Convert.ToDouble(100)*Convert.ToDouble(ClientSize.Width))), 0)
             };

            // Create a GraphicsPath object and add a polygon.
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddPolygon(myArray);

            Region = new Region(myPath);
            base.OnPaint(pevent);
        }

        public static string GenerateHexagonButtonName(int x, int y) => x + ", " + y;

        public static string GenerateHexName(int x, int y)
        {
            string letter;
            switch (x)
            {
                case 0:
                    letter = "a";
                    break;
                case 1:
                    letter = "b";
                    break;
                case 2:
                    letter = "c";
                    break;
                case 3:
                    letter = "d";
                    break;
                case 4:
                    letter = "e";
                    break;
                default:
                    letter = "XXX";
                    break;
            }

            return letter + ", " + y;
        }


        public Tuple<int, int> GetHexCoordinates()
        {
            var coordinates = Name.Split(',');

            int x;
            switch (coordinates[0])
            {
                case "a":
                    x = 0;
                    break;
                case "b":
                    x = 1;
                    break;
                case "c":
                    x = 2;
                    break;
                case "d":
                    x = 3;
                    break;
                case "e":
                    x = 4;
                    break;
                default:
                    x = 666;
                    break;
            }

            return new Tuple<int, int>(x, int.Parse(coordinates[1]));
        }



        public Tuple<int, int> GetButtonCoordinates(int boardSize)
        {
            var coordinates = Name.Split(',');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);

            return new Tuple<int, int>(x, y + (x > boardSize - 1 ? x - boardSize + 1 : 0));
        }

        public Tuple<int, int> GetButtonCoordinates()
        {
            var coordinates = Name.Split(',');

            return new Tuple<int, int>(int.Parse(coordinates[0]), int.Parse(coordinates[1]));
        }


    }
}
