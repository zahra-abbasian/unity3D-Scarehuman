Shader "Unlit/GlassShader"
{
    Properties {
      _SkyBoxCube("Reflection Skybox", Cube) = "" {}
   }
   SubShader {
      Pass {   
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         #include "UnityCG.cginc"
 
         uniform samplerCUBE _SkyBoxCube;   
 
         struct vertIn {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
         };
         struct vertOut {
            float4 pos : SV_POSITION;
            float3 normalDir : TEXCOORD0;
            float3 viewDir : TEXCOORD1;
         };
 
         vertOut vert(vertIn v) 
         {
            vertOut o;
 
            float4x4 modelMatrix = unity_ObjectToWorld;
            float4x4 modelMatrixInverse = unity_WorldToObject; 
 
            o.viewDir = mul(modelMatrix, v.vertex).xyz - _WorldSpaceCameraPos;
            o.normalDir = normalize(mul(float4(v.normal, 0.0), modelMatrixInverse).xyz);
            o.pos = UnityObjectToClipPos(v.vertex);

            return o;
         }
 
         float4 frag(vertOut v) : COLOR
         {
            float glassRefractiveIndex = 1.5;
            float airRefractiveIndex = 1.0;

            float3 refractedDir = refract(normalize(v.viewDir), 
               normalize(v.normalDir), airRefractiveIndex / glassRefractiveIndex);

            return texCUBE(_SkyBoxCube, refractedDir);
         }
 
         ENDCG
      }
   }
}