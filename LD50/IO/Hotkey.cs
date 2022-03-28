using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.IO
{
    /// <summary>
    /// Hotkey class that keeps track of all buttons associated with an action
    /// </summary>
    public class Hotkey
    {
        private bool _repeat;
        private List<Keys> _keys;

        /// <summary>
        /// Create a new Hotkey list
        /// </summary>
        /// <param name="repeat"><code>true</code> if the hotkey should keep firing if the button is held</param>
        public Hotkey(bool repeat)
        {
            _repeat = repeat;
            _keys = new List<Keys>();
        }

        /// <summary>
        /// Add a key to the hotkey list
        /// </summary>
        /// <param name="k">The key to add</param>
        /// <returns>The changed hotkey object</returns>
        public Hotkey AddKey(Keys k)
        {
            _keys.Add(k);
            return this;
        }

        /// <summary>
        /// Add multiple keys to the hotkey list
        /// </summary>
        /// <param name="k">The keys to be added</param>
        /// <returns>The changed hotkey object</returns>
        public Hotkey AddKeys(params Keys[] k)
        {
            foreach (Keys key in k)
            {
                _keys.Add(key);
            }
            return this;
        }

        /// <summary>
        /// Checks if any key in the hotkey list is pressed
        /// </summary>
        /// <returns><code>true</code> if a key is pressed, or held if repeat is true</returns>
        public bool IsPressed()
        {
            if (Globals.selectedElement != null) return false;
            foreach (Keys k in _keys)
            {
                if (_repeat)
                {
                    if (Globals.inputHandler.IsKeyDown(k))
                    {
                        return true;
                    }
                }
                else if (Globals.inputHandler.IsKeyPressed(k))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
