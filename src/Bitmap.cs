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

using System.Drawing;
using System.Runtime.InteropServices;
using static Tilengine.TLN;

#endregion

#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Tilengine
{
    public struct  Vec2Int
    {
        public int X, Y;

        public Vec2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Overload the '+' operator to add two Vec2Int instances.
        public static Vec2Int operator +(Vec2Int left, Vec2Int right)
        {
            return new Vec2Int(left.X + right.X, left.Y + right.Y);
        }

        // Overload the '-' operator to subtract two Vec2Int instances.
        public static Vec2Int operator -(Vec2Int left, Vec2Int right)
        {
            return new Vec2Int(left.X - right.X, left.Y - right.Y);
        }
        // Overload the '*' operator to multiply two Vec2Int instances.
        public static Vec2Int operator *(Vec2Int left, Vec2Int right)
        {
            return new Vec2Int(left.X * right.X, left.Y * right.Y);
        }

        // Overload the '/' operator to divide two Vec2Int instances.
        public static Vec2Int operator /(Vec2Int left, Vec2Int right)
        {
            return new Vec2Int(left.X / right.X, left.Y / right.Y);
        }
        // Overload the '%' operator to modulo two Vec2Int instances.
        public static Vec2Int operator %(Vec2Int left, Vec2Int right)
        {
            return new Vec2Int(left.X % right.X, left.Y % right.Y);
        }

        // Overload the '*' operator to multiply a Vec2Int by an integer.
        public static Vec2Int operator *(Vec2Int vec, int scalar)
        {
            return new Vec2Int(vec.X * scalar, vec.Y * scalar);
        }

        // Overload the '/' operator to divide a Vec2Int by an integer.
        public static Vec2Int operator /(Vec2Int vec, int divisor)
        {
            return new Vec2Int(vec.X / divisor, vec.Y / divisor);
        }

        // Overload the '%' operator to modulo a Vec2Int by an integer.
        public static Vec2Int operator %(Vec2Int vec, int modulator)
        {
            return new Vec2Int(vec.X % modulator, vec.Y % modulator);
        }

        // Optionally, you can override ToString to provide a string representation of the Vec2Int.
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
    public struct Rect
    {
        public Vec2Int Position;
        public Vec2Int Dimensions;

        public Rect(int x, int y, int width, int height)
        {
            Position = new Vec2Int(x, y);
            Dimensions = new Vec2Int(width, height);
        }

        // Constructor with two Vec2Int instances.
        public Rect(Vec2Int position, Vec2Int dimensions)
        {
            Position = position;
            Dimensions = dimensions;
        }
    }
    public struct Bitmap
    {
        private IntPtr _bitmapAddr;
        public IntPtr Bmap { get { return _bitmapAddr; } set { _bitmapAddr = value; } }

        public Bitmap(int width, int height, int bpp)
        {
            _bitmapAddr = TLN_CreateBitmap(width, height, bpp);
        }

        public Bitmap(string filename)
        {
            _bitmapAddr = TLN_LoadBitmap(filename);
        }

        public Bitmap(IntPtr bitmapAddr)
        {
            _bitmapAddr = bitmapAddr;
        }

        public static Bitmap LoadBitmap(string filename)
        {
            return new Bitmap(TLN_LoadBitmap(filename));
        }

        public int Width { get { return TLN_GetBitmapWidth(_bitmapAddr); } }
        public int Height { get { return TLN_GetBitmapHeight(_bitmapAddr); } }
        public int Depth { get { return TLN_GetBitmapDepth(_bitmapAddr); } }
        public int Pitch { get { return TLN_GetBitmapPitch(_bitmapAddr); } }
        public Palette Palette
        {
            get { return new Palette(TLN_GetBitmapPalette(_bitmapAddr)); }
            set { TLN_SetBitmapPalette(_bitmapAddr, value.Pal); }
        }
        public unsafe void SetPixel(int x, int y, byte color)
        {
            if (x < 0 || y < 0 || x > this.Width || y > this.Height)
            {
                return;
            }
            IntPtr myPtr = TLN_GetBitmapPtr(_bitmapAddr, x, y);
            var ptr = (byte*)myPtr;
            ptr[0] = color;
        }
        public byte[] PixelData
        {
            get
            {
                byte[] pixelData = new byte[Pitch * Height];
                IntPtr ptrPixelData = TLN_GetBitmapPtr(_bitmapAddr, 0, 0);
                Marshal.Copy(ptrPixelData, pixelData, 0, pixelData.Length);
                return pixelData;
            }

            set
            {
                if (Pitch * Height != value.Length) { throw new ArgumentException(); }
                IntPtr ptrPixelData = TLN_GetBitmapPtr(_bitmapAddr, 0, 0);
                Marshal.Copy(value, 0, ptrPixelData, value.Length);
            }
        }

        public void Clear()
        {
            for (int j = 0; j < TLN_GetBitmapHeight(_bitmapAddr); j++)
                for (int i = 0; i < TLN_GetBitmapWidth(_bitmapAddr); i++)
                    SetPixel(i, j, 0);
        }
        public void DrawHLine(Vec2Int position, int length, byte color)
        {
            if(length == 0) return;
            if(length > 0)
            {
                for(int i = 0; i < length; i++)
                {
                    SetPixel(position.X + i, position.Y, color);
                }
                return;
            }
            for (int i = 0; i > length; i--)
            {
                SetPixel(position.X + i, position.Y, color);
            }
        }
        public void DrawVLine(Vec2Int position, int length, byte color)
        {
            if (length == 0) return;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    SetPixel(position.X, position.Y + i, color);
                }
                return;
            }
            for (int i = 0; i > length; i--)
            {
                SetPixel(position.X, position.Y + i, color);
            }
        }
        public void DrawRect(Rect rect, byte color = 1, bool filled = false)
        {
            if (filled)
                for (int j = 0; j < rect.Dimensions.X; j++)
                    for (int i = 0; i < rect.Dimensions.Y; i++)
                    {
                        SetPixel(i + rect.Position.X, j + rect.Position.Y, color);
                    }
            else
            {
                // Top
                DrawHLine(rect.Position, rect.Dimensions.X, color);
                // Bottom
                DrawHLine(rect.Position + new Vec2Int(0, rect.Dimensions.Y - 1), rect.Dimensions.X, color);
                // Left
                DrawVLine(rect.Position, rect.Dimensions.Y, color);
                // Right
                DrawVLine(rect.Position + new Vec2Int(rect.Dimensions.X - 1, 0), rect.Dimensions.Y, color);
            }
        }

        public void Delete()
        {
            TLN_DeleteBitmap(_bitmapAddr);
            _bitmapAddr = IntPtr.Zero;
        }

        public static bool operator ==(Bitmap bitmap, object obj)
        {
            if (obj == null)
                return bitmap._bitmapAddr == IntPtr.Zero;

            if (obj is Bitmap otherBitmap)
                return bitmap._bitmapAddr == otherBitmap._bitmapAddr;

            return false;
        }

        public static bool operator !=(Bitmap bitmap, object obj)
        {
            return !(bitmap == obj);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Bitmap otherBitmap)
                return this._bitmapAddr == otherBitmap._bitmapAddr;

            return false;
        }

        public override int GetHashCode()
        {
            return _bitmapAddr.GetHashCode();
        }
    }
}