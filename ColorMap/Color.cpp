#include "stdafx.h"
#include "Color.h"


float GammaDecompressLookup [256];
float GammaCompressLookup [255];


// Gamma decompression.
float gammaDecompress (unsigned char n) {
  float x = static_cast <float> (n) / 255.0f;
  if (x <= .04045f)
    return x / 12.92f;
  return pow ((x + .055f) / 1.055f, 2.4f);
}


// Generate all lookup tables for gamma correction.
void InitGamma () {
  for (int i = 0; i < 256; i++) {
    GammaDecompressLookup [i] = gammaDecompress (i);
  } // for
  for (int i = 0; i < 255; i++) {
    GammaCompressLookup [i] = .5f * (GammaDecompressLookup [i] +
                                       GammaDecompressLookup [i + 1]);
  }
}


// Gamma decompression with lookup table.
float GammaDecompressFast (unsigned char n) {
  return GammaDecompressLookup [n];
}


// Gamma compression with lookup table (binary search).
unsigned char GammaCompressFast (float x) {
  unsigned char pos = 127;
  for (unsigned char i = 64; i > 0; i = i >> 1) {
    if (x < GammaCompressLookup [pos])
	    pos -= i;
	  else
	    pos += i;
  }
  if (x < GammaCompressLookup [pos])
    return pos;
  return pos + 1;
}
