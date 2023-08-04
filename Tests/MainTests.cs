using System.Numerics;
using Tilengine;
using SDL2;
using static SDL2.SDL;

namespace Game
{
    public class Test
    {
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
                        MainStandard();
                        MainDelayed();
                        MainManaged();
                        MainLegacy();
                        break;
                    default:
                        throw new NotImplementedException("Test "+test+" does not exist");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured:");
                Console.WriteLine("At "+(e.Source ?? "unknown"));
                Console.WriteLine("At "+(e.StackTrace ?? "unknown"));
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

            while(window.Process)
            {
                BeforeFrame(window, new());
                window.DrawFrame(0);
                AfterFrame(window, new());
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