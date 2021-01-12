// Name:		xy2xyz
// Submenu:		
// Author:		iOrange & xrModder
// Title:		xy2xyz
// Version:		0.1
// Desc:		Restore Z from X and Y (convert XY Normal Map to XYZ Normal Map)
// Keywords:	Normal Map
// URL:			https://github.com/xrModder/Paint.NET_plugins
// Help:		

float Clamp(float v, float a, float b)
{
	return Math.Min(b, Math.Max(a, v));
}

void Render(Surface dst, Surface src, Rectangle rect)
{
	ColorBgra srcPixel, dstPixel = new ColorBgra();

	// [RUS] Залить альфа канал белым цветом
	// [ENG] Fill alpha channel with white
	dstPixel.A = 255;

	for (int y = rect.Top; y < rect.Bottom; ++y)
	{
		if (IsCancelRequested) return;

		for (int x = rect.Left; x < rect.Right; ++x)
		{
			srcPixel = src[x, y];

			// [RUS] Конвертировать R и G канал (0...255) в X и Y (-1...+1)
			// [ENG] Convert R and G channel (0...255) to X and Y (-1...+1)
			float nx = (float)srcPixel.R / 127.5f - 1.0f;
			float ny = (float)srcPixel.G / 127.5f - 1.0f;

			// [RUS] Вычислить Z
			// [ENG] Calculate Z
			float nz = (float)Math.Sqrt(Clamp(1.0f - (nx * nx + ny * ny), 0.0f, 1.0f));

			// [RUS] Оставить каналы R и G без изменений
			// [ENG] Keep R and G channel unchanged
			dstPixel.R = srcPixel.R;
			dstPixel.G = srcPixel.G;

			// [RUS] Конвертировать Z (0...1) в B канал (0...255)
			// [ENG] Convert Z (0...1) to B channel (0...255)
			dstPixel.B = (byte)Clamp(nz * 127.5f + 127.5f, 0.0f, 255.0f);

			dst[x, y] = dstPixel;
		}
	}
}