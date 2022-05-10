Shader "Custom/Lava"
{
    Properties
    {
        _LavaTex ("Lava", 2D) = "white" {}
        _NoiseTex ("Noise", 2D) = "black" {}
        _Flow1 ("flow1", Vector) = (1, 0, 0, 0)
        _Flow2 ("flow2", Vector) = (-1, -1, 0, 0)
        _Pulse ("pulse", Float) = 1.0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
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
                float2 lava_uv : TEXCOORD0;
                float2 noise_uv : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _LavaTex;
            sampler2D _NoiseTex;
            float4 _LavaTex_ST;
            float4 _NoiseTex_ST;
            float4 _Flow1;
            float4 _Flow2;
            float _Pulse;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.lava_uv = TRANSFORM_TEX(v.uv, _LavaTex);
                o.lava_uv += _Flow1.xy * _Time.x;
                o.noise_uv = TRANSFORM_TEX(v.uv, _NoiseTex);
                o.noise_uv += _Flow2.xy * _Time.x;
                return o;
            }

            fixed4 frag(const v2f i) : SV_Target
            {
                fixed4 noise = tex2D(_NoiseTex, i.noise_uv);
                const fixed2 p = noise.xy * 0.5 - 0.5;

                fixed4 lava_col = tex2D(_LavaTex, i.lava_uv + p);
                const fixed pulse = tex2D(_NoiseTex, i.noise_uv + p).a;

                fixed4 temper = lava_col * pulse * _Pulse + (lava_col * lava_col - 0.1);
                if (temper.r > 1.0)
                {
                    temper.bg += clamp(temper.r - 2.0, 0.0, 5.0);
                }
                if (temper.g > 1.0)
                {
                    temper.rb += temper.g - 1.0;
                }
                if (temper.b > 1.0)
                {
                    temper.rg += temper.b - 1.0;
                }

                lava_col = temper;
                lava_col.a = 1.0;
                return lava_col;
            }
            ENDCG
        }
    }
}