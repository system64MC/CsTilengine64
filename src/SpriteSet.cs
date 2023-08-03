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
    public struct SpriteSet
    {
        private IntPtr _sprSetAddr;
        public IntPtr SprSet
        {
            get { return _sprSetAddr; }
        }

        public SpriteSet(Bitmap bitmap, SpriteData[] data, int numEntries)
        {
            _sprSetAddr = TLN_CreateSpriteset(bitmap.Bmap, data, numEntries);
        }
        public SpriteSet(string filename)
        {
            _sprSetAddr = TLN_LoadSpriteset(filename);
        }
        public SpriteSet(IntPtr sprSetAddr)
        {
            _sprSetAddr = sprSetAddr;
        }
        public static SpriteSet LoadSpriteset(string fileName)
        {
            return new SpriteSet(TLN_LoadSpriteset(fileName));
        }
        public SpriteInfo GetInfos(int entry)
        {
            var infos = new SpriteInfo();
            TLN_GetSpriteInfo(_sprSetAddr, entry, out infos);
            return infos;
        }

        public Palette Palette
        {
            get
            {
                return new Palette(TLN_GetSpritesetPalette(_sprSetAddr));
            }
        }
        public int FindSprite(string name)
        {
            return TLN_FindSpritesetSprite(SprSet, name);
        }
        public void Delete()
        {
            TLN_DeleteSpriteset(_sprSetAddr);
            _sprSetAddr = IntPtr.Zero;
        }
    }
}