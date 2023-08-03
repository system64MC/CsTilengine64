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

using static Tilengine.TLN;

#endregion

#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Tilengine
{
    public struct Tileset
    {
        private IntPtr _tilesetAddr;
        public IntPtr Tset
        {
            get { return _tilesetAddr; }
            //set { _tilesetAddr = value; } 
        }

        public Tileset(IntPtr tilesetAddr) { _tilesetAddr = tilesetAddr; }

        public Tileset(int numTiles, int width, int height, Palette palette, SequencePack sp, TileAttribute[] attributes)
        {
            _tilesetAddr = TLN_CreateTileset(numTiles, width, height, palette.Pal, sp.Seq, attributes);
        }
        public Tileset(int numTiles, TileImage[] images)
        {
            TLN_TileImage[] imgs = new TLN_TileImage[images.Length];
            for (int i = 0; i < images.Length; i++)
            {
                imgs[i].bitmap = images[i].Bitmap.Bmap;
                imgs[i].type = images[i].Type;
                imgs[i].id = images[i].Id;
            }
            _tilesetAddr = TLN_CreateImageTileset(numTiles, imgs);
        }
        public Tileset(string filename)
        {
            _tilesetAddr = TLN_LoadTileset(filename);
        }
        public static Tileset LoadTileset(string filename)
        {
            return new Tileset(TLN_LoadTileset(filename));
        }
        public Tileset clone()
        {
            return new Tileset(TLN_CloneTileset(_tilesetAddr));
        }
        public bool SetTilesetPixels(int entry, byte[] data, int srcPitch)
        {
            return TLN_SetTilesetPixels(_tilesetAddr, entry, data, srcPitch);
        }
        public int TileWidth { get { return TLN_GetTileWidth(_tilesetAddr); } }
        public int TileHeight { get { return TLN_GetTileHeight(_tilesetAddr); } }
        public int NumTiles { get { return TLN_GetTilesetNumTiles(_tilesetAddr); } }
        public Palette Palette { get { return new Palette(TLN_GetTilesetPalette(_tilesetAddr)); } }
        public SequencePack SequencePack { get { return new SequencePack(TLN_GetTilesetSequencePack(_tilesetAddr)); } }
        public void Delete()
        {
            TLN_DeleteTileset(_tilesetAddr);
            _tilesetAddr = IntPtr.Zero;
        }
    }
}