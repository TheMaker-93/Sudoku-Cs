using System;
using System.IO;
namespace sudoku
{
	public class Board
	{
		string _line;           // linea siendo escaneada
		const int _size = 9;    // tamaño maximo de la esttuctura

		Cell[,] _cellMatrix;    // matriz de celdas


		public Board()
		{
			_line = "";
			_cellMatrix = new Cell[_size, _size];
		}

		// se encarga de una vez recibido el nombr del archivo, rellenar la matriz con sus valores
		public void LoadFile(StreamReader loadFile)
		{

			// cargamos la linea leida a la variable _line
			_line = loadFile.ReadLine();

			// generamos las celdas
			CellsInitialization();

			// iteramos por todas las celdas para guardar los valores
			CellAsignation(loadFile);


		}

		// esta funcion creara una instancia de Cell donde sea neesrio
		public void CellsInitialization()
		{
			for (int row = 0; row < _cellMatrix.GetLength(0); row++)
			{
				for (int coll = 0; coll < _cellMatrix.GetLength(1); coll++)
				{
					// en cada posicion de la matriz creamos una instancia de celda
					_cellMatrix[row, coll] = new Cell();

				}

			}


		}

		// esta funcion le da un valor a cada una de las celdas iterando por toda la matriz
		public void CellAsignation(StreamReader loadFile)
		{
			for (int row = 0; row < _cellMatrix.GetLength(0); row++)
			{
				int pos = 0;                // contador de indice de _line
				bool found = false;         // variable para controlar el aparicion de carcteres

				if (_line != null)  // si la linea leida contiene algo
				{

					while (_line == "") // saltamos a la siguiente linea si esta esta vacia
					{
						_line = loadFile.ReadLine();
					}

					for (int coll = 0; coll < _cellMatrix.GetLength(1); coll++)
					{
						// buscqueda del siguiente elemento que no este vacio
						while (found == false && pos < _line.Length)
						{
							// si la linea contiene algo, es decir, no contiene un vacio...
							if (_line[pos] != ' ')
							{
								found = true;
							}
							else // aumentamos el contador de posicion de cursor de escaneo
							{
								pos++;
							}
						}

						// cuando encontremos otro valor en la linea se lo asignamos a la proxima celda
						if (found == true)
						{
							_cellMatrix[row, coll].Set_Cell_Value(_line[pos]);

							// si se trata de un valor difernte a cero lo declaramos como _locked
							// TODO si el valor se consdiera bloqueado nada mas cargarlo jamas podremos comprovar si el archivo cargado es correcto o no, no usando su atributo 
							if (_line[pos] != '0')
							{
								_cellMatrix[row, coll].Set_Cell_Type(true);		// bloqueamos la celda
								_cellMatrix[row, coll].Set_Correct(true);		// le damos valor de correcto 

							}

							pos++;
							found = false;
						}

					}
					// cuando acabamos la linea saltamos a la siguiente
					_line = loadFile.ReadLine();

				}
			}
		}

		// esta funcion mostarar por pantalla los valores de las celdas
		public void DisplayBoard()
		{

			Console.Clear();
			for (int row = 0; row < _cellMatrix.GetLength(0); row++)
			{
				for (int coll = 0; coll < _cellMatrix.GetLength(1); coll++)
				{
					_cellMatrix[row, coll].Display_Cell();
				}
				Console.WriteLine();
			}


		}

