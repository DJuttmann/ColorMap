using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Input;


namespace ColorMapGui
{

  class ColorPair
  {
    public Color InputColor {get; private set;}
    public Color OutputColor {get; private set;}
    public SolidColorBrush InputFill {get; private set;}
    public SolidColorBrush OutputFill {get; private set;}
    public int Index {get; private set;}


    public ColorPair (int index)
    {
      InputColor = Colors.Gray;
      OutputColor = Colors.Gray;
      InputFill = new SolidColorBrush (InputColor);
      OutputFill = new SolidColorBrush (OutputColor);
      Index = index;
    }


    public ColorPair (Color input, Color output, int index)
    {
      InputColor = input;
      OutputColor = output;
      InputFill = new SolidColorBrush (InputColor);
      OutputFill = new SolidColorBrush (OutputColor);
      Index = index;
    }


    // public event MouseButtonEventHandler InputClickHandler;
    // public event MouseButtonEventHandler OutputClickHandler;
  }

}
