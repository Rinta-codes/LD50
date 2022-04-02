using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Collections.Generic;

namespace LD50.UI
{
    public class UIElements : UIElement
    {
        protected List<UIElement> elements = new List<UIElement>();

        /// <summary>
        /// Creates a list of UIElements
        /// </summary>
        public UIElements()
        {

        }

        public void Add(UIElement element)
        {
            elements.Add(element);
        }

        public void Add(UIElements elementsToAdd)
        {
            foreach (var element in elementsToAdd.elements)
            {
                elements.Add(element);
            }
        }

        public void AddRange(IEnumerable<UIElement> elements)
        {
            this.elements.AddRange(elements);
        }

        public override void Draw()
        {
            if (_hidden) return;
            foreach (UIElement element in elements)
            {
                element.Draw();
            }
        }

        public override void Update()
        {
            if (_hidden) return;
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                elements[i].Update();
            }
        }

        public override void OnClick(MouseButtonEventArgs e, Vector2 mousePosition)
        {
            if (_hidden) return;
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                if (elements[i].IsInElement(mousePosition))
                {
                    elements[i].OnClick(e, mousePosition);
                }
            }
        }

        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            // Should not occur
        }

        public override void OnMouseMove(MouseMoveEventArgs e)
        {
            // Should not occur
        }

        public override void UnLoad()
        {
            foreach (UIElement element in elements)
            {
                element.UnLoad();
            }
        }

        public override bool OnHover(Vector2 mousePosition)
        {
            if (_hidden) return false;
            foreach (UIElement element in elements)
            {
                if (element.IsInElement(mousePosition))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool IsInElement(Vector2 mousePosition)
        {
            if (_hidden) return false;
            foreach (UIElement element in elements)
            {
                if (element.IsInElement(mousePosition))
                {
                    return true;
                }
            }
            return false;
        }

        public override Vector2 GetPosition()
        {
            return Vector2.Zero;
        }

        public override Vector2 GetSize()
        {
            return Vector2.Zero;
        }
    }
}
