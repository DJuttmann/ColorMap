using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Runtime.InteropServices;


namespace ColorMapGui
{

  class ColorMap: IDisposable
  {
    private IntPtr Map;
    private List <(Color input, Color output)> controlColors;
    public readonly ReadOnlyCollection <(Color input, Color output)> ControlColors;
    private bool disposed = false;

    public int Count {get {return ColorMapExtern.MapColorCount (Map);}}


    public ColorMap ()
    {
      Map = ColorMapExtern.NewMap ();
      controlColors = new List <(Color input, Color output)> ();
      ControlColors = new ReadOnlyCollection <(Color input, Color output)> (controlColors);
    }


    ~ColorMap ()
    {
      Dispose ();
    }


    public void AddColor (float xIn , float yIn , float zIn, 
                          float xOut, float yOut, float zOut)
    {
      ColorMapExtern.MapAddColor (Map, xIn, yIn, zIn, xOut, yOut, zOut);
    }


    public void SetColor (int index, 
                          float xIn , float yIn , float zIn , 
                          float xOut, float yOut, float zOut)
    {
      ColorMapExtern.MapSetColor (Map, index, xIn, yIn, zIn, xOut, yOut, zOut);
    }


    public void SetInputColor (int index,
                               float xIn, float yIn, float zIn)
    {
      ColorMapExtern.MapSetInputColor (Map, index, xIn, yIn, zIn);
    }


    public void SetOutputColor (int index,
                                float xOut, float yOut, float zOut)
    {
      ColorMapExtern.MapSetOutputColor (Map, index, xOut, yOut, zOut);
    }


    public void RemoveColor (int index)
    {
      ColorMapExtern.MapRemoveColor (Map, index);
    }


    public void SetColors (IList <ColorPair> colorPairs)
    {
      int byteCount = 6 * colorPairs.Count;
      byte [] colorArray = new byte [byteCount];
      for (int i = 0; i < colorPairs.Count; i++)
      {
        colorArray [6 * i    ] = colorPairs [i].InputColor.R;
        colorArray [6 * i + 1] = colorPairs [i].InputColor.G;
        colorArray [6 * i + 2] = colorPairs [i].InputColor.B;
        colorArray [6 * i + 3] = colorPairs [i].OutputColor.R;
        colorArray [6 * i + 4] = colorPairs [i].OutputColor.G;
        colorArray [6 * i + 5] = colorPairs [i].OutputColor.B;
      }
      IntPtr unmanagedArray = Marshal.AllocHGlobal (byteCount);
      Marshal.Copy (colorArray, 0, unmanagedArray, byteCount);
      ColorMapExtern.MapSetColors (Map, unmanagedArray, colorPairs.Count);
      Marshal.Release (unmanagedArray);
    }


    public void Invoke (IntPtr data, int count, int start, int step)
    {
      ColorMapExtern.MapColors (Map, data, count, start, step);
    }


    public void Dispose ()
    {
      if (!disposed)
      {
        ColorMapExtern.DeleteMap (Map);
        disposed = true;
      }
    }
  }

}
