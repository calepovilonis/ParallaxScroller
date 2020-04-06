using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ParallaxStarter
{
   /// <summary>
   /// This is the main type for your game.
   /// </summary>
   public class Game1 : Game
   {
      GraphicsDeviceManager graphics;
      SpriteBatch spriteBatch;

      Texture2D lava;

      Player player;

      public Game1()
      {
         graphics = new GraphicsDeviceManager(this);
         graphics.PreferredBackBufferWidth = 1042;  // set this value to the desired width of your window
         graphics.PreferredBackBufferHeight = 742;
         Content.RootDirectory = "Content";
      }

      /// <summary>
      /// Allows the game to perform any initialization it needs to before starting to run.
      /// This is where it can query for any required services and load any non-graphic
      /// related content.  Calling base.Initialize will enumerate through any components
      /// and initialize them as well.
      /// </summary>
      protected override void Initialize()
      {
         // TODO: Add your initialization logic here

         base.Initialize();
      }

      /// <summary>
      /// LoadContent will be called once per game and is the place to load
      /// all of your content.
      /// </summary>
      protected override void LoadContent()
      {
         // Create a new SpriteBatch, which can be used to draw textures.
         spriteBatch = new SpriteBatch(GraphicsDevice);

         var backgroundTexture = Content.Load<Texture2D>("bg");
         var backgroundSprite = new StaticSprite(backgroundTexture);
         var backgroundLayer = new ParallaxLayer(this);
         backgroundLayer.Sprites.Add(backgroundSprite);
         backgroundLayer.DrawOrder = 0;
         Components.Add(backgroundLayer);

         // TODO: use this.Content to load your game content here
         var spritesheet = Content.Load<Texture2D>("boat");
         player = new Player(spritesheet);

         var playerLayer = new ParallaxLayer(this);
         playerLayer.Sprites.Add(player);
         playerLayer.DrawOrder = 2;
         Components.Add(playerLayer);

         var midgroundTextures = new Texture2D[]
         {
            Content.Load<Texture2D>("mid"),
            Content.Load<Texture2D>("mid")
         };

         var midgroundSprites = new StaticSprite[]
         {
            new StaticSprite(midgroundTextures[0]),
            new StaticSprite(midgroundTextures[1], new Vector2(3500, 0))
         };

         var midgroundLayer = new ParallaxLayer(this);
         midgroundLayer.Sprites.AddRange(midgroundSprites);
         midgroundLayer.DrawOrder = 1;
         //var midgroundScrollController = midgroundLayer.ScrollController as AutoScrollController;
         //midgroundScrollController.Speed = 40f;
         Components.Add(midgroundLayer);

         lava = Content.Load<Texture2D>("lava");
         

         var foregroundSprites = new StaticSprite(lava);

         var foregroundLayer = new ParallaxLayer(this);
         foregroundLayer.Sprites.Add(foregroundSprites);

         foregroundLayer.DrawOrder = 4;
         //var foregroundScrollController = foregroundLayer.ScrollController as AutoScrollController;
         //foregroundScrollController.Speed = 80f;
         Components.Add(foregroundLayer);

         //var playerScrollController = playerLayer.ScrollController as AutoScrollController;
         //playerScrollController.Speed = 80f;

         backgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.1f);
         midgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.4f);
         playerLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);
         foregroundLayer.ScrollController = new LavaScrollController(player, 1.0f);
      }

      /// <summary>
      /// UnloadContent will be called once per game and is the place to unload
      /// game-specific content.
      /// </summary>
      protected override void UnloadContent()
      {
         // TODO: Unload any non ContentManager content here
      }

      /// <summary>
      /// Allows the game to run logic such as updating the world,
      /// checking for collisions, gathering input, and playing audio.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Update(GameTime gameTime)
      {
         if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

         // TODO: Add your update logic here
         player.Update(gameTime);

         base.Update(gameTime);
      }

      /// <summary>
      /// This is called when the game should draw itself.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Draw(GameTime gameTime)
      {
         GraphicsDevice.Clear(Color.CornflowerBlue);

         // TODO: Add your drawing code here
         spriteBatch.Begin();
         spriteBatch.End();

         base.Draw(gameTime);
      }
   }
}
