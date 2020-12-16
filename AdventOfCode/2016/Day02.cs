﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2016
{
    public class Day02 : IDay
    {
		private string[][] keypad;
		private string[][] keypad2;
		private Position position;
		private Position position2;
		private List<char[]> instructions;
		private string combination;
		private string combination2;

		public Day02()
		{
			keypad = new string[][]
			{
				new string[] { "1", "2", "3" },
				new string[] { "4", "5", "6" },
				new string[] { "7", "8", "9" }
			};
			keypad2 = new string[][]
			{
				new string[] { " ", " ", "1", " ", " " },
				new string[] { " ", "2", "3", "4", " " },
				new string[] { "5", "6", "7", "8", "9" },
				new string[] { " ", "A", "B", "C", " " },
				new string[] { " ", " ", "D", " ", " " }
			};

			position = new Position { X = 1, Y = 1 };
			position2 = new Position { X = 0, Y = 2 };

			combination = "";
			combination2 = "";
		}

        public void GetInput()
        {
            instructions = File.ReadAllLines("2016/input/day02.txt")
                .Where(l => !string.IsNullOrEmpty(l))
                .Select(l => l.Replace("\n", "").ToCharArray())
                .ToList();
        }

		public void Solve()
		{
			foreach (var instruction in instructions)
			{
				foreach (var move in instruction)
				{
					ProcessMove(move);
				}
				combination += keypad[position.Y][position.X];
				combination2 += keypad2[position2.Y][position2.X];
			}
			Console.WriteLine($"The combination (part one) is: {combination}");
			Console.WriteLine($"The combination (part two) is: {combination2}");
		}

		private void ProcessMove(char move)
		{
			switch (move)
			{
				case 'U':
					SafeSubtractY();
					break;
				case 'R':
					SafeAddX();
					break;
				case 'D':
					SafeAddY();
					break;
				case 'L':
					SafeSubtractX();
					break;
			}
		}

		private void SafeAddX()
		{
			if (position.X < 2)
			{
				position.X++;
			}
			if (position2.X < 4 && !string.IsNullOrWhiteSpace(keypad2[position2.Y][position2.X + 1]))
			{
				position2.X++;
			}
		}

		private void SafeAddY()
		{
			if (position.Y < 2)
			{
				position.Y++;
			}
			if (position2.Y < 4 && !string.IsNullOrWhiteSpace(keypad2[position2.Y + 1][position2.X]))
			{
				position2.Y++;
			}
		}

		private void SafeSubtractX()
		{
			if (position.X > 0)
			{
				position.X--;
			}
			if (position2.X > 0 && !string.IsNullOrWhiteSpace(keypad2[position2.Y][position2.X - 1]))
			{
				position2.X--;
			}
		}

		private void SafeSubtractY()
		{
			if (position.Y > 0)
			{
				position.Y--;
			}
			if (position2.Y > 0 && !string.IsNullOrWhiteSpace(keypad2[position2.Y - 1][position2.X]))
			{
				position2.Y--;
			}
		}

        internal class Position
        {
            public int X { get; set; } = 0;
            public int Y { get; set; } = 0;
        }
    }
}
