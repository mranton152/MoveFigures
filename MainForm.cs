using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MoveFigures {
	public partial class MainForm : Form {
		List<IFigure> _figures = new List<IFigure>();
		private IFigure _figureToMove;
		private Point _startPoint;
		public MainForm() {
			InitializeComponent();
			DoubleBuffered = true;

			Paint += MainForm_Paint;
			MouseDown += MainForm_MouseDown;
			MouseMove += MainForm_MouseMove;
			MouseUp += MainForm_MouseUp;
			MouseClick += MainForm_MouseClick;
			_figures.ForEach(f => f.FigureChanged += (s, e) => Refresh());
		}

		void MainForm_MouseClick(object sender, MouseEventArgs e) {
			IFigure f = _figures.FirstOrDefault(fi => fi.Contains(e.Location));
			if (f == null) {
				_figures.ForEach(fi => fi.Highlighted = false);
				return;
			}
			f.Highlighted = true;
		}

		void MainForm_MouseUp(object sender, MouseEventArgs e) {
			_figureToMove = null;
			Cursor = Cursors.Default;
		}

		void MainForm_MouseDown(object sender, MouseEventArgs e) {
			_figureToMove = _figures.FirstOrDefault(f => f.Contains(e.Location));
			if (_figureToMove == null) return;
			_figureToMove.Highlighted = true;
			_startPoint = e.Location;
		}

		void MainForm_MouseMove(object sender, MouseEventArgs e) {
			switch (e.Button) {
				case MouseButtons.None:
					Cursor = _figures.Exists(f => f.Contains(e.Location)) ? Cursors.Hand : Cursors.Default;
					break;
				case MouseButtons.Left:
					if (_figureToMove == null) return;
					_figureToMove.Move(e.X - _startPoint.X, e.Y - _startPoint.Y);
					_startPoint = e.Location;
					break;
			}
            Refresh();
		}

		void MainForm_Paint(object sender, PaintEventArgs e) {
			foreach (IFigure figure in _figures) {
				figure.Fill(e.Graphics, figure.Brush ?? Brushes.Green);
				figure.Draw(e.Graphics, figure.Pen ?? Pens.Black);
			}
		}

        List<Point> points = new List<Point>(); 
        private void button1_Click(object sender, System.EventArgs e)
        {
            points.Clear();
            if (tbCount.Text.Length == 0 )
            {
                MessageBox.Show("Введите количество вершин!", "Ошибка!");
                return;
            }
            if ( tbLength.Text.Length == 0)
            {
                MessageBox.Show("Введите длину стороны!", "Ошибка!");
                return;
            }
            int count = int.Parse(tbCount.Text);  //текстбокс "Количество вершин"
            int length = int.Parse(tbLength.Text);//текстбокс "Длина стороны"
            if (count<3 || count>100000 || length < 0 || length > 100000) {
                MessageBox.Show("Вы ввели недопустимый диапазон значений!", "Ошибка!");
                return ;
            }
            double R = length / (2 * Math.Sin(Math.PI / count)); //Радиус описанной окружности
            for (double angle = 0.0; angle <= 2 * Math.PI; angle += 2 * Math.PI / count) //цикл по углу
            {
                int x = (int)(R * Math.Cos(angle)); //расчет координаты x точки
                int y = (int)(R * Math.Sin(angle)); //расчет координаты y точки
                points.Add(new Point((int)R + x, (int)R + y)); //добавление точки в список точек
            }
            _figures.Add(new Polygon(points.ToArray()));
           
            this.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<IFigure> CompareFigures = new List<IFigure>();
            foreach (IFigure figure in _figures)
            {
                if (figure.Highlighted)
                {
                    CompareFigures.Add(figure);
                }
            }
            if (CompareFigures.Count == 2 )
            {
                Point[] p = CompareFigures[1].GetPoints();
                bool flag = false;
                foreach (Point point in p)
                {
                    if (!CompareFigures[0].Contains(point))
                    {
                        flag = false;
                        break;
                    }
                    flag = true;
                }
                Point[] p1 = CompareFigures[0].GetPoints();
                bool flag2 = false;
                foreach (Point point in p1)
                {
                    if (!CompareFigures[1].Contains(point))
                    {
                        flag2 = false;
                        break;
                    }
                    flag2 = true;
                }
                if (flag || flag2)
                {
                    MessageBox.Show("Фигура вписана!", "Проверка на вписанность!");
                }
                else
                {
                    MessageBox.Show("Фигура НЕ вписана!", "Проверка на вписанность!");
                }
            }
            else
            {
                MessageBox.Show("Выделите две фигуры!", "Ошибка!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<IFigure> figuresToRemove = new List<IFigure>();
            foreach (IFigure figure in _figures)
            {
                if (figure.Highlighted)
                {
                    figuresToRemove.Add(figure);
                }
            }
            if (figuresToRemove.Count == 0)
            {
                MessageBox.Show("Выделите фигуру!", "Ошибка!");

            }
            else
            {
                foreach (IFigure figure in figuresToRemove)
                {
                    _figures.Remove(figure);
                }

            }
        }
    }
}
