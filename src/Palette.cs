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
    public struct Palette
    {
        private IntPtr _paletteAddr;
        public IntPtr Pal { get { return _paletteAddr; } }
        public Palette(int entries)
        {
            _paletteAddr = TLN_CreatePalette(entries);
        }
        public Palette(string filename)
        {
            _paletteAddr = TLN_LoadPalette(filename);
        }

        public Palette(IntPtr paletteAddr)
        {
            _paletteAddr = paletteAddr;
        }

        public static Palette LoadPalette(string filename)
        {
            return new Palette(TLN_LoadPalette(filename));
        }

        public Palette clone()
        {
            return new Palette(TLN_ClonePalette(_paletteAddr));
        }

        public bool SetColor(int index, byte r, byte g, byte b)
        {
            return TLN_SetPaletteColor(_paletteAddr, index, r, g, b);
        }
        public bool SetColor(int index, Color color)
        {
            return TLN_SetPaletteColor(_paletteAddr, index, color.r, color.g, color.b); ;
        }

        public bool Mix(Palette src1, Palette src2, byte factor)
        {
            return TLN_MixPalettes(src1.Pal, src2.Pal, _paletteAddr, factor);
        }

        public bool AddColor(byte r, byte g, byte b, byte startIndex, byte range)
        {
            return TLN_AddPaletteColor(_paletteAddr, r, g, b, startIndex, range);
        }
        public bool AddColor(Color color, byte startIndex, byte range)
        {
            return TLN_AddPaletteColor(_paletteAddr, color.r, color.g, color.b, startIndex, range);
        }

        public bool SubColor(byte r, byte g, byte b, byte startIndex, byte range)
        {
            return TLN_SubPaletteColor(_paletteAddr, r, g, b, startIndex, range);
        }
        public bool SubColor(Color color, byte startIndex, byte range)
        {
            return TLN_SubPaletteColor(_paletteAddr, color.r, color.g, color.b, startIndex, range);
        }

        public bool ModColor(byte r, byte g, byte b, byte startIndex, byte range)
        {
            return TLN_ModPaletteColor(_paletteAddr, r, g, b, startIndex, range);
        }
        public bool ModColor(Color color, byte startIndex, byte range)
        {
            return TLN_ModPaletteColor(_paletteAddr, color.r, color.g, color.b, startIndex, range);
        }

        public Color GetData(int index)
        {
            Color color = new Color();
            unsafe
            {
                var myPtr = (byte*)(TLN_GetPaletteData(_paletteAddr, index));
                if (myPtr == null)
                {
                    throw new Exception("Palette Index out of bounds.");
                }
                color.r = *(myPtr + 3);
                color.g = *(myPtr + 2);
                color.b = *(myPtr + 1);
            }
            return color;
        }
        public bool SetAnimation(int index, Sequence sequence, bool blend)
        {
            return TLN_SetPaletteAnimation(index, _paletteAddr, sequence.Seq, blend);
        }
        public int AnimationSource
        {
            set { TLN_SetPaletteAnimationSource(value, _paletteAddr); }
        }
        public void Delete()
        {
            TLN_DeletePalette(_paletteAddr);
            _paletteAddr = IntPtr.Zero;
        }

    }
}