using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TileEngine;

namespace BountyHunter
{
	public class Enemy : GameObject
	{
		private Vector2 fallSpeed = new Vector2(0, 20);
		private float walkSpeed = 20.0f;
		private bool facingLeft = true;
		public bool Dead = false;

		#region Constructor
		public Enemy (ContentManager Content, int cellX, int cellY)
		{
			animations.Add ("run", new AnimationStrip (Content.Load<Texture2D> (@"monsterRun"),
			                                          32, "run"));
			animations ["run"].FrameLength = 0.1f;
			animations ["run"].LoopAnimation = true;
			animations.Add ("die", new AnimationStrip (Content.Load <Texture2D> (@"monsterDie"),
			                                          32, "die"));
			animations ["die"].LoopAnimation = false;
			frameWidth = 32;
			frameHeight = 32;
			collisionRectangle = new Rectangle (6, 0, 21, 16);
			worldLocation = new Vector2 (cellX * TileMap.TileWidth, cellY * TileMap.TileHeight);
			enabled = true;
			codeBasedBlocks = true;
			PlayAnimation ("run");
		}
		#endregion

		#region Public Methods
		public override void Update(GameTime gameTime){
			Vector2 oldLocation = worldLocation;
			if(!Dead){
				velocity = new Vector2 (0, velocity.Y);
				Vector2 direction = new Vector2 (1, 0);
				flipped = true;
				if(facingLeft){
					direction = new Vector2 (-1, 0);
					flipped = false;
				}
				direction *= walkSpeed;
				velocity += direction;
				velocity += fallSpeed;
			}
			base.Update (gameTime);
			if(!Dead){
				if(oldLocation == worldLocation){
					facingLeft = !facingLeft;
				}
			}else{
				if(animations[currentAnimation].FinishedPlaying){
					enabled = false;
				}
			}
		}
		#endregion
	}
}