		// funcion que nos preguntara por donde queremos escrivir un valor en el array
		public void Write_Cell()
		{
			// posicion que el usuario introduce
			int posX = -1;
			int posY = -1;

			// controlamos que el valor de la posicion sea valido
			bool valid_PosX = false;
			bool valid_PosY = false;
			bool valid_Value = false;

			// valor que el usuario pone en la posicion
			string value = "";

			// variables de retorno de informacion (se encargara de controlar las condiciones de juego y de dar pistas al usuario)
			int[] area_Start;
			bool area_is_Correct;
			bool line_Correct;
			bool coll_Correct;



			// preguntamos				 COMRPVAR QUE LA POSICION INTRODUCIDA ESTE ENTRE 0 Y 8 
			Console.SetCursorPosition(0, 12);
			Console.WriteLine("En que posicion deseas colocar un valor? ");

			// REPETICION EN CASO QUE EL VALOR NO SEA VALIDO (de 0 a 8)
			// ASIGNAMOS VALOR A LA HILERA
			while (valid_PosX == false)
			{

				Console.Write("Hilera (0 a 8): ");
				Console.ForegroundColor = ConsoleColor.Green;

				try
				{
					Console.ForegroundColor = ConsoleColor.Green;
					posX = Convert.ToInt32(Console.ReadLine());
					Console.ResetColor();

					if ((posX >= 0 && posX <= 8) && Convert.ToString(posX).Length == 1)   // si solo contiene un caracter
					{
						valid_PosX = true;
					}
					else
					{
						valid_PosX = false;
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\tIncorrect value, it must be just one character");
						Console.ResetColor();
					}
				}
				catch
				{
					valid_PosX = false;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\tIncorrect value, it must be just one character");
					Console.ResetColor();
				}
				Console.ResetColor();
			}

			// ASIGNAMOS VALOR A LA COLUMNA
			while (valid_PosY == false)
			{
				Console.Write("Columna (0 a 8): ");
				// uso el try y el cacth ya que no pueo convertiro el valor int en char ya que de este modo me sacaria la letra que es representada por el int (ascii)
				// por eso deciid usar esta tactica 
				try
				{
					Console.ForegroundColor = ConsoleColor.Green;
					posY = Convert.ToInt32(Console.ReadLine());
					Console.ResetColor();

					if ((posY >= 0 && posY <= 8) && Convert.ToString(posY).Length == 1)
					{
						valid_PosY = true;
					}
					else
					{
						valid_PosY = false;
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\tIncorrect value, it must be between 0 and 8");
						Console.ResetColor();
					}
				}
				catch
				{
					valid_PosY = false;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\tIncorrect value, it must be between 0 and 8");
					Console.ResetColor();
				}

				Console.ResetColor();

			}

			Console.WriteLine();

			// ASIGNAMOS VALOR AL VALOR
			while (valid_Value == false)
			{
				Console.Write("Que valor te gustaria introducir? (1 a 9) ");
				Console.ForegroundColor = ConsoleColor.Green;
				value = Console.ReadLine();
				Console.ResetColor();

				if (value.Length == 1 && checkIsInRange(Convert.ToChar(value), 49, 57) )
				{
					valid_Value = true;

					// aplicamos a la casilla si esta no es una de las bloqueadas
					if (_cellMatrix[posX, posY].Get_Cell_Type() == false)
					{
						// le applicamos el valor a la celda
						_cellMatrix[posX, posY].Set_Cell_Value(Convert.ToChar(value));
						// la marcamos como celda ya interactuada
						_cellMatrix[posX, posY].Set_Interacted(true);
					}

				}
				else 
				{
					valid_Value = false;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\tIncorrect value, it must be between 1 and 9");
					Console.ResetColor();
				}

			}

			//---------------------------------------------------------------
			// testeando info de area
			area_Start = Get_Area_Starting_Point(posX, posY);

			// Console.WriteLine(area_Start[0] + ", " + area_Start[1] + " punto incial de escaneo del area");

			area_is_Correct = Not_Repeated_in_Area(area_Start,posY,posX);

			Console.Write("\n\n\tarea correcta ");
			if (area_is_Correct) Console.ForegroundColor = ConsoleColor.Green;
			else Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(area_is_Correct);
			Console.ResetColor();

			line_Correct = Not_Repeated_in_Line(posX,posY);			

			Console.Write("\tlinea correcta ");
			if (line_Correct) Console.ForegroundColor = ConsoleColor.Green;
			else Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(line_Correct + " ");
			Console.ResetColor();

			coll_Correct = Not_Repeated_in_Collumn(posY,posX);		

			Console.Write("\tcolumna correcta ");
			if (coll_Correct) Console.ForegroundColor = ConsoleColor.Green;
			else Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(coll_Correct + " ");
			Console.ResetColor();

			// antes de mostrar en pantalla el resultado miraremos que el valor introducido sea correcto o no y lo marcaremos como tal
			if (line_Correct == true && coll_Correct == true && area_is_Correct == true)
			{
				_cellMatrix[posX, posY].Set_Correct(true);
			}
			else
			{
				_cellMatrix[posX, posY].Set_Correct(false);
			}

			// ------------------------------------------------------------

			Console.ReadKey();
			// mostramos entonces el board con el valor actualizado
			DisplayBoard();

			// cuando no se detecten mas 0 en la matriz y la matriz sea correcta
			if (Matrix_Is_Correct() == true)
			{
				// Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("You have completed the game with a succesfull result!");


				Console.SetCursorPosition(0, Console.WindowHeight - 5);
				Console.WriteLine("press any key to exit");
				Console.ReadKey(true);

			}


		}


