#region License

/* CsTilenginePure - 1:1 Api C# Wrapper for Tilengine
 * Copyright (C) 2022 Simon Vonhoff <mailto:simon.vonhoff@outlook.com>
 *
 * Tilengine - The 2D retro graphics engine with raster effects
 * Copyright (c) 2018 Marc Palacios Domènech <mailto:megamarc@hotmail.com>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
 */

/*
 *****************************************************************************
 * C# Tilengine binding - Up to date to library version 2.11.0 and above.
 * http://www.tilengine.org
 *****************************************************************************
 */

#endregion

#region Using Statements

using System.Numerics;
using static Tilengine.TLN;

#endregion

#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Tilengine
{
    //public struct Window
    //{
    //    private static Window _window;
    //    private static bool _init = false;
    //    public static Window? WinInstance { get { return _init ? _window : null; } }

    //    public Window() : this((WindowFlags.S2 | WindowFlags.NOVSYNC), false, null, null)
    //    {
    //        Console.WriteLine("Creating window...");
    //    }
    //    public Window(WindowFlags flags = (WindowFlags.S2 | WindowFlags.NOVSYNC), bool threaded = false, string title = null, string overlay = null)
    //    {
    //        if (!_init)
    //        {
    //            if (!threaded)
    //                TLN_CreateWindow(overlay, flags);
    //            else
    //                TLN_CreateWindowThread(overlay, flags);
    //            _window = this;
    //            _init = true;
    //            if (title != null)
    //                TLN_SetWindowTitle(title);
    //        }
    //    }
    //    public string Title { set { TLN_SetWindowTitle(value); } }
    //    public bool Process { get { return TLN_ProcessWindow(); } }
    //    public bool Active { get { return TLN_IsWindowActive(); } }
    //    public bool GetInput(Input input)
    //    {
    //        return TLN_GetInput(input);
    //    }
    //    public void EnableInput(Player player, bool enable)
    //    {
    //        TLN_EnableInput(player, enable);
    //    }
    //    public void AssignInputJoystick(Player player, int index)
    //    {
    //        TLN_AssignInputJoystick(player, index);
    //    }
    //    public void DefineInputKey(Player player, Input input, UInt32 keycode)
    //    {
    //        TLN_DefineInputKey(player, input, keycode);
    //    }
    //    public void DefineInputButton(Player player, Input input, byte button)
    //    {
    //        TLN_DefineInputButton(player, input, button);
    //    }
    //    public void DrawFrame(int frame)
    //    {
    //        TLN_DrawFrame(frame);
    //    }
    //    public void WaitRedraw()
    //    {
    //        TLN_WaitRedraw();
    //    }
    //    public void Delete()
    //    {
    //        TLN_DeleteWindow();
    //        _init = false;
    //    }
    //    public bool Blur { set { TLN_EnableBlur(value); } }
    //    public void ConfigCrt(CRT type, bool blur)
    //    {
    //        TLN_ConfigCRTEffect(type, blur);
    //    }
    //    public void EnableCrt(Overlay overlay, byte overlayFactor, byte threshold, byte v0, byte v1, byte v2, byte v3, bool blur, byte glowFactor)
    //    {
    //        TLN_EnableCRTEffect(overlay, overlayFactor, threshold, v0, v1, v2, v3, blur, glowFactor);
    //    }
    //    public void DisableCrt()
    //    {
    //        TLN_DisableCRTEffect();
    //    }
    //    public uint Delay { set { TLN_Delay(value); } }
    //    public uint Ticks { get { return TLN_GetTicks(); } }
    //    public uint AvgFPS { get { return TLN_GetAverageFps(); } }
    //    public int Width { get { return TLN_GetWindowWidth(); } }
    //    public int Height { get { return TLN_GetWindowHeight(); } }
    //    public int ScaleFactor
    //    {
    //        get { return TLN_GetWindowScaleFactor(); }
    //        set { TLN_SetWindowScaleFactor(value); }
    //    }

    //}


    public struct FrameArgs
    {
        public float deltaTime;
        // TODO: WHATEVER YOU WANT TO PASS ALONG
    }
    public struct EngineArgs
    {
        public readonly bool IsDefault = true;
        int hres;
        int vres;
        int numLayers;
        int numSprites;
        int numAnimations;
        int fps;
        string loadPath;

        public int Hres { get => hres; set => hres = value; }
        public int Vres { get => vres; set => vres = value; }
        public int NumLayers { get => numLayers; set => numLayers = value; }
        public int NumSprites { get => numSprites; set => numSprites = value; }
        public int NumAnimations { get => numAnimations; set => numAnimations = value; }
        public int Fps { get => fps; set => fps = value; }
        public string LoadPath { get => loadPath; set => loadPath = value; }

        public EngineArgs(int hres = 384, int vres = 216, int numLayers = 3, int numSprites = 64, int numAnimations = 64, int fps = 60, string loadPath = null!)
        {
            IsDefault = false;
            this.Hres = hres;
            this.Vres = vres;
            this.NumLayers = numLayers;
            this.NumSprites = numSprites;
            this.NumAnimations = numAnimations;
            this.Fps = fps;
            if (loadPath != null)
                this.loadPath = loadPath;
            else
                this.loadPath = "";
        }
        public EngineArgs(Vector2 resolution, int numLayers, int numSprites, int numAnimations, int fps, string loadPath) : this((int)resolution.X, (int)resolution.Y, numLayers, numSprites, numAnimations, fps, loadPath) { }
        public EngineArgs() : this(384, 216, 3, 64, 64, 60, null!) {
            IsDefault = true;
         }

        // equality operator
        public static bool operator ==(EngineArgs a, EngineArgs b) => a.Equals(b);
        public static bool operator !=(EngineArgs a, EngineArgs b) => !a.Equals(b);

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            if (obj is EngineArgs other)
            {
                return this.Hres == other.Hres &&
                    this.Vres == other.Vres &&
                    this.NumLayers == other.NumLayers &&
                    this.NumSprites == other.NumSprites &&
                    this.NumAnimations == other.NumAnimations &&
                    this.Fps == other.Fps &&
                    this.LoadPath == other.LoadPath;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


    public class Ref<T>
    {
        private Func<T> getter;
        private Action<T> setter;
        public Ref(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
        public T Value
        {
            get { return getter(); }
            set { setter(value); }
        }
        // implicit conversion to nullable T
        public static implicit operator T(Ref<T> r) => r.Value;
    }

    public class EngineRef : Ref<Engine>
    {
        public EngineRef(Func<Engine> getter, Action<Engine> setter) : base(getter, setter) { }
        public static implicit operator Engine(EngineRef r) => r.Value;
        public static implicit operator EngineRef(Engine e) => new EngineRef(() => e, (Engine v) => e = v);
        public Layer[] Layers => Value.Layers;
        public Sprite[] Sprites => Value.Sprites;
        public Animation[] Animations => Value.Animations;
        public uint NumObjects => Value.NumObjects;
        public int numLayers => Value.numLayers;
        public int NumSprites => Value.NumSprites;
        public uint UsedMemory => Value.UsedMemory;
        public int Width => Value.Width;
        public int Height => Value.Height;
        public string LastError => Value.LastError;
        public int TargetFps => Value.TargetFps;
        public Color BackgroundColor
        {
            set
            {
                var val = Value;
                val.BackgroundColor = value;
                Value = val;
            }
        }
        public Bitmap BGBitmap
        {
            set
            {
                var val = Value;
                val.BGBitmap = value;
                Value = val;
            }
        }
        public Palette BGPalette
        {
            set
            {
                var val = Value;
                val.BGPalette = value;
                Value = val;
            }
        }
        public VideoCallback RasterCallback
        {
            set
            {
                var val = Value;
                val.RasterCallback = value;
                Value = val;
            }
        }
        public VideoCallback FrameCallback
        {
            set
            {
                var val = Value;
                val.FrameCallback = value;
                Value = val;
            }
        }
        public bool SetGlobalPalette(int index, Palette palette) => Value.SetGlobalPalette(index, palette);
        public Palette GetGlobalPalette(int index) => Value.GetGlobalPalette(index);
        public void SetBackgroundColor(byte r, byte g, byte b) => Value.SetBackgroundColor(r, g, b);
        public void SetRenderTarget(byte[] data, int pitch) => Value.SetRenderTarget(data, pitch);
        public void UpdateFrame(int frame) => Value.UpdateFrame(frame);
        public string LoadPath
        {
            set
            {
                var val = Value;
                val.LoadPath = value;
                Value = val;
            }
        }
        public BlendFunction BlendFunction
        {
            set
            {
                var val = Value;
                val.BlendFunction = value;
                Value = val;
            }
        }
        public LogLevel LogLevel
        {
            set
            {
                var val = Value;
                val.LogLevel = value;
                Value = val;
            }
        }
        public bool OpenResourcePack(string filename, string key = null!) => Value.OpenResourcePack(filename, key);
        public void CloseResourcePack() => Value.CloseResourcePack();
        public void DeInit() => Value.DeInit();
        public bool SetContext() => Value.SetContext();
        public void Delete() => Value.Delete();
        public void DisableBackgroundColor() => Value.DisableBackgroundColor();
    }

    public class Window
    {
        private bool _init = false;
        private CancellationTokenSource? _loop = null;
        private bool _threaded = false;
        private Engine? _engine;
        private EngineRef? _engineRef = null;
        private void SetEngine(Engine engine)
        {
            _engine = engine;
        }
        private Engine GetEngine()
        {
#pragma warning disable CS8629
            return _engine.Value;
            // throws an error if engine is null, so it is not a safe getter
            // for our purposes, however, it just replaces a lambda
#pragma warning restore CS8629
        }
        public Engine Engine
        {
            get
            {
                if (_engine == null)
                {
                    return _engineRef!;
                }
                return new EngineRef(GetEngine, SetEngine);
            }
        }
        public bool HasEngine { get { return _engine != null || _engineRef != null; } }
        public static List<Window> Instances { get; private set; } = new List<Window>();
        public delegate void FrameEvent(Window window, FrameArgs e);
        public event FrameEvent BeforeFrame = (window, e) => { };
        public event FrameEvent AfterFrame = (window, e) => { };
        private EngineArgs _engineArgs;
        public Window() : this(engineArgs: new EngineArgs()) { }
        public Window(WindowFlags flags = (WindowFlags.S2 | WindowFlags.NOVSYNC), bool threaded = false, string title = null!, string overlay = null!, bool autostart = true)
            : this(engineArgs: new EngineArgs(), flags, threaded, title, overlay, autostart) { }
        public Window(EngineArgs engineArgs = default, WindowFlags flags = (WindowFlags.S2 | WindowFlags.NOVSYNC), bool threaded = false, string title = null!, string overlay = null!, bool autostart = true)
            : this(engineArgs as EngineArgs?, flags, threaded, title, overlay, autostart) { }
        public Window(EngineArgs? engineArgs, WindowFlags flags = (WindowFlags.S2 | WindowFlags.NOVSYNC), bool threaded = false, string title = null!, string overlay = null!, bool autostart = true)
        {
            if (!_init)
            {
                if (engineArgs != null)
                {
                    if (engineArgs.Value.IsDefault)
                    {
                        engineArgs = new EngineArgs();
                    }
                    _engineArgs = engineArgs.Value;
                    _engine = new Engine(_engineArgs.Hres, _engineArgs.Vres, _engineArgs.NumLayers, _engineArgs.NumSprites, _engineArgs.NumAnimations, _engineArgs.Fps, _engineArgs.LoadPath);
                }
                if (!threaded)
                    TLN_CreateWindow(overlay ?? "", flags);
                else
                    TLN_CreateWindowThread(overlay ?? "", flags);
                _threaded = threaded;
                Instances.Add(this);
                _init = true;
                if (title != null)
                    TLN_SetWindowTitle(title);
                if (autostart)
                    Start();
            }
        }
        public static void ClearWindows()
        {
            for (int i = 0; i < Instances.Count; i++)
            {
                var window = Instances[i];
                window.Stop();
                window.Delete();
                if (window._engine != null)
                {
                    window._engine.Value.Delete();
                    window._engine = null;
                }
                window._engineRef = null;
                if (window._loop != null)
                {
                    window._loop.Dispose();
                    window._loop = null;
                }
                Instances[i] = window;
            }
            Instances.Clear();
        }
        public string Title { set { TLN_SetWindowTitle(value); } }
        public bool Process { get { return TLN_ProcessWindow(); } }
        public bool Active { get { return TLN_IsWindowActive(); } }
        public bool DeleteManagedEngine()
        {
            if (_engineRef != null)
            {
                _engineRef = null;
                return true;
            }
            return false;
        }
        public bool SetManagedEngine(EngineRef engine)
        {
            _engine?.Delete();
            _engine = null;
            _engineRef = engine;
            return true;
        }
        public bool GetInput(Input input)
        {
            return TLN_GetInput(input);
        }
        public void EnableInput(Player player, bool enable)
        {
            TLN_EnableInput(player, enable);
        }
        public void AssignInputJoystick(Player player, int index)
        {
            TLN_AssignInputJoystick(player, index);
        }
        public void DefineInputKey(Player player, Input input, UInt32 keycode)
        {
            TLN_DefineInputKey(player, input, keycode);
        }
        public void DefineInputButton(Player player, Input input, byte button)
        {
            TLN_DefineInputButton(player, input, button);
        }
        public void DrawFrame(int frame)
        {
            TLN_DrawFrame(frame);
        }
        public void WaitRedraw()
        {
            TLN_WaitRedraw();
        }
        public void Delete()
        {
            TLN_DeleteWindow();
            _init = false;
        }
        [Obsolete("Use EnableCrt instead")]
        public bool Blur { set { TLN_EnableBlur(value); } }
        public void ConfigCrt(CRT type, bool blur)
        {
            TLN_ConfigCRTEffect(type, blur);
        }
        public void EnableCrt(Overlay overlay, byte overlayFactor, byte threshold, byte v0, byte v1, byte v2, byte v3, bool blur, byte glowFactor)
        {
            TLN_EnableCRTEffect(overlay, overlayFactor, threshold, v0, v1, v2, v3, blur, glowFactor);
        }
        public void DisableCrt()
        {
            TLN_DisableCRTEffect();
        }
        public uint Delay { set { TLN_Delay(value); } }
        public uint Ticks { get { return TLN_GetTicks(); } }
        public uint AvgFPS { get { return TLN_GetAverageFps(); } }
        public int Width { get { return TLN_GetWindowWidth(); } }
        public int Height { get { return TLN_GetWindowHeight(); } }
        public int ScaleFactor
        {
            get { return TLN_GetWindowScaleFactor(); }
            set { TLN_SetWindowScaleFactor(value); }
        }
        public void Start()
        {
            if (_loop != null)
                return;
            _loop = new CancellationTokenSource();
            Loop(_loop.Token);
        }
        public void Stop()
        {
            _loop?.Cancel();
        }
        private DateTime _lastFrame = DateTime.Now;
        private FrameArgs CollectFrameArgs()
        {
            float dt = (float)(DateTime.Now - _lastFrame).TotalSeconds;
            _lastFrame = DateTime.Now;
            return new FrameArgs()
            {
                deltaTime = dt
                // TODO: collect relevant data
            };
        }
        private async void Loop(CancellationToken token)
        {
            bool CheckToken()
            {
                if (token.IsCancellationRequested)
                {
                    return true;
                }
                return false;
            }
            await Task.Yield(); // wait for the engine to be set up
            await Task.Delay(1, token);
            if(CheckToken()) return;
            if (!HasEngine)
            {
                throw new Exception("No engine set for window: cannot draw");
            }
            while (Process)
            {
                if (!Engine.SetContext())
                {
                    throw new Exception("Failed to set context");
                }
                BeforeFrame.Invoke(this, CollectFrameArgs());
                try
                {
                    DrawFrame(0);
                }
                catch (Exception)
                {
                    Console.WriteLine("TODO: handle exception");
                    throw;
                    // TODO: handle exception
                    // ensure it doesn't happen again, or at least not too often
                    // It's only O(1) when there are no exceptions
                    // If the error is likely to happen every frame, just politely exit
                    // This might be done by using an exception type created for this purpose (e.g. SafeException, ContinuableException, etc.)
                }
                AfterFrame.Invoke(this, CollectFrameArgs());
                await Task.Yield();
                // housekeeping
                if(CheckToken()) return;
                if (!_threaded)
                    TLN_ProcessWindow();
                // TODO: rest of housekeeping
            }
        }
    }
}