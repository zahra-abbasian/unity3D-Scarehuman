// This script is inspired by the following tutorials:
// https://www.youtube.com/watch?v=EBrAdahFtuo
// https://www.youtube.com/watch?v=0flY11lVCwY&t=1226s

Shader "Unlit/RainyGlass"
{
  
 Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GridSize ("Rain Grid Size", float) = 5
        _Distortion ("Distortion", range(-10, 10)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex, _GrabTexture;
            float4 _MainTex_ST;
            float _GridSize;
            float _T;
            float _Distortion;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                
                float4 col = 0;

                float2 aspect = float2(2,1); // Make the rain grids a rectangle of one side 2 times the other side
                float2 uv = i.uv * _GridSize * aspect;
                uv.y += _Time.y * 0.25;
                float2 grid = frac(uv) - 0.5; // Make grids and put the origin in the centre of the grid
               
                float x = 0 ;
                float y = -sin(_Time.y + sin(_Time.y + sin(_Time.y) * 0.5)) * 0.45; // the factor of 0.45 is for to make the drop not go beyond the edges which are from -0.5 to 0.5
                y -= (grid.x - x) * (grid.x - x); // Make the drop saggy

                float2 dropPos = (grid - float2(x,y)) / aspect; // move the drop
                float drop = smoothstep(0.05, 0.03, length(dropPos)); // Length of grid is the distance from the corner of a grid to its center
                
                float2 trailPos = (grid - float2(x,_Time.y * 0.25)) / aspect; // So the trail doesn't move up and down (y = 0)
                trailPos.y = (frac(trailPos.y * 8) - 0.5) / 8;
                float trail = smoothstep(0.03, 0.01, length(trailPos)); // Length of grid is the distance from the corner of a grid to its center
                
                float fogtrail = smoothstep(-0.05, 0.05, dropPos.y); // Remove the trail underneath the drop
                fogtrail *= smoothstep(0.5, y, grid.y); // Make the trail small at top and bigger in bottom close to the drop 
                trail *= fogtrail;
                fogtrail *= smoothstep(0.05, 0.04, abs(dropPos.x)); // Make a trail of fug

                col += fogtrail * 0.5;
                col += drop;
                col += trail;

                float2 offs = drop * dropPos + trail * trailPos; // make an offset pattern
               
                col = tex2D(_MainTex, i.uv + offs * _Distortion); // Display the backgorund texture with the rain offset pattern
               
                return col;
            }

            ENDCG
        }
    }
}
