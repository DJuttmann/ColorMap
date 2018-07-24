#pragma once

#include "Map.h"


#ifdef COLORMAP_EXPORTS
#define COLORMAP_API __declspec (dllexport)
#else
#define COLORMAP_API __declspec (dllimport)
#endif


extern "C" COLORMAP_API void Init ();
 
extern "C" COLORMAP_API ColorMap *NewMap ();
extern "C" COLORMAP_API void DeleteMap (ColorMap *map);
extern "C" COLORMAP_API void MapAddColor (ColorMap *map, float xIn , float yIn , float zIn,
                                          float xOut, float yOut, float zOut);
extern "C" COLORMAP_API void MapSetColor (ColorMap *map, int32_t index,
                                          float xIn , float yIn , float zIn,
                                          float xOut, float yOut, float zOut);
extern "C" COLORMAP_API void MapSetInputColor (ColorMap *map, int32_t index,
                                                float xIn , float yIn , float zIn);
extern "C" COLORMAP_API void MapSetOutputColor (ColorMap *map, int32_t index,
                                                float xOut, float yOut, float zOut);
extern "C" COLORMAP_API void MapRemoveColor (ColorMap *map, int32_t index);

extern "C" COLORMAP_API void MapSetColors (ColorMap *map, unsigned char *colors, int32_t count);

extern "C" COLORMAP_API void MapColors (ColorMap *map, unsigned char *data, int32_t count,
                                        int32_t start, int32_t step);

extern "C" COLORMAP_API int32_t MapColorCount (ColorMap *map);
