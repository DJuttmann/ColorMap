#pragma once

#include <vector>
#include <cmath>
#include "Vec3.h"

using std::vector;


class ColorMap
{
public:
  void AddColor (Vec3 input, Vec3 output);
  void SetColor (unsigned int index, Vec3 input, Vec3 output);
  void SetInputColor (unsigned int index, Vec3 input);
  void SetOutputColor (unsigned int index, Vec3 output);
  bool RemoveColor (unsigned int index);
  int ColorCount () {return ControlColors.size ();}

  void Clear ();
  Vec3 operator () (Vec3 input);

private:
  vector <Vec3> ControlColors;
  vector <Vec3> ControlDeltas;
  vector <float> ControlWeights;

  void CalculateInputWeights (Vec3 color);
};