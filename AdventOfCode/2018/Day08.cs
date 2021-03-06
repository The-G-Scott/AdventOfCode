﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2018
{
	public class Day08 : IDay
	{
		public List<int> Input;
		private int i;

		public void GetInput()
		{
			Input = File.ReadAllLines("2018/input/day08.txt")
				.Where(l => !string.IsNullOrEmpty(l))
				.First()
				.Split(' ')
				.Select(n => Convert.ToInt32(n))
				.ToList();
		}

		public void Solve()
		{
			i = 0;
			var root = GetNode();
			Console.WriteLine($"The sum of metadata values (part one) is: {root.Sum()}");
			Console.WriteLine($"The value of the root node (part two) is: {root.Value()}");
		}

		public Node GetNode()
		{
			var node = new Node();

			var childCount = Input[i];
			var dataCount = Input[i + 1];

			i += 2;
			// recursively add number of specified children
			for (int c = 0; c < childCount; c++)
			{
				node.Children.Add(GetNode());
			}

			for (int d = 0; d < dataCount; d++)
			{
				node.Data.Add(Input[i]);
				i++;
			}

			return node;
		}

		public class Node
		{
			public List<Node> Children { get; set; } = new List<Node>();

			public List<int> Data { get; set; } = new List<int>();

			public int Sum()
			{
				return Data.Sum() + Children.Sum(c => c.Sum());
			}

			public int Value()
			{
				if (Children.Count == 0)
				{
					return Data.Sum();
				}

				var value = 0;
				foreach (var index in Data)
				{
					if (index <= Children.Count)
					{
						value += Children[index - 1].Value();
					}
				}
				return value;
			}
		}
	}
}
