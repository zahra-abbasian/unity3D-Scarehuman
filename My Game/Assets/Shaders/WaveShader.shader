//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/WaveShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType"="Transparent"
        }
		Pass
		{
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;	

			struct vertIn
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				// Displace the original vertex in model space
				//float4 displacement = float4(0.0f, 0.0f, 0.0f, 0.0f);
				//float4 displacement = float4(0.0f, -5.0f, 0.0f, 0.0f); // Q2a
				//float4 displacement = float4(0.0f, _Time.y, 0.0f, 0.0f); // Q2b
				//float4 displacement = float4(0.0f, sin(_Time.y), 0.0f, 0.0f); // Q2c
				//float4 displacement = float4(0.0f, sin(v.vertex.x), 0.0f, 0.0f); // Q3
				//float4 displacement = float4(0.0f, sin(v.vertex.x + _Time.y), 0.0f, 0.0f); // Q4
				//float4 displacement = float4(0.0f, sin(v.vertex.x + _Time.y) * 0.5f, 0.0f, 0.0f); // Q5a
				float4 displacement = float4(0.0f, sin(v.vertex.x + _Time.y * 2.0f), 0.0f, 0.0f); // Q5b
				//float4 displacement = float4(0.0f, sin(v.vertex.x + _Time.y * _Time.y), 0.0f, 0.0f); // Q5c

				//v.vertex += float4(0.0f, 0.0 * sin(v.vertex.x + _Time.y), 0.0f, 0.0f);
				v.vertex += displacement;

				vertOut o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;

				
				
			}
			
			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, v.uv);
				return col;
			}
			ENDCG
		}
	}
}