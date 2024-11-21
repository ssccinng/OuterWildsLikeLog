Shader "Unlit/GlowShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (1, 1, 1, 1)
        _GlowStrength ("Glow Strength", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _GlowColor;
            float _GlowStrength;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 glow = _GlowColor * _GlowStrength;
                col.rgb += glow.rgb * col.a; // 为图像增加发光效果
                return col;
            }
            ENDCG
        }
    }
}
