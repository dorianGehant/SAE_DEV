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
        
        const int LONGUEUR_CASE = 30, HAUTEUR_CASE = 30,TAILLE_CASE = 32;
        const int HAUTEUR_FENETRE = 960;
        const int LARGEUR_FENETRE = 960;

        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseState _etatSouris;
        int xSouris;
        int ySouris;
        int xSourisPrecedent;
        int ySourisPrecedent;

        private KeyboardState _etatClavier;

        private Case[,] _tableauCases;
        private Carte _carte;
        private int _nbLignesCarte;
        private int _nbColonnesCarte;
        private int _longueurCase;
        private int _hauteurCase;

        private CreateurCarte _map01;
        private Texture2D _bordureCase;
        private Texture2D _bordureCasePossible;
        private Texture2D _bordureCaseSelectionne;
        private Texture2D _bordureSortPossible;

        private Joueur _joueur1;
        private Texture2D _texturePersonnage;
        private Texture2D _textureSelectionne;
        Texture2D ancienneTexture;
        Carte cases;
        private List<Sort> sortsBaseJoueurs = new List<Sort>();
        private List<Sort> sortsEnnemi = new List<Sort>();
        Joueur j1;
        Joueur j2;
        Ennemi ennemi;
        GameManager gameManager;
        SpriteFont _font;
        bool KeyPressedSpace = false; 
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
            _bordureSortPossible = Content.Load<Texture2D>("bordureSortLancable");
            _bordureCaseSelectionne = Content.Load<Texture2D>("bordureCaseSelectionne");
            _textureSelectionne = Content.Load<Texture2D>("New Piskel-1");
            _texturePersonnage = Content.Load<Texture2D>("perso");
            _font = Content.Load<SpriteFont>("Font");


            //creation des objets utiles
            _map01 = new CreateurCarte("mapPrincipale", this);
            cases = new Carte(LONGUEUR_CASE, HAUTEUR_CASE, TAILLE_CASE, _bordureCase,_map01);
            
            gameManager = new GameManager(_bordureCasePossible);

            sortsBaseJoueurs.Add(new SortMonocible("attaqueCAC", -6, 1, 1, effetSort.MODIF_PV));
            sortsBaseJoueurs.Add(new SortMonocible("degatDistance", -4, 4, 2, effetSort.MODIF_PV));
            sortsBaseJoueurs.Add(new SortMonocible("soinDistance", 2, 4, 2, effetSort.MODIF_PV));
            sortsEnnemi.Add(new SortMonocible("ennemiattaque", -4, 1, 1, effetSort.MODIF_PV));
            j1 = new Joueur(spriteSheet, "j1", cases.TableauCases[5, 5], 8, 7, 3, 5, cases, sortsBaseJoueurs, gameManager);
            j2 = new Joueur(spriteSheet, "j2", cases.TableauCases[10, 5], 8, 7, 3, 5, cases, sortsBaseJoueurs, gameManager);
            gameManager.AjouterCombattant(j1);
            gameManager.AjouterCombattant(j2);
            ennemi = new Ennemi(spriteSheet, "e1", cases.TableauCases[5, 5],7, 3, 2, 2, cases, sortsEnnemi, gameManager);
            gameManager.AjouterCombattant(ennemi);

            ancienneTexture = _bordureCase;

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
            if (xSouris >= 0 && xSouris < LARGEUR_FENETRE && ySouris >= 0 && ySouris < HAUTEUR_FENETRE)
            {
                xSourisPrecedent = xSouris;
                ySourisPrecedent = ySouris;

            }
            xSouris = (_etatSouris.X - Window.Position.X) / TAILLE_CASE;
            ySouris = (_etatSouris.Y + - Window.Position.Y) / TAILLE_CASE;

            Entite jouable = gameManager.GetEntiteTour();
            
            //verification si pas d'attente
            if (jouable.Jouable)
            {
                jouable.Possible(_bordureCasePossible);

                //on verifie si la souris se trouve bien sur une case


                    //verification clique
                    if (simulation.IsMouseDown(InputMouseButtons.Left) && mouseClick == false && jouable.clicDansZonePossible(cases.TableauCases[xSouris, ySouris]))
                    {
                        ancienneTexture = _bordureCase;
                        //On bouge le joueur ou on clique

                        jouable.enleverPossible(_bordureCase);
                        Console.WriteLine("cases souris " + ySouris + "   " + xSouris);
                        Console.WriteLine("case jouable " + jouable.Position.Y + " " + jouable.Position.X);
                        jouable.PlayAnim("Walk");
                        jouable.Chemin_A_Star(cases.TableauCases[jouable.Position.Y, jouable.Position.X], cases.TableauCases[ySouris, xSouris]);
                        Console.WriteLine(jouable.Grille.TableauCases[jouable.Position.X, jouable.Position.Y].Collision);
                    }

                if (simulationKey.IsKeyDown(InputKeys.Space) && KeyPressedSpace == false)
                {
                    jouable.enleverPossible(_bordureCase);
                    gameManager.ProchaineEntite();
                }

                if (simulationKey.IsKeyDown(InputKeys.A) && jouable.PointAction >= jouable.Sorts[0].Cout)
                {
                    jouable.SortEnLancement = jouable.Sorts[0];
                    jouable.Jouable = false;
                    jouable.PlayAnim("attaque");
                }

                if (simulationKey.IsKeyDown(InputKeys.Z) && jouable.PointAction >= jouable.Sorts[1].Cout)
                {
                    jouable.SortEnLancement = jouable.Sorts[1];
                    jouable.Jouable = false;
                    jouable.PlayAnim("Magic");
                }

                if (simulationKey.IsKeyDown(InputKeys.E) && jouable.PointAction >= jouable.Sorts[2].Cout)
                {
                    jouable.SortEnLancement = jouable.Sorts[2];
                    jouable.Jouable = false;
                    jouable.PlayAnim("Magic");
                }
            }
            else
            {
                if (jouable.SortEnLancement != null)
                {
                    jouable.enleverPossible(_bordureCase);
                    ancienneTexture = _bordureCase;
                    jouable.Possible(_bordureSortPossible, jouable.SortEnLancement);

                    if (simulation.IsMouseDown(InputMouseButtons.Left) && mouseClick == false && jouable.clicDansZonePossible(cases.TableauCases[xSouris, ySouris]))
                    {
                        jouable.SortEnLancement.Lancer(cases.TableauCases[xSouris, ySouris], jouable, gameManager.EntitesCombat);
                        cases.resetTextureCases(_bordureCase);
                        ancienneTexture = _bordureCase;
                        jouable.SortEnLancement = null;
                        jouable.PlayAnim("Idle");
                    }

                    if (simulationKey.IsKeyDown(InputKeys.Escape))
                    {
                        jouable.SortEnLancement = null;
                        cases.resetTextureCases(_bordureCase);
                    }
                }
                else
                {
                    jouable.MoveChemin(deltaSeconds);
                }
            }

            if (xSouris >= 0 && xSouris < LONGUEUR_CASE && ySouris >= 0 && ySouris < HAUTEUR_CASE && xSourisPrecedent >= 0 && xSourisPrecedent < LONGUEUR_CASE && ySourisPrecedent >= 0 && ySourisPrecedent < HAUTEUR_CASE)
            {
                cases.TableauCases[xSourisPrecedent, ySourisPrecedent].Texture = ancienneTexture;
                ancienneTexture = cases.TableauCases[xSouris, ySouris].Texture;
                cases.TableauCases[xSouris, ySouris].Texture = _bordureCaseSelectionne;
            }

            KeyPressedSpace = simulationKey.IsKeyDown(InputKeys.Space);
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
            foreach(Entite entite in gameManager.EntitesCombat)
            {
                entite.Afficher(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }


    }
    
}