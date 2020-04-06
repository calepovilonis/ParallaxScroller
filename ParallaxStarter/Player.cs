using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace ParallaxStarter
{
   public class Player : ISprite
   {
      /// <summary>
      /// A spritesheet containing a helicopter image
      /// </summary>
      Texture2D spritesheet;

      /// <summary>
      /// The portion of the spritesheet that is the helicopter
      /// </summary>
      Rectangle sourceRect = new Rectangle
      {
         X = 0,
         Y = 0,
         Width = 400,
         Height = 400
      };

      /// <summary>
      /// The origin of the helicopter sprite
      /// </summary>
      Vector2 origin = new Vector2(0, 0);


      SpriteEffects effects = SpriteEffects.None;
      /// <summary>
      /// The angle the helicopter should tilt
      /// </summary>
      float angle = 0;

      /// <summary>
      /// The player's position in the world
      /// </summary>
      public Vector2 Position { get; set; }

      /// <summary>
      /// How fast the player moves
      /// </summary>
      public float Speed { get; set; } = 100;

      /// <summary>
      /// Constructs a player
      /// </summary>
      /// <param name="spritesheet">The player's spritesheet</param>
      public Player(Texture2D spritesheet)
      {
         this.spritesheet = spritesheet;
         this.Position = new Vector2(200, 350);
      }

      /// <summary>
      /// Updates the player position based on GamePad or Keyboard input
      /// </summary>
      /// <param name="gameTime">The GameTime object</param>
      public void Update(GameTime gameTime)
      {
         Vector2 direction = Vector2.Zero;

         // Use GamePad for input
         var gamePad = GamePad.GetState(0);

         // The thumbstick value is a vector2 with X & Y between [-1f and 1f] and 0 if no GamePad is available
         direction.X = gamePad.ThumbSticks.Left.X;

         // We need to inverty the Y axis
         direction.Y = -gamePad.ThumbSticks.Left.Y;

         // Override with keyboard input
         var keyboard = Keyboard.GetState();
         if (keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.A))
         {
            if (this.Position.X > 200)
            {
               effects = SpriteEffects.FlipHorizontally;
               direction.X -= 1;
            }
            else Position = new Vector2(200, 350);
         }
         if (keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.D))
         {
            if (this.Position.X < 3000)
            {
               effects = SpriteEffects.None;
               direction.X += 1;
            }
            else Position = new Vector2(3000, 350);
         }

         // Caclulate the tilt of the helicopter
         angle = 0.5f * direction.X;

         // Move the helicopter
         Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Speed * direction;
      }

      /// <summary>
      /// Draws the player sprite
      /// </summary>
      /// <param name="spriteBatch"></param>
      public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
      {
         // Render the helicopter, rotating about the rotors
         spriteBatch.Draw(spritesheet, Position, sourceRect, Color.White, 0, origin, 1f, effects, 0.7f);
      }

   }
}
