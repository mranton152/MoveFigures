using System;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace MoveFigures {
	class Polygon : Figure {
		private readonly Point[] _points;

		public Polygon(Point[] points) {
			_points = points;
		}

        public override bool Contains(Point pt) {
			Point[] pts = new Point[_points.Length + 1];
			_points.CopyTo(pts, 0);
			pts[pts.Length - 1] = pts[0];
			//вектор из вершины к точке
			Point v1, v2;
			int newSign = 0;
			//Проходим по всем вершинам и проверяем знак векторного произведения
			for (int i = 0; i < pts.Length - 1; i++) {
				v1 = new Point(pt.X - pts[i].X, pt.Y - pts[i].Y);
				v2 = new Point(pts[i + 1].X - pts[i].X, pts[i + 1].Y - pts[i].Y);
				if (i == 0) {
					newSign = Math.Sign(v1.X * v2.Y - v1.Y * v2.X);
					continue;
				}
				int sign = Math.Sign(v1.X * v2.Y - v1.Y * v2.X);
				//Точка лежит на стороне
				if (sign == 0) return true;
				//Если знак поменялся, значит точка лежит вне
				if (sign != newSign) return false;
			}
			return true;
		}

        public override Point[] GetPoints()
        {
            return _points;
        }

        public override void Draw(Graphics g, Pen pen) {
			g.DrawPolygon(Highlighted ? new Pen(pen.Color, pen.Width * 2) : pen, _points);
			base.Draw(g, pen);
		}

		public override void Fill(Graphics g, Brush brush) {
			g.FillPolygon(brush, _points);
			base.Fill(g, brush);
		}

		public override void Move(int dx, int dy) {
			using (Matrix m = new Matrix()) {
				m.Translate(dx, dy);
				m.TransformPoints(_points);
			}
			base.Move(dx, dy);
		}
	}
}
