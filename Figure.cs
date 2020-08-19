using System;
using System.Drawing;
using System.Collections.Generic;

namespace MoveFigures {
	class Figure : IFigure {
		public Brush Brush { get; private set; }

		public Pen Pen { get; private set; }

		private bool _highlighted;
		public bool Highlighted {
			get {
				return _highlighted;
			}
			set {
				_highlighted = value;
				OnFigureChanged(new EventArgs());
			}
		}

		public virtual bool Contains(Point pt) {
			return false;
			//throw new NotImplementedException();
		}

        public virtual Point[] GetPoints()
        { return new Point[100]; }

        public virtual void Draw(Graphics g, Pen pen) {
			Pen = pen;
		}

		public virtual void Fill(Graphics g, Brush brush) {
			Brush = brush;
		}

		public virtual void Move(int dx, int dy) {
			OnFigureChanged(new EventArgs());
		}

		protected virtual void OnFigureChanged(EventArgs e) {
			EventHandler handler = FigureChanged;
			if (handler == null) return;
			handler(this, e);
		}

		public event EventHandler FigureChanged;

	}
}
