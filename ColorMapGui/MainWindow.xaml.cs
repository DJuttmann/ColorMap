using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorMapGui
{

  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow: Window
  {
    private ImageProcessor MyImageProcessor;
    private BindingList <ColorPair> ColorPairs;
    private int SelectedColor = -1;
    private bool InputSelected = true;
    private bool EnableSliders = true;


    public MainWindow ()
    {
      InitializeComponent();
      ColorMapExtern.Init ();

      MyImageProcessor = new ImageProcessor ();
      MyImageProcessor.ProcessingDone += UpdateOutputImage;
      ColorPairs = new BindingList <ColorPair> ();
      ColorListView.ItemsSource = ColorPairs;

      MainGrid.PreviewDrop += MainWindow_Drop;
      // MainGrid.Drop += MainWindow_Drop;

      MyImageProcessor.Map = new ColorMap ();
      // MyImageProcessor.Map.AddColor (0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
      // MyImageProcessor.Map.AddColor (1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
      // MyImageProcessor.Map.AddColor (0.5f, 0.5f, 0.5f, 0.6f, 0.4f, 0.6f);
    }


    void MainWindow_Drop (object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent (DataFormats.FileDrop))
      {
        string [] files = e.Data.GetData (DataFormats.FileDrop) as string [];
        if (files.Length > 0)
          LoadImage (files [0]);
      }
    }


    void LoadImage (string fileName)
    {
      BitmapSource b;
      try
      {
        b = new BitmapImage (new Uri (fileName, UriKind.RelativeOrAbsolute));
        InputImage.Source = b;
        MyImageProcessor.SourceImage = b;
      }
      catch (Exception ex)
      {
        MessageBox.Show (ex.Message);
      }
    }


    void UpdateOutputImage (object sender, EventArgs e)
    {
      OutputImage.Dispatcher.Invoke (() => {
        OutputImage.Source = MyImageProcessor.DestImage;
      });
    }
    

    private void Map_Click (object sender, RoutedEventArgs e)
    {
      MyImageProcessor.Process (ColorPairs);
    }


    private void Add_Click (object sender, RoutedEventArgs e)
    {
      ColorPair pair = new ColorPair (ColorPairs.Count);
      ColorPairs.Add (pair);
    }

    
    private void Del_Click (object sender, RoutedEventArgs e)
    {
      if (ColorPairs.Count > 0)
        ColorPairs.RemoveAt (ColorPairs.Count - 1);
    }
    

    private void InputColor_MouseDown (object sender, MouseButtonEventArgs e)
    {
      SelectedColor = (int) (sender as Rectangle).Tag;
      InputSelected = true;
      SlidersFromColor (ColorPairs [SelectedColor].InputColor.R,
                        ColorPairs [SelectedColor].InputColor.G,
                        ColorPairs [SelectedColor].InputColor.B);
    }


    private void OutputColor_MouseDown (object sender, MouseButtonEventArgs e)
    {
      SelectedColor = (int) (sender as Rectangle).Tag;
      InputSelected = false;
      SlidersFromColor (ColorPairs [SelectedColor].OutputColor.R,
                        ColorPairs [SelectedColor].OutputColor.G,
                        ColorPairs [SelectedColor].OutputColor.B);
    }


    private void SlidersFromColor (int R, int G, int B)
    {
      EnableSliders = false;
      RedSlider.Value = R;
      GreenSlider.Value = G;
      BlueSlider.Value = B;
      EnableSliders = true;
    }


    private Color ColorFromSliders ()
    {
      return new Color
      {
        R = (byte) RedSlider.Value,
        G = (byte) GreenSlider.Value,
        B = (byte) BlueSlider.Value,
        A = 255
      };
    }


    private void Slider (object sender, EventArgs e)
    {
      if (EnableSliders && SelectedColor >= 0 && SelectedColor < ColorPairs.Count)
      {
        if (InputSelected)
          ColorPairs [SelectedColor] = new ColorPair (ColorFromSliders (), 
                                                      ColorPairs [SelectedColor].OutputColor,
                                                      ColorPairs [SelectedColor].Index);
        else
          ColorPairs [SelectedColor] = new ColorPair (ColorPairs[SelectedColor].InputColor,
                                                      ColorFromSliders (),
                                                      ColorPairs [SelectedColor].Index);
      }
    }

    private void Slider_ManipulationCompleted (object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      Slider (sender, e);
    }
    
    private void Slider_ManipulationCompleted (object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
    {
      Slider (sender, e);
    }
  }

}