		// esta funcion se encargara de devolver al usuario el area (r,c) en la cual esta situada la casilla
		public int[] Get_Area_Starting_Point(int row, int coll)
		{
			int[] area_Starting_Point = new int[2] { -1, -1 };         // variable que guardara ambas coordenadas de la matriz simplificada / 3

			// primero dividireos ambas coordenadas entre 3 para sber su coordenada (,) en la matriz
			area_Starting_Point[0] = row / 3 * 3;
			area_Starting_Point[1] = coll / 3 * 3;
			// "area ahora contiene las coordenadas del area "
			return area_Starting_Point;
			// despues haces una iteracion de 0,1,2 en x e igual en Y para recorrer toda el area
		}

		// ----- COMPROVACIONES GAMEPLAY---------------------------------------------------------------------------------------- //

		// este metodo se encargara de expresar si la matriz HA SIDO COMPLETADA correctamente
		public bool Matrix_Is_Correct()
		{
			// iteramos por todas las casillas y pararemos en caso de encontar un caso en el cual no hay un valor correcto
			for (int row = 0; row < _cellMatrix.GetLength(0); row++)
			{
				for (int coll = 0; coll < _cellMatrix.GetLength(1); coll++)
				{
					if (_cellMatrix[row, coll].Get_Correct() == false)
					{
						return false;
					}
				}
			}
			return true;		// devovlemos true si no encontramos ningun valor falso
		}

		// esta funcion se encargara de comprovar que la matriz introducida , si esta llena, sea correcta
		// se que esto no es mas que un rodeo pero me estoy quedando sin tiempo para pensar en algo mejor
		public bool Input_Matrix_is_Correct()
		{

			int[] areaStartinPoint;

			for (int row = 0; row < _cellMatrix.GetLength(0); row++)
			{
				for (int coll = 0; coll < _cellMatrix.GetLength(1); coll++)
				{
					areaStartinPoint = Get_Area_Starting_Point(row, coll);

					if (Not_Repeated_in_Line(row, coll) == false && Not_Repeated_in_Collumn(coll, row) == false && Not_Repeated_in_Area(areaStartinPoint, coll, row) == false)
					{
						// _cellMatrix[row, coll].Set_Correct(true);
						return false;
					}

				}
			}
			return true; 	// en el caso que ningun valor sea incorrecto devolveremos true

		}

		// este metodo se encargara de comprovar que el ARCHIVO CARGADO o LA MATRIZ esta completo o no y despues de indicar si es correcto o sino lo es
		public bool File_Is_Filled()
		{
			for (int row = 0; row < _cellMatrix.GetLength(0); row++)
			{
				for (int coll = 0; coll < _cellMatrix.GetLength(1); coll++)
				{
					if (_cellMatrix[row, coll].Get_Cell_Value() == '0')
					{
						return false;
					}
				}
			}
			return true;
		}


