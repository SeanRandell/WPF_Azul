using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace WPF_Azul.ViewModel
{
    public class TileViewModel: ViewModelBase
    {

        private Color circleFill;
        public Color CircleFill
        {
            get { return circleFill; }
            set { circleFill = value; }
        }

        private int circleHeight;
        public int CircleHeight
        {
            get { return circleHeight; }
            set { circleHeight = value; }
        }

        private int circleWidth;
        public int CircleWidth
        {
            get { return circleWidth; }
            set { circleWidth = value; }
        }

        public TileViewModel()
        {
            circleWidth = 100;
            circleHeight = 100;
            circleFill = (Color)ColorConverter.ConvertFromString("Blue");
        }
    }
}
