﻿using System;
using Citysim.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Citysim.Map.Tiles;
using Microsoft.Xna.Framework.Content;

namespace Citysim.Views
{
    public class MapView : IView
    {
        private Texture2D hitbox;

        private int tileSize = 64;

        public void LoadContent(ContentManager content)
        {
            hitbox = content.Load<Texture2D>("hitbox");
        }

        public void Render(SpriteBatch spriteBatch, Citysim game, GameTime gameTime)
        {
            World world = game.city.world;

            int cameraX = (int)game.camera.position.X;
            int cameraY = (int)game.camera.position.Y;

            // Loop through tiles.
            for (int x = 0; x < world.width; x++)
            {
                for (int y = 0; y < world.height; y++)
                {
                    for (int z = 0; z < World.depth; z++)
                    {
                        ITile tile = game.tileRegistry.GetTile(world.tiles[x, y, z]);
                        if (tile == null)
                            continue; // unknown tile

                        Rectangle tileRect = new Rectangle(cameraX + x * tileSize, cameraY + y * tileSize, tileSize, tileSize);
                        
                        spriteBatch.Draw(tile.GetTexture(gameTime), tileRect, Color.White);
                    }
                }
            }

            if (world.InBounds(game.camera.hovering))
            {
                // Draw hitbox
                int hitX = (int)(game.camera.hovering.X * tileSize + game.camera.position.X);
                int hitY = (int)(game.camera.hovering.Y * tileSize + game.camera.position.Y);
                spriteBatch.Draw(hitbox, new Rectangle(hitX, hitY, tileSize, tileSize), Color.White);
            }
        }

        public void Update(Citysim game, GameTime gameTime)
        {
            
        }
    }
}
