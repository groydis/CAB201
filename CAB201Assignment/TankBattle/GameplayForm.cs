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

        public GameplayForm(Gameplay game)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            InitializeComponent();

            // THIS STUFF MAY NEED TO GO ABOVE ALL OF THAT ^
            this.currentGame = game;
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

            int randomInt = rng.Next(4);
            backgroundImage = Image.FromFile(imageFilenames[randomInt]);
            landscapeColour = landscapeColours[randomInt];

            backgroundGraphics = InitialiseBuffer();
            gameplayGraphics = InitialiseBuffer();
            DrawBackground();
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
        }
        public void SetWeapon(int weapon)
        {
            weaponComboBox.SelectedValue = weapon;
        }

        public void Fire()
        {
            currentGame.GetCurrentPlayerTank();
            controlPanel.Enabled = false;
            // ENABLE TIMER???
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
            // CREATED AS PER INSTRUCTIONS FOR public GameplayForm
            throw new NotImplementedException();
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
    }
}
