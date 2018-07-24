using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;




namespace ColorMapGui
{

  public static class ColorMapExtern
  {
#if DEBUG
    private const string DllPath = "..\\..\\..\\Debug\\ColorMap";
#else
    const string DllPath = "ColorMap";
#endif

    [DllImport (DllPath, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Init ();

    [DllImport (DllPath, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr NewMap ();

    [DllImport (DllPath, CallingConvention = CallingConvention.Cdecl)]
    public static extern void DeleteMap (IntPtr map);

    [DllImport (DllPath, CallingConvention = CallingConvention.Cdecl)]
    public static extern void MapAddColor (IntPtr map,
                                           float xIn,
                                           float yIn,
                                           float zIn,
                                           float xOut,
                                           float yOut,
                                           float zOut);
    
    [DllImport (DllPath, CallingConvention = CallingConvention.Cdecl)]
    public static extern void MapSetColor (IntPtr map,
                                            int index,
                                            float xIn,
                                            float yIn,
                                            float zIn,
                                            float xOut,
                                            float yOut,
                                            float zOut);

    [DllImport (DllPath, CallingConvention = CallingConvention.Cdecl)]
    public static extern void MapSetInputColor (IntPtr map,
                                                int index,
                                                float xIn, 
                                                float yIn, 
                                                float zIn);

    [DllImport (DllPath, CallingConvention = CallingConvention.Cdecl)]
    public static extern void MapSetOutputColor (IntPtr map,
                                                 int index,
                                                 float xOut,
                                                 float yOut,
                                                 float zOut);

    [DllImport (DllPath, CallingConvention = CallingConvention.Cdecl)]
    public static extern void MapRemoveColor (IntPtr map,
                                              int index);


    [DllImport (DllPath, CallingConvention = CallingConvention.Cdecl)]
    public static extern void MapSetColors (IntPtr map,
                                            IntPtr colors,
                                            int count);

    [DllImport (DllPath, CallingConvention = CallingConvention.Cdecl)]
    public static extern void MapColors (IntPtr map, 
                                         IntPtr data,
                                         int count,
                                         int start,
                                         int step);

    [DllImport (DllPath, CallingConvention = CallingConvention.Cdecl)]
    public static extern int MapColorCount (IntPtr map);

  } // class ColorMapping

}
