using System.Drawing;

namespace MoveFigures {
	class Ellipse : Figure, IFigure {
		public Ellipse(Rectangle rect) {
			_rectangle = rect;
		}

		private Rectangle _rectangle;


		public override bool Contains(Point pt) {
			return _rectangle.Contains(pt);
		}

		public override void Draw(Graphics g, Pen pen) {
			g.DrawEllipse(Highlighted ? new Pen(pen.Color, pen.Width * 2) : pen, _rectangle);
			base.Draw(g, pen);
		}

		public override void Fill(Graphics g, Brush brush) {
			g.FillEllipse(brush, _rectangle);
			base.Fill(g, brush);
		}


		public override void Move(int dx, int dy) {
			_rectangle.Offset(dx, dy);
			base.Move(dx, dy);
		}
	}
}
