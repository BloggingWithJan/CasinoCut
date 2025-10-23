Shader "Custom/InteractableGlow"
{
    Properties
    {
        _GlowColor ("Glow Color", Color) = (1,1,1,1) // The color property exposed in the editor
        _EmissionStrength ("Emission Strength", Range(0, 5)) = 1 // Control for intensity
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            // Simple pass to render the color without lighting
            Lighting Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 _GlowColor;
            float _EmissionStrength;

            fixed4 frag (v2f i) : SV_Target
            {
                // The output color is the GlowColor multiplied by the strength
                return _GlowColor * _EmissionStrength;
            }
            ENDCG
        }
    }
}