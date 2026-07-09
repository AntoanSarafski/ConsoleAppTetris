using System.Text.RegularExpressions;
using System.Transactions;

class Program
{
    // Settings for the Tetris game

    static int TetrisRows = 20;
    static int TetrisCols = 10;
    static int InfoCols = 10;
    static int ConsoleRows = 1 + TetrisRows + 1;
    static int ConsoleCols = 1 + TetrisCols + 1 + InfoCols + 1;
    static List<bool[,]> TetrisFigures = new List<bool[,]>()
        {
            // L
            new bool[,] { {true, true, true, true} },  // I
            new bool[,]                                // O
                {
                    { true, true },
                    { true, true }
                },
            new bool[,]                                // T
                {
                    { false, true, false },
                    { true, true, true }
                },
            new bool[,]                               // S
                {
                    { false, true, true },
                    { true, true, false }
                },

            new bool[,]
                {                                     // Z
                    { true, true, false },
                    { false, true, true }
                },
            new bool[,]
                {                                     // J
                    { true, false, false },
                    { true, true, true }
                },
            new bool[,]                               // L
                {
                    {false, false, true },
                    { true, true, true }
                }
        };
    static string ScoresFileName = "scores.txt";


    // State
    static int HighScore = 0;
    static int Score = 0;
    static int Frame = 0;
    static int FramesToMoveFigure = 15;
    static bool[,] CurrentFigure = null;
    static int CurrentFigureRow = 0;
    static int CurrentFigureCol = 0;
    static bool[,] TetrisField = new bool[TetrisRows, TetrisCols];
    static Random Random = new Random();


    static void Main(string[] args)
    {
        if (File.Exists(ScoresFileName))
        {
            var allScores = File.ReadAllLines(ScoresFileName);
            foreach(var score in allScores)
            {
                var match = Regex.Match(score, @" => (?<score>[0-9]+)");
                HighScore = Math.Max(HighScore, int.Parse(match.Groups["score"].Value));
            }
        }

        Console.Title = "Tetris v1.0";
        Console.CursorVisible = false;
        Console.WindowHeight = ConsoleRows + 1;
        Console.WindowWidth = ConsoleCols;
        Console.BufferHeight = ConsoleRows + 1;
        Console.BufferWidth = ConsoleCols;
        CurrentFigure = TetrisFigures[Random.Next(0, TetrisFigures.Count)];

        while (true)
        {
            Frame++;

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                {

                    if (CurrentFigureCol > 0)
                    {
                        CurrentFigureCol--;
                    }
                }
                if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                {
                    if (CurrentFigureCol < TetrisCols - CurrentFigure.GetLength(1))
                    {
                        CurrentFigureCol++;
                    }

                }
                if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                {
                    // Check if the current figure can move down
                    Frame = 1;
                    Score++;
                    CurrentFigureRow++;

                }
                if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.UpArrow)
                {
                    // TODO: Implement 90-degree rotation of the current figure
                }

            }
            // Update game state
            if (Frame % FramesToMoveFigure == 0)
            {
                CurrentFigureRow++;
                Frame = 0;

            }

            if (Collision())
            {
                AddCurrentFigureToTetrisField();

                //CheckForFullLines();
                //if(lines remove) Score++;

                CurrentFigure = TetrisFigures[Random.Next(0, TetrisFigures.Count)];
                CurrentFigureRow = 0;
                CurrentFigureCol = 0;
                if (Collision())
                {
                    File.AppendAllLines(ScoresFileName, new List<string>
                    {
                        $"[{DateTime.Now.ToString()}] {Environment.UserName} => {Score}"
                    });
                    var scoreAsString = Score.ToString();
                    scoreAsString += new string(' ',4 - scoreAsString.Length);
                    Write("╔════════════╗", 5, 5);
                    Write("║  Game      ║", 6, 5);
                    Write("║   Over!    ║", 7, 5);
                    Write("║Score:      ║", 8, 5);
                    Write($"║ {scoreAsString}       ║", 9, 5);
                    Write("╚════════════╝", 10, 5);
                    Thread.Sleep(100000);
                    return;
                }
            }


