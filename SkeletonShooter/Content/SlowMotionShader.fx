#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float SlowTime;

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 color = tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
	
	if(SlowTime < 1){
		float SlowTimeInverse = 1 - SlowTime;
		float a = color.r + color.g + color.b / 3;
		float4 gray = float4(a, a, a, color.a);
		
		color = float4(
			(gray.r * SlowTime) + (color.r * SlowTimeInverse),
			(gray.g * SlowTime) + (color.g * SlowTimeInverse),
			(gray.b * SlowTime) + (color.b * SlowTimeInverse),
			color.a
		);
	}
	
	return color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};