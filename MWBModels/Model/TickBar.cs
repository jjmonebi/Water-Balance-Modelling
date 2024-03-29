﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MWBModels.Model
{
    public class MyTickBar : TickBar

    {

        protected override void OnRender(DrawingContext dc)

        {
            Size size = new Size(base.ActualWidth, base.ActualHeight);

            int tickCount = (int)((this.Maximum - this.Minimum) / this.TickFrequency) + 1;

            if ((this.Maximum - this.Minimum) % this.TickFrequency == 0)

                tickCount -= 1;

            double tickFrequencySize;

            // Calculate tick's setting

            tickFrequencySize = (size.Width * this.TickFrequency / (this.Maximum - this.Minimum));

            string text = "";

            FormattedText formattedText = null;

            double num = this.Maximum - this.Minimum;

            int i = 0;

            // Draw each tick text

            //for (i = 0; i <= tickCount; i++)

            //{

            //    text = Convert.ToString(Convert.ToInt32(this.Minimum + this.TickFrequency * i), 10);

            //    //g.DrawString(text, font, brush, drawRect.Left + tickFrequencySize * i, drawRect.Top + drawRect.Height/2, stringFormat);



            //    formattedText = new FormattedText(text, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 8, Brushes.Black);

            //    dc.DrawText(formattedText, new Point((tickFrequencySize * i), 20));

            //}

            foreach (var item in this.Ticks)
            {
                text = Convert.ToString(item);

                //g.DrawString(text, font, brush, drawRect.Left + tickFrequencySize * i, drawRect.Top + drawRect.Height/2, stringFormat);



                formattedText = new FormattedText(text, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 8, Brushes.Black);
                tickFrequencySize = ((size.Width - i*2) / (this.Maximum - this.Minimum) * (item - this.Minimum));
                dc.DrawText(formattedText, new Point((tickFrequencySize), 20)); i++;
            }

        }

    }
}
