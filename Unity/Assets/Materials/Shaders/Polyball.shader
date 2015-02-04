 Shader "Custom/Polyball" {
        SubShader {
            Pass {

    CGPROGRAM

    #pragma vertex vert
    #pragma fragment frag
    
    // vertex input
    struct appdata {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
        fixed4 color : COLOR;
        float4 texcoord : TEXCOORD0;
    };

	// VS->PS inputs
    struct v2f {
        float4 pos : SV_POSITION;
        float3 color : COLOR0;
        float2 uv : TEXCOORD0;
        float3 normal : TEXCOORD1;
    };

    v2f vert (appdata v)
    {
        v2f o;
        o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
        o.color = v.color;
        o.uv = v.texcoord.xy;
        o.normal = v.normal;
        
        return o;
    }

    float4 frag (v2f i) : COLOR
    {
    	//return float4( i.uv.xy, 0.0, 1.0);
        return float4 (i.color, 1);
    }
    ENDCG

            }
        }
        Fallback "VertexLit"
    }
