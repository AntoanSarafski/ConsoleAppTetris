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


    // State
    static int Score = 0;
    static int Frame = 0;
    static int FramesToMoveFigure = 15;
    static bool[,] CurrentFigure = TetrisFigures[2]; // Must be random
    static int CurrentFigureRow = 0;
    static int CurrentFigureCol = 0;
    static bool[,] TetrisField = new bool[TetrisRows, TetrisCols];


    static void Main(string[] args)
    {
        Console.Clear();

        Console.Title = "Tetris v1.0";
        Console.WindowHeight = ConsoleRows + 1;
        Console.WindowWidth = ConsoleCols;
        Console.BufferHeight = ConsoleRows + 1;
        Console.BufferWidth = ConsoleCols;
        Console.CursorVisible = false;
        DrawBorder();
        DrawInfo();
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
                    
                    if(CurrentFigureCol > 0)
                    {
                        CurrentFigureCol--; 
                    }
                }
                if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                {
                    if(CurrentFigureCol < TetrisCols - CurrentFigure.GetLength(1))
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

                /*if (Collision())
                {
                    AddCurrentFigureToTetrisField();
                    CheckForFullLines();
                    if(lines remove) Score++;
                }
                */

                // redraw UI
                // TODO: DrawTetrisField();

                DrawBorder(); DrawInfo();
                DrawCurrentFigure();

                Thread.Sleep(40);
            }
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
            Write("Score:", 1, 3 + TetrisCols);
            Write(Score.ToString(), 2, 3 + TetrisCols);
            Write("Frame:", 4, 3 + TetrisCols);
            Write(Frame.ToString(), 5, 3 + TetrisCols);
            Write("Position:", 7, 3 + TetrisCols);
            Write($"{CurrentFigureRow}, {CurrentFigureCol}".ToString(), 8, 3 + TetrisCols);
            Write("Keys:", 10, 3 + TetrisCols);
            Write(" ^ ".ToString(), 11, 3 + TetrisCols);
            Write("<v> ".ToString(), 12, 3 + TetrisCols);

    }

    static void DrawCurrentFigure()
        {
            for (int row = 0; row < CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                {
                    if (CurrentFigure[row, col])
                    {
                        Write("█", CurrentFigureRow + row + 1, CurrentFigureCol + col + 1);
                    }
                }
            }
        }


        static void Write(string text, int row, int col, ConsoleColor color = ConsoleColor.DarkRed)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
            Console.ResetColor();
        }


    }

 