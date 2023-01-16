using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace minesweeper.cs;

public class MyGame : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    struct Cell
    {
        public bool hasFlag;
        public bool hasBomb;
        public bool isUncovered;
        public int neighbouringBombs;
        public Rectangle position;
        public Texture2D texture;
    }

    const int BOARDSIZE = 16;
    const int CELLWIDTH = 48;
    const int BOMBS = 40;
    Cell[,] cell = new Cell[BOARDSIZE + 2, BOARDSIZE + 2];
    Texture2D bombTexture, flagTexture, blankTexture;
    Texture2D[] numbers = new Texture2D[9];
    MouseState mouse, prevMouse;
    public bool GameOver;

    public MyGame()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = 768;
        graphics.PreferredBackBufferHeight = 768;
        graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        IsMouseVisible = true;
        InitializeBoard();
        PlantBombs();
        CountNeighbours();
        GameOver = false;

        base.Initialize();
    }

    void InitializeBoard()
    {
        for (int row = 0; row < BOARDSIZE + 2; row++)
        {
            for (int column = 0; column < BOARDSIZE + 2; column++)
            {
                cell[row, column].hasBomb = false;
                cell[row, column].hasFlag = false;
                cell[row, column].isUncovered = false;
                cell[row, column].position.Width = CELLWIDTH;
                cell[row, column].position.Height = CELLWIDTH;
                
                // Board size = 500px * 500px
                cell[row, column].position.X = (column - 1) * CELLWIDTH;
                cell[row, column].position.Y = (row - 1) * CELLWIDTH;
                cell[row, column].neighbouringBombs = 0;
            }
        }
    }

    void PlantBombs()
    {
        Random random = new Random();
        bool[] array = new bool[BOARDSIZE * BOARDSIZE];

        for (int i = 0; i < BOARDSIZE * BOARDSIZE; i++)
            array[i] = false;

        for (int i = BOARDSIZE * BOARDSIZE - BOMBS; i < BOARDSIZE * BOARDSIZE; i++)
            array[i] = true;
        
        for (int i = 0; i < BOARDSIZE * BOARDSIZE; i++)
        {
            int pos = random.Next(BOARDSIZE * BOARDSIZE);
            bool save = array[i];
            array[i] = array[pos];
            array[pos] = save;
        }

        for (int i = 0; i < BOARDSIZE * BOARDSIZE; i++)
        {
            int column = (i % BOARDSIZE) + 1;
            int row = (i / BOARDSIZE) + 1;
            cell[row, column].hasBomb = array[i];
        }

        // for (int i = 0; i < 100; i++)
        // {
        //     if (array[i])
        //         Console.Write("*");
        //     else
        //         Console.Write(".");
        //     if ((i + 1) % 10 == 0)
        //         Console.WriteLine();
        // }

        // Console.ReadLine();
    }

    void CountNeighbours()
    {
        for (int row = 1; row <= BOARDSIZE; row++)
            for (int column = 1; column <= BOARDSIZE; column++)
            {
                int count = 0;
                if (cell[row - 1, column - 1].hasBomb)
                    count++;
                if (cell[row - 1, column].hasBomb)
                    count++;
                if (cell[row - 1, column + 1].hasBomb)
                    count++;
                if (cell[row, column - 1].hasBomb)
                    count++;
                if (cell[row, column + 1].hasBomb)
                    count++;
                if (cell[row + 1, column - 1].hasBomb)
                    count++;
                if (cell[row + 1, column].hasBomb)
                    count++;
                if (cell[row + 1, column + 1].hasBomb)
                    count++;
                cell[row, column].neighbouringBombs = count;
            }
        for (int row = 1; row <= BOARDSIZE; row++)
        {
            for (int column = 1; column <= BOARDSIZE; column++)
            {
                Console.Write(" {0}", cell[row, column].neighbouringBombs);
            }
            Console.WriteLine();
        }
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        for(int row = 1; row <= BOARDSIZE; row++)
            for(int column = 1; column <= BOARDSIZE; column++)
                cell[row, column].texture = Content.Load<Texture2D>("blank");

        bombTexture = Content.Load<Texture2D>("bomb");
        flagTexture = Content.Load<Texture2D>("flag");
        blankTexture = Content.Load<Texture2D>("blank");
        numbers[0] = Content.Load<Texture2D>("zero");
        numbers[1] = Content.Load<Texture2D>("one");
        numbers[2] = Content.Load<Texture2D>("two");
        numbers[3] = Content.Load<Texture2D>("three");
        numbers[4] = Content.Load<Texture2D>("four");
        numbers[5] = Content.Load<Texture2D>("five");
        numbers[6] = Content.Load<Texture2D>("six");
        numbers[7] = Content.Load<Texture2D>("seven");
        numbers[8] = Content.Load<Texture2D>("eight");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();
        for (int row = 0; row <= BOARDSIZE; row++)
            for (int column = 0; column <= BOARDSIZE; column++)
                if (cell[row, column].hasBomb)
                    spriteBatch.Draw(bombTexture, cell[row, column].position, Color.White);
                else
                    spriteBatch.Draw(blankTexture, cell[row, column].position, Color.White);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}
