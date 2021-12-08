Shader "WaveVR/SeeThroughPerEye"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    _Hor("Is Horizontal Filp", Range(0, 1)) = 1
    }
    SubShader{
        Tags{  "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100

        Cull Off

        Pass
        {
        Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            int _Hor;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.uv.x = (1 - v.uv.x)*_Hor + v.uv.x*(1 - _Hor);
                o.uv.y = v.uv.y;
                o.vertex = float4(v.vertex.xy, 1, 1);
                //o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                //col.a = 0.5f;
                return col;
            }
            ENDCG
        }

        Pass
        {
                Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            int _Hor;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = float4(v.vertex.x - (unity_StereoEyeIndex*2.0f-1), v.vertex.y, 1, 1);

                o.uv.x = (1 - v.uv.x)*_Hor + v.uv.x*(1 - _Hor);
                o.uv.y = v.uv.y;
               // o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                //col.a = 0.5f;
                return col;
            }
            ENDCG
        }
    }
}
