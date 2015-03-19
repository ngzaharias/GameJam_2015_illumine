Shader "Custom/NPR"
{
    Properties
    {
        _Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _MainTex("Main Texture", 2D) = "white" {}
		_RampTex("Ramp Texture", 2D) = "white" {}
		_DiffuseScale("Diffuse Scale", Float) = 1
        _DiffuseBias("Diffuse Bias", Float) = 0
        _DiffuseExponent("Diffuse Exponent", float) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }    // This allows Unity to intelligently substitute this shader when needed.
        LOD 200
 
        CGPROGRAM
        #include "UnityCG.cginc"
        #pragma surface surf NPR
 
        float4 _Color;
        sampler2D _MainTex;
        sampler2D _RampTex;
		float _DiffuseScale; 
		float _DiffuseBias; 
		float _DiffuseExponent;
 
		struct Input
        {
            float2 uv_MainTex;
        };

        half4 LightingNPR(SurfaceOutput o, half3 lightdir, half3 viewdir, half atten)
        {
			float lambert = saturate(dot(o.Normal, lightdir));
            lambert = pow(lambert*_DiffuseScale + _DiffuseBias, _DiffuseExponent);
            half4 diffuse = half4(_LightColor0.rgb * atten * o.Albedo.rgb, 1.0);
            diffuse *= tex2D(_RampTex, float2(lambert, 0.0));
 
            return diffuse;
        }
 
        void surf(Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = _Color.rgb * c;
            o.Alpha = 1.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}