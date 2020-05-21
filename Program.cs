using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrisNuovo
{
    class Program
    {
        //genero il segno del computer e lo inserisco nella matrice
        static void CompFacile(ref char[,] griglia)
        {
            Random gen = new Random();
            int row = 0, col = 0;
            do
            {
                col = gen.Next(3);
                row = gen.Next(3);
            }while(griglia[row, col] != '-');
            griglia[row, col] = 'O';
        } 

        //chiedo le posizioni per inserire il segno
        static void Utente (ref char[,] griglia)
        {
            int row, col;
            bool check = false;
            do
            {
                do
                {
                    Console.WriteLine("Inserisci la riga");
                    check = int.TryParse(Console.ReadLine(), out row);
                } while (!check || row > 3);
                do
                {
                    Console.WriteLine("Insersci la colonna");
                    check = int.TryParse(Console.ReadLine(), out col);
                } while (!check || col > 3);
            } while (griglia[row - 1, col -1] != '-');
            griglia[row - 1, col - 1] = 'X';
        }     
        //stampo la matrice nella tabella
        static void Interfaccia (char[,] griglia)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"| {griglia[i, 0]} | {griglia[i, 1]} | {griglia[i, 2]} |");
                Console.WriteLine("-------------");
            }
        }
        //verifico chi ha vinto, e grazie a quale serie
        static void Vincita(char[,] griglia, ref bool continua)
        {
            // VITTORIA A RIGHE
            for (int row = 0; row < 3; row++)
            {
                if (griglia[row, 0] == griglia[row, 1] && griglia[row, 1] == griglia[row, 2] && griglia[row, 2] != '-')
                {
                    Console.WriteLine($"Ha vinto il segno {griglia[row, 0]}, tramite la serie di righe");
                    continua = false;
                }
            }
            // VITTORIA A COLONNE
            for (int col = 0; col < 3; col++)
            {
                if (griglia[0, col] == griglia[1, col] && griglia[1, col] == griglia[2, col] && griglia[2, col] != '-')
                {
                    Console.WriteLine($"Ha vinto il segno {griglia[0, col]}, tramite la serie di colonne");
                    continua = false;
                }
            }
            // VITTORIA IN DIAGONALE
            if (griglia[0, 0] == griglia[1, 1] && griglia[1, 1] == griglia[2, 2] && griglia[2, 2] != '-')
            {
                Console.WriteLine($"Ha vinto il segno {griglia[0, 0]}");
                continua = false;
            }
            if (griglia[0, 2] == griglia[1, 1] && griglia[1, 1] == griglia[2, 0] && griglia[2, 0] != '-')
            {
                Console.WriteLine($"Ha vinto il segno {griglia[2, 0]}, tramite la serie diagonale");
                continua = false;
            }
        }

        static void Main(string[] args)
        {
            string diff = " ";
            bool continua = true;
            //chiedo la modalità di gioco
            while(diff != "facile" && diff != "difficile")
            {
                Console.WriteLine("Facile o Difficile?");
                diff = Console.ReadLine();
            }

            if (diff.Contains("difficile"))
            {
                Console.WriteLine("Non c'è ancora la modalità difficile");
                diff = "facile";
            }

            char[,] griglia = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    griglia[i, j] = '-';
                }
            }

            // CICLO CHE SI RIPETE FINCHE' UNO DEI DUE NON VINCE
            while(continua)
            {
                Interfaccia(griglia);
                Utente(ref griglia);
                if (diff.Contains("facile"))
                {
                    CompFacile(ref griglia);
                }
                Vincita(griglia, ref continua);
            }
        }
    }
}
