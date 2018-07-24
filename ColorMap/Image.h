#pragma once

#include <vector>
#include <cmath>
#include "Vec3.h"
#include "Map.h"

using std::vector;


class Image
{

public:
  Image (int width, int height);
  Image (int width, int height, float* Data);
  
  Vec3 &Pixel (int x, int y) {return Data [x] [y];}

  void Map (ColorMap map);

private:
  int Width;
  int Height;
  vector <Vec3*> Data;

};