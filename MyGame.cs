using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

    protected void InitializeBoard()
    {

    }

    protected void PlantBombs()
    {
        
    }

    protected void CountNeighbours()
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

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
