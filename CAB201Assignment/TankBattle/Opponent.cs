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
        private string names;
        private TankModel tanks;
        private Color colours;
        private int roundsWon;

        public Opponent(string name, TankModel tank, Color colour)
        {
            names = name;
            tanks = tank;
            colours = colour;
            roundsWon = 0;
        }
        public TankModel GetTank()
        {
            return tanks;
        }
        public string Identifier()
        {
            return names;
        }
        public Color GetColour()
        {
            return colours;
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
