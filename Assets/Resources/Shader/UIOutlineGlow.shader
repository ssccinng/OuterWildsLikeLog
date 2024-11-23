Shader "Custom/UIGlowEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (0, 1, 1, 1)
        _GlowWidth ("Glow Width", Float) = 0.1
    }

    SubShader
{
    Tags { "Queue"="Transparent" "RenderType"="Transparent" }
    LOD 100

    Pass
    {
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        struct appdata_t
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
        };

        struct v2f
        {
            float4 vertex : SV_POSITION;
            float2 texcoord : TEXCOORD0;
        };

        sampler2D _MainTex;
        float4 _GlowColor;
        float _GlowWidth;

        v2f vert (appdata_t v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.texcoord = v.texcoord;
            return o;
        }

        fixed4 frag (v2f i) : SV_Target
        {
            fixed4 color = tex2D(_MainTex, i.texcoord);

            // 光晕逻辑：使边缘发光
            float alpha = color.a;
            float glow = smoothstep(1.0 - _GlowWidth, 1.0, alpha);
            fixed4 glowColor = _GlowColor * glow;

            return color + glowColor;
        }
        ENDCG
    }
}

}