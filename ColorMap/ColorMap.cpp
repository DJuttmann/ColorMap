// ColorMap.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "ColorMap.h"
#include "Color.h"
#include <iostream>

/*
int32_t CreateMap ()
{
  ColorMaps.insert (std::pair <int32_t, ColorMap> (counter, ColorMap ()));
  return counter++;
}


void MapAddColor (int32_t id, float xIn , float yIn , float zIn,
                                            float xOut, float yOut, float zOut)
{
  auto index = ColorMaps.find (id);
  if (index != ColorMaps.end)
  {
    index->second.AddColor (Vec3 (xIn, yIn, zIn), Vec3 (xOut, yOut, zOut));
  }
}
*/


void Init ()
{
  InitGamma ();
}


ColorMap *NewMap ()
{
  return new ColorMap;
}


void DeleteMap (ColorMap *map)
{
  delete static_cast <ColorMap*> (map);
}


void MapAddColor (ColorMap *map, float xIn , float yIn , float zIn,
                                float xOut, float yOut, float zOut)
{
  map->AddColor (Vec3 (xIn, yIn, zIn), Vec3 (xOut, yOut, zOut));
}


void MapSetColor (ColorMap *map, int32_t index,
                                float xIn , float yIn , float zIn,
                                float xOut, float yOut, float zOut)
{
  map->SetColor (static_cast <unsigned int> (index),
                 Vec3 (xIn, yIn, zIn), Vec3 (xOut, yOut, zOut));
}

void MapSetInputColor (ColorMap *map, int32_t index,
                                     float xIn , float yIn , float zIn)
{
  map->SetInputColor (static_cast <unsigned int> (index), Vec3 (xIn, yIn, zIn));
}


void MapSetOutputColor (ColorMap *map, int32_t index,
                                      float xOut, float yOut, float zOut)
{
  map->SetOutputColor (static_cast <unsigned int> (index), Vec3 (xOut, yOut, zOut));
}


void MapRemoveColor (ColorMap *map, int32_t index)
{
  map->RemoveColor (static_cast <unsigned int> (index));
}


void MapSetColors (ColorMap *map, unsigned char *colors, int32_t count)
{
  int max = count * 6;
  map->Clear ();
  for (int i = 0; i < max; i += 6)
  {
    map->AddColor (Vec3 (GammaDecompressFast (colors [i    ]),
                         GammaDecompressFast (colors [i + 1]),
                         GammaDecompressFast (colors [i + 2])),
                   Vec3 (GammaDecompressFast (colors [i + 3]),
                         GammaDecompressFast (colors [i + 4]),
                         GammaDecompressFast (colors [i + 5])));
  }
}



// Apply a colormap to an array of colors. Parameters are:
// colorCount: number of colors in array (grouped as three consecutive unsigned chars)
// start     : index in array of the first color
// step      : step size between the locations of consecutive colors in the array
void MapColors (ColorMap *map, unsigned char *data, int32_t count,
                int32_t start, int32_t step)
{
  if (count < 0 || start < 0 || step < 3)
    throw ERROR_BAD_ARGUMENTS;

  const int maxValue = start + count * step;
  for (int i = 0; i < maxValue; i += step)
  {
    Vec3 color (GammaDecompressFast (data [i    ]),
                GammaDecompressFast (data [i + 1]),
                GammaDecompressFast (data [i + 2]));
    color = (*map) (color);
    data [i    ] = GammaCompressFast (color.x);
    data [i + 1] = GammaCompressFast (color.y);
    data [i + 2] = GammaCompressFast (color.z);
  }
}



int32_t MapColorCount (ColorMap *map)
{
  return map->ColorCount ();
}