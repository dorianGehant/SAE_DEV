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
using System.Collections.Generic;

namespace SAE_1._01
{
    public class Game1 : Game
    {

        const int LONGUEUR_CASE = 30, HAUTEUR_CASE = 30, TAILLE_CASE = 32;
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
        public Texture2D _bordureCasePossible;

        private Joueur _joueur1;
        private Texture2D _texturePersonnage;
        private Texture2D _textureSelectionne;
        Case selectionne;
        Carte cases;
        Joueur j1;
        Joueur j2;
        Ennemi ennemi;
        GameManager gameManager;
        SpriteFont _font;
        bool KeyPressedE = false;
        bool mouseClick = false;
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
            _bordureCasePossible = Content.Load<Texture2D>("case_proposer");
            _textureSelectionne = Content.Load<Texture2D>("New Piskel-1");
            _texturePersonnage = Content.Load<Texture2D>("perso");
            _font = Content.Load<SpriteFont>("Font");


            //creation des objets utiles
            _map01 = new CreateurCarte("mapPrincipale", this);
            selectionne = new Case(-100, -100, _textureSelectionne, _map01);
            cases = new Carte(LONGUEUR_CASE, HAUTEUR_CASE, TAILLE_CASE, _bordureCase,_map01);
            
            gameManager = new GameManager(_bordureCasePossible);

            j1 = new Joueur(spriteSheet, "j1", cases.TableauCases[5, 5], 1, 7, cases, gameManager);
            j2 = new Joueur(spriteSheet, "j2", cases.TableauCases[10, 5], 1, 7, cases, gameManager);
            gameManager.AjouterCombattant(j1);
            gameManager.AjouterCombattant(j2);
            ennemi = new Ennemi(spriteSheet, "e1", cases.TableauCases[5, 5], 1, 5, cases, gameManager);
            gameManager.AjouterCombattant(ennemi);



            //valeur des tailles 
            _longueurCase = (int)_map01.TailleCase().X;
            _hauteurCase = (int)_map01.TailleCase().Y;
            _nbLignesCarte = (int)_map01.TailleCarte().Y / _hauteurCase;
            _nbColonnesCarte = (int)_map01.TailleCarte().X / _longueurCase;

            gameManager.CommencerJeu();


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //les inputs
            var simulation = Inputs.Use<IMouseSimulation>();
            var simulationKey = Inputs.Use<IKeyboardSimulation>();
            _etatClavier = Keyboard.GetState();
            _etatSouris = Mouse.GetState();


            //positions souris
            int x = (_etatSouris.X - Window.Position.X) / TAILLE_CASE;
            int y = (_etatSouris.Y + -Window.Position.Y) / TAILLE_CASE;

            Entite jouable = gameManager.GetEntiteTour();
            
            //verification si pas d'attente
            if (jouable.jouable)
            {
                jouable.Possible(_bordureCasePossible);

                //on met le carre bleu sur la case ou il y a la souris
                selectionne.X = x;
                selectionne.Y = y ;

                //on verifie si la souris se trouve bien sur une case
                if (x >= 0 && x < LONGUEUR_CASE && y >= 0 && y < HAUTEUR_CASE)
                    {
                    //verification clique
                    //Console.WriteLine(jouable.Nom);
                    if (simulation.IsMouseDown(InputMouseButtons.Left) && mouseClick == false && jouable.clicDansZonePossible(cases.TableauCases[x, y]))
                    {
                        //On bouge le joueur ou on clique

                        jouable.enleverPossible(_bordureCase);
                        jouable.Chemin_A_Star(cases.TableauCases[jouable.Position.Y, jouable.Position.X], cases.TableauCases[y, x]);

                    }
                }
                if (simulationKey.IsKeyDown(InputKeys.E) && KeyPressedE == false)
                {
                    jouable.enleverPossible(_bordureCase);
                    gameManager.ProchaineEntite();

                }
            }
            else
            {
                jouable.MoveChemin(deltaSeconds);
            }
            KeyPressedE = simulationKey.IsKeyDown(InputKeys.E);
            mouseClick = simulation.IsMouseDown(InputMouseButtons.Left);
            //mise a jour / update
            j1.UpdateAnim(deltaSeconds);
            j2.UpdateAnim(deltaSeconds);
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
            _spriteBatch.Draw(selectionne.Texture, new Vector2(selectionne.X * TAILLE_CASE, selectionne.Y * TAILLE_CASE), Color.White);
            j1.Afficher(_spriteBatch);
            j2.Afficher(_spriteBatch);
            ennemi.Afficher(_spriteBatch);
            _spriteBatch.DrawString(_font, gameManager.GetIndexTurn().ToString(), new Vector2(100, 100), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


    }
    
}