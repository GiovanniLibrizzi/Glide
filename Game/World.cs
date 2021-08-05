using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
//using static Glide.Content.Util;
using Newtonsoft.Json;
using static Microsoft.Xna.Framework.Content.ContentManager;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace Glide.Content {
    class World {
        public List<Entity> scene;

        private string levelFile;

        private Texture2D tileset;
        private Vector2Int worldSize, tileSize;
        private List<int> worldData = new List<int>();

        ContentManager content;

        public World (string levelFile, ContentManager content) {
            this.levelFile = levelFile;
            this.content = content;
            loadFromJson();
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



                // Get world tile data
                foreach (int d in layers.data) {
                    worldData.Add(d);
                }

            }
        }




    }
}
