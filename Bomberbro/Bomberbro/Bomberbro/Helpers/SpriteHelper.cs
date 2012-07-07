#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
#endregion

namespace Bomberbro.Helpers
{
	/// <summary>
	/// Sprite helper class to manage and render sprites.
	/// </summary>
	public class SpriteHelper
	{
		#region SpriteToRender helper class
		class SpriteToRender
		{
			public Texture2D texture;
			public Rectangle rect;
			public Rectangle? sourceRect;
			public Color color;

			public SpriteToRender(Texture2D setTexture, Rectangle setRect,
				Rectangle? setSourceRect, Color setColor)
			{
				texture = setTexture;
				rect = setRect;
				sourceRect = setSourceRect;
				color = setColor;
			} // SpriteToRender(setTexture, setRect, setColor)
		} // SpriteToRender
		#endregion

		#region Variables
		/// <summary>
		/// Keep a list of all sprites we have to render this frame.
		/// </summary>
		static List<SpriteToRender> sprites =
			new List<SpriteToRender>();
		/// <summary>
		/// Sprite batch for rendering
		/// </summary>
		static SpriteBatch spriteBatch = null;

		/// <summary>
		/// Texture for this sprite
		/// </summary>
		Texture2D texture;
		/// <summary>
		/// Graphic rectangle used for this sprite inside the texture.
		/// Can be null to use the whole texture.
		/// </summary>
		Rectangle gfxRect;
		#endregion

		#region Constructor
		public SpriteHelper(Texture2D setTexture, Rectangle? setGfxRect)
		{
			texture = setTexture;
			if (setGfxRect == null)
				gfxRect = new Rectangle(0, 0, texture.Width, texture.Height);
			else
				gfxRect = setGfxRect.Value;
		} // SpriteHelper(setTexture, setGfxRect)
		#endregion

		#region Dispose
		/// <summary>
		/// Dispose the static spriteBatch and sprites helpers in case
		/// the device gets lost.
		/// </summary>
		public static void Dispose()
		{
			sprites.Clear();
			if (spriteBatch != null)
				spriteBatch.Dispose();
			spriteBatch = null;
		} // Dispose()
		#endregion

		#region RenderSprite
		public void Render(Rectangle rect, Color color)
		{
			sprites.Add(new SpriteToRender(texture, rect, gfxRect, color));
		} // Render(texture, rect, sourceRect, color)

		public void Render(Rectangle rect)
		{
			Render(rect, Color.White);
		} // Render(texture, rect, sourceRect)

		public void Render(int x, int y, Color color)
		{
			Render(new Rectangle(x, y, gfxRect.Width, gfxRect.Height), color);
		} // Render(texture, rect, sourceRect)

		public void Render(int x, int y)
		{
			Render(new Rectangle(x, y, gfxRect.Width, gfxRect.Height));
		} // Render(texture, rect, sourceRect)

		public void Render()
		{
			Render(new Rectangle(0, 0, 1024, 768));
		} // Render(texture)

		public void RenderCentered(float x, float y, float scale)
		{
			Render(new Rectangle(
				(int)(x * 1024 - scale * gfxRect.Width/2),
				(int)(y * 768 - scale * gfxRect.Height/2),
				(int)(scale * gfxRect.Width),
				(int)(scale * gfxRect.Height)));
		} // RenderCentered(x, y)

		public void RenderCentered(float x, float y)
		{
			RenderCentered(x, y, 1);
		} // RenderCentered(x, y)

		public void RenderCentered(Vector2 pos)
		{
			RenderCentered(pos.X, pos.Y);
		} // RenderCentered(pos)
		#endregion

		#region DrawSprites
		public static void DrawSprites(int width, int height)
		{
			// No need to render if we got no sprites this frame
			if (sprites.Count == 0)
				return;

			// Create sprite batch if we have not done it yet.
			// Use device from texture to create the sprite batch.
			if (spriteBatch == null)
				spriteBatch = new SpriteBatch(sprites[0].texture.GraphicsDevice);

			// Start rendering sprites
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

			// Render all sprites
			foreach (SpriteToRender sprite in sprites)
				spriteBatch.Draw(sprite.texture,
					// Rescale to fit resolution
					new Rectangle(
					sprite.rect.X * width / 1024,
					sprite.rect.Y * height / 768,
					sprite.rect.Width * width / 1024,
					sprite.rect.Height * height / 768),
					sprite.sourceRect, sprite.color);

			// We are done, draw everything on screen with help of the end method.
			spriteBatch.End();

			// Kill list of remembered sprites
			sprites.Clear();
		} // DrawSprites()
		#endregion
	} // class SpriteHelper
} // namespace XnaBreakout
