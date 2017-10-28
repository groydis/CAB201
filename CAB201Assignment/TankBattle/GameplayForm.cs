using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace TankBattle
{
    public partial class GameplayForm : Form
    {
        private Color landscapeColour;
        private Random rng = new Random();
        private Image backgroundImage = null;
        private int levelWidth = 160;
        private int levelHeight = 120;
        private Gameplay currentGame;

        private BufferedGraphics backgroundGraphics;
        private BufferedGraphics gameplayGraphics;

        private BattleTank currentTank;

        public GameplayForm(Gameplay game)
        {

            currentGame = game;

            string[] imageFilenames =
            {
                "Images\\background1.jpg",
                "Images\\background2.jpg",
                "Images\\background3.jpg",
                "Images\\background4.jpg",
            };

            Color[] landscapeColours =
            {
                Color.FromArgb(255, 0, 0, 0),
                Color.FromArgb(255, 73, 58, 47),
                Color.FromArgb(255, 148, 116, 93),
                Color.FromArgb(255, 133, 119, 109),
            };

            int randomInt = rng.Next(0, 3);
            backgroundImage = Image.FromFile(imageFilenames[randomInt]);
            landscapeColour = landscapeColours[randomInt];

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            InitializeComponent();

            backgroundGraphics = InitialiseBuffer();
            gameplayGraphics = InitialiseBuffer();

            DrawBackground();
;
            DrawGameplay();

            NewTurn();

        }

        // From https://stackoverflow.com/questions/13999781/tearing-in-my-animation-on-winforms-c-sharp
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        public void EnableControlPanel()
        {
            controlPanel.Enabled = true;
        }

        public void SetAngle(float angle)
        {
            angleNumericUpDown.Value = (decimal)angle;
        }

        public void SetPower(int power)
        {
            powerTrackBar.Value = power;
            powerValueLabel.Text = power.ToString();
        }
        public void SetWeapon(int weapon)
        {
            weaponComboBox.SelectedValue = weapon;
            weaponComboBox.SelectedValue = 0;
        }

        public void Fire()
        {
            BattleTank bTank = currentGame.GetCurrentPlayerTank();
            bTank.Fire();
            controlPanel.Enabled = false;
            timer1.Enabled = true;

        }

        private void DrawBackground()
        {
            Graphics graphics = backgroundGraphics.Graphics;
            Image background = backgroundImage;
            graphics.DrawImage(backgroundImage, new Rectangle(0, 0, displayPanel.Width, displayPanel.Height));

            Map battlefield = currentGame.GetArena();
            Brush brush = new SolidBrush(landscapeColour);

            for (int y = 0; y < Map.HEIGHT; y++)
            {
                for (int x = 0; x < Map.WIDTH; x++)
                {
                    if (battlefield.Get(x, y))
                    {
                        int drawX1 = displayPanel.Width * x / levelWidth;
                        int drawY1 = displayPanel.Height * y / levelHeight;
                        int drawX2 = displayPanel.Width * (x + 1) / levelWidth;
                        int drawY2 = displayPanel.Height * (y + 1) / levelHeight;
                        graphics.FillRectangle(brush, drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1);
                    }
                }
            }
        }

        private void DrawGameplay()
        {
            backgroundGraphics.Render(gameplayGraphics.Graphics);
            currentGame.DrawPlayers(gameplayGraphics.Graphics, displayPanel.Size);
            currentGame.DrawAttacks(gameplayGraphics.Graphics, displayPanel.Size);

        }

        private void NewTurn()
        {
            currentTank = currentGame.GetCurrentPlayerTank();

            Opponent opponentTank = currentTank.GetPlayer();

            Text = String.Format("Tank Battle - Round {0} of {1}", currentGame.GetRoundNumber(), currentGame.GetMaxRounds());
            BackColor = opponentTank.GetColour();
            playerNameLabel.Text = opponentTank.Identifier();
            SetAngle(currentTank.GetTankAngle());
            SetPower(currentTank.GetCurrentPower());

            int currentWind = currentGame.GetWindSpeed();
            if (currentWind > 0)
            {
                currWindLabel.Text = String.Format("{0} E", currentWind);
            }
            else
            {
                currentWind = currentWind * -1;
                currWindLabel.Text = String.Format("{0} W", currentWind);
            }
            weaponComboBox.Items.Clear();
            TankModel currentTankModel = currentTank.GetTank();
            foreach (String weapon in currentTankModel.WeaponList())
            {
                weaponComboBox.Items.Add(weapon);
            }
            SetWeapon(weaponComboBox.SelectedIndex);
            opponentTank.NewTurn(this, currentGame);
            controlPanel.BackColor = opponentTank.GetColour();
        }

        public BufferedGraphics InitialiseBuffer()
        {
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            Graphics graphics = displayPanel.CreateGraphics();
            Rectangle dimensions = new Rectangle(0, 0, displayPanel.Width, displayPanel.Height);
            BufferedGraphics bufferedGraphics = context.Allocate(graphics, dimensions);
            return bufferedGraphics;
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = displayPanel.CreateGraphics();
            gameplayGraphics.Render(graphics);
        }

        private void fireButton_Click(object sender, EventArgs e)
        {
            Fire();
        }

        private void weaponComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentTank.SetWeapon(weaponComboBox.SelectedIndex);
        }

        private void angleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            currentTank.SetAngle((float)angleNumericUpDown.Value);
            DrawGameplay();
            displayPanel.Invalidate();
        }

        private void powerTrackBar_Scroll(object sender, EventArgs e)
        {
            currentTank.SetPower(powerTrackBar.Value);
            powerValueLabel.Text = currentTank.GetCurrentPower().ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (currentGame.ProcessWeaponEffects() == false)
            {
                currentGame.GravityStep();
                DrawBackground();
                DrawGameplay();
                displayPanel.Invalidate();
                if (currentGame.GravityStep() == true)
                {
                    return;
                }
                else
                {
                    timer1.Enabled = false;
                    if (currentGame.FinishTurn() == true)
                    {
                        NewTurn();
                    }
                    else
                    {
                        Dispose();
                        currentGame.NextRound();
                        return;
                    }

                }
            }
            else
            {
                DrawGameplay();
                displayPanel.Invalidate();
                return;
            }
        }
    }
}
