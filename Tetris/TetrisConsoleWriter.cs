using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class TetrisConsoleWriter
    {


        public void DrawGameState(int startColumn, TetrisGameState state, int highScore)
        {
            this.Write("Level:", 1, startColumn);
            this.Write($"{state.Level}", 2, startColumn);
            this.Write("Score:", 4, startColumn);
            this.Write(state.Score.ToString(), 5, startColumn);
            this.Write("Best:", 6, startColumn);
            this.Write(highScore.ToString(), 7, startColumn);
            this.Write("Frame:", 9, startColumn);
            this.Write(state.Frame.ToString(), 10, startColumn);
            this.Write("Keys:", 12, startColumn);
            this.Write(" ^ ".ToString(), 13, startColumn);
            this.Write("<v> ".ToString(), 14, startColumn);

        }

        public void DrawBorder(int tetrisColumnss, int tetrisRows, int infoColumnss)
        {
            Console.SetCursorPosition(0, 0);
            string line = "╔";
            line += new string('═', tetrisColumnss);

            /*for (int i = 0; i < TetrisCols; i++)
            {
                line += "═";
            }*/

            line += "╦";
            line += new string('═', infoColumnss);
            line += "╗";

            Console.Write(line);

            for (int i = 0; i < tetrisRows; i++)
            {
                string middleLIne = "║";
                middleLIne += new string(' ', tetrisColumnss);
                middleLIne += "║";
                middleLIne += new string(' ', infoColumnss);
                middleLIne += "║";
                Console.Write(middleLIne);
            }

            string endLine = "╚";
            endLine += new string('═', tetrisColumnss);
            endLine += "╩";
            endLine += new string('═', infoColumnss);
            endLine += "╝";
            Console.Write(endLine);


        }

        public void WriteGameOver(int score, int row, int column )
        {
            var scoreAsString = score.ToString();
            scoreAsString = new string(' ', 7 - scoreAsString.Length) + scoreAsString;
            Write("╔═════════╗", row, column);
            Write("║  Game   ║", row + 1, column);
            Write("║   Over! ║", row + 2, column);
            Write($"║ {scoreAsString} ║", row + 3, column);
            Write("╚═════════╝", row + 4, column);
        }

        public void DrawTetrisField(bool[,] tetrisField)
        {
            for (int row = 0; row < tetrisField.GetLength(0); row++)
            {
                string line = "";
                for (int col = 0; col < tetrisField.GetLength(1); col++)
                {
                    if (tetrisField[row, col])
                    {
                        line += "█";

                    }
                    else
                    {
                        line += " ";
                    }
                }
                this.Write(line, row + 1, 1);
            }
        }

        public void DrawCurrentFigure(bool[,] currentFigure, int currentFigureRow, int currentFigureColumn)
        {
            for (int row = 0; row < currentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < currentFigure.GetLength(1); col++)
                {
                    if (currentFigure[row, col])
                    {
                        Write("█", row + 1 + currentFigureRow, 1 + currentFigureColumn + col);
                    }
                }
            }
        }

        private void Write(string text, int row, int col)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(text);
        }
    }
}