            // redraw UI

            DrawBorder(); DrawInfo();
            DrawCurrentFigure();

            DrawTetrisField();
            Thread.Sleep(40);
        }
    }



    private static void AddCurrentFigureToTetrisField()
    {
        for (int row = 0; row < CurrentFigure.GetLength(0); row++)
        {
            for (int col = 0; col < CurrentFigure.GetLength(1); col++)
            {
                if (CurrentFigure[row, col])
                {
                    TetrisField[CurrentFigureRow + row, CurrentFigureCol + col] = true;
                }
            }
        }
        //CurrentFigure
        //TetrisField
    }

    static bool Collision()
    {
        // TODO: Collide with existing figure
        if (CurrentFigureRow + CurrentFigure.GetLength(0) == TetrisRows)
        {
            return true;
        }

        for (int row = 0; row < CurrentFigure.GetLength(0); row++)
        {
            for (int col = 0; col < CurrentFigure.GetLength(1); col++)
            {
                if (CurrentFigure[row, col] && 
                    TetrisField[CurrentFigureRow + row + 1, CurrentFigureCol + col])
                {
                    return true;
                }
            }
        }

        return false;
    }

    static void DrawBorder()
    {
        Console.SetCursorPosition(0, 0);
        string line = "╔";
        line += new string('═', TetrisCols);

        /*for (int i = 0; i < TetrisCols; i++)
        {
            line += "═";
        }*/

        line += "╦";
        line += new string('═', InfoCols);
        line += "╗";

        Console.Write(line);

        for (int i = 0; i < TetrisRows; i++)
        {
            string middleLIne = "║";
            middleLIne += new string(' ', TetrisCols);
            middleLIne += "║";
            middleLIne += new string(' ', InfoCols);
            middleLIne += "║";
            Console.Write(middleLIne);
        }

        string endLine = "╚";
        endLine += new string('═', TetrisCols);
        endLine += "╩";
        endLine += new string('═', InfoCols);
        endLine += "╝";
        Console.Write(endLine);


    }

    static void DrawInfo()
    {
        if(Score > HighScore)
        {
            HighScore = Score;
        }

        Write("Score:", 1, 3 + TetrisCols);
        Write(Score.ToString(), 2, 3 + TetrisCols);
        Write("Best:", 4, 3 + TetrisCols);
        Write(HighScore.ToString(), 5, 3 + TetrisCols);
        Write("Frame:", 7, 3 + TetrisCols);
        Write(Frame.ToString(), 8, 3 + TetrisCols);
        Write("Position:", 10, 3 + TetrisCols);
        Write($"{CurrentFigureRow}, {CurrentFigureCol}".ToString(), 11, 3 + TetrisCols);
        Write("Keys:", 12, 3 + TetrisCols);
        Write(" ^ ".ToString(), 13, 3 + TetrisCols);
        Write("<v> ".ToString(), 14, 3 + TetrisCols);

    }

    static void DrawTetrisField()
    {
        for (int row = 0; row < TetrisRows; row++)
        {
            for (int col = 0; col < TetrisCols; col++)
            {
                if (TetrisField[row, col])
                {
                    Write("█", row + 1, col + 1);

                }
                /*else
                {
                    Write(" ", row + 1, col + 1);
                }*/
            }
        }
    }

    static void DrawCurrentFigure()
    {
        for (int row = 0; row < CurrentFigure.GetLength(0); row++)
        {
            for (int col = 0; col < CurrentFigure.GetLength(1); col++)
            {
                if (CurrentFigure[row, col])
                {
                    Write("█", CurrentFigureRow + row + 1, CurrentFigureCol + col + 1, ConsoleColor.DarkRed);
                }
            }
        }
    }


    static void Write(string text, int row, int col, ConsoleColor color = ConsoleColor.Yellow)
    {
        Console.ForegroundColor = color;
        Console.SetCursorPosition(col, row);
        Console.Write(text);
        Console.ResetColor();
    }
}






 