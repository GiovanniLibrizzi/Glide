using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
//using static Glide.Content.Util;
using Newtonsoft.Json;
using static Microsoft.Xna.Framework.Content.ContentManager;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Glide.Content.Entities;

namespace Glide.Content {
    class World {
        public List<Entity> scene = new List<Entity>();

        private string levelFile;

        private Texture2D tileset;
        public Vector2Int worldSize;
        private Vector2Int tileSize, worldGridSize, tilesetSize;
        private List<int> worldData = new List<int>();

        private List<int> collisionData = new List<int>() { 1, 2, 3, 4, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };

        ContentManager content;

        enum Layer {  
            tileMain
        }

        public World (string levelFile, ContentManager content) {
            this.levelFile = levelFile;
            this.content = content;
            loadFromJson();
            tilesetSize.x = tileset.Width / tileSize.x;
            tilesetSize.y = tileset.Height / tileSize.y;

            addCollision(0);
        }

        private void loadFromJson() {
            using (StreamReader r = new StreamReader("content/"+levelFile)) {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);

                // Get World Size
                worldSize.x = array.width;
                worldSize.y = array.height;

                // Enter layers (hard coded at one layer for now)
                dynamic layers = array.layers[0];

                // Get tileset 
                string name = layers.tileset;   
                tileset = content.Load<Texture2D>(name);

                // Get Tile Size
                tileSize.x = layers.gridCellWidth;
                tileSize.y = layers.gridCellHeight;

                // Get world grid size
                worldGridSize.x = layers.gridCellsX;
                worldGridSize.y = layers.gridCellsY;

                // Get world tile data
                foreach (int d in layers.data) {
                    worldData.Add(d);
                }

            }
        }


        public void drawLevel(int layer, SpriteBatch spriteBatch) {
            for (int i = 0; i < worldData.Count; i++) {
                int tileID = worldData[i];

                if (tileID != -1) {
                    int column = tileID % tilesetSize.x;
                    int row = tileID / tilesetSize.x;

                    float x = (i % worldGridSize.x) * tileSize.x;
                    float y = (float)Math.Floor(i / (double)worldGridSize.x) * tileSize.y;
                    //System.Diagnostics.Debug.WriteLine(i + ": tileID: " + tileID + " | column: " + column + " | row: " + row + " | x: " + x + " | y: " + y + " | worldGridSize.x: " + worldGridSize.x);

                    Rectangle tilesetRect = new Rectangle(tileSize.x * column, tileSize.y * row, tileSize.x, tileSize.y);

                    Rectangle tilePosition = new Rectangle((int)x, (int)y, tileSize.x, tileSize.y);
                    spriteBatch.Draw(tileset, tilePosition, tilesetRect, Color.White);

                    

                }
            }
        }

        public void addCollision(int layer) {
            Texture2D tCollision = content.Load<Texture2D>("Collision");

            for (int i = 0; i < worldData.Count; i++) {
                int tileID = worldData[i];

                if (tileID != -1) {
                    int column = tileID % tilesetSize.x;
                    int row = tileID / tilesetSize.x;

                    float x = (i % worldGridSize.x) * tileSize.x;
                    float y = (float)Math.Floor(i / (double)worldGridSize.x) * tileSize.y;
                    //System.Diagnostics.Debug.WriteLine(i + ": tileID: " + tileID + " | column: " + column + " | row: " + row + " | x: " + x + " | y: " + y + " | worldGridSize.x: " + worldGridSize.x);

                    Rectangle tilePosition = new Rectangle((int)x, (int)y, tileSize.x, tileSize.y);

                    //if (collisionData.Contains(tileID)) {
                        scene.Add(new Collision(new Vector2(x, y), new Vector2Int(tileSize.x, tileSize.y), this));
                        //scene.Add(new NormalTile(tCollision, new Vector2((int)x, (int)y), this));
                    //}

                }
            }
        }



    }
}
