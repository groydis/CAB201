using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    abstract public class Opponent
    {
        private string name;
        private TankModel tank;
        private Color colour;
        private int roundsWon;

        public Opponent(string name, TankModel tank, Color colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
            roundsWon = 0;
        }
        public TankModel GetTank()
        {
            return tank;
        }
        public string Identifier()
        {
            return name;
        }
        public Color GetColour()
        {
            return colour;
        }
        public void AddScore()
        {
            roundsWon++;
        }
        public int GetScore()
        {
            return roundsWon;
        }

        public abstract void CommenceRound();

        public abstract void NewTurn(GameplayForm gameplayForm, Gameplay currentGame);

        public abstract void HitPos(float x, float y);
    }
}
