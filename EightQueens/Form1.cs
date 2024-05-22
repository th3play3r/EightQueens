using System;
using System.Drawing;
using System.Windows.Forms;

namespace EightQueens
{
    public partial class Form1 : Form
    {
        private const int N = 8; // Розмір шахової дошки
        private Button[,] squares = new Button[N, N]; // Масив кнопок для дошки

        public Form1()
        {
            InitializeComponent();
            InitializeBoard(); // Ініціалізація дошки
        }

        private void InitializeBoard()
        {
            this.panel1.SuspendLayout();
            this.SuspendLayout();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    squares[i, j] = new Button();
                    squares[i, j].Size = new Size(50, 50);
                    squares[i, j].Location = new Point(j * 50, i * 50);
                    squares[i, j].BackColor = (i + j) % 2 == 0 ? Color.White : Color.Black;
                    this.panel1.Controls.Add(squares[i, j]); // Додавання кнопки до панелі
                }
            }

            Button solveButton = new Button();
            solveButton.Text = "Solve";
            solveButton.Location = new Point(420, 20);
            solveButton.Click += new EventHandler(SolveButton_Click); // Обробник події для кнопки "Solve"
            this.Controls.Add(solveButton);

            Button resetButton = new Button();
            resetButton.Text = "Reset";
            resetButton.Location = new Point(420, 60);
            resetButton.Click += new EventHandler(ResetButton_Click); // Обробник події для кнопки "Reset"
            this.Controls.Add(resetButton);

            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            int[] queens = new int[N];
            PlaceQueens(queens, 0); // Почати розміщення королев
        }

        private bool PlaceQueens(int[] queens, int row)
        {
            if (row == N)
            {
                DisplayQueens(queens); // Відобразити королев, якщо всі розміщені
                return true;
            }

            for (int col = 0; col < N; col++)
            {
                if (IsSafe(queens, row, col)) // Перевірка безпеки розміщення королеви
                {
                    queens[row] = col;
                    if (PlaceQueens(queens, row + 1)) // Рекурсивне розміщення наступної королеви
                    {
                        return true;
                    }
                }
            }

            return false; // Якщо розміщення неможливе
        }

        private bool IsSafe(int[] queens, int row, int col)
        {
            for (int i = 0; i < row; i++)
            {
                int otherCol = queens[i];
                if (otherCol == col || otherCol - i == col - row || otherCol + i == col + row)
                {
                    return false; // Перевірка конфліктів
                }
            }
            return true; // Можна розміщувати
        }

        private void DisplayQueens(int[] queens)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    squares[i, j].Text = "";
                    squares[i, j].Font = this.Font; // Повернення до стандартного шрифту
                }
            }

            for (int i = 0; i < N; i++)
            {
                int col = queens[i];
                squares[i, col].Text = "♕";
                squares[i, col].ForeColor = Color.Red;
                squares[i, col].Font = new Font(squares[i, col].Font.FontFamily, 24); // Збільшення розміру шрифту
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    squares[i, j].Text = "";
                    squares[i, j].BackgroundImage = null; // Очищення кнопок
                }
            }
        }
    }
}
