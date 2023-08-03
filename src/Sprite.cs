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
    public struct Sprite
    {
        private int _index;
        public readonly int Index { get { return _index; } }
        public Sprite(int index)
        {
            _index = index;
        }

        public bool Config(SpriteSet spriteset, TileFlags flags)
        {
            return TLN_ConfigSprite(_index, spriteset.SprSet, flags);
        }
        public SpriteSet SpriteSet { set { TLN_SetSpriteSet(_index, value.SprSet); } }
        public TileFlags Flags { set { TLN_SetSpriteFlags(_index, value); } }
        public bool EnableFlag(TileFlags flag, bool enable)
        {
            return TLN_EnableSpriteFlag(_index, flag, enable);
        }
        public Vector2 Pivot { set { TLN_SetSpritePivot(_index, value.X, value.Y); } }
        public bool SetPivot(float px, float py)
        {
            return TLN_SetSpritePivot(_index, px, py);
        }
        public Vector2 Position
        {
            get { return new Vector2(TLN_GetSpriteX(_index), TLN_GetSpriteY(_index)); }
            set { TLN_SetSpritePosition(_index, (int)value.X, (int)value.Y); }
        }
        public bool SetPosition(int x, int y)
        {
            return TLN_SetSpritePosition(_index, x, y);
        }
        public int X
        {
            get { return TLN_GetSpriteX(_index); }
            set { TLN_SetSpritePosition(_index, value, TLN_GetSpriteY(_index)); }
        }
        public int Y
        {
            get { return TLN_GetSpriteY(_index); }
            set { TLN_SetSpritePosition(_index, TLN_GetSpriteX(_index), value); }
        }
        public int Picture
        {
            get { return TLN_GetSpritePicture(_index); }
            set { TLN_SetSpritePicture(_index, value); }
        }
        public Palette Palette
        {
            get { return new Palette(TLN_GetSpritePalette(_index)); }
            set { TLN_SetSpritePalette(_index, value.Pal); }
        }
        public bool SetBlendMode(Blend mode, byte factor)
        {
            return TLN_SetSpriteBlendMode(_index, mode, factor);
        }
        public Vector2 Scaling { set { TLN_SetSpriteScaling(_index, value.X, value.Y); } }
        public bool SetScaling(float sx, float sy)
        {
            return TLN_SetSpriteScaling(_index, sx, sy);
        }
        public bool ResetScaling()
        {
            return TLN_ResetSpriteScaling(_index);
        }
        public bool EnableCollision { set { TLN_EnableSpriteCollision(_index, value); } }
        public bool Collision { get { return TLN_GetSpriteCollision(_index); } }
        public SpriteState SpriteState
        {
            get
            {
                var st = new TLN_SpriteState();
                TLN_GetSpriteState(_index, out st);
                var newSt = new SpriteState();
                newSt.spriteset = new SpriteSet(st.spriteset);
                newSt.flags = st.flags;
                newSt.w = st.w;
                newSt.h = st.h;
                newSt.x = st.x;
                newSt.y = st.y;
                newSt.collision = st.collision;
                newSt.enabled = st.enabled;
                newSt.index = st.index;
                newSt.palette = new Palette(st.palette);
                return newSt;
            }
        }
        public bool SetFirstSprite()
        {
            return TLN_SetFirstSprite(_index);
        }
        public Sprite Next { set { TLN_SetNextSprite(_index, value.Index); } }
        public bool Masking { set { TLN_EnableSpriteMasking(_index, value); } }
        //public Vector2 MaskRegion {set{TLN_SetSpritesMaskRegion((int)value.X, (int)value.Y);}}
        public bool SetAnimation(Sequence sequence, int loop)
        {
            return TLN_SetSpriteAnimation(_index, sequence.Seq, loop);
        }
        public bool DisableAnimation()
        {
            return TLN_DisableSpriteAnimation(_index);
        }
        public bool PauseAnimation()
        {
            return TLN_PauseSpriteAnimation(_index);
        }
        public bool ResumeAnimation()
        {
            return TLN_ResumeSpriteAnimation(_index);
        }
        public bool DisableAnim()
        {
            return TLN_DisableAnimation(_index);
        }
        public bool Disable()
        {
            return TLN_DisableSprite(_index);
        }
        // TODO : Get Availlable sprite
    }
}