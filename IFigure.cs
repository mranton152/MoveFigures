using System;
using System.Drawing;
using System.Collections.Generic;

namespace MoveFigures {
	interface IFigure {
		Brush @Brush { get; }
		Pen @Pen { get; }
		bool Highlighted { get; set; }
		bool Contains(Point pt);
        Point[] GetPoints();
        void Draw(Graphics g, Pen pen);
		void Fill(Graphics g, Brush brush);
		void Move(int dx, int dy);
		event EventHandler FigureChanged;
	}

}
