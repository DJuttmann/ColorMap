#pragma once

#include <cmath>


struct Vec3
{
public:
  float x;
  float y;
  float z;

  Vec3 ()
  {
  }


  Vec3 (float x, float y, float z)
  {
    this->x = x;
    this->y = y;
    this->z = z;
  }


  Vec3 &operator += (const Vec3 &rhs)
  {
    x += rhs.x;
    y += rhs.y;
    z += rhs.z;
    return *this;
  }


  Vec3 &operator -= (const Vec3 &rhs)
  {
    x -= rhs.x;
    y -= rhs.y;
    z -= rhs.z;
    return *this;
  }


  Vec3 &operator *= (const float rhs)
  {
    x *= rhs;
    y *= rhs;
    z *= rhs;
    return *this;
  }
  

  Vec3 &operator /= (const float rhs)
  {
    x /= rhs;
    y /= rhs;
    z /= rhs;
    return *this;
  }


  Vec3 operator + (const Vec3 &rhs)
  {
    Vec3 sum = *this;
    sum += rhs;
    return sum;
  }


  Vec3 operator - (const Vec3 &rhs)
  {
    Vec3 difference = *this;
    difference -= rhs;
    return difference;
  }


  Vec3 operator * (const float rhs)
  {
    Vec3 scaled = *this;
    scaled *= rhs;
    return scaled;
  }


  Vec3 operator / (const float rhs)
  {
    Vec3 scaled = *this;
    scaled /= rhs;
    return scaled;
  }


  static float dot (const Vec3 lhs, const Vec3 rhs)
  {
    return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
  }


  float size ()
  {
    return sqrt (x * x + y * y + z * z);
  }


  void normalize ()
  {
    float s = size ();
    if (s > 0.0f)
      *this /= s;
  }

};