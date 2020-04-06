using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallaxStarter
{
   class LavaScrollController : IScrollController
   {
      Player player;

      public float ScrollRatio = 1.0f;

      public float Offset = 200;

      float radius = 7.0f;
      float elapsedTime = 0;
      double random;

      public Matrix Transform
      {
         get
         {
            float x = ScrollRatio * (Offset - player.Position.X);
            return Matrix.CreateTranslation(x, 525 + (float)(Math.Sin(elapsedTime)*random) * radius, 0);
         }
      }

      public void Update(GameTime gameTime)
      {
         elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
      }

      public LavaScrollController(Player player, float ratio)
      {
         this.player = player;
         this.ScrollRatio = ratio;
         random = (new Random().NextDouble() + .05) * 3.0;
      }
   }
}
