// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Sphere"
{
 Properties {
      _Cube("Reflection Map", Cube) = "" {}
      //excess
       _MainAlpha("MainAlpha", Range(0, 1)) = 1
       
        _MainTex ("MainTex (RGBA)", 2D) = ""
   }
   SubShader {
      Tags {
            "Queue" = "Transparent"
            
            "RenderType"="Opaque"
        }
        LOD 200

      Pass {   
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag
         #pragma surface surf Standard fullforwardshadows alpha:fade

         #include "UnityCG.cginc"

         uniform samplerCUBE _Cube;

         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float3 normalDir : TEXCOORD0;
            float3 viewDir : TEXCOORD1;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            float4x4 modelMatrix = unity_ObjectToWorld;
            float4x4 modelMatrixInverse = unity_WorldToObject; 
 
            output.viewDir = mul(modelMatrix, input.vertex).xyz 
               - _WorldSpaceCameraPos;
            output.normalDir = normalize(
               mul(float4(input.normal, 5.0), modelMatrixInverse).xyz);
            output.pos = UnityObjectToClipPos(input.vertex);
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
            float3 reflectedDir = 
               reflect(input.viewDir, normalize(input.normalDir));
            return texCUBE(_Cube, reflectedDir);
         }
 
         ENDCG
         Pass
        {
            SetTexture[_MainTex] { constantColor(0,0,0, [_MainAlpha]) combine texture * primary, texture * constant}
        }
      }
   }
}