using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Runtime.InteropServices;


namespace ColorMapGui
{

  class ImageProcessor: IDisposable
  {
    public ColorMap Map = null;

    private BitmapSource sourceImage;
    public BitmapSource SourceImage
    {
      get {return sourceImage;}
      set 
      {
        if (allocated)
        {
          Marshal.FreeHGlobal (RawData);
          allocated = false;
        }
        sourceImage = value;
        if (sourceImage != null)
        {
          BytePerPixel = sourceImage.Format.BitsPerPixel / 8;
          Width = sourceImage.PixelWidth;
          Height = sourceImage.PixelHeight;
          ImageSize = Width * Height * BytePerPixel;
          Format = sourceImage.Format;
          RawData = Marshal.AllocHGlobal (ImageSize);
          allocated = true;
          UpToDate = false;
        }
      }
    }
    private bool UpToDate = false;

    private IntPtr RawData;
    bool allocated = false;

    public WriteableBitmap DestImage {get; private set;}

    public int Width {get; private set;} = 0;
    public int Height {get; private set;} = 0;
    public int BytePerPixel {get; private set;} = 0;
    public int ImageSize {get; private set;} = 0;
    public PixelFormat Format;

    public event EventHandler ProcessingDone;

    
    private void BitmapToByteArray ()
    {
      sourceImage.CopyPixels (System.Windows.Int32Rect.Empty, RawData, ImageSize, Width * BytePerPixel);
    }


    private WriteableBitmap ByteArrayToBitmap ()
    {
      var dest = new WriteableBitmap (Width, Height, 96.0, 96.0, Format, null);
      var rect = new System.Windows.Int32Rect (0, 0, Width, Height);
      dest.WritePixels (rect, RawData, ImageSize, Width * BytePerPixel);
      dest.Freeze ();
      return dest;
    }
    

    public void Process (IList <ColorPair> colorPairs)
    {
      if (sourceImage != null && Map != null)
      {
        BitmapToByteArray ();
        Task.Run (() => {
          try
          {
            Map.SetColors (colorPairs);
            Map.Invoke (RawData, Width * Height, 0, BytePerPixel);
            DestImage = ByteArrayToBitmap ();
            UpToDate = true;
            ProcessingDone (this, EventArgs.Empty);
          }
          catch (Exception ex)
          {
            Console.WriteLine (ex.Message);
          }
        });

      }
    }


    public void Dispose ()
    {
      if (allocated)
      {
        Marshal.FreeHGlobal (RawData);
        allocated = false;
      }
    }

  } // class ImageProcessing



}
