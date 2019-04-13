using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;

namespace Nave
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static SkyBox skyBox;

        ResourceManager resourceManager;

        public NaveModel NaveModel { get; set; } = new NaveModel();

        //Criação de Matrizes para desenhar o modelo

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Camera.Initialize(graphics.GraphicsDevice);
            InputInterpeter.Initialize(graphics.GraphicsDevice);
            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            resourceManager = new ResourceManager(Content);

            //Load Skybox
            skyBox = new SkyBox(graphics.GraphicsDevice);
            skyBox.Textures[0] = Content.Load<Texture2D>("skybox/front");
            skyBox.Textures[1] = Content.Load<Texture2D>("skybox/back");
            skyBox.Textures[2] = Content.Load<Texture2D>("skybox/bottom");
            skyBox.Textures[3] = Content.Load<Texture2D>("skybox/top");
            skyBox.Textures[4] = Content.Load<Texture2D>("skybox/left");
            skyBox.Textures[5] = Content.Load<Texture2D>("skybox/right");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            InputInterpeter.Update(gameTime, graphics.GraphicsDevice);
            Camera.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //Frustum - Se a nave estiver contida na área visivel pela câmara, desenha a nave
            if (Camera.frustum.Contains(NaveModel.BoundingSphere) != ContainmentType.Disjoint)
            {
                resourceManager.Pool.Draw();
            }
            //Fazer Draw às naves da NavePool

            base.Draw(gameTime);
        }

        
    }
}
