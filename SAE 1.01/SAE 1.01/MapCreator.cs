using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SAE_1._01
{
    internal class MapCreator : Game
{
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private GraphicsDevice _graphics;
        private GraphicsDeviceManager _graphicsManager;

        public MapCreator(string nomMap, Game1 game1)
        {
            Content = game1.Content;
            _graphics = game1.GraphicsDevice;
            _graphics.BlendState = BlendState.AlphaBlend;
            _graphicsManager = game1._graphics;
            GetContent(nomMap);
            
            
        }

        void GetContent(string nomMap)
        {
            _tiledMap = Content.Load<TiledMap>(nomMap);
            _tiledMapRenderer = new TiledMapRenderer(_graphics, _tiledMap);
        }

        public void DrawMap()
        {
            _tiledMapRenderer.Draw();
        }

        public void UpdateMap(GameTime gt)
        {
            _tiledMapRenderer.Update(gt);
        }

        public Vector2 GetMapSize()
        {
            return new Vector2(_tiledMap.WidthInPixels, _tiledMap.HeightInPixels);
        }

        void SetWindowToMapSize()
        {
            Vector2 size = this.GetMapSize();
            _graphicsManager.PreferredBackBufferWidth = (int)size.X;
            _graphicsManager.PreferredBackBufferHeight = (int)size.Y;
            _graphicsManager.ApplyChanges();
        }

      
      

    }
}
