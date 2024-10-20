using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
    internal class Program
    {

        enum Cell
        {
            Empty = 0,
            AliveShip,
            DeadShip,
            Miss
        }

        enum Step
        {
            User,
            Comp
        }

        static void Main(string[] args)
        {

            #region Create game field and variables

            int fieldSize = 10;
            Cell[,] userField = new Cell[fieldSize, fieldSize];
            Cell[,] compField = new Cell[fieldSize, fieldSize];

            int countUserShips = fieldSize;
            int countCompShips = fieldSize;

            Random rnd = new Random();

            int coordI, coordJ;

            bool inputResultCoordI;
            bool inputResultCoordJ;

            Step currentStep = Step.User;
            Step winner = Step.User;

            bool playGame = true;
            #endregion



            #region Filling fields with emptiness

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    userField[i, j] = Cell.Empty;
                    compField[i, j] = Cell.Empty;
                }
            }

            #endregion



            #region Random placement ships on the game field

            for (int k = 0; k < countUserShips; k++)
            {

                do
                {

                    coordI = rnd.Next(0, fieldSize);
                    coordJ = rnd.Next(0, fieldSize);

                } while (userField[coordI, coordJ] != Cell.Empty);

                userField[coordI, coordJ] = Cell.AliveShip;

            }



            for (int k = 0; k < countCompShips; k++)
            {

                do
                {

                    coordI = rnd.Next(0, fieldSize);
                    coordJ = rnd.Next(0, fieldSize);

                } while (compField[coordI, coordJ] != Cell.Empty);

                compField[coordI, coordJ] = Cell.AliveShip;

            }

            #endregion



            #region Game cycle
            while (playGame)
            {
                #region Output field on screen

                Console.Clear();

                Console.WriteLine("User field");

                for (int i = 0; i < fieldSize; i++)
                {
                    for (int j = 0; j < fieldSize; j++)
                    {

                        switch (userField[i, j])
                        {
                            case Cell.Empty:

                                Console.Write(".");

                                break;

                            case Cell.AliveShip:

                                Console.Write("K");

                                break;

                            case Cell.DeadShip:

                                Console.Write("X");

                                break;

                            case Cell.Miss:

                                Console.Write("O");

                                break;
                        }

                    }

                    Console.WriteLine();

                }



                Console.WriteLine("Comp field");

                for (int i = 0; i < fieldSize; i++)
                {
                    for (int j = 0; j < fieldSize; j++)
                    {

                        switch (compField[i, j])
                        {
                            case Cell.Empty:

                                Console.Write(".");

                                break;

                            case Cell.AliveShip:

                                Console.Write(".");

                                break;

                            case Cell.DeadShip:

                                Console.Write("X");

                                break;

                            case Cell.Miss:

                                Console.Write("O");

                                break;
                        }

                    }

                    Console.WriteLine();

                }

                #endregion



                #region Output/random coordinates for the shot and check shoot + transfer of the stroke in case of a miss

                switch (currentStep)
                {
                    case Step.User:

                        Console.WriteLine("User step now...");

                        do
                        {

                                Console.Write("Input coordinat of I");

                            inputResultCoordI = int.TryParse
                                (Console.ReadLine(), out coordI);

                                Console.Write("Input coordinat of J");

                            inputResultCoordJ = int.TryParse
                                (Console.ReadLine(), out coordJ);
                            
                        } while (inputResultCoordI == false ||
                        inputResultCoordJ == false || 
                        coordI - 1 < 0 || coordI - 1 > fieldSize - 1
                        ||
                        coordJ - 1 < 0 || coordJ - 1 > fieldSize - 1 || compField[coordI-1, coordJ-1] 
                        == Cell.DeadShip || compField[coordI-1, coordJ-1] ==
                        Cell.Miss);

                            if (compField[coordI - 1, coordJ - 1] ==
                         Cell.AliveShip)
                        {
                            compField[coordI - 1, coordJ - 1] =
                         Cell.DeadShip;
                            countCompShips--;
                        }
                        else if (compField[coordI - 1, coordJ - 1] == Cell.Empty)
                        {
                            compField[coordI - 1, coordJ - 1] = Cell.Miss;
                            currentStep = Step.Comp;
                        
                        }

                        break;

                    case Step.Comp:

                        Console.WriteLine("Comp step now <Press enter>");
                        Console.ReadKey();

                        do
                        {

                            coordI = rnd.Next(0, fieldSize);
                            coordJ = rnd.Next(0, fieldSize);

                        } while (userField[coordI, coordJ] == Cell.DeadShip ||
                        userField[coordI,coordJ] == Cell.Miss);

                        if (userField[coordI, coordJ] == Cell.AliveShip)
                        {

                            userField[coordI, coordJ] = Cell.DeadShip;
                            countUserShips--;

                        }
                        else if (userField[coordI, coordJ] == Cell.Empty)
                        {

                            userField[coordI, coordJ] = Cell.Miss;
                            currentStep = Step.User;
                          
                        }

                        break;
                }

                #endregion



                #region Check win

                if (countUserShips == 0 || countCompShips == 0)
                {
                    playGame = false;
                    winner = currentStep;
                }

                #endregion

            }
            #endregion

            #region Output field on screen

            Console.Clear();

            Console.WriteLine("User field");

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {

                    switch (userField[i, j])
                    {
                        case Cell.Empty:

                            Console.Write(".");

                            break;

                        case Cell.AliveShip:

                            Console.Write("K");

                            break;

                        case Cell.DeadShip:

                            Console.Write("X");

                            break;

                        case Cell.Miss:

                            Console.Write("O");

                            break;
                    }

                }

                Console.WriteLine();

            }



            Console.WriteLine("Comp field");

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {

                    switch (compField[i, j])
                    {
                        case Cell.Empty:

                            Console.Write(".");

                            break;

                        case Cell.AliveShip:

                            Console.Write(".");

                            break;

                        case Cell.DeadShip:

                            Console.Write("X");

                            break;

                        case Cell.Miss:

                            Console.Write("O");

                            break;
                    }

                }

                Console.WriteLine();

            }

            #endregion

            #region Find winner

            switch (winner)
            {
                case Step.User: Console.WriteLine("User WIN!");

                    break;

                case Step.Comp: Console.WriteLine("Comp WIN!");

                    break;
            }


            #endregion



            Console.ReadKey();
        }
    }
}
