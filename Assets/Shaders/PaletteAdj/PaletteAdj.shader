Shader "Custom/PaletteAdj" 
{
	Properties 
	{
		[HideInInspector]_MainTex ("", 2D) = "white" {}
		_SampleTex ("Sample Texture", 2D) = "white" {}
	}

	SubShader 
	{
		ZTest Always Cull Off ZWrite Off Fog { Mode Off }
 
		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc" 

			#pragma vertex vert
			#pragma fragment frag
    
			struct v2f 
			{
				float4 worldPos : POSITION;
				half2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			sampler2D _SampleTex;
			
			inline fixed lightness(fixed3 col)
			{
 				return (max(col.r, max(col.g, col.b)) + min(col.r, min(col.g, col.b))) * 0.5f;
			}

			inline fixed average(fixed3 col)
			{
				return (col.r + col. g + col.b) * 0.3333333333f;
			}

			inline fixed luminosity(fixed3 col)
			{
				return (col.r * 0.21f) + (col.g * 0.72f) + (col.b * 0.07f);
			}

			v2f vert (appdata_img IN)
			{
				v2f OUT;
				OUT.worldPos = mul (UNITY_MATRIX_MVP, IN.vertex);
				OUT.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, IN.texcoord.xy);
				return OUT; 
			}

			fixed3 frag (v2f IN) : COLOR
			{
				fixed3 colour =  tex2D(_MainTex, IN.uv);
				fixed gray = average(colour);
				return tex2D(_SampleTex, fixed2(gray,0.5));
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
