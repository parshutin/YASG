Shader "Custom/BasicDiffuse"
{
	Properties
	{
		//_MainTex("Base (RGB)", 2D) = "white" {}
		_EmissiveColor("Emissive Color", Color) = (1,1,1,1)
		_AmbientColor("Ambient Color",  Color) = (1,1,1,1)
		_SliderValue("Slider Value", Range(0, 10)) = 2
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf BasicDiffuse

		//sampler2D _MainTex;
		float4 _EmissiveColor;
		float4 _AmbientColor;
		float _SliderValue;

		struct Input
		{
			float2 uv_MainTex;
		};

		inline float4 LightingBasicDiffuse(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			float difLight = max(0, dot(s.Normal, lightDir));
			float hLambert = difLight * 0.5 + 0.5;
			float4 col;
			col.rgb = s.Albedo * _LightColor0.rgb * (hLambert * atten * 2);
			col.a = s.Alpha;
			return col;
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			//half4 c = tex2D(_MainTex, IN.uv_MainTex);
			float4 c;
			c = pow((_EmissiveColor + _AmbientColor), _SliderValue);

			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
