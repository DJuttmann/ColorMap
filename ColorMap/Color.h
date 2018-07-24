#pragma once

#include <cmath>
#include "Vec3.h"


void InitGamma ();
float GammaDecompressFast (unsigned char n);
unsigned char GammaCompressFast (float x);