		public bool Is_Correct()
		{
			for (int r = 0; r < _cellMatrix.GetLength(0); r++)
			{
				for (int c = 0; c < _cellMatrix.GetLength(1); c++)
				{   // si el caracter de la celda no es alguno de estos entonces mandamos el error de que hay caracteres invalidos
					if (_cellMatrix[r, c].Get_Cell_Value() != '0' && _cellMatrix[r, c].Get_Cell_Value() != '1' && _cellMatrix[r, c].Get_Cell_Value() != '2' &&
						_cellMatrix[r, c].Get_Cell_Value() != '3' && _cellMatrix[r, c].Get_Cell_Value() != '4' && _cellMatrix[r, c].Get_Cell_Value() != '5' && _cellMatrix[r, c].Get_Cell_Value() != '6' &&
						_cellMatrix[r, c].Get_Cell_Value() != '7' && _cellMatrix[r, c].Get_Cell_Value() != '8' && _cellMatrix[r, c].Get_Cell_Value() != '9')
					{
						return false;
					}
				}
			}
			return true;
		}

		public bool Not_Repeated_in_Line(int row, int currentColl)
		{
			// esta funcions e encargara de escanear la linea en busqueda del valor actual. En caso de encontrarlo devolvera false
			for (int coll = 0; coll < _cellMatrix.GetLength(1); coll++)
			{
				// si la columna en la que estamos es diferente al actual entonces haremos la comprovacion
				if (coll != currentColl)
				{
					// si la casilla a escanear actualmente tiene el mismo valor que la siguiente
					if (_cellMatrix[row, currentColl].Get_Cell_Value() == _cellMatrix[row, coll].Get_Cell_Value())
					{
						return false;
					}

				}

			}
			// en el caso que el valor no se repita devovleremos true
			return true;
		}

		public bool Not_Repeated_in_Collumn(int coll, int currentRow)
		{
			// esta funcion se encargara de iterar en la columna y en caso de entcontrar una coincidencia devovler false
			// funciona como la funcion previa pero en las columnas
			for (int row = 0; row < _cellMatrix.GetLength(0); row++)
			{
				// solo queremos que se compare con el resto de valores y no consigo mismo
				if (row != currentRow)
				{
					if (_cellMatrix[currentRow, coll].Get_Cell_Value() == _cellMatrix[row, coll].Get_Cell_Value())
					{
						return false;
					}
				}
			}
			return true;
		}

		public bool Not_Repeated_in_Area (int[] area_Starting_Point, int currentColl, int CurrentRow)
		{
			// esta funcion iterara en las casillas del area para buscar repeticiones, en caso de aparecer deevovleremos false
			// iteramos por la hilera actual y las 2 siguientes
			for (int row = area_Starting_Point[0]; row < area_Starting_Point[0] + 3; row++)
			{
				// iteramos por cada una de las columnas
				for (int coll = area_Starting_Point[1]; coll < area_Starting_Point[1] + 3; coll++)
				{

					// solo realizaremos la comprovacion con los demas valores, nunca consigo mismo
					if (currentColl != coll || CurrentRow != row)
					{
						// si el valor es igual decimos que el valor esta repetido en el area
						if (_cellMatrix[CurrentRow, currentColl].Get_Cell_Value() == _cellMatrix[row, coll].Get_Cell_Value())
						{
							return false;
						}

					}


				}
			}

			return true;
		}


		// comrpueva que el valor introducido este entre 0 y 9 ambos incluidos (por defecto)
		public static bool checkIsInRange(char value, int minAscii = 49, int maxAscii = 57)
		{
			// convertimos el valor a ascii para saber su codigo
			int ascii = Convert.ToInt32(value);
			if (ascii >= minAscii && ascii <= maxAscii)
			{
				return true;
			}
			else
			{
				return false;
			}

		}






	}
}
