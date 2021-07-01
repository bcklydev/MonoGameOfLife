using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace GameOfLife
{
    public class Game1 : Game
    {
        Texture2D WhiteSquare;
        Texture2D RedSquare;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //TIMED LOOP
        private double millisecondsPerFrame = 500;
        private double timeSinceLastUpdate = 0;
        //DETERMINES WIDTH OF CELLS
        private int size = 10;
        //CREATE ARRAY OF CELLS, SQUARE ROOT OF LENGTH MUST BE AN INTEGER
        private Cell[] Cells = new Cell[2500];
        private Cell[] NextFrame = new Cell[2500];
        private double width;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public int checkNeighbours(int currentCell)
        {
            int count = 0;
            //CHECK ALL EIGHT NEIGHBOURS
            //-WIDTH - 1
            if (currentCell > 50)
            {
                if (Cells[currentCell - (int)width - 1].GetState() == 1)
                {
                    count++;
                }
            }
            //-WIDTH
            if (currentCell > 49)
            {
                if (Cells[currentCell - (int)width].GetState() == 1)
                {
                    count++;
                }
            }
            //-WIDTH + 1
            if (currentCell > 48)
            {
                if (Cells[currentCell - (int)width + 1].GetState() == 1)
                {
                    count++;
                }
            }
            //-1
            if (currentCell > 0)
            {
                if (Cells[currentCell - 1].GetState() == 1)
                {
                    count++;
                }
            }
            //+1
            if (currentCell < 2499)
            {
                if (Cells[currentCell + 1].GetState() == 1)
                {
                    count++;
                }
            }
            //+WIDTH - 1
            if (currentCell < 2450)
            {
                if (Cells[currentCell + (int)width - 1].GetState() == 1)
                {
                    count++;
                }
            }
            //+WIDTH
            if (currentCell < 2449)
            {
                if (Cells[currentCell + (int)width].GetState() == 1)
                {
                    count++;
                }
            }
            //+WIDTH + 1
            if (currentCell < 2448)
            {
                if (Cells[currentCell + (int)width + 1].GetState() == 1)
                {
                    count++;
                }
            }
            return count;
        }

        protected override void Initialize()
        {
            // INIT LOGIC
            _graphics.PreferredBackBufferWidth = 500;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            for (var i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new Cell();
            }
            width = Math.Sqrt(Cells.Length);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // LOAD CONTENT
            WhiteSquare = new Texture2D(GraphicsDevice, 1, 1);
            WhiteSquare.SetData(new Color[] { Color.White });
            RedSquare = new Texture2D(GraphicsDevice, 1, 1);
            RedSquare.SetData(new Color[] { Color.Red });
        }

        protected override void Update(GameTime gameTime)
        {
            timeSinceLastUpdate = timeSinceLastUpdate + gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdate > millisecondsPerFrame)
            {
                // UPDATE
                Cells.CopyTo(NextFrame, 0);
                for (var i = 0; i < NextFrame.Length; i++)
                {
                    int n = checkNeighbours(i);
                    int s = Cells[i].GetState();

                    if (n < 2 && s == 1)
                    {
                        NextFrame[i].MakeDead();
                    }
                    else if (n > 3 && s == 1)
                    {
                        NextFrame[i].MakeDead();
                    }
                    else if (n == 3 && s == 0)
                    {
                        NextFrame[i].MakeAlive();
                    }
                }
                NextFrame.CopyTo(Cells, 0);
                timeSinceLastUpdate = 0;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // DRAW
            _spriteBatch.Begin();
            for (var y = 0; y < width; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (Cells[((int)width * y) + x].GetState() == 1)
                    {
                        _spriteBatch.Draw(WhiteSquare, new Rectangle(x * size, y * size, size, size), Color.White);
                    }
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}