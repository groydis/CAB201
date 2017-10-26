using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    /// <summary>
    /// This abstract class represents either a computer or human player. A player has an associated name, 
    /// Tank and colour, and also keeps track of the number of rounds won by that player. 
    /// The AIOpponent and PlayerController inherit from Opponent.
    /// Author Greyden Scott & Sean O'Connell October 2017
    /// Written, edited and tested by both team members
    /// </summary>
    public abstract class Opponent
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
