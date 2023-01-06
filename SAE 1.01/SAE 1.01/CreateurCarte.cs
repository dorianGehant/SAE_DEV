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
    internal class CreateurCarte : Game
{
        private TiledMap carte;
        private TiledMapRenderer carteAfficheur;
        private GraphicsDevice graphiques;
        private GraphicsDeviceManager manageurGraphiques;
        private TiledMapTileLayer collisionLayer;

        public TiledMapTileLayer CollisionLayer
        {
            get
            {
                return this.collisionLayer;
            }

            set
            {
                this.collisionLayer = value;
            }
        }

        public CreateurCarte(string nomMap, Game1 game1)
        {
            Content = game1.Content;
            graphiques = game1.GraphicsDevice;
            graphiques.BlendState = BlendState.AlphaBlend;
            manageurGraphiques = game1._graphics;

            //lad de la carte
            carte = Content.Load<TiledMap>(nomMap);
            //load des collissions
            CollisionLayer = carte.GetLayer<TiledMapTileLayer>("collisionable");
            carteAfficheur = new TiledMapRenderer(graphiques, carte);
        }

        public void Dessiner()
        {
            carteAfficheur.Draw();
        }

        public void MiseAJour(GameTime gt)
        {
            carteAfficheur.Update(gt);
        }

        public Vector2 TailleCarte()
        {
            return new Vector2(carte.WidthInPixels, carte.HeightInPixels);
        }

        public Vector2 TailleCase()
        {
            return new Vector2(carte.TileWidth, carte.TileHeight);
        }

        public void AdapterFenetreAMap()
        {
            Vector2 size = this.TailleCarte();
            manageurGraphiques.PreferredBackBufferWidth = (int)size.X;
            manageurGraphiques.PreferredBackBufferHeight = (int)size.Y;
            manageurGraphiques.ApplyChanges();
        }

      
      

    }
}
