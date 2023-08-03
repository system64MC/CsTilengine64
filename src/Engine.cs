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
    public struct Engine
    {
        private static Engine _engineInstance;
        private static bool init = false;
        private IntPtr _engineAddr;
        private Layer[] _layers = null!;
        private Sprite[] _sprites = null!;
        private Animation[] _animations = null!;
        public Layer[] Layers { get { return _layers; } }
        public Sprite[] Sprites { get { return _sprites; } }
        public Animation[] Animations { get { return _animations; } }

        public static Engine? Instance { get { return init ? _engineInstance : null; } }

        public Engine(int hres = 384, int vres = 216, int numLayers = 3, int numSprites = 64, int numAnimations = 64, int fps = 60, string loadPath = null!)
        {
            // _engineAddr = TLN_Init(hres, vres, numLayers, numSprites, numAnimations);
            // _layers = new Layer[numLayers];
            // _sprites = new Sprite[numSprites];
            // _animations = new Animation[numAnimations];

            // for(int i = 0; i < numLayers; i++)
            //     _layers[i] = new Layer(i);

            // for(int i = 0; i < numSprites; i++)
            //     _sprites[i] = new Sprite(i);

            // for(int i = 0; i < numAnimations; i++)
            //     _animations[i] = new Animation(i);
            // _instance = this;
            if (!init)
            {
                _engineAddr = TLN_Init(hres, vres, numLayers, numSprites, numAnimations);
                _layers = new Layer[numLayers];
                _sprites = new Sprite[numSprites];
                _animations = new Animation[numAnimations];
                for (int i = 0; i < numLayers; i++)
                    _layers[i] = new Layer(i);

                for (int i = 0; i < numSprites; i++)
                    _sprites[i] = new Sprite(i);

                for (int i = 0; i < numAnimations; i++)
                    _animations[i] = new Animation(i);
                _engineInstance = this;
                init = true;
                TLN_SetTargetFps(fps);
                if(loadPath != null)
                    LoadPath = loadPath;
            }
        }

        public Engine(Vector2 windowDimentions, int numLayers = 3, int numSprites = 64, int numAnimations = 64, int fps = 60, string loadPath = null!)
            : this((int)windowDimentions.X, (int)windowDimentions.Y, numLayers, numSprites, numAnimations, fps, loadPath) {}

        public uint NumObjects { get { return TLN_GetNumObjects(); } }
        public int numLayers { get { return TLN_GetNumLayers(); } }
        public int NumSprites { get { return TLN_GetNumSprites(); } }
        public uint UsedMemory { get { return TLN_GetUsedMemory(); } }
        public int Width { get { return TLN_GetWidth(); } }
        public int Height { get { return TLN_GetHeight(); } }
        public string LastError
        {
            get { return TLN_GetErrorString(TLN_GetLastError()); }
        }
        public int TargetFps
        {
            get { return TLN_GetTargetFps(); }
            set { TLN_SetTargetFps(value); }
        }
        public Color BackgroundColor { set { TLN_SetBGColor(value.r, value.g, value.b); } }
        public Bitmap BGBitmap { set { TLN_SetBGBitmap(value.Bmap); } }
        public Palette BGPalette { set { TLN_SetBGPalette(value.Pal); } }
        public VideoCallback RasterCallback { set { TLN_SetRasterCallback(value); } }
        public VideoCallback FrameCallback { set { TLN_SetFrameCallback(value); } }
        public bool SetGlobalPalette(int index, Palette palette)
        {
            return TLN_SetGlobalPalette(index, palette.Pal);
        }
        public Palette GetGlobalPalette(int index)
        {
            return new Palette(TLN_GetGlobalPalette(index));
        }
        public void SetBackgroundColor(byte r, byte g, byte b)
        {
            TLN_SetBGColor(r, g, b);
        }
        public void SetRenderTarget(byte[] data, int pitch)
        {
            TLN_SetRenderTarget(data, pitch);
        }
        public void UpdateFrame(int frame)
        {
            TLN_UpdateFrame(frame);
        }
        public string LoadPath { set { TLN_SetLoadPath(value); } }
        public BlendFunction BlendFunction { set { TLN_SetCustomBlendFunction(value); } }
        public LogLevel LogLevel { set { TLN_SetLogLevel(value); } }
        public bool OpenResourcePack(string filename, string key = null!)
        {
            return TLN_OpenResourcePack(filename, key);
        }
        public void CloseResourcePack()
        {
            TLN_CloseResourcePack();
        }
        public void DeInit()
        {
            TLN_Deinit();
        }
        public bool SetContext()
        {
            return TLN_SetContext(_engineAddr);
        }
        public void Delete()
        {
            TLN_DeleteContext(_engineAddr);
        }
        public void DisableBackgroundColor()
        {
            TLN_DisableBGColor();
        }
    }
}