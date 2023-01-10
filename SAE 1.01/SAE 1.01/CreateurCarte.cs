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

        public TiledMap Carte
        {
            get
            {
                return this.carte;
            }

            set
            {
                this.carte = value;
            }
        }

        public CreateurCarte(string nomMap, Game1 game1)
        {
            Content = game1.Content;
            graphiques = game1.GraphicsDevice;
            graphiques.BlendState = BlendState.AlphaBlend;
            manageurGraphiques = game1._graphics;

            //load de la carte
            Carte = Content.Load<TiledMap>(nomMap);
            //load des collissions
            CollisionLayer = Carte.GetLayer<TiledMapTileLayer>("Collision");
            carteAfficheur = new TiledMapRenderer(graphiques, Carte);
        }

        public void Dessiner()
        {
            carteAfficheur. Draw();
        }

        public void MiseAJour(GameTime gt)
        {
            carteAfficheur.Update(gt);
        }

        public Vector2 TailleCarte()
        {
            return new Vector2(Carte.WidthInPixels, Carte.HeightInPixels);
        }

        public Vector2 TailleCase()
        {
            return new Vector2(Carte.TileWidth, Carte.TileHeight);
        }

        public void AdapterFenetreAMap()
        {
            Vector2 size = this.TailleCarte();
            manageurGraphiques.PreferredBackBufferWidth = (int)size.X;
            manageurGraphiques.PreferredBackBufferHeight = (int)size.Y;
            manageurGraphiques.ApplyChanges();
        }

        public void RenseignerGame(Game1 game1)
        {
            Content = game1.Content;
            graphiques = game1.GraphicsDevice;
            graphiques.BlendState = BlendState.AlphaBlend;
            manageurGraphiques = game1._graphics;
        }

      
      

    }
}
