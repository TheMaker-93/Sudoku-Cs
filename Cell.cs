using System;
namespace sudoku
{
	public class Cell
	{

		// CLASE COMPLETADA //

		// declaraciones
		private bool _locked;        	  // indicara si la casilla puede ser modificada o no
		private bool _used;              // indicara si la casilla es usable por el usuario
		private bool _correct;			// variable que indicara si el valor que contiene es correcto o no
		private char _value;            // valor de la celda


		public Cell()
		{
			_locked = false;
			_used = false;
		 	_correct = false;

			_value = '/';
		}

		// para inicializar el valor de alguna celda
		public void Set_Cell_Value(char value) { _value = value;}
		// para darle un tipo u otro a la celda 
		public void Set_Cell_Type(bool type) { _locked = type; }
		// para bloquear la casilla a la hora de crear la matriz
		public void Set_Interaction_Type() { _locked = true; }
		// para marcar una casilla como usada
		public void Set_Interacted(bool status) { _used = status; }
		// para activbar el flag de correcto en la casilla
		public void Set_Correct(bool correct) { _correct = correct; }

		// para saber que valor tiene la celda
		public char Get_Cell_Value() { return _value; }
		// para extraer si es correcta o no
		public bool Get_Correct() { return _correct; }


		// para preguntar sobre el typo de celda
		public bool Get_Cell_Type() { return _locked; }



		// para mostrar el valor por pantalla
		public void Display_Cell()
		{
			
			if (_locked == false && _used == false)                               // si no esta bloqueada SE VERA COMO EL FONDO
			{
				Console.ForegroundColor = Console.BackgroundColor;
			}
			else if (_locked == true)							// si esta bloqueada se vera BLANCA
			{
				Console.ForegroundColor = ConsoleColor.White;
			}
			else if (_locked == false && _used == true) {                           // SI HEMOS hemos escrito en ella DURANTE EL JUEGO se vera rojo y azul si esta bien

				if (_correct == true) Console.ForegroundColor = ConsoleColor.Blue;
				else if(_correct == false) Console.ForegroundColor = ConsoleColor.Red;

			}
			// lo mostramos en pantalla
			Console.Write(" " + _value + " ");
			Console.ResetColor();

		}





	}
}
