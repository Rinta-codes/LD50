using LD50.Audio;
using LD50.Logic;
using LD50.UI;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD50.Scenes
{
    public class TestScene : Scene
    {
        Label counter, counter2;
        int counterNum = 0;

        public TestScene() : base(new Vector2(0, 0))
        {
            uiElements.Add(new Rectangle(Vector4.One, new Vector2(100, 100), new Vector2(200, 100), false, new Vector4(0, 1, 0, 1), 10.0f, TexName.PIXEL));
            uiElements.Add(new Textbox("[Test]", TextAlignment.CENTER, new Vector4(0, 0, 0, 1), new Vector2(400, 100), new Vector2(200, 100), new Vector4(1, 0, 0, 1), TexName.PIXEL, new Vector4(0, 0, 0, 1), 10f, false, 100, false));

            var slider = new Slider(Vector4.One, new Vector4(0, 1, 0, 1), new Vector2(400, 400), new Vector2(300, 10), Graphics.DrawLayer.UI, true, SliderLayout.LEFT, 0.5f);
            slider.ShowValue();
            slider.SetDivider(TexName.PIXEL, new Vector2(50, 50), new Vector4(1, 0, 0, 1));
            uiElements.Add(slider);

            var label = new Label("Draggable?", TextAlignment.CENTER, Vector4.One, new Vector2(800, 500), 64, true)
            {
                IsDraggable = true
            };

            uiElements.Add(label);

            slider.SliderValueChanged += Test;

            var checkBox = new CheckBox(Vector4.One, new Vector4(0, 1, 0, 1), new Vector2(500, 500), new Vector2(50, 50), true);
            checkBox.CheckBoxChanged += Test2;
            uiElements.Add(checkBox);

            var button = new Button(new Vector4(0.5f, 0.5f, 0.5f, 1), new Vector4(0, 0, 0.5f, 1), new Vector2(700, 500), new Vector2(200, 80), 10, Graphics.DrawLayer.UI, true)
            {
                OnClickAction = () => { slider.IsHidden = !slider.IsHidden; },
                OnRightClickAction = () => { Globals.Logger.Log("Right Clicked D:!", utils.LogType.SUCCESS); }
            };

            uiElements.Add(button);

            counter = new Label(counterNum.ToString(), TextAlignment.CENTER, Vector4.One, new Vector2(900, 900), 64, true)
            {
                IsDraggable = true
            };
            uiElements.Add(counter);

            counter2 = new Label(counterNum.ToString(), TextAlignment.CENTER, Vector4.One, new Vector2(1000, 900), 64, true)
            {
                IsDraggable = true
            };
            uiElements.Add(counter2);
        }

        public override void Update()
        {
            counterNum++;
            counter.SetText(counterNum.ToString(), TextAlignment.CENTER, 64);
            counter2.SetText(counterNum.ToString(), TextAlignment.CENTER, 64);
            base.Update();
        }

        public void Test(object sender, SliderValueChangedEventArgs e)
        {

        }
        public void Test2(object sender, CheckBoxEventArgs e)
        {

        }
    }
}
