using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakuzuWithSolver.Logic
{
    public enum State
    {
        Empty = ' ',
        Zero = '0',
        One = '1'
    }
    public class Logic: IGameModel
    {
        public int Dimension { get; private set; }
        public State[,] Map { get; private set; }
        public State[,] Solution { get; private set; }

        public Logic(int dim)
        {
            Import(dim);
            Solve();
        }
        public Logic getCopy()
        {
            return new Logic(Dimension) { Map = this.Map };
        }
        void Import(int dim)
        {
            Random rnd = new Random();
            string json = File.ReadAllText("takuzuList.json");
            var takuzus = JsonConvert.DeserializeObject<List<Logic>>(json).Where(x => x.Dimension == dim);
            Logic sel = takuzus.ElementAt(rnd.Next(0, takuzus.Count()));

            this.Dimension = sel.Dimension;
            this.Map = sel.Map;
        }
        void Solve()
        {
            this.Solution = Solver.Solve(this.Map);
        }

        public void SetState(int x, int y, State state)
        {
            if (IsValidPosition(x, y)) this.Map[x, y] = state;
            else throw new Exception("State can't be set due to unvalid parameters.");

        }
        public State GetState(int x, int y)
        {
            if (IsValidPosition(x, y)) return this.Map[x, y];
            else throw new Exception("State can't be retrieved due to unvalid parameters.");
        }

        bool IsComplete()
        {
            foreach (State state in this.Map)
            {
                if (state == State.Empty) return false;
            }
            return true;
        }
        bool IsSolved()
        {
            for (int i = 0; i < this.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Map.GetLength(1); j++)
                {
                    if (this.Map[i, j] != this.Solution[i, j]) return false;
                }
            }
            return true;
        }
        bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < this.Dimension &&
                   y >= 0 && y < this.Dimension;
        }
    }
}
