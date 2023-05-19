
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using System;
	using System.Collections.Generic;

	public class GameImplementation : Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		private IKeyboard monoGameKeyboard;
		private IKeyboard previousKeyboard;

		private IMouse monoGameMouse;
		private IMouse previousMouse;

		private IFrame<GameImage, GameFont, GameSound, GameMusic> frame;

		private ISoundOutput<GameSound> monoGameSoundOutput;
		private IMusic<GameMusic> monoGameMusic;
		private MonoGameDisplay monoGameDisplay;

		private bool debugMode;
		private int fps;
		private long numberOfElapsedTicks;

		private bool logAchievementsToConsole;
		private HashSet<string> loggedAchievements;

		private DisplayLogger displayLogger;
		private bool shouldRenderDisplayLogger;

		private long ticksForFpsCounter;
		private int fpsCounter;
		private int fpsCounterSnapshot;

		public GameImplementation(GlobalConfigurationManager.GlobalConfiguration globalConfiguration, IFileIO fileIO, bool logAchievementsToConsole)
		{
			bool debugMode = globalConfiguration.DebugMode;
			int fps = globalConfiguration.Fps;

			this.graphics = new GraphicsDeviceManager(this);

			this.Content.RootDirectory = "Content";
			this.IsMouseVisible = true;
			this.IsFixedTimeStep = false;
			this.Window.AllowUserResizing = false;

			this.monoGameKeyboard = new MonoGameKeyboard();
			this.previousKeyboard = new EmptyKeyboard();

			IDTLogger logger;
			if (debugMode)
			{
				this.displayLogger = new DisplayLogger(x: 5, y: 95);
				logger = this.displayLogger;
			}
			else
			{
				this.displayLogger = null;
				logger = new EmptyLogger();
			}

			this.shouldRenderDisplayLogger = true;

			GlobalState globalState = new GlobalState(
				fps: fps,
				rng: new DTRandom(),
				guidGenerator: new GuidGenerator("2342935728"),
				logger: logger,
				timer: new SimpleTimer(),
				fileIO: fileIO,
				buildType: BuildType.Desktop,
				debugMode: debugMode,
				useDebugAI: false,
				initialMusicVolume: null);

			this.monoGameMouse = new MonoGameMouse(windowHeight: GlobalConstants.WINDOW_HEIGHT);
			this.previousMouse = new EmptyMouse();

			this.frame = GameInitialization.GetFirstFrame(globalState: globalState);

			this.monoGameSoundOutput = new MonoGameSoundOutput(elapsedMicrosPerFrame: globalState.ElapsedMicrosPerFrame);
			this.monoGameMusic = new MonoGameMusic();

			this.debugMode = debugMode;
			this.fps = fps;
			this.numberOfElapsedTicks = 0;

			this.ticksForFpsCounter = 0;
			this.fpsCounter = 0;
			this.fpsCounterSnapshot = 0;

			this.logAchievementsToConsole = logAchievementsToConsole;
			this.loggedAchievements = new HashSet<string>();
		}

		protected override void Initialize()
		{
			// https://github.com/MonoGame/MonoGame/issues/7298
			this.graphics.PreferredBackBufferWidth = GlobalConstants.WINDOW_WIDTH;
			this.graphics.PreferredBackBufferHeight = GlobalConstants.WINDOW_HEIGHT;
			this.graphics.IsFullScreen = false;
			this.graphics.ApplyChanges();

			this.Window.Title = "Chess Comp Stomp with Hacks";

			base.Initialize();
		}

		protected override void LoadContent()
		{
			this.spriteBatch = new SpriteBatch(graphicsDevice: this.GraphicsDevice);
			this.monoGameDisplay = new MonoGameDisplay(contentManager: this.Content, spriteBatch: this.spriteBatch, windowHeight: GlobalConstants.WINDOW_HEIGHT);
		}

		protected override void Update(GameTime gameTime)
		{
			long elapsedTicksThisUpdate = gameTime.ElapsedGameTime.Ticks;
			this.numberOfElapsedTicks += elapsedTicksThisUpdate;

			long ticksPerFrame = 10 * 1000 * 1000 / this.fps;

			if (this.debugMode)
				this.ticksForFpsCounter += elapsedTicksThisUpdate;

			if (this.numberOfElapsedTicks >= ticksPerFrame)
			{
				this.numberOfElapsedTicks -= ticksPerFrame;

				if (this.numberOfElapsedTicks >= ticksPerFrame * 5)
					this.numberOfElapsedTicks = ticksPerFrame * 5;

				IKeyboard currentKeyboard = new CopiedKeyboard(this.monoGameKeyboard);
				IMouse currentMouse = new CopiedMouse(this.monoGameMouse);

				this.frame = this.frame.GetNextFrame(
					keyboardInput: currentKeyboard,
					mouseInput: currentMouse,
					previousKeyboardInput: this.previousKeyboard,
					previousMouseInput: this.previousMouse,
					displayProcessing: this.monoGameDisplay,
					soundOutput: this.monoGameSoundOutput,
					musicProcessing: this.monoGameMusic);

				if (this.frame == null)
					this.Exit();

				if (this.frame != null)
					this.frame.ProcessMusic();

				if (this.frame != null)
					this.monoGameSoundOutput.ProcessFrame();

				if (this.frame != null)
				{
					HashSet<string> completedAchievements = this.frame.GetCompletedAchievements();
					if (completedAchievements != null)
					{
						foreach (string completedAchievement in completedAchievements)
						{
							if (!this.loggedAchievements.Contains(completedAchievement))
							{
								this.loggedAchievements.Add(completedAchievement);
								if (this.logAchievementsToConsole)
									Console.WriteLine("Completed achievement [" + completedAchievement + "]");
							}
						}
					}
				}

				if (currentKeyboard.IsPressed(Key.L) && !this.previousKeyboard.IsPressed(Key.L))
					this.shouldRenderDisplayLogger = !this.shouldRenderDisplayLogger;

				this.previousKeyboard = currentKeyboard;
				this.previousMouse = currentMouse;

				if (this.debugMode)
				{
					this.fpsCounter += 1;

					if (this.ticksForFpsCounter >= 10 * 1000 * 1000)
					{
						this.ticksForFpsCounter -= 10 * 1000 * 1000;
						this.fpsCounterSnapshot = this.fpsCounter;
						this.fpsCounter = 0;

						if (this.ticksForFpsCounter >= 50 * 1000 * 1000)
							this.ticksForFpsCounter = 50 * 1000 * 1000;
					}
				}
			}
			else
			{
				long remainingTicksUntilNextFrame = ticksPerFrame - this.numberOfElapsedTicks;
				this.frame.ProcessExtraTime(milliseconds: (int)(remainingTicksUntilNextFrame / (10 * 1000)));
			}

			if (this.frame == null)
			{
				this.monoGameSoundOutput.DisposeSounds();
				this.monoGameMusic.DisposeMusic();
				this.monoGameDisplay.DisposeImages();
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			this.GraphicsDevice.Clear(Color.White);

			this.spriteBatch.Begin();

			if (this.frame != null)
			{
				this.frame.Render(display: this.monoGameDisplay);
				this.frame.RenderMusic(musicOutput: this.monoGameMusic);
				if (this.displayLogger != null && this.shouldRenderDisplayLogger)
				{
					this.displayLogger.Render(displayOutput: this.monoGameDisplay, font: GameFont.GameFont14Pt, color: DTColor.Black());
				}

				if (this.debugMode)
				{
					if (this.monoGameDisplay.HasFinishedLoading())
					{
						this.monoGameDisplay.DrawText(
							x: 10,
							y: GlobalConstants.WINDOW_HEIGHT - 10,
							text: "fps: " + this.fpsCounterSnapshot.ToStringCultureInvariant(),
							font: GameFont.GameFont14Pt,
							color: DTColor.Black());
					}
				}
			}

			this.spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
