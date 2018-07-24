#include "stdafx.h"
#include "Image.h"


Image::Image (int width, int height)
{
  Width = width > 0 ? width : 1;
  Height = height > 0 ? height : 1;
  Data.resize (Width);
  for (int i = 0; i < Width; i++)
    Data [i] = new Vec3 [Height];
}


Image::Image (int width, int height, float* data)
{
  Width = width > 0 ? width : 1;
  Height = height > 0 ? height : 1;
  Data.resize (Width);
  for (int x = 0; x < Width; x++)
  {
    Data [x] = new Vec3 [Height];
    for (int y = 0; y < height; y++)
    {
      int index = 3 * (height * x + y);
      Data [x] [y] = Vec3 (data [index], data [index + 1], data [index + 2]);
    }
  }
}


void Image::Map (ColorMap map)
{
  for (int x = 0; x < Width; x++)
  {
    for (int y = 0; y < Height; y++)
    {
      Data [x] [y] = map (Data [x] [y]);
    }
  }
}

