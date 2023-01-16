﻿using Microsoft.Xna.Framework;
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

    const int BOARDSIZE = 10;
    const int CELLWIDTH = 50;
    Cell[,] cell = new Cell[BOARDSIZE + 2, BOARDSIZE + 2];
    Texture2D bombTexture, flagTexture, blankTexture;
    Texture2D[] numbers = new Texture2D[9];
    MouseState mouse, prevMouse;
    public bool GameOver;

    public MyGame()
    {
        graphics = new GraphicsDeviceManager(this);
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
        for (int row = 0; row < BOARDSIZE; row++)
        {
            for (int column = 0; column < BOARDSIZE; column++)
            {
                cell[row, column].hasBomb = false;
                cell[row, column].hasFlag = false;
                cell[row, column].isUncovered = false;
                cell[row, column].position.Width = CELLWIDTH;
                cell[row, column].position.Height = CELLWIDTH;
                
                // Board size = 500px * 500px
                cell[row, column].position.X = column * CELLWIDTH;
                cell[row, column].position.Y = row * CELLWIDTH;
                cell[row, column].neighbouringBombs = 0;
            }
        }
    }

    void PlantBombs()
    {
        Random random = new Random();
        bool[] array = new bool[100];

        for (int i = 0; i < 90; i++)
            array[i] = false;

        for (int i = 90; i < 100; i++)
            array[i] = true;
        
        for (int i = 0; i < 100; i++)
        {
            int pos = random.Next(100);
            bool save = array[i];
            array[i] = array[pos];
            array[pos] = save;
        }

        for (int i = 0; i < 100; i++)
        {
            int column = i % 10;
            int row = i / 10;
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
        for (int row = 0; row < BOARDSIZE; row++)
            for (int column = 0; column < BOARDSIZE; column++)
                if (cell[row, column].hasBomb)
                    spriteBatch.Draw(bombTexture, cell[row, column].position, Color.White);
                else
                    spriteBatch.Draw(blankTexture, cell[row, column].position, Color.White);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}
