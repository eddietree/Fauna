 Shader "Custom/Polyball" {
        SubShader {
            Pass {
            	Cull Off

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
        float3 worldPos : TEXCOORD0;
        float2 uv : TEXCOORD1;
        float3 normal : TEXCOORD2;
    };

	// vertex shader
    v2f vert (appdata v)
    {
    	float3 worldPos = v.vertex;
    	
        v2f o;
        o.pos = mul (UNITY_MATRIX_MVP, float4(worldPos,1.0));
        o.color = v.color;
        o.uv = v.texcoord.xy;
        o.normal = v.normal;
        o.worldPos = worldPos;
        
        return o;
    }

	// fragment shader
    float4 frag (v2f i) : COLOR
    {
    	//clip( sin(i.worldPos.y * 20.0) );
    	//return float4( i.uv.xy, 0.0, 1.0);
        return float4 (i.color, 1);
    }
    ENDCG

            }
        }
        Fallback "VertexLit"
    }
