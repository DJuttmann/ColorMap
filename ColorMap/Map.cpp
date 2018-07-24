#include "stdafx.h"
#include "Map.h"
#include <iostream>

using std::vector;


// Adds a control color and desired output.
void ColorMap::AddColor (Vec3 input, Vec3 output)
{
  ControlColors.push_back (input);
  ControlDeltas.push_back (output - input);
  ControlWeights.push_back (0.0f);
}


// Overwrite an existing control color mapping.
void ColorMap::SetColor (unsigned int index, Vec3 input, Vec3 output)
{
  if (index < ControlColors.size ())
  {
    ControlColors [index] = input;
    ControlDeltas [index] = output - input;
  }
}


// Set the dersired output for existing control color.
void ColorMap::SetInputColor (unsigned int index, Vec3 input)
{
  if (index < ControlColors.size ())
  {
    ControlColors [index] = input;
  }
}


// Set the dersired output for existing control color.
void ColorMap::SetOutputColor (unsigned int index, Vec3 output)
{
  if (index < ControlColors.size ())
  {
    ControlDeltas [index] = output - ControlColors [index];
  }
}


// Remove a control color from the color map.
bool ColorMap::RemoveColor (unsigned int index)
{
  if (index < ControlColors.size ())
  {
    ControlColors.erase (ControlColors.begin () + index);
    ControlDeltas.erase (ControlDeltas.begin () + index);
    ControlWeights.pop_back ();
    return true;
  }
  else
    return false;
}


// Remove all control colors from the map;
void ColorMap::Clear ()
{
  ControlColors.clear ();
  ControlDeltas.clear ();
  ControlWeights.clear ();
}


// Map an input color interpolated from control color mappings.
Vec3 ColorMap::operator () (Vec3 input)
{
  CalculateInputWeights (input);

  Vec3 Delta (0.0f, 0.0f, 0.0f);
  for (unsigned int i = 0; i < ControlDeltas.size (); i++)
  {
    Delta += ControlDeltas [i] * ControlWeights [i];
  }
  return input + Delta;
}


// Calculates the influence each control color has on the input color when mapping it.
void ColorMap::CalculateInputWeights (Vec3 color)
{
  float totalWeight = 0.0f;

  for (unsigned int i = 0; i < ControlColors.size (); i++)
  {
    float weight = 1.0f;
    for (unsigned int j = 0; j < ControlColors.size (); j++)
    {
      if (j == i)
        continue;
      Vec3 normal = ControlColors [i] - ControlColors [j];
      float size = normal.size ();
      normal /= (size * size);
      float offset = Vec3::dot (normal, ControlColors [j]);
      float newWeight = Vec3::dot (normal, color) - offset;
      if (newWeight < weight)
        weight = newWeight;
    }
    if (weight < 0.0f)
      weight = 0.0f;
    ControlWeights [i] = weight;
    totalWeight += weight;
  }

  for (unsigned int i = 0; i < ControlColors.size (); i++)
    ControlWeights [i] /= totalWeight;
}