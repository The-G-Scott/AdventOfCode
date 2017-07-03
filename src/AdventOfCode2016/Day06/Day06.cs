﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016.Day06
{
    public class Day06 : IDay
    {
		private List<Dictionary<char, int>> columns;
		private char[] message;
		private char[] message2;

		public Day06()
		{
			columns = new List<Dictionary<char, int>>()
			{
				new Dictionary<char, int>(),
				new Dictionary<char, int>(),
				new Dictionary<char, int>(),
				new Dictionary<char, int>(),
				new Dictionary<char, int>(),
				new Dictionary<char, int>(),
				new Dictionary<char, int>(),
				new Dictionary<char, int>()
			};
			message = new char[8];
			message2 = new char[8];
		}

		public void Go()
		{
			GetColumns();
			GetMessages();
			Console.WriteLine($"The message (part 1) is: {new string(message)}");
			Console.WriteLine($"The message (part 2) is: {new string(message2)}");
		}

		private void GetColumns()
		{
			var inputLines = File.ReadAllLines("Day06/input.txt")
				.Where(l => !string.IsNullOrWhiteSpace(l))
				.Select(l => l.Replace("\n", ""));
			foreach (var line in inputLines)
			{
				for (int i = 0; i < 8; i++)
				{
					if (columns[i].ContainsKey(line[i]))
					{
						columns[i][line[i]]++;
					}
					else
					{
						columns[i][line[i]] = 1;
					}
				}
			}
		}

		private void GetMessages()
		{
			for (int i = 0; i < 8; i++)
			{
				message[i] = columns[i].OrderByDescending(c => c.Value).Take(1).SingleOrDefault().Key;
				message2[i] = columns[i].OrderBy(c => c.Value).Take(1).SingleOrDefault().Key;
			}
		}
    }
}
