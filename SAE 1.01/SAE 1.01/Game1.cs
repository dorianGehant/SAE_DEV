using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SAE_1._01
{
    public class Game1 : Game
    {

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


            _map01 = new CreateurCarte("mapaTest", this);
            _longueurCase = (int)_map01.TailleCase().X;
            _hauteurCase = (int)_map01.TailleCase().Y;
            _nbLignesCarte = (int)_map01.TailleCarte().Y / _hauteurCase;
            _nbColonnesCarte = (int)_map01.TailleCarte().X / _longueurCase;


            _bordureCase = Content.Load<Texture2D>("contour_case");

            _tableauCases = new Case[_nbLignesCarte, _nbColonnesCarte];
            for (int i = 0; i < _nbLignesCarte; i++)
            {
                for (int j = 0; j < _nbColonnesCarte; j++)
                {
                    _tableauCases[i, j] = new Case(j, i, _bordureCase);
                }
            }

            _carte = new Carte(_nbColonnesCarte, _nbLignesCarte, _tableauCases);

            _texturePersonnage = Content.Load<Texture2D>("characters");
            _joueur1 = new Joueur(_texturePersonnage, "J1", _carte.TableauCases[5,5], 25, 3);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _etatClavier = Keyboard.GetState();
            _etatSouris = Mouse.GetState();

            if (_etatSouris.LeftButton == ButtonState.Pressed)
            {
                _joueur1.Deplacer(new Vector2(_etatSouris.Position.X/_longueurCase, _etatSouris.Position.Y / _hauteurCase), _carte);
            }

            if (_etatClavier.IsKeyDown(Keys.Right))
            {
                _joueur1.Deplacer(new Vector2(_joueur1.Position.X, _joueur1.Position.Y + 1), _carte);
            }

            _map01.MiseAJour(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here
            _map01.Dessiner();
            _spriteBatch.Begin();
            foreach (Case casecarte in _carte.TableauCases)
            {
                _spriteBatch.Draw(casecarte.Texture, new Vector2(casecarte.X * _longueurCase, casecarte.Y * _hauteurCase), Color.White);
            }
            _spriteBatch.Draw(_joueur1.Texture, new Vector2((_joueur1.Position.X * _longueurCase) - _joueur1.Texture.Width/2, (_joueur1.Position.Y * _hauteurCase) - _joueur1.Texture.Height/2), Color.White);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}