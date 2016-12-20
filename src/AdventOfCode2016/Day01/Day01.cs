﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2016.Day01
{
    public class Day01
    {
		private Direction currentDirection;
		private string[] instructions;
		private Position position;
		private Position realPosition;
		private List<Position> visitedPositions;

		public Day01()
		{
			var instructionsFile = File.OpenText("Day01/input.txt");
			instructions = instructionsFile.ReadLine().Replace(" ", "").Split(',');
			currentDirection = Direction.NORTH;
			position = new Position();
			visitedPositions = new List<Position>();
		}

		public void Go()
		{
			AddAndCheckPosition();
			foreach (var instruction in instructions)
			{
				UpdateDirection(instruction[0]);
				UpdatePosition(Convert.ToInt32(instruction.Substring(1)));
			}
			var distance = CalculateDistance(position);
			Console.WriteLine($"The distance to the Easter Bunny Headquarters (part 1) is: {distance}");
			var realDistance = CalculateDistance(realPosition);
			Console.WriteLine($"The real distance to the Easter Bunny Headquarters (part 2) is: {realDistance}");
		}

		private void UpdateDirection(char turn)
		{
			if (turn == 'L')
			{
				if (currentDirection == Direction.NORTH)
				{
					currentDirection = Direction.WEST;
				}
				else if (currentDirection == Direction.WEST)
				{
					currentDirection = Direction.SOUTH;
				}
				else if (currentDirection == Direction.SOUTH)
				{
					currentDirection = Direction.EAST;
				}
				else
				{
					currentDirection = Direction.NORTH;
				}
			}
			else
			{
				if (currentDirection == Direction.NORTH)
				{
					currentDirection = Direction.EAST;
				}
				else if (currentDirection == Direction.EAST)
				{
					currentDirection = Direction.SOUTH;
				}
				else if (currentDirection == Direction.SOUTH)
				{
					currentDirection = Direction.WEST;
				}
				else
				{
					currentDirection = Direction.NORTH;
				}
			} 
		}

		private void UpdatePosition(int distance)
		{
			for (int i = 0; i < distance; i++)
			{
				switch (currentDirection)
				{
					case Direction.NORTH:
						position.Y++;
						break;
					case Direction.EAST:
						position.X++;
						break;
					case Direction.SOUTH:
						position.Y--;
						break;
					case Direction.WEST:
						position.X--;
						break;
				}
				AddAndCheckPosition();
			}
		}

		private void AddAndCheckPosition()
		{
			if (realPosition == null)
			{
				if (!visitedPositions.Any(vp => vp.X == position.X && vp.Y == position.Y))
				{
					visitedPositions.Add(new Position { X = position.X, Y = position.Y });
				}
				else
				{
					realPosition = new Position { X = position.X, Y = position.Y };
				}
			}
		}

		private int CalculateDistance(Position destination)
		{
			return Math.Abs(destination.X) + Math.Abs(destination.Y);
		}
    }
}
