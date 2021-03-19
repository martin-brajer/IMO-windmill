using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IMO_windmill
{
    public partial class Form_main : Form
    {
        private Bitmap bitmap;
        private Graphics graphics;
        private Random random = new Random();
        private Point[] cornerPoints = new Point[4];

        /// <summary>
        /// Members of S set. Coordinate origin is in top left.
        /// </summary>
        private List<Point> points = new List<Point>();
        private bool[] pointsColour;
        /// <summary>
        /// Index the next point.
        /// </summary>
        private Tuple<int, double> nextIndexAngle;
        private double distance;
        private LastPointIndexes lastPointIndexes = new LastPointIndexes(-1, -1);

        /// <summary>
        /// Index of a point around which to rotate. (-1) means empty.
        /// </summary>
        private int _centerOfRotationIndex = -1;
        /// <summary>
        /// Line direction.
        /// Start at positive x-axis, counterclockwise.
        /// </summary>
        private double _angle = Math.PI / 2;  // pi/2 == Vertical.
        private double velocity = 1;
        /// <summary>
        /// Directions pointing from center of rotation to corners.
        /// </summary>
        private double[] cornerAgnles = new double[4];

        private Timer timer = new Timer();
        private readonly int timerInterval = 40;  // [ms]. E.g. 25 times per sec.

        public double Angle
        {
            get => _angle;
            set => _angle = AngleTrim(value);
        }

        public Point CenterOfRotation
        {
            get
            {
                if (CenterOfRotationIndex == -1)
                    return Point.Empty;

                return points[CenterOfRotationIndex];
            }
        }

        public int CenterOfRotationIndex
        {
            get
            {
                return _centerOfRotationIndex;
            }

            set
            {
                _centerOfRotationIndex = value;
                if (_centerOfRotationIndex >= 0)
                {
                    FindCornerAngles(CenterOfRotation);
                    lastPointIndexes.Add(_centerOfRotationIndex);
                    if (pointsColour != null)
                    {
                        pointsColour[_centerOfRotationIndex] = true;
                    }
                }
            }
        }

        public Form_main()
        {
            InitializeComponent();
        }

        private void Form_main_Load(object sender, EventArgs e)
        {
            PrintPointPartition();
            FindCornerPoints();

            Draw();
            BtnRnd_Click(this, EventArgs.Empty);

            timer.Tick += Timer_Tick;
            timer.Interval = timerInterval;

            Draw();
        }

        private void FindCornerPoints()
        {
            cornerPoints = new Point[4] {
                new Point(0, pictureBox.Height),
                new Point(pictureBox.Width, pictureBox.Height),
                new Point(pictureBox.Width, 0),
                new Point(0, 0),
            };
        }

        private double AngleTrim(double angle)
        {
            return AngleTrim(angle, -Math.PI);
        }
        private double AngleTrim(double angle, double lowerLimit)
        {
            // Too low.
            if (angle < lowerLimit)
            {
                return AngleTrim(angle + (2 * Math.PI), lowerLimit: lowerLimit);
            }
            // Too high.
            else if (angle >= lowerLimit + (2 * Math.PI))
            {
                return AngleTrim(angle - (2 * Math.PI), lowerLimit: lowerLimit);
            }
            // In range.
            else
            {
                return angle;
            }
        }

        private Tuple<int, double> AngleTrim(Tuple<int, double> tuple)
        {
            return new Tuple<int, double>(tuple.Item1, AngleTrim(tuple.Item2));
        }

        private void Draw()
        {
            if (bitmap != null)
                bitmap.Dispose();
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = bitmap;
            if (graphics != null)
                graphics.Dispose();
            graphics = Graphics.FromImage(pictureBox.Image);

            // Points.
            float pointRadius = 4;
            for (int i = 0; i < points.Count; i++)
            {
                Brush brush = Brushes.Blue;
                if (pointsColour != null)
                {
                    brush = pointsColour[i] ? Brushes.Red : Brushes.Blue;
                }

                graphics.FillEllipse(
                    brush: brush,
                    x: points[i].X - pointRadius,
                    y: points[i].Y - pointRadius,
                    width: 2 * pointRadius,
                    height: 2 * pointRadius);
            }

            // Mark center of rotation of the line.
            if (CenterOfRotation.IsEmpty) return;

            float centerOfRotationDiameter = 13;
            graphics.DrawEllipse(
                    pen: new Pen(Brushes.Green, 4),
                    x: CenterOfRotation.X - (centerOfRotationDiameter / 2),
                    y: CenterOfRotation.Y - (centerOfRotationDiameter / 2),
                    width: centerOfRotationDiameter,
                    height: centerOfRotationDiameter);

            // The line.
            var p12 = PointAngle2BorderPoints(CenterOfRotation, Angle);
            graphics.DrawLine(
                pen: new Pen(Brushes.Black, 2),
                pt1: p12.Item1, pt2: p12.Item2);
        }

        private Tuple<Point, Point> PointAngle2BorderPoints(Point point, double angle)
        {
            return new Tuple<Point, Point>(
                PointAngle2BorderPoint_direct(point, angle),
                PointAngle2BorderPoint_direct(point, AngleTrim(angle + Math.PI))
                );
        }

        private Point PointAngle2BorderPoint_direct(Point point, double angle)
        {
            switch (FindDirection(angle))
            {
                case Direction.Right:
                    return new Point(pictureBox.Width, (int)Math.Round(point.Y - (pictureBox.Width - point.X) * Math.Tan(angle)));

                case Direction.Top:
                    return new Point((int)Math.Round(point.X - point.Y * Math.Tan(angle - (Math.PI / 2))), 0);

                case Direction.Left:
                    return new Point(0, (int)Math.Round(point.Y + point.X * Math.Tan(AngleTrim(angle - Math.PI))));

                case Direction.Bottom:
                    return new Point((int)Math.Round(point.X + (pictureBox.Height - point.Y) * Math.Tan(angle + (Math.PI / 2))), pictureBox.Height);

                default:
                    return new Point();
            }
        }

        private Direction FindDirection(double angle)
        {
            for (int i = 0; i < 4; i++)
            {
                if (angle < cornerAgnles[i])
                    return (Direction)i;
            }

            return 0;
        }

        private void FindCornerAngles(Point center)
        {
            cornerAgnles = Carcartesian2Polar(CenterOfRotation, cornerPoints);
        }

        private double[] Carcartesian2Polar(Point center, Point[] directions)
        {
            double[] output = new double[directions.Length];
            for (int i = 0; i < directions.Length; i++)
            {
                double dx = directions[i].X - center.X;
                double dy = directions[i].Y - center.Y;
                output[i] = Math.Atan2(-dy, dx);
            }

            return output;
        }

        private Tuple<int, double>[] Carcartesian2Polar_WithIndex(Point center, Point[] directions)
        {
            Tuple<int, double>[] output = new Tuple<int, double>[directions.Length];
            for (int i = 0; i < directions.Length; i++)
            {
                double dx = directions[i].X - center.X;
                double dy = directions[i].Y - center.Y;

                if (lastPointIndexes.IsThere(i))
                {
                    output[i] = new Tuple<int, double>(i, double.NaN);
                }
                else
                {
                    output[i] = new Tuple<int, double>(i, Math.Atan2(-dy, dx));
                }
            }

            return output;
        }

        private void PrintPointPartition()
        {
            if (CenterOfRotation == Point.Empty)
            {
                lblNum.Text = "Clockwise / CounterCW: - / -";
                return;
            }

            var angles = Carcartesian2Polar(CenterOfRotation, points.ToArray());

            int CW = 0;
            int CCW = 0;

            foreach (double angle in angles)
            {
                if (AngleTrim(angle - Angle, lowerLimit: 0) <= Math.PI)
                {
                    CCW++;
                }
                else
                {
                    CW++;
                }
            }

            lblNum.Text = string.Format("Clockwise / CounterCW: {0} / {1}", CW, CCW);
        }

        private void FindNextAngle()  // For positive velocity only!
        {
            var indexAngles = Carcartesian2Polar_WithIndex(CenterOfRotation, points.ToArray());
            indexAngles = indexAngles.OrderBy(x => x.Item2).ToArray();

            var direct = FindNextAngle(ref indexAngles, Angle);
            var opposite = FindNextAngle(ref indexAngles, AngleTrim(Angle + Math.PI));
            opposite = new Tuple<double, Tuple<int, double>>(opposite.Item1, new Tuple<int, double>(opposite.Item2.Item1, AngleTrim(opposite.Item2.Item2 + Math.PI)));

            if (direct.Item1 < opposite.Item1)
            {
                nextIndexAngle = direct.Item2;
                distance = AngleTrim(nextIndexAngle.Item2 - Angle, lowerLimit: 0);
            }
            else
            {
                nextIndexAngle = opposite.Item2;
                distance = opposite.Item1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortedArray">Elements are tuples of index and corresponding angle.</param>
        /// <param name="angle"></param>
        /// <returns></returns>
        private Tuple<double, Tuple<int, double>> FindNextAngle(ref Tuple<int, double>[] sortedArray, double angle)
        {
            Tuple<double, Tuple<int, double>> output = null;
            foreach (var indexAngleTuple in sortedArray)
            {
                if (double.IsNaN(indexAngleTuple.Item2))
                {
                    continue;
                }
                if (indexAngleTuple.Item2 > angle)
                {
                    double diff = indexAngleTuple.Item2 - angle;
                    output = new Tuple<double, Tuple<int, double>>(diff, indexAngleTuple);
                    break;
                }
            }

            if (output == null)  // Means that angle is bigger than any other.
            {
                int i = 0;
                for (i = 0; i < sortedArray.Length; i++)
                {
                    if (!double.IsNaN(sortedArray[i].Item2))
                    {
                        break;
                    }
                }
                var indexAngleTuple = sortedArray[i];
                double diff = (2 * Math.PI) + (sortedArray[i].Item2 - angle);
                output = new Tuple<double, Tuple<int, double>>(diff, indexAngleTuple);
            }

            return output;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Angle += velocity;
            distance -= Math.Abs(velocity);

            if (distance < 0)
            {
                Angle = nextIndexAngle.Item2;
                CenterOfRotationIndex = nextIndexAngle.Item1;
                FindNextAngle();
            }

            Draw();
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (chBoxStart.Checked)
                return;

            if (e.Button == MouseButtons.Left)
            {
                points.Add(new Point(
                    (int)Math.Ceiling((double)e.X),
                    (int)Math.Ceiling((double)e.Y)));
                pointsColour = new bool[points.Count];
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (points.Any())
                {
                    int index = 0;
                    // Find index of the closest point in points.
                    int i = 0;
                    double minDistance = double.MaxValue;
                    foreach (Point point in points)
                    {
                        double distance = Math.Sqrt(
                            Math.Pow((point.X - e.X), 2) +
                            Math.Pow((point.Y - e.Y), 2));
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            index = i;
                        }

                        i++;
                    }
                    CenterOfRotationIndex = index;
                    PrintPointPartition();
                }
                else  // Is empty.
                {
                    CenterOfRotationIndex = -1;
                }
            }

            Draw();
        }

        private void ChBoxStart_CheckedChanged(object sender, EventArgs e)
        {
            timer.Enabled = chBoxStart.Checked;
            tBoxPeriod.Enabled = !chBoxStart.Checked;
            chBoxStart.Text = chBoxStart.Checked ? "Stop" : "Start";

            if (chBoxStart.Checked)
            {
                if (double.TryParse(tBoxPeriod.Text, out double degrees)
                    && degrees > 0
                    && CenterOfRotationIndex >= 0
                    && points.Count > 2)
                {
                    lastPointIndexes = new LastPointIndexes(-1, CenterOfRotationIndex);
                    velocity = degrees * Math.PI / 180 / (1000 / timerInterval);
                    FindNextAngle();

                    pointsColour = new bool[points.Count];
                    pointsColour[CenterOfRotationIndex] = true;
                }
                else
                {
                    chBoxStart.Checked = false;
                }
            }
        }

        private void BtnErase_Click(object sender, EventArgs e)
        {
            if (chBoxStart.Checked) return;

            Erase();
            Draw();
        }

        private void Erase()
        {
            points.Clear();
            CenterOfRotationIndex = -1;
        }

        private void BtnRnd_Click(object sender, EventArgs e)
        {
            if (chBoxStart.Checked) return;

            if (int.TryParse(tBoxRng.Text, out int num))
            {
                Erase();
                for (int i = 0; i < num; i++)
                {
                    points.Add(new Point(random.Next(0, pictureBox.Image.Width), random.Next(0, pictureBox.Image.Height)));
                }
                pointsColour = new bool[points.Count];
                CenterOfRotationIndex = 0;
                PrintPointPartition();
                Draw();
            }
        }

        private void Form_main_SizeChanged(object sender, EventArgs e)
        {
            pictureBox.Size = new Size(this.Width - 40, this.Height - 116);
            FindCornerPoints();
            Draw();
        }

        private void LinkLabel_IMO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.imo-official.org/problems.aspx");
        }
    }

    internal enum Direction
    {
        Left = 0,
        Bottom = 1,
        Right = 2,
        Top = 3,
    }

    internal struct LastPointIndexes
    {
        private int oldIndex;
        private int newIndex;

        public LastPointIndexes(int oldIndex, int newIndex)
        {
            this.oldIndex = oldIndex;
            this.newIndex = newIndex;
        }

        public void Add(int index)
        {
            oldIndex = newIndex;
            newIndex = index;
        }

        public bool IsThere(int index)
        {
            if (oldIndex != -1 && index == oldIndex) return true;
            if (newIndex != -1 && index == newIndex) return true;

            return false;
        }
    }
}
