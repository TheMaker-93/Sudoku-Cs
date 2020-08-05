using System;
using System.IO;

namespace sudoku
{
	public class Sudoku
	{
		// creacion
		string loadFileName;        // nombre del arhvivo a cargar
		Board _board; 
		bool file_Is_Correct;        

		public Sudoku()
		{
			_board = new Board();// instancia de board
			file_Is_Correct = false; // control de la usabilidad de la info introducida
		}


		// inicia el juego
		public void Play()
		{
			
			// cargamso el archivo comprovando que no tenga errores de escritura
			file_Is_Correct = LoadFile();

			// pediremos el arhico hata que la boleana de return de load file sea true (cuando consiga cargar el archivo)
			while (!file_Is_Correct)
			{
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\tERROR. Invalid file.");
				Console.WriteLine("\tSudoku file must be in the same directory as the sudoku executable.");
				Console.ResetColor();
				Console.Write("\tPress any key to continue.");
				Console.ReadKey(true);
				Console.Clear();
				file_Is_Correct = LoadFile();		// el procedimiento devolera true or false dependiendo si ha podido o no cargar el archivo
			}

			// nos pedira el tablero a cargar hasta que el contenido no tenga caracteres especiales 
			while (!_board.Is_Correct()) 
			{
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\tERROR. Invalid file.");
				Console.WriteLine("\tThe file contained invalid characters.");
				Console.WriteLine("\t\t Use only numbers from 0 (for spaces to fill) to 9");
				Console.ResetColor();
				Console.ReadKey(true);
				Console.Clear();
				file_Is_Correct = LoadFile();
			}

			// en el caso que el tablero cargado tenga todas sus posiciones llenas diremos que es correcto o no
			// en el caso qu eno este lleno jugaremos normalmente

			_board.DisplayBoard();	// mostramos el tablero

			if (_board.File_Is_Filled())
			{
				Console.WriteLine("\nThe sudoku you have introduced is completed and the resolution is");

				// aqui adentro haremos la comprovacion de si esta lleno comprovar que este correctamente hecho
				if (_board.Input_Matrix_is_Correct() == true)
				{
					Console.BackgroundColor = ConsoleColor.Green;
					Console.ForegroundColor = ConsoleColor.Black;
					Console.Write("\nCORRECT");
					Console.ResetColor();

					Console.SetCursorPosition(0, Console.WindowHeight - 1);
					Console.WriteLine("Press any key to exit");
				}
				else
				{
					Console.BackgroundColor = ConsoleColor.Red;
					Console.ForegroundColor = ConsoleColor.Black;
					Console.Write("\nINCORRECT");
					Console.ResetColor();

					Console.SetCursorPosition(0, Console.WindowHeight - 1);
					Console.WriteLine("Press any key to exit");
				}
			}
			else
			{
				// iniciamos el juego 
				while (_board.Matrix_Is_Correct() == false)
				{
					_board.Write_Cell();
				}

			}





		}

		// se encargara de llamar al metodo LoadFile de board
		public bool LoadFile()
		{
			Console.Write("\n\n\tPlease enter the name (with its extension) of the board you want to load: ");
			Console.ForegroundColor = ConsoleColor.Green;
				loadFileName = Console.ReadLine();
			Console.ResetColor();

			try
			{
				// probamos de cargarlo
				StreamReader fileToLoad = new StreamReader(loadFileName);
				_board.LoadFile(fileToLoad);
				Console.Clear();

				return true;
			}
			catch
			{


				return false;
			}
		}



	}
}
