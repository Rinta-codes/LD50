using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LD50.IO
{
    public class InputHandler
    {
        private KeyboardState state, prevstate;
        private MouseState mState;

        /// <summary>
        /// Dictionary that maps a key to special characters, both when holding shift and not
        /// </summary>
        public static Dictionary<Keys, Tuple<char, char>> KeysToChar = new Dictionary<Keys, Tuple<char, char>>()
        {
            {Keys.Apostrophe, new Tuple<char, char>('\'', '\'') },
            {Keys.Backslash, new Tuple<char, char>('\\',  '|')},
            {Keys.Comma, new Tuple<char, char>(',', '<') },
            {Keys.Equal, new Tuple<char, char>('=', '+') },
            {Keys.GraveAccent, new Tuple<char, char>('`', '~')},
            {Keys.LeftBracket, new Tuple<char, char>('[', '{')},
            {Keys.Minus, new Tuple<char, char>('-', '_')},
            {Keys.Period, new Tuple<char, char>('.', '>')},
            {Keys.RightBracket, new Tuple<char, char>(']', '}')},
            {Keys.Semicolon, new Tuple<char, char>(';', ':')},
            {Keys.Slash, new Tuple<char, char>('/', '?')},
            {Keys.D0, new Tuple<char, char>('0', ')')},
            {Keys.D1, new Tuple<char, char>('1', '!')},
            {Keys.D2, new Tuple<char, char>('2', '@')},
            {Keys.D3, new Tuple<char, char>('3', '#')},
            {Keys.D4, new Tuple<char, char>('4', '$')},
            {Keys.D5, new Tuple<char, char>('5', '%')},
            {Keys.D6, new Tuple<char, char>('6', '^')},
            {Keys.D7, new Tuple<char, char>('7', '&')},
            {Keys.D8, new Tuple<char, char>('8', '*')},
            {Keys.D9, new Tuple<char, char>('9', '(')},
            {Keys.KeyPad0, new Tuple<char, char>('0', '0')},
            {Keys.KeyPad1, new Tuple<char, char>('1', '1')},
            {Keys.KeyPad2, new Tuple<char, char>('2', '2')},
            {Keys.KeyPad3, new Tuple<char, char>('3', '3')},
            {Keys.KeyPad4, new Tuple<char, char>('4', '4')},
            {Keys.KeyPad5, new Tuple<char, char>('5', '5')},
            {Keys.KeyPad6, new Tuple<char, char>('6', '6')},
            {Keys.KeyPad7, new Tuple<char, char>('7', '7')},
            {Keys.KeyPad8, new Tuple<char, char>('8', '8')},
            {Keys.KeyPad9, new Tuple<char, char>('9', '9')},
        };

        public void Update(KeyboardState kstate, MouseState mstate)
        {
            prevstate = state;
            state = kstate;
            mState = mstate;
        }

        public bool IsKeyDown(Keys key)
        {
            return state.IsKeyDown(key);
        }

        public bool IsKeyPressed(Keys key)
        {
            return state.IsKeyDown(key) && !prevstate.IsKeyDown(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            return state.IsKeyReleased(key);
        }

        public bool IsAnyKeyDown()
        {
            return state.IsAnyKeyDown;
        }
    }
}
