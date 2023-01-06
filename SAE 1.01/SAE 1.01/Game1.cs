using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Input;
using System.Diagnostics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Tiled;

namespace SAE_1._01
{
    public class Game1 : Game
    {
        
        const int LONGUEUR_CASE = 19, HAUTEUR_CASE = 13,TAILLE_CASE = 25;
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseState _etatSouris;
        private KeyboardState _etatClavier;

        private Case[,] _tableauCases;
        private Carte _carte;
        private int _nbLignesCarte;
        private int _nbColonnesCarte;
        private int _longueurCase;
        private int _hauteurCase;

        private CreateurCarte _map01;
        private Texture2D _bordureCase;

        private Joueur _joueur1;
        private Texture2D _texturePersonnage;
        private Texture2D _textureSelectionne;
        Case selectionne;
        Carte cases;
        Joueur j1;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //On load les differents elements
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new JsonContentLoader());
            _bordureCase = Content.Load<Texture2D>("contour_case");
            _textureSelectionne = Content.Load<Texture2D>("New Piskel-1");
            _texturePersonnage = Content.Load<Texture2D>("perso");


            //creation des objets utiles
            _map01 = new CreateurCarte("mapaTest", this);
            selectionne = new Case(15, 15, _textureSelectionne, _map01);
            cases = new Carte(LONGUEUR_CASE, HAUTEUR_CASE, TAILLE_CASE, _bordureCase, _map01);
            j1 = new Joueur(spriteSheet, "j1", cases.TableauCases[0, 0], 1, 1);

            // valeur des tailles
            _longueurCase = (int)_map01.TailleCase().X;
            _hauteurCase = (int)_map01.TailleCase().Y;
            _nbLignesCarte = (int)_map01.TailleCarte().Y / _hauteurCase;
            _nbColonnesCarte = (int)_map01.TailleCarte().X / _longueurCase;



        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //les inputs
            var simulation = Inputs.Use<IMouseSimulation>();
            _etatClavier = Keyboard.GetState();
            _etatSouris = Mouse.GetState();

            //positions souris
            int x = (_etatSouris.X - Window.Position.X) / TAILLE_CASE;
            int y = (_etatSouris.Y + - Window.Position.Y) / TAILLE_CASE;

            //on verifie si la souris se trouve bien sur une case
            if(x >= 0 && x < LONGUEUR_CASE && y >= 0 && y < HAUTEUR_CASE)
            {
                //on met le carre bleu sur la case ou il y a la souris
                selectionne.X = x * TAILLE_CASE;
                selectionne.Y = y * TAILLE_CASE;

                //verification clique
                if (simulation.IsMouseDown(InputMouseButtons.Left))
                {
                    //On bouge le joueur ou on clique
                    j1.MovePlayer(cases.TableauCases[x,y]);
                }
            }    

            //mise a jour / update
            j1.UpdateAnim(deltaSeconds);
            _map01.MiseAJour(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _map01.Dessiner();
            cases.AfficherMap(_spriteBatch);
            _spriteBatch.Draw(selectionne.Texture, new Vector2(selectionne.X, selectionne.Y), Color.White);
            j1.Afficher(_spriteBatch);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }



    }
}