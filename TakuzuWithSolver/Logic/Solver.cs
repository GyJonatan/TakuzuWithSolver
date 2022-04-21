using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakuzuWithSolver.Logic;

namespace TakuzuWithSolver
{
    public class Solver
    {
        public static State[,] Solve(State[,] map)
        {
            State[,] original, newGame = map;
            do
            {
                original = newGame;
                try
                {
                    newGame = SimpleRules(GetCopy(original));
                }
                catch (SolverException e)
                {
                    return original;
                }
                if (original.Equals(newGame))
                {
                    try
                    {
                        return FindGuess(original); 
                    }
                    finally { }
                }
            } while (!IsComplete(newGame));
            return newGame;
        }
        static State[,] GetCopy(State[,] map)
        {
            State[,] result = map;
            return result;
        }
        static bool IsComplete(State[,] map)
        {
            foreach (State state in map)
            {
                if (state == State.Empty)
                {
                    return false;
                }
            }
            return true;
        }
        static State[,] SimpleRules(State[,] map)
        {
            DoubleRuleRow(map);
            DoubleRuleColumn(map);
            GapRuleRow(map);
            GapRuleColumn(map);
            EvenRuleRow(map);
            EvenRuleColumn(map);
            return map;
        }
        static State[,] FindGuess(State[,] map)
        {
            int x = 0, y = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == State.Empty)
                    {
                        x = i;
                        y = j;
                        i = map.GetLength(0);
                        j = map.GetLength(1);
                    }
                }
            }

            try
            {
                GuessSolve(map, x, y, State.Zero);
            }
            catch (SolverException e)
            {
                try
                {
                    GuessSolve(map, x, y, State.One);
                }
                finally { }
            }
            return map;
        }

        static void GuessSolve(State[,] map, int x, int y, State state)
        {
            State[,] copy = map;
            try
            {
                CheckUpdate(copy, x, y, Invert(state));
                Solve(copy);
            }
            catch (SolverException e)
            {
                try
                {
                    CheckUpdate(copy, x, y, Invert(state));
                    Solve(copy);
                }
                finally { }
            }
        }

        static State Invert(State state)
        {
            State result = State.Empty;
            switch (state)
            {
                case State.Zero:
                    result = State.One;
                    break;
                case State.One:
                    result = State.Zero;
                    break;
            }
            return result;
        }
        static void CheckUpdate(State[,] map, int x, int y, State state)
        {
            if (map[x, y] == State.Empty)
            {
                map[x, y] = state;
            }
            else
            {
                throw new SolverException();
            }
        }
        static void FillRow(State[,] map, int row, State state)
        {
            for (int i = 0; i < map.GetLength(1); i++)
            {
                if (map[i, row] == State.Empty)
                {
                    CheckUpdate(map, i, row, state);
                }
            }
        }
        static void FillColumn(State[,] map, int column, State state)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[column, j] == State.Empty)
                {
                    CheckUpdate(map, column, j, state);
                }
            }
        }
        static void DoubleRuleRow(State[,] map)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                for (int i = 0; i < map.GetLength(0) - 1; i++)
                {
                    State state = map[i, j];
                    if (state != State.Empty &&
                        state == map[i + 1, j])
                    {
                        State inverse = Invert(state);
                        CheckUpdate(map, i - 1, j, inverse);
                        CheckUpdate(map, i + 2, j, inverse);
                    }
                }
            }
        }
        static void DoubleRuleColumn(State[,] map)
        {
            for (int j = 0; j < map.GetLength(1) - 1; j++)
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    State state = map[i, j];
                    if (state != State.Empty &&
                        state == map[i, j + 1])
                    {
                        State inverse = Invert(state);
                        CheckUpdate(map, i, j - 1, inverse);
                        CheckUpdate(map, i, j + 2, inverse);
                    }
                }
            }
        }
        static void GapRuleRow(State[,] map)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                for (int i = 1; i < map.GetLength(0) - 1; i++)
                {
                    if (map[i, j] == State.Empty &&
                        map[i - 1, j] == map[i + 1, j])
                    {
                        State inverse = Invert(map[i + 1, j]);
                        CheckUpdate(map, i, j, inverse);
                    }
                }
            }
        }
        static void GapRuleColumn(State[,] map)
        {
            for (int j = 1; j < map.GetLength(1) - 1; j++)
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    if (map[i, j] == State.Empty &&
                        map[i, j - 1] == map[i, j + 1])
                    {
                        State inverse = Invert(map[i, j + 1]);
                        CheckUpdate(map, i, j, inverse);
                    }
                }
            }
        }
        static void EvenRuleRow(State[,] map)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            for (int j = 0; j < width; j++)
            {
                int one = 0, zero = 0;
                for (int i = 0; i < height; i++)
                {
                    State state = map[i, j];
                    if (state == State.Zero) zero++;
                    else if (state == State.One) one++;
                }
                if (2 * one >= height)
                    FillRow(map, j, State.Zero);
                else if (2 * one >= width)
                    FillRow(map, j, State.One);
            }
        }
        static void EvenRuleColumn(State[,] map)
        {

            int width = map.GetLength(0);
            int height = map.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                int one = 0, zero = 0;
                for (int j = 0; j < height; j++)
                {
                    State state = map[i, j];
                    if (state == State.Zero) zero++;
                    else if (state == State.One) one++;
                }
                if (2 * one >= height)
                    FillColumn(map, i, State.Zero);
                else if (2 * one >= width)
                    FillColumn(map, i, State.One);
            }
        }

        class SolverException : Exception { }
    }
}
