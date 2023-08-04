using Tilengine;
using static SDL2.SDL;

namespace Game
{
    public class Test
    {
        private static void KeepAlive()
        {
            SDL_Event e;
            while (SDL_WaitEvent(out e) != 0)
            {
                if (e.type == SDL_EventType.SDL_QUIT)
                {
                    break;
                }
            }
        }
        private static Tilemap foreground;
        public static void Main(string[] args)
        {
            // if it has a number in its argument, directly switch to that test
            if (args.Length == 0 || !int.TryParse(args[0], out int test))
            {
                Console.WriteLine("Which test do you want to run?");
                Console.WriteLine("0: Standard");
                Console.WriteLine("1: Delayed");
                Console.WriteLine("2: Managed");
                Console.WriteLine("3: Legacy");
                Console.WriteLine("4: All");
                test = Console.ReadKey().KeyChar - '0';
                Console.Write("\b \b");
            }
            try
            {
                switch (test)
                {
                    case 0:
                        MainStandard();
                        break;
                    case 1:
                        MainDelayed();
                        break;
                    case 2:
                        MainManaged();
                        break;
                    case 3:
                        MainLegacy();
                        break;
                    case 4:
                        Thread thread0 = new(MainStandard);
                        Thread thread1 = new(MainDelayed);
                        Thread thread2 = new(MainManaged);
                        Thread thread3 = new(MainLegacy);
                        thread0.Start();
                        thread1.Start();
                        thread2.Start();
                        thread3.Start();
                        break;
                    default:
                        throw new NotImplementedException("Test "+test+" does not exist");
                }
                KeepAlive();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: "+e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
            }
        }
        public static void MainStandard()
        {
            var engineargs = new EngineArgs(384, 216, loadPath: "assets");
            var window = new Window(engineargs, WindowFlags.NOVSYNC|WindowFlags.S1);

            foreground = new("Sonic_md_fg1.tmx");

            Bitmap bitmap = new(256, 256, 8);
            {
                Palette palette = new(16);
            }

            window.Engine.Layers[0].Bitmap = bitmap;
            window.Engine.Layers[0].Bitmap.Palette.SetColor(1, 255, 0, 0);
            window.Engine.Layers[0].Bitmap = bitmap;

            Console.WriteLine(window.Engine.LastError);

            foreground.SetTileAttribute(23, 0, flipy: true);
            window.BeforeFrame += BeforeFrame;
            window.AfterFrame += AfterFrame;
        }
        public static void MainDelayed()
        {
            var engineargs = new EngineArgs(384, 216, loadPath: "assets");
            var window = new Window(engineargs, WindowFlags.NOVSYNC|WindowFlags.S1, autostart: false);

            foreground = new("Sonic_md_fg1.tmx");
            Bitmap bitmap = new(256, 256, 8);
            {
                Palette palette = new(16);
            }
            window.Engine.Layers[0].Bitmap = bitmap;
            window.Engine.Layers[0].Bitmap.Palette.SetColor(1, 255, 0, 0);
            window.Engine.Layers[0].Bitmap = bitmap;

            Console.WriteLine(window.Engine.LastError);

            foreground.SetTileAttribute(23, 0, flipy: true);

            window.BeforeFrame += BeforeFrame;
            window.AfterFrame += AfterFrame;

            window.Start();
        }
        public static void MainManaged() // engine is still started using the Window
        {
            var engine = new Engine(384, 216, loadPath: "assets");
            var window = new Window(null, WindowFlags.NOVSYNC|WindowFlags.S1, autostart: false);

            // not required, only if Window.Engine is used by later code
            window.SetManagedEngine(new EngineRef(() => engine, (e) => engine = e));

            Tilemap foreground = new("Sonic_md_fg1.tmx");
            Bitmap bitmap = new(256, 256, 8)
            {
                Palette = new(16)
            };
            engine.Layers[0].Bitmap = bitmap;
            engine.Layers[0].Bitmap.Palette.SetColor(1, 255, 0, 0);
            engine.Layers[0].Bitmap = bitmap;

            Console.WriteLine(engine.LastError);

            var tile = foreground.GetTile(23, 0);
            tile.flipy = true;
            foreground.SetTile(23, 0, tile);

            window.BeforeFrame += BeforeFrame;
            window.AfterFrame += AfterFrame;

            window.Start();
        }

        public static void MainLegacy() // or fully managed
        {
            var engine = new Engine(384, 216, loadPath: "assets");
            var window = new Window(null, WindowFlags.NOVSYNC|WindowFlags.S1, autostart: false);

            // not required, only if Window.Engine is used by later code
            window.SetManagedEngine(new EngineRef(() => engine, (e) => engine = e));

            Tilemap foreground = new("Sonic_md_fg1.tmx");
            Bitmap bitmap = new(256, 256, 8)
            {
                Palette = new(16)
            };
            engine.Layers[0].Bitmap = bitmap;
            engine.Layers[0].Bitmap.Palette.SetColor(1, 255, 0, 0);
            engine.Layers[0].Bitmap = bitmap;

            Console.WriteLine(engine.LastError);

            var tile = foreground.GetTile(23, 0);
            tile.flipy = true;
            foreground.SetTile(23, 0, tile);
            Console.WriteLine(engine.LastError);

            while(window.Process)
            {
                BeforeFrame(window, new());
                window.DrawFrame(0);
                AfterFrame(window, new());
                //Console.WriteLine(engine.LastError);
            }
        }

        private static void BeforeFrame(Window window, FrameArgs args)
        {
            return;
            window.Engine.Layers[0].Bitmap.Clear();
            window.Engine.Layers[0].Bitmap.DrawRect(new Rect(20, 20, 16, 16), 1, false);
            foreground.SetTileAttribute(23, 0, flipy: true);
        }

        private static void AfterFrame(Window window, FrameArgs args)
        {
        }
    }
